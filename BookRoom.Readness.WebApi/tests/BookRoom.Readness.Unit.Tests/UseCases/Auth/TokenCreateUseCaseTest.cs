using AutoBogus;
using BookRoom.Readness.Application.UseCases.Auth;
using BookRoom.Readness.Domain.Contract.Configurations;
using BookRoom.Readness.Domain.Contract.Responses.UserResponses;
using BookRoom.Readness.Domain.Contract.UseCases.Auth;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;

namespace BookRoom.Unit.Tests.UseCases.Auth
{
    public class TokenCreateUseCaseTest
    {
        private Mock<IOptions<AuthenticationConfiguration>> _options;
        private ITokenCreateUseCase _useCase;

        public TokenCreateUseCaseTest()
        {
            _options = new Mock<IOptions<AuthenticationConfiguration>>();
            _options.Setup(x => x.Value)
                .Returns(new AuthenticationConfiguration()
                {
                    AuthSecret = "MySuperSecurityKey"
                });

            _useCase = new TokenCreateUseCase(_options.Object);
        }

        [Fact(DisplayName = "ShouldCreateToken")]
        public async Task ShouldCreateToken()
        {            
            var authResponse = new AutoFaker<UserResponse>().Generate();

            var token = await _useCase.HandleAsync(authResponse, new CancellationToken());

            token
                .Should()
                .NotBeNull();

            token
                .Should()
                .NotBeEmpty();
        }
    }
}
