using AutoBogus;
using AutoMapper;
using BookRoom.Service.Application.UseCases.RoomPropagation;
using BookRoom.Service.Application.UseCases.UserPropagation;
using BookRoom.Service.Domain.Contract.Notifications;
using BookRoom.Service.Domain.Contract.UseCases;
using BookRoom.Service.Domain.Repositories;
using BookRoom.Service.Unit.Tests.Utils;
using BookRoom.Services.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookRoom.Service.Unit.Tests.UseCases.RoomPropagation
{
    public class PropagateRoomUseCaseTest
    {
        private readonly Mock<IRoomRepository> _repository;        
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<PropagateRoomUseCase>> _logger;
        private readonly Mock<IUpdateRoomBookRoomUseCase> _propagateToBooks;
        private readonly IPropagateRoomUseCase _useCase;

        public PropagateRoomUseCaseTest()
        {
            _repository = new Mock<IRoomRepository>();
            _mapper = MapperCreate.CreateMappers();
            _logger = new Mock<ILogger<PropagateRoomUseCase>>();
            _propagateToBooks = new Mock<IUpdateRoomBookRoomUseCase>();

            _useCase = new PropagateRoomUseCase(
                _repository.Object,
                _mapper,
                _logger.Object,
                _propagateToBooks.Object
                );
        }

        [Fact(DisplayName = "ShouldCreateBookRoom")]
        public async Task ShouldCreateBookRoom()
        {
            var notification = new AutoFaker<RoomNotification>().Generate();

            var entity = _mapper.Map<Room>(notification);

            _repository.Setup(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()));

            _repository.Setup(x => x.InsertAsync(It.IsAny<Room>(), It.IsAny<CancellationToken>()));

            _propagateToBooks.Setup(x => x.HandleAsync(It.IsAny<RoomNotification>(), It.IsAny<CancellationToken>()));

            await _useCase.HandleAsync(notification, new CancellationToken());

            _repository.Verify(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
            _repository.Verify(x => x.InsertAsync(It.IsAny<Room>(), It.IsAny<CancellationToken>()), Times.Once);
            _propagateToBooks.Verify(x => x.HandleAsync(It.IsAny<RoomNotification>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "ShouldUpdateBookRoom")]
        public async Task ShouldUpdateBookRoom()
        {
            var notification = new AutoFaker<RoomNotification>().Generate();

            var entity = _mapper.Map<Room>(notification);

            _repository.Setup(x => x.UpdateAsync(It.IsAny<Room>(), It.IsAny<CancellationToken>()));

            _repository.Setup(x => x.FindOneAsync(notification.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);

            _propagateToBooks.Setup(x => x.HandleAsync(It.IsAny<RoomNotification>(), It.IsAny<CancellationToken>()));

            await _useCase.HandleAsync(notification, new CancellationToken());

            _repository.Verify(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
            _repository.Verify(x => x.UpdateAsync(It.IsAny<Room>(), It.IsAny<CancellationToken>()), Times.Once);
            _propagateToBooks.Verify(x => x.HandleAsync(It.IsAny<RoomNotification>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "ShouldExceptionBookRoom")]
        public async Task ShouldExceptionBookRoom()
        {
            var notification = new AutoFaker<RoomNotification>().Generate();

            var entity = _mapper.Map<Room>(notification);

            _repository.Setup(x => x.FindOneAsync(notification.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);

            _repository.Setup(x => x.UpdateAsync(It.IsAny<Room>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            _propagateToBooks.Setup(x => x.HandleAsync(It.IsAny<RoomNotification>(), It.IsAny<CancellationToken>()));

            await _useCase.HandleAsync(notification, new CancellationToken());

            _repository.Verify(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateAsync(It.IsAny<Room>(), It.IsAny<CancellationToken>()), Times.Once);
            _propagateToBooks.Verify(x => x.HandleAsync(It.IsAny<RoomNotification>(), It.IsAny<CancellationToken>()), Times.Never);
        }

    }
}
