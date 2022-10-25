using BookRoom.Readness.Domain.Contract.Responses.RoomResponses;

namespace BookRoom.Readness.Domain.Contract.Responses.BookRoomsResponses
{
    public class CheckAvailabilityResponse
    {
        public bool Available { get; set; }

        public string UnavailableMessage { get; set; }

        public RoomResponse Room { get; set; }
    }
}
