using BookRoom.Domain.Contract.Requests.Queries.AuthQueries;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.AuthResponses;
using BookRoom.Domain.Contract.Responses.UserResponses;

namespace BookRoom.Domain.Contract.UseCases.Auth
{
    public interface IAuthenticateUseCase : IUseCaseBase<AuthRequest, CommonResponse<AuthResponse>>
    {
    }
}
