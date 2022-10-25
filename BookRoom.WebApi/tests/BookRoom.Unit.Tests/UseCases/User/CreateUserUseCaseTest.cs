using AutoBogus;
using BookRoom.Application.Extensions;
using BookRoom.Application.UseCases.UserUseCases;
using BookRoom.Domain.Contract.Constants;
using BookRoom.Domain.Contract.Requests.Commands.UserCommands;
using BookRoom.Domain.Contract.UseCases.Users;
using BookRoom.Domain.Queue;
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
        private readonly Mock<IUserProducer> _producer;
        public CreateUserUseCaseTest()
        {
            _repository = new Mock<IUserRepository>();
            _logger = new Mock<ILogger<CreateUserUseCase>>();
            _producer = new Mock<IUserProducer>();
            _useCase = new CreateUserUseCase(_repository.Object, MapperCreate.CreateMappers(), _logger.Object, _producer.Object);
        }

        [Fact(DisplayName = "ShouldCreateUser")]
        public async Task ShouldCreateUser()
        {
            var mapper = MapperCreate.CreateMappers();

            var request = new AutoFaker<UserCreateRequest>().Generate();

            var userResult = mapper.Map<Domain.Entities.User>(request);
            userResult.Id = 1;
            userResult.Password = userResult.Password.ComputeHash();

            _repository.Setup(x => x.InsertAsync(It.IsAny<Domain.Entities.User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(userResult);

            var response = await _useCase.HandleAsync(request, new CancellationToken());

            response.Data
                .Reference
                .Should()
                .Be(1);
            response.Status
                .Should()
                .Be(200);
        }

        [Fact(DisplayName = "ShouldHasEmailRegistered")]
        public async Task ShouldHasEmailRegistered()
        {
            var mapper = MapperCreate.CreateMappers();

            var request = new AutoFaker<UserCreateRequest>().Generate();

            var userResult = mapper.Map<Domain.Entities.User>(request);
            userResult.Id = 1;

            _repository.Setup(x => x.GetByMailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(userResult);

            _repository.Setup(x => x.InsertAsync(It.IsAny<Domain.Entities.User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(userResult);

            var response = await _useCase.HandleAsync(request, new CancellationToken());

            response.Status
                .Should()
                .Be(400);
            response.Data
                .Should()
                .BeNull();
            response.Error
                .Should()
                .Be(ErrorMessages.UserMessages.USER_REGISTERED);
        }

        [Fact(DisplayName = "ShouldNotCreateUser")]
        public async Task ShouldNotCreateUser()
        {
            var mapper = MapperCreate.CreateMappers();

            var request = new AutoFaker<UserCreateRequest>().Generate();

            var userResult = mapper.Map<Domain.Entities.User>(request);
            userResult.Id = 1;

            _repository.Setup(x => x.GetByMailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            _repository.Setup(x => x.InsertAsync(It.IsAny<Domain.Entities.User>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            var response = await _useCase.HandleAsync(request, new CancellationToken());

            response.Status
                .Should()
                .Be(400);
            response.Data
                .Should()
                .BeNull();
            response.Error
                .Should()
                .Be(ErrorMessages.EXCEPTION_ERROR);
        }
    }
}
