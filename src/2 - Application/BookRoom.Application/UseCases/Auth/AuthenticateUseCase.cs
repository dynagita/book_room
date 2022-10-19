using AutoMapper;
using BookRoom.Domain.Contract.Requests.Queries.Auth;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.Auth;
using BookRoom.Domain.Contract.UseCases.Auth;
using BookRoom.Domain.Repositories.EntityFramework;
using Microsoft.Extensions.Logging;

namespace BookRoom.Application.UseCases.Auth
{
    public class AuthenticateUseCase : IAuthenticateUseCase
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthenticateUseCase> _logger;
        public AuthenticateUseCase(
            IUserRepository userRepository,
            IMapper mapper,
            ILogger<AuthenticateUseCase> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<CommonResponse<AuthResponse>> HandleAsync(AuthRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
 