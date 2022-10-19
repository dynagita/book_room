using BookRoom.Domain.Contract.Requests.Queries.Auth;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.Auth;

namespace BookRoom.Domain.Contract.UseCases.Auth
{
    public interface IAuthenticateUseCase : IUseCaseBase<AuthRequest, CommonResponse<AuthResponse>>
    {
    }
}
