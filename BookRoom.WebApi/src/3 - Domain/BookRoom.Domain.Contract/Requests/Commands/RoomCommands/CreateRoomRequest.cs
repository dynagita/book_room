using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.RoomResponses;
using MediatR;

namespace BookRoom.Domain.Contract.Requests.Commands.RoomCommands
{
    public class CreateRoomRequest : IRequest<CommonResponse<RoomResponse>>
    {
        public int Reference { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Number { get; set; }
    }
}
