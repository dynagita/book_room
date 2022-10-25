using BookRoom.Domain.Contract.Responses;
using MediatR;

namespace BookRoom.Domain.Contract.Requests.Commands.RoomCommands
{
    public class DeleteRoomRequest : IRequest<CommonResponse<bool>>
    {
        public int Id { get; set; }
    }
}
