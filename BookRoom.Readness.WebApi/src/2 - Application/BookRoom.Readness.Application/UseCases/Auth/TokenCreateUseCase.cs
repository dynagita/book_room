using BookRoom.Readness.Domain.Contract.Configurations;
using BookRoom.Readness.Domain.Contract.Responses.UserResponses;
using BookRoom.Readness.Domain.Contract.UseCases.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookRoom.Readness.Application.UseCases.Auth
{
    public class TokenCreateUseCase : ITokenCreateUseCase
    {
        private readonly AuthenticationConfiguration _configuration;

        public TokenCreateUseCase(IOptions<AuthenticationConfiguration> options)
        {
            _configuration = options.Value;
        }

        public async Task<string> HandleAsync(UserResponse request, CancellationToken cancellationToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.AuthSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, request.FirstName),
                    new Claim(ClaimTypes.Email, request.Email),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
