using BookRoom.Domain.Contract.Responses.User;
using System.Diagnostics.CodeAnalysis;

namespace BookRoom.Domain.Contract.Responses.Auth
{
    [ExcludeFromCodeCoverage]
    public class AuthResponse
    {        
        public UserResponse User { get; set; }
        public string Token { get; set; }
    }
}
