using BookRoom.Readness.Domain.Contract.Responses;
using BookRoom.Readness.Domain.Contract.Responses.AuthResponses;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace BookRoom.Readness.Domain.Contract.Requests.Queries.AuthQueries
{
    [ExcludeFromCodeCoverage]
    public class AuthRequest : IRequest<CommonResponse<AuthResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
