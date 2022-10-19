using BookRoom.Domain.Contract.Responses.UserResponses;
using System.Diagnostics.CodeAnalysis;

namespace BookRoom.Domain.Contract.Responses.AuthResponses
{
    [ExcludeFromCodeCoverage]
    public class AuthResponse
    {        
        public UserResponse User { get; set; }
        public string Token { get; set; }
    }
}
