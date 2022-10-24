using BookRoom.Readness.Domain.Contract.Responses.UserResponses;

namespace BookRoom.Readness.Domain.Contract.UseCases.Auth
{
    public interface ITokenCreateUseCase : IUseCaseBase<UserResponse, string>
    {
    }
}
