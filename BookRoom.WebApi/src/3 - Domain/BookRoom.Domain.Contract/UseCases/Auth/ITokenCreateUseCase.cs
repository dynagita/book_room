using BookRoom.Domain.Contract.Responses.UserResponses;

namespace BookRoom.Domain.Contract.UseCases.Auth
{
    public interface ITokenCreateUseCase : IUseCaseBase<UserResponse, string>
    {
    }
}
