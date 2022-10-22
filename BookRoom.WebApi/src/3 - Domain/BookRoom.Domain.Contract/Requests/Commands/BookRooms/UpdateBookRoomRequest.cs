using BookRoom.Domain.Contract.Enums;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.BookRoomsResponses;
using BookRoom.Domain.Contract.Responses.RoomResponses;
using BookRoom.Domain.Contract.Responses.UserResponses;
using MediatR;

namespace BookRoom.Domain.Contract.Requests.Commands.BookRooms
{
    public class UpdateBookRoomRequest : IRequest<CommonResponse<BookRoomResponse>>
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public BookStatusRoom Status { get; set; }

        public UserResponse User { get; set; }

        public RoomResponse Room { get; set; }
    }
}
