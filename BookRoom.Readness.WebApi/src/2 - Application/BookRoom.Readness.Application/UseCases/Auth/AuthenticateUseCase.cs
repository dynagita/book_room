using AutoMapper;
using BookRoom.Readness.Application.Extensions;
using BookRoom.Readness.Domain.Contract.Constants;
using BookRoom.Readness.Domain.Contract.Requests.Queries.AuthQueries;
using BookRoom.Readness.Domain.Contract.Responses;
using BookRoom.Readness.Domain.Contract.Responses.AuthResponses;
using BookRoom.Readness.Domain.Contract.Responses.UserResponses;
using BookRoom.Readness.Domain.Contract.UseCases.Auth;
using BookRoom.Readness.Domain.Repositories;
using BookRoom.Readness.Domain.Validation.AuthValidations;
using Microsoft.Extensions.Logging;

namespace BookRoom.Readness.Application.UseCases.Auth
{
    public class AuthenticateUseCase : IAuthenticateUseCase
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthenticateUseCase> _logger;
        private readonly ITokenCreateUseCase _tokenCreateUseCase;
        public AuthenticateUseCase(
            IUserRepository userRepository,
            IMapper mapper,
            ILogger<AuthenticateUseCase> logger,
            ITokenCreateUseCase tokenCreateUse
        )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _tokenCreateUseCase = tokenCreateUse;
        }
        
        public async Task<CommonResponse<AuthResponse>> HandleAsync(AuthRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByMailAsync(request.Email, cancellationToken);
            
            if (user == null)
                return CommonResponse<AuthResponse>.BadRequest(ErrorMessages.AuthMessages.USER_NOTFOUND);

            request.Password = request.Password.ComputeHash();

            var mathPasswordValidation = new MatchPasswordValidation(request.Password);
            var mathPassword = await mathPasswordValidation.ValidateAsync(user);
            
            if(!mathPassword.IsValid)
                return CommonResponse<AuthResponse>.BadRequest(mathPassword.Errors.FirstOrDefault().ErrorMessage);

            var userResponse = _mapper.Map<UserResponse>(user);
            AuthResponse response = new AuthResponse()
            {
                Token = await _tokenCreateUseCase.HandleAsync(userResponse, cancellationToken),
                User = userResponse
            };

            return CommonResponse<AuthResponse>.Ok(response);
        }

        
    }
}
 