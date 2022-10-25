using BookRoom.Domain.Contract.Handlers.Commands.RoomHandlers;
using BookRoom.Domain.Contract.Requests.Commands.RoomCommands;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.UseCases.Rooms;

namespace BookRoom.Application.Handlers.Commands.RoomHandlers
{
    public class DeleteRoomHandler : IDeleteRoomHandler
    {
        private readonly IDeleteRoomUseCase _useCase;

        public DeleteRoomHandler(IDeleteRoomUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task<CommonResponse<bool>> Handle(DeleteRoomRequest request, CancellationToken cancellationToken)
        {
            return await _useCase.HandleAsync(request.Id, cancellationToken);
        }
    }
}
