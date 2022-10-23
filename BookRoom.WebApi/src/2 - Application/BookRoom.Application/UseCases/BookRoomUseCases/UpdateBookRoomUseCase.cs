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
using Microsoft.Extensions.Logging;

namespace BookRoom.Application.UseCases.BookRoomUseCases
{
    public class UpdateBookRoomUseCase : IUpdateBookRoomUseCase
    {
        private readonly IBookRoomsRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CancelBookRoomUseCase> _logger;
        private readonly IBookRoomProducer _producer;
        private readonly IBookRoomRequestProducer _requestProducer;

        public UpdateBookRoomUseCase(
            IBookRoomsRepository repository,
            IMapper mapper,
            ILogger<CancelBookRoomUseCase> logger,
            IBookRoomProducer producer,
            IBookRoomRequestProducer requestProducer)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _producer = producer;
            _requestProducer = requestProducer;
        }

        public async Task<CommonResponse<BookRoomResponse>> HandleAsync(UpdateBookRoomRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var bookRoom = _mapper.Map<BookRooms>(request);

                var updated = await _repository.UpdateAsync(bookRoom.Id, bookRoom, cancellationToken);

                await _producer.SendAsync(updated, cancellationToken);

                var requestUpdated = _mapper.Map<BookRoomNotification>(updated);

                await _requestProducer.SendAsync(requestUpdated, cancellationToken);

                var response = _mapper.Map<BookRoomResponse>(updated);

                return CommonResponse<BookRoomResponse>.Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{UseCase} - {Method} - Exception Thrown", nameof(UpdateBookRoomUseCase), nameof(HandleAsync));
                return CommonResponse<BookRoomResponse>.BadRequest(ErrorMessages.EXCEPTION_ERROR);
            }
        }
    }
}
