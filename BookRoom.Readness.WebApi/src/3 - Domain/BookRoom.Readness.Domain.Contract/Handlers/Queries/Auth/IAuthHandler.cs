using BookRoom.Readness.Domain.Contract.Requests.Queries.AuthQueries;
using BookRoom.Readness.Domain.Contract.Responses;
using BookRoom.Readness.Domain.Contract.Responses.AuthResponses;
using MediatR;

namespace BookRoom.Readness.Domain.Contract.Handlers.Queries.Auth
{
    public interface IAuthHandler : IRequestHandler<AuthRequest, CommonResponse<AuthResponse>>
    {
    }
}
