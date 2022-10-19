using System.Diagnostics.CodeAnalysis;

namespace BookRoom.Domain.Contract.Responses.User
{
    [ExcludeFromCodeCoverage]
    public class UserResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BornDate { get; set; }
        public string Email { get; set; }
        public int Reference { get; set; }
    }
}
