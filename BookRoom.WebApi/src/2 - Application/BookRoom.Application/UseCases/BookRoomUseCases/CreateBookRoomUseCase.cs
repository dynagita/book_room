using AutoMapper;
using BookRoom.Domain.Contract.Constants;
using BookRoom.Domain.Contract.Notification.BookRooms;
using BookRoom.Domain.Contract.Requests.Commands.BookRooms;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.BookRoomsResponses;
using BookRoom.Domain.Contract.UseCases.BookRooms;
using BookRoom.Domain.Entities;
using BookRoom.Domain.Queue;
using BookRoom.Domain.Repositories.EntityFramework;
using BookRoom.Domain.Validation.BookRoomValidations;
using Microsoft.Extensions.Logging;

namespace BookRoom.Application.UseCases.BookRoomUseCases
{
    public class CreateBookRoomUseCase : ICreateBookRoomUseCase
    {
        private readonly IBookRoomsRepository _repository;
        private readonly IRoomRepository _roomRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CancelBookRoomUseCase> _logger;
        private readonly IBookRoomProducer _producer;
        private readonly IBookRoomRequestProducer _requestProducer;

        public CreateBookRoomUseCase(
            IBookRoomsRepository repository,
            IMapper mapper,
            ILogger<CancelBookRoomUseCase> logger,
            IRoomRepository roomRepository,
            IUserRepository userRepository,
            IBookRoomProducer producer,
            IBookRoomRequestProducer requestProducer)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _roomRepository = roomRepository;
            _userRepository = userRepository;
            _producer = producer;
            _requestProducer = requestProducer;
        }
        public async Task<CommonResponse<BookRoomResponse>> HandleAsync(CreateBookRoomRequest request, CancellationToken cancellationToken)
        {
			try
			{
                var book = _mapper.Map<BookRooms>(request);
                book.User = await _userRepository.GetAsync(request.User.Reference, cancellationToken);
                book.Room = await _roomRepository.GetAsync(request.Room.Reference, cancellationToken);

                book.Status = Domain.Contract.Enums.BookStatusRoom.Requested;

                var cantStart30DaysAdvancedValidation = new BookRoomCantStart30DaysAdvanced();
                var cantStart30DaysAdvancedResult = await cantStart30DaysAdvancedValidation.ValidateAsync(book, cancellationToken);
                if(!cantStart30DaysAdvancedResult.IsValid)
                    return CommonResponse<BookRoomResponse>.BadRequest(cantStart30DaysAdvancedResult
                        .Errors
                        .FirstOrDefault()
                        .ErrorMessage);

                var bookRoomGrater3DaysValidation = new BookRoomGreater3DaysValidation();
                var bookRoomGrater3DaysResult = await bookRoomGrater3DaysValidation.ValidateAsync(book, cancellationToken);
                if(!bookRoomGrater3DaysResult.IsValid)
                    return CommonResponse<BookRoomResponse>.BadRequest(bookRoomGrater3DaysResult
                        .Errors
                        .FirstOrDefault()
                        .ErrorMessage);

                var startsAtLeast1DayAfterBookingValidation = new BookRoomStartsAtLeast1DayAfterBooking();
                var startsAtLeast1DayAfterBookingResult = await startsAtLeast1DayAfterBookingValidation.ValidateAsync(book, cancellationToken);
                if(!startsAtLeast1DayAfterBookingResult.IsValid)
                    return CommonResponse<BookRoomResponse>.BadRequest(startsAtLeast1DayAfterBookingResult
                        .Errors
                        .FirstOrDefault()
                        .ErrorMessage);

                var created = await _repository.InsertAsync(book, cancellationToken);

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
