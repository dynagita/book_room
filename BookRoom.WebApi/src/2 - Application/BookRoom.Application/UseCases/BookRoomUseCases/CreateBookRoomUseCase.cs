﻿using AutoMapper;
using BookRoom.Domain.Contract.Constants;
using BookRoom.Domain.Contract.DataTransferObjects.BookRoomDtos;
using BookRoom.Domain.Contract.Notification.BookRooms;
using BookRoom.Domain.Contract.Requests.Commands.BookRooms;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.BookRoomsResponses;
using BookRoom.Domain.Contract.UseCases.BookRooms;
using BookRoom.Domain.Entities;
using BookRoom.Domain.Queue;
using BookRoom.Domain.Repositories.EntityFramework;
using Microsoft.Extensions.Logging;

namespace BookRoom.Application.UseCases.BookRoomUseCases
{
    public class CreateBookRoomUseCase : ICreateBookRoomUseCase
    {
        private readonly IBookRoomsRepository _repository;
        private readonly IRoomRepository _roomRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateBookRoomUseCase> _logger;
        private readonly IBookRoomProducer _producer;
        private readonly IBookRoomRequestProducer _requestProducer;
        private readonly IBookRoomValidationUseCase _bookRoomValidationUseCase;
        public CreateBookRoomUseCase(
            IBookRoomsRepository repository,
            IMapper mapper,
            ILogger<CreateBookRoomUseCase> logger,
            IRoomRepository roomRepository,
            IUserRepository userRepository,
            IBookRoomProducer producer,
            IBookRoomRequestProducer requestProducer,
            IBookRoomValidationUseCase bookRoomValidationUseCase)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _roomRepository = roomRepository;
            _userRepository = userRepository;
            _producer = producer;
            _requestProducer = requestProducer;
            _bookRoomValidationUseCase = bookRoomValidationUseCase;
        }
        public async Task<CommonResponse<BookRoomResponse>> HandleAsync(CreateBookRoomRequest request, CancellationToken cancellationToken)
        {
			try
			{
                var bookRoom = _mapper.Map<BookRooms>(request);
                bookRoom.User = await _userRepository.GetAsync(request.User.Reference, cancellationToken);
                bookRoom.Room = await _roomRepository.GetAsync(request.Room.Reference, cancellationToken);

                bookRoom.Status = Domain.Contract.Enums.BookStatusRoom.Requested;

                var dtoValidation = _mapper.Map<BookRoomValidationDTO>(bookRoom);
                var validationResult = await _bookRoomValidationUseCase.HandleAsync(dtoValidation, cancellationToken);
                if(!validationResult.Valid)
                    return CommonResponse<BookRoomResponse>.BadRequest(validationResult.Error);

                var created = await _repository.InsertAsync(bookRoom, cancellationToken);

                await _producer.SendAsync(created, cancellationToken);

                var requestCreated = _mapper.Map<BookRoomNotification>(created);

                await _requestProducer.SendAsync(requestCreated, cancellationToken);

                var response = _mapper.Map<BookRoomResponse>(created);

                return CommonResponse<BookRoomResponse>.Ok(response);
			}
			catch (Exception ex)
			{
                _logger.LogError(ex, "{UseCase} - {Method} - Exception Thrown", nameof(CreateBookRoomUseCase), nameof(HandleAsync));
                return CommonResponse<BookRoomResponse>.BadRequest(ErrorMessages.EXCEPTION_ERROR);
            }
        }
    }
}
