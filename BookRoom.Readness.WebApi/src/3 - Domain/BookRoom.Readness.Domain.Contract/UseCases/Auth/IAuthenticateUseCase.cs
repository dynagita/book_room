using BookRoom.Readness.Domain.Contract.Requests.Queries.AuthQueries;
using BookRoom.Readness.Domain.Contract.Responses;
using BookRoom.Readness.Domain.Contract.Responses.AuthResponses;

namespace BookRoom.Readness.Domain.Contract.UseCases.Auth
{
    public interface IAuthenticateUseCase : IUseCaseBase<AuthRequest, CommonResponse<AuthResponse>>
    {
    }
}
