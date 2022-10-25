using BookRoom.Domain.Contract.Requests.Commands.RoomCommands;
using BookRoom.Domain.Contract.Responses;
using MediatR;

namespace BookRoom.Domain.Contract.Handlers.Commands.RoomHandlers
{
    public interface IDeleteRoomHandler : IRequestHandler<DeleteRoomRequest, CommonResponse<bool>>
    {
    }
}
