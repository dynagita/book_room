using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.Auth;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace BookRoom.Domain.Contract.Requests.Queries.Auth
{
    [ExcludeFromCodeCoverage]
    public class AuthRequest : IRequest<CommonResponse<AuthResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
