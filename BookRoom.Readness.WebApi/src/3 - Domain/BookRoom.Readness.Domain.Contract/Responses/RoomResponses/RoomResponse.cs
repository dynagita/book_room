using BookRoom.Readness.Domain.Contract.Responses.BookRoomsResponses;

namespace BookRoom.Readness.Domain.Contract.Responses.RoomResponses
{
    public class RoomResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Number { get; set; }

        public List<BookRoomResponse> Books { get; set; }
    }
}
