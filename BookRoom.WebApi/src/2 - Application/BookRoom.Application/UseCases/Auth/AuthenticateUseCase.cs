using AutoMapper;
using BookRoom.Application.Extensions;
using BookRoom.Domain.Contract.Configurations;
using BookRoom.Domain.Contract.Constants;
using BookRoom.Domain.Contract.Requests.Queries.AuthQueries;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.AuthResponses;
using BookRoom.Domain.Contract.Responses.UserResponses;
using BookRoom.Domain.Contract.UseCases.Auth;
using BookRoom.Domain.Repositories.EntityFramework;
using BookRoom.Domain.Validation.AuthValidations;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookRoom.Application.UseCases.Auth
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
 