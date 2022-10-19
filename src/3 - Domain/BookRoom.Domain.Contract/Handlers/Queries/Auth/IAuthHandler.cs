using BookRoom.Domain.Contract.Requests.Queries.AuthQueries;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.AuthResponses;
using MediatR;

namespace BookRoom.Domain.Contract.Handlers.Queries.Auth
{
    public interface IAuthHandler : IRequestHandler<AuthRequest, CommonResponse<AuthResponse>>
    {
    }
}
