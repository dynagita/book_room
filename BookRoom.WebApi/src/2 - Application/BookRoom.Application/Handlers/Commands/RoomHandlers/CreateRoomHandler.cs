using BookRoom.Domain.Contract.Handlers.Commands.RoomHandlers;
using BookRoom.Domain.Contract.Requests.Commands.RoomCommands;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.RoomResponses;
using BookRoom.Domain.Contract.UseCases.Rooms;

namespace BookRoom.Application.Handlers.Commands.RoomHandlers
{
    public class CreateRoomHandler : ICreateRoomHandler
    {
        private readonly ICreateRoomUseCase _useCase;

        public CreateRoomHandler(ICreateRoomUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task<CommonResponse<RoomResponse>> Handle(CreateRoomRequest request, CancellationToken cancellationToken)
        {
            return await _useCase.HandleAsync(request, cancellationToken);
        }
    }
}
