using BookRoom.Readness.Domain.Contract.Responses.BookRoomsResponses;
using System.Diagnostics.CodeAnalysis;

namespace BookRoom.Readness.Domain.Contract.Responses.UserResponses
{
    [ExcludeFromCodeCoverage]
    public class UserResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BornDate { get; set; }
        public string Email { get; set; }
        public int Reference { get; set; }
        public List<BookRoomResponse> Books { get; set; }
    }
}
