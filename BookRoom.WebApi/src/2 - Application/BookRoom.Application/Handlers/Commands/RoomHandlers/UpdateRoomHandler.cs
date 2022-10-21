using BookRoom.Domain.Contract.Handlers.Commands.RoomHandlers;
using BookRoom.Domain.Contract.Requests.Commands.RoomCommands;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.RoomResponses;
using BookRoom.Domain.Contract.UseCases.Rooms;

namespace BookRoom.Application.Handlers.Commands.RoomHandlers
{
    public class UpdateRoomHandler : IUpdateRoomHandler
    {
        private readonly IUpdateRoomUseCase _useCase;

        public UpdateRoomHandler(IUpdateRoomUseCase useCase)
        {
            _useCase = useCase;
        }

        public Task<CommonResponse<RoomResponse>> Handle(UpdateRoomRequest request, CancellationToken cancellationToken)
        {
            return _useCase.HandleAsync(request.Reference, request, cancellationToken);
        }
    }
}
