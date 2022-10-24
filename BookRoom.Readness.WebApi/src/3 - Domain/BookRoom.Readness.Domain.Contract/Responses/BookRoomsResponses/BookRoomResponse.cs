using BookRoom.Readness.Domain.Contract.Enums;
using BookRoom.Readness.Domain.Contract.Responses.RoomResponses;
using BookRoom.Readness.Domain.Contract.Responses.UserResponses;

namespace BookRoom.Readness.Domain.Contract.Responses.BookRoomsResponses
{
    public class BookRoomResponse
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public BookStatusRoom Status { get; set; }

        public UserResponse User { get; set; }

        public RoomResponse Room { get; set; }
    }
}
