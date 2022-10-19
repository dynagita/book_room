using BookRoom.Domain.Contract.Handlers.Commands.User;
using BookRoom.Domain.Contract.Requests.Commands.User;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.User;
using BookRoom.Domain.Contract.UseCases.User;
using MediatR;

namespace BookRoom.Application.Handlers.Commands.User
{
    public class UserCreateHandler : IUserCreateHandler
    {
        private readonly ICreateUserUseCase _useCase;

        public UserCreateHandler(ICreateUserUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task<CommonResponse<UserResponse>> Handle(UserCreateRequest request, CancellationToken cancellationToken)
        {
            return await _useCase.HandleAsync(request, cancellationToken);
        }
    }
}
