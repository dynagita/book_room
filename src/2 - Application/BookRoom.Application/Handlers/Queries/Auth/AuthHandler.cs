using BookRoom.Domain.Contract.Handlers.Queries.Auth;
using BookRoom.Domain.Contract.Requests.Queries.Auth;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.Auth;
using BookRoom.Domain.Contract.UseCases.Auth;

namespace BookRoom.Application.Handlers.Queries.Auth
{
    public class AuthHandler : IAuthHandler
    {
        private readonly IAuthenticateUseCase _useCase;
        public AuthHandler(IAuthenticateUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task<CommonResponse<AuthResponse>> Handle(AuthRequest request, CancellationToken cancellationToken)
        {
            return await _useCase.HandleAsync(request, cancellationToken);
        }
    }
}
