using AutoBogus;
using AutoMapper;
using BookRoom.Service.Application.UseCases.UserPropagation;
using BookRoom.Service.Domain.Contract.Notifications;
using BookRoom.Service.Domain.Contract.UseCases;
using BookRoom.Service.Domain.Repositories;
using BookRoom.Service.Unit.Tests.Utils;
using BookRoom.Services.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookRoom.Service.Unit.Tests.UseCases.UserPropagation
{
    public class PropagateUserUseCaseTest
    {
        private readonly Mock<IUserRepository> _repository;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<PropagateUserUseCase>> _logger;
        private readonly Mock<IUpdateUserBookRoomUseCase> _propagateToBooks;
        private readonly IPropagateUserUseCase _useCase;

        public PropagateUserUseCaseTest()
        {
            _repository = new Mock<IUserRepository>();
            _mapper = MapperCreate.CreateMappers();
            _logger = new Mock<ILogger<PropagateUserUseCase>>();
            _propagateToBooks = new Mock<IUpdateUserBookRoomUseCase>();

            _useCase = new PropagateUserUseCase(
                _repository.Object,
                _mapper,
                _logger.Object,
                _propagateToBooks.Object
                );
        }
        [Fact(DisplayName = "ShouldCreateBookRoom")]
        public async Task ShouldCreateBookRoom()
        {
            var notification = new AutoFaker<UserNotification>().Generate();

            var entity = _mapper.Map<User>(notification);

            _repository.Setup(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()));

            _repository.Setup(x => x.InsertAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()));

            _propagateToBooks.Setup(x => x.HandleAsync(It.IsAny<UserNotification>(), It.IsAny<CancellationToken>()));

            await _useCase.HandleAsync(notification, new CancellationToken());

            _repository.Verify(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
            _repository.Verify(x => x.InsertAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
            _propagateToBooks.Verify(x => x.HandleAsync(It.IsAny<UserNotification>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "ShouldUpdateBookRoom")]
        public async Task ShouldUpdateBookRoom()
        {
            var notification = new AutoFaker<UserNotification>().Generate();

            var entity = _mapper.Map<User>(notification);

            _repository.Setup(x => x.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()));

            _repository.Setup(x => x.FindOneAsync(notification.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);

            _propagateToBooks.Setup(x => x.HandleAsync(It.IsAny<UserNotification>(), It.IsAny<CancellationToken>()));

            await _useCase.HandleAsync(notification, new CancellationToken());

            _repository.Verify(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
            _repository.Verify(x => x.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
            _propagateToBooks.Verify(x => x.HandleAsync(It.IsAny<UserNotification>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "ShouldExceptionBookRoom")]
        public async Task ShouldExceptionBookRoom()
        {
            var notification = new AutoFaker<UserNotification>().Generate();

            var entity = _mapper.Map<User>(notification);

            _repository.Setup(x => x.FindOneAsync(notification.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);

            _repository.Setup(x => x.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            _propagateToBooks.Setup(x => x.HandleAsync(It.IsAny<UserNotification>(), It.IsAny<CancellationToken>()));

            await _useCase.HandleAsync(notification, new CancellationToken());

            _repository.Verify(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
            _propagateToBooks.Verify(x => x.HandleAsync(It.IsAny<UserNotification>(), It.IsAny<CancellationToken>()), Times.Never);
        }

    }
}
