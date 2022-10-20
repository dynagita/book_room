using BookRoom.Domain.Contract.Handlers.Queries.Auth;
using BookRoom.Domain.Contract.Requests.Queries.AuthQueries;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.AuthResponses;
using BookRoom.Domain.Contract.UseCases.Auth;

namespace BookRoom.Application.Handlers.Queries.AuthQueries
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
