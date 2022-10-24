using BookRoom.Readness.Domain.Contract.Handlers.Queries.Auth;
using BookRoom.Readness.Domain.Contract.Requests.Queries.AuthQueries;
using BookRoom.Readness.Domain.Contract.Responses;
using BookRoom.Readness.Domain.Contract.Responses.AuthResponses;
using BookRoom.Readness.Domain.Contract.UseCases.Auth;

namespace BookRoom.Readness.Application.Handlers.Queries.AuthQueries
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
