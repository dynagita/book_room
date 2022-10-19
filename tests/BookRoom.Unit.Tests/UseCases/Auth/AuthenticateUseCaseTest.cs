using AutoBogus;
using BookRoom.Application.Extensions;
using BookRoom.Application.UseCases.Auth;
using BookRoom.Domain.Contract.Configurations;
using BookRoom.Domain.Contract.Constants;
using BookRoom.Domain.Contract.Requests.Queries.AuthQueries;
using BookRoom.Domain.Contract.UseCases.Auth;
using BookRoom.Domain.Repositories.EntityFramework;
using BookRoom.Unit.Tests.Utils;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace BookRoom.Unit.Tests.UseCases.Auth
{
    public class AuthenticateUseCaseTest
    {
        private readonly IAuthenticateUseCase _useCase;
        private readonly Mock<IUserRepository> _repository;
        private readonly Mock<ILogger<AuthenticateUseCase>> _logger;
        private readonly ITokenCreateUseCase _tokenCreator;
        private readonly Mock<IOptions<AuthenticationConfiguration>> _options;
        public AuthenticateUseCaseTest()
        {
            _repository = new Mock<IUserRepository>();
            _logger = new Mock<ILogger<AuthenticateUseCase>>();
            _options = new Mock<IOptions<AuthenticationConfiguration>>();
            _options.Setup(x => x.Value)
                .Returns(new AuthenticationConfiguration()
                {
                    AuthSecret = "MySuperSecretKey"
                });
            _tokenCreator = new TokenCreateUseCase(_options.Object);

            _useCase = new AuthenticateUseCase(_repository.Object, 
                MapperCreate.CreateMappers(), 
                _logger.Object,
                _tokenCreator);
        }

        [Fact(DisplayName = "ShouldAuthenticate")]
        public async Task ShouldAuthenticate()
        {
            var user = new AutoFaker<Domain.Entities.User>().Generate();

            var request = new AuthRequest()
            {
                Email = user.Email,
                Password = user.Password
            };

            user.Password = user.Password.ComputeHash();

            _repository.Setup(x => x.GetByMailAsync(user.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);
            
            var auth = await _useCase.HandleAsync(request, new CancellationToken());

            auth.Data.Token
                .Should()
                .NotBeNull();
            auth.Data.Token
                .Should()
                .NotBeEmpty();
            auth.Error
                .Should()
                .BeEmpty();
            auth.Data.User
                .Should()
                .NotBeNull();
        }

        [Fact(DisplayName = "ShouldNotFindUserAuthenticate")]
        public async Task ShouldNotFindUserAuthenticate()
        {
            var user = new AutoFaker<Domain.Entities.User>().Generate();

            _repository.Setup(x => x.GetByMailAsync(user.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);
            var request = new AuthRequest()
            {
                Email = "fool@fakeuser.com",
                Password = user.Password
            };
            var auth = await _useCase.HandleAsync(request, new CancellationToken());

            auth.Data
                .Should()
                .BeNull();
            auth.Error
                .Should()
                .Be(ErrorMessages.AuthMessages.USER_NOTFOUND);
            auth.Status
                .Should()
                .Be(400);
        }

        [Fact(DisplayName = "ShouldFailPassword")]
        public async Task ShouldFailPassword()
        {
            var user = new AutoFaker<Domain.Entities.User>().Generate();

            _repository.Setup(x => x.GetByMailAsync(user.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);
            var request = new AuthRequest()
            {
                Email = user.Email,
                Password = "0"
            };
            var auth = await _useCase.HandleAsync(request, new CancellationToken());

            auth.Status
                .Should()
                .Be(400);
            auth.Error
                .Should()
                .Be(ErrorMessages.AuthMessages.PASSWORD_NOTMATCH);
            auth.Data
                .Should()
                .BeNull();
        }
    }
}
