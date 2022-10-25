using BookRoom.Readness.Domain.Contract.Responses.UserResponses;
using System.Diagnostics.CodeAnalysis;

namespace BookRoom.Readness.Domain.Contract.Responses.AuthResponses
{
    [ExcludeFromCodeCoverage]
    public class AuthResponse
    {        
        public UserResponse User { get; set; }
        public string Token { get; set; }
    }
}
