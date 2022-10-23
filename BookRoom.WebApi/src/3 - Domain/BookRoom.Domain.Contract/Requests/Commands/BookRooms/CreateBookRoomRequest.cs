using BookRoom.Domain.Contract.Responses.BookRoomsResponses;
using BookRoom.Domain.Contract.Responses;
using MediatR;
using BookRoom.Domain.Contract.Enums;
using BookRoom.Domain.Contract.Responses.UserResponses;
using BookRoom.Domain.Contract.Responses.RoomResponses;

namespace BookRoom.Domain.Contract.Requests.Commands.BookRooms
{
    public class CreateBookRoomRequest : IRequest<CommonResponse<BookRoomResponse>>
    {
        public int Reference { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public BookStatusRoom Status { get; set; }

        public UserResponse User { get; set; }

        public RoomResponse Room { get; set; }
    }
}
