using AutoBogus;
using BookRoom.Application.UseCases.User;
using BookRoom.Domain.Contract.Requests.Commands.User;
using BookRoom.Domain.Contract.UseCases.User;
using BookRoom.Domain.Repositories.EntityFramework;
using BookRoom.Unit.Tests.Utils;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookRoom.Unit.Tests.UseCases.User
{
    public class CreateUserUseCaseTest
    {
        private readonly Mock<IUserRepository> _repository;
        private readonly ICreateUserUseCase _useCase;
        private readonly Mock<ILogger<CreateUserUseCase>> _logger;
        
        public CreateUserUseCaseTest()
        {
            _repository = new Mock<IUserRepository>();
            _logger = new Mock<ILogger<CreateUserUseCase>>();
            _useCase = new CreateUserUseCase(_repository.Object, MapperCreate.CreateMappers(), _logger.Object);
        }

        [Fact(DisplayName = "ShouldCreateUser")]
        public async Task ShouldCreateUser()
        {
            var mapper = MapperCreate.CreateMappers();

            var request = new AutoFaker<UserCreateRequest>().Generate();

            var userResult = mapper.Map<Domain.Entities.User>(request);
            userResult.Id = 1;

            _repository.Setup(x => x.InsertAsync(It.IsAny<Domain.Entities.User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(userResult);

            var response = await _useCase.HandleAsync(request, new CancellationToken());

            response.Data
                .Reference
                .Should()
                .Be(1);
        }

        [Fact(DisplayName = "ShouldNotCreateUser")]
        public async Task ShouldNotCreateUser()
        {
            var mapper = MapperCreate.CreateMappers();

            var request = new AutoFaker<UserCreateRequest>().Generate();

            var userResult = mapper.Map<Domain.Entities.User>(request);
            userResult.Id = 1;

            _repository.Setup(x => x.InsertAsync(It.IsAny<Domain.Entities.User>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            var response = await _useCase.HandleAsync(request, new CancellationToken());

            response.Data
                .Reference
                .Should()
                .Be(0);
            response.Error
                .Should()
                .NotBeNull();
        }

    }
}
