using AutoMapper;
using BookRoom.Domain.Contract.Constants;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.BookRoomsResponses;
using BookRoom.Domain.Contract.UseCases.BookRooms;
using BookRoom.Domain.Queue;
using BookRoom.Domain.Repositories.EntityFramework;
using Serilog;

namespace BookRoom.Application.UseCases.BookRoomUseCases
{
    public class CancelBookRoomUseCase : ICancelBookRoomUseCase
    {
        private readonly IBookRoomsRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IBookRoomProducer _producer;

        public CancelBookRoomUseCase(
            IBookRoomsRepository repository,
            IMapper mapper,
            ILogger logger,
            IBookRoomProducer producer)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _producer = producer;
        }

        public async Task<CommonResponse<BookRoomResponse>> HandleAsync(int request, CancellationToken cancellationToken)
        {
            try
            {
                var bookResponse = await _repository.GetAsync(request, cancellationToken);

                bookResponse.Status = Domain.Contract.Enums.BookStatusRoom.Canceled;

                var updated = await _repository.UpdateAsync(bookResponse.Id, bookResponse, cancellationToken);

                await _producer.SendAsync(updated, cancellationToken);

                var response = _mapper.Map<BookRoomResponse>(updated);

                return CommonResponse<BookRoomResponse>.Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{UseCase} - {Method} - Exception Thrown", nameof(CancelBookRoomUseCase), nameof(HandleAsync));
                return CommonResponse<BookRoomResponse>.BadRequest(ErrorMessages.EXCEPTION_ERROR);
            }            
        }
    }
}
