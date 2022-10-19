using BookRoom.Domain.Contract.Requests.Commands.User;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.User;

namespace BookRoom.Domain.Contract.UseCases.User
{
    public interface ICreateUserUseCase : IUseCaseBase<UserCreateRequest, CommonResponse<UserResponse>>
    {
    }
}
