using BookRoom.Domain.Contract.Responses.BookRoomsResponses;
using BookRoom.Domain.Contract.Responses;
using MediatR;

namespace BookRoom.Domain.Contract.Requests.Commands.BookRooms
{
    public class CancelBookRoomRequest : IRequest<CommonResponse<BookRoomResponse>>
    {
        public int Reference { get; set; }
    }
}
