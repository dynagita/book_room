using BookRoom.Domain.Contract.Enums;
using BookRoom.Domain.Contract.Responses.RoomResponses;
using BookRoom.Domain.Contract.Responses.UserResponses;

namespace BookRoom.Domain.Contract.Responses.BookRoomsResponses
{
    public class BookRoomResponse
    {
        public int Reference { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public BookStatusRoom Status { get; set; }

        public UserResponse User { get; set; }

        public RoomResponse Room { get; set; }
    }
}
