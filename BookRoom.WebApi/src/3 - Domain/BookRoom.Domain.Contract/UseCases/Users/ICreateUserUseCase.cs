using BookRoom.Domain.Contract.Requests.Commands.UserCommands;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.UserResponses;

namespace BookRoom.Domain.Contract.UseCases.Users
{
    public interface ICreateUserUseCase : IUseCaseBase<UserCreateRequest, CommonResponse<UserResponse>>
    {
    }
}
