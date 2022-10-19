using AutoBogus;
using BookRoom.Application.UseCases.Auth;
using BookRoom.Domain.Contract.Requests.Queries.Auth;
using BookRoom.Domain.Contract.UseCases.Auth;
using BookRoom.Domain.Repositories.EntityFramework;
using BookRoom.Unit.Tests.Utils;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookRoom.Unit.Tests.UseCases.Auth
{
    public class AuthenticateUseCaseTest
    {
        private readonly IAuthenticateUseCase _useCase;
        private readonly Mock<IUserRepository> _repository;
        private readonly Mock<ILogger<AuthenticateUseCase>> _logger;
        public AuthenticateUseCaseTest()
        {
            _repository = new Mock<IUserRepository>();
            _logger = new Mock<ILogger<AuthenticateUseCase>>();
            _useCase = new AuthenticateUseCase(_repository.Object, MapperCreate.CreateMappers(), _logger.Object);
        }

        [Fact(DisplayName = "ShouldAuthenticate")]
        public async Task ShouldAuthenticate()
        {
            var user = new AutoFaker<Domain.Entities.User>().Generate();
            
            _repository.Setup(x => x.GetByMailAsync(user.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);
            var request = new AuthRequest()
            {
                Email = user.Email,
                Password = user.Password
            };
            var auth = await _useCase.HandleAsync(request, new CancellationToken());

            auth.Data.Token
                .Should()
                .NotBeNull();
            auth.Error
                .Should()
                .BeNull();
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

            auth.Data.Token
                .Should()
                .BeNull();
            auth.Error
                .Should()
                .NotBeNull();
            auth.Data.User
                .Should()
                .BeNull();
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

            auth.Data.Token
                .Should()
                .BeNull();
            auth.Error
                .Should()
                .NotBeNull();
            auth.Data.User
                .Should()
                .NotBeNull();
        }
    }
}
