using BookRoom.Domain.Contract.Requests.Commands.User;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.User;
using MediatR;

namespace BookRoom.Domain.Contract.Handlers.Commands.User
{
    public interface IUserCreateHandler : IRequestHandler<UserCreateRequest, CommonResponse<UserResponse>>
    {
    }
}
