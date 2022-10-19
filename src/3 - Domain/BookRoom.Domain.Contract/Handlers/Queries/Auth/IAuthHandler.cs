using BookRoom.Domain.Contract.Requests.Queries.Auth;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.Auth;
using MediatR;

namespace BookRoom.Domain.Contract.Handlers.Queries.Auth
{
    public interface IAuthHandler : IRequestHandler<AuthRequest, CommonResponse<AuthResponse>>
    {
    }
}
