using BookRoom.Domain.Contract.Constants;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.UseCases.Rooms;
using BookRoom.Domain.Queue;
using BookRoom.Domain.Repositories.EntityFramework;
using Serilog;

namespace BookRoom.Application.UseCases.RoomUseCases
{
    public class DeleteRoomUseCase : IDeleteRoomUseCase
    {
        private readonly IRoomRepository _repository;
        private readonly ILogger _logger;
        private readonly IRoomProducer _producer;
        public DeleteRoomUseCase(IRoomRepository repository, ILogger logger, IRoomProducer producer)
        {
            _repository = repository;
            _logger = logger;
            _producer = producer;
        }

        public async Task<CommonResponse<bool>> HandleAsync(int request, CancellationToken cancellationToken)
        {
            try 
            {
                var room = await _repository.DeleteAsync(request, cancellationToken);
                
                await _producer.SendAsync(room, cancellationToken);

                return CommonResponse<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{UseCase} - {Method} - Exception Thrown", nameof(DeleteRoomUseCase), nameof(HandleAsync));
                return CommonResponse<bool>.BadRequest(ErrorMessages.EXCEPTION_ERROR);
            }
        }
    }
}
