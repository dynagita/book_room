using BookRoom.Domain.Contract.Requests.Commands.RoomCommands;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.RoomResponses;
using MediatR;

namespace BookRoom.Domain.Contract.Handlers.Commands.RoomHandlers
{
    public interface IUpdateRoomHandler : IRequestHandler<UpdateRoomRequest, CommonResponse<RoomResponse>>
    {
    }
}
