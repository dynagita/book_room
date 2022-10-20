using BookRoom.Domain.Contract.Requests.Commands.UserCommands;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.UserResponses;
using MediatR;

namespace BookRoom.Domain.Contract.Handlers.Commands.User
{
    public interface IUserCreateHandler : IRequestHandler<UserCreateRequest, CommonResponse<UserResponse>>
    {
    }
}
