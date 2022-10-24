using AutoBogus;
using AutoMapper;
using BookRoom.Service.Application.UseCases.BookRoomPropagation;
using BookRoom.Service.Domain.Contract.Notifications;
using BookRoom.Service.Domain.Contract.UseCases;
using BookRoom.Service.Domain.Repositories;
using BookRoom.Service.Unit.Tests.Utils;
using BookRoom.Services.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookRoom.Service.Unit.Tests.UseCases.BookRoomPropagation
{
    public class PropagateBookRoomUseCaseTest 
    {
        private readonly Mock<IBookRoomRepository> _repository;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<PropagateBookRoomUseCase>> _logger;
        private readonly Mock<IUpdateBookRoomInRoomUseCase> _propagateToRoom;
        private readonly Mock<IUpdateBookRoomInUserUseCase> _propagateToUser;
        private readonly IPropagateBookRoomUseCase _useCase;

        public PropagateBookRoomUseCaseTest()
        {
            _repository = new Mock<IBookRoomRepository>();
            _mapper = MapperCreate.CreateMappers();
            _logger = new Mock<ILogger<PropagateBookRoomUseCase>>();
            _propagateToRoom = new Mock<IUpdateBookRoomInRoomUseCase>();
            _propagateToUser = new Mock<IUpdateBookRoomInUserUseCase>();

            _useCase = new PropagateBookRoomUseCase(
                _logger.Object,
                _mapper,
                _repository.Object,                               
                _propagateToRoom.Object,
                _propagateToUser.Object
                );
        }

        [Fact(DisplayName = "ShouldCreateBookRoom")]
        public async Task ShouldCreateBookRoom()
        {
            var notification = new AutoFaker<BookRoomsNotification>().Generate();

            var entity = _mapper.Map<BookRooms>(notification);

            _repository.Setup(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()));

            _repository.Setup(x => x.InsertAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()));
            
            _propagateToRoom.Setup(x => x.HandleAsync(It.IsAny<BookRoomsNotification>(), It.IsAny<CancellationToken>()));
            _propagateToUser.Setup(x => x.HandleAsync(It.IsAny<BookRoomsNotification>(), It.IsAny<CancellationToken>()));

            await _useCase.HandleAsync(notification, new CancellationToken());

            _repository.Verify(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
            _repository.Verify(x => x.InsertAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()), Times.Once);
            _propagateToRoom.Verify(x => x.HandleAsync(It.IsAny<BookRoomsNotification>(), It.IsAny<CancellationToken>()), Times.Once);
            _propagateToUser.Verify(x => x.HandleAsync(It.IsAny<BookRoomsNotification>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "ShouldUpdateBookRoom")]
        public async Task ShouldUpdateBookRoom()
        {
            var notification = new AutoFaker<BookRoomsNotification>().Generate();

            var entity = _mapper.Map<BookRooms>(notification);

            _repository.Setup(x => x.UpdateAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()));

            _repository.Setup(x => x.FindOneAsync(notification.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);

            _propagateToRoom.Setup(x => x.HandleAsync(It.IsAny<BookRoomsNotification>(), It.IsAny<CancellationToken>()));
            _propagateToUser.Setup(x => x.HandleAsync(It.IsAny<BookRoomsNotification>(), It.IsAny<CancellationToken>()));

            await _useCase.HandleAsync(notification, new CancellationToken());

            _repository.Verify(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
            _repository.Verify(x => x.UpdateAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()), Times.Once);
            _propagateToRoom.Verify(x => x.HandleAsync(It.IsAny<BookRoomsNotification>(), It.IsAny<CancellationToken>()), Times.Once);
            _propagateToUser.Verify(x => x.HandleAsync(It.IsAny<BookRoomsNotification>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "ShouldExceptionBookRoom")]
        public async Task ShouldExceptionBookRoom()
        {
            var notification = new AutoFaker<BookRoomsNotification>().Generate();

            var entity = _mapper.Map<BookRooms>(notification);

            _repository.Setup(x => x.FindOneAsync(notification.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);

            _repository.Setup(x => x.UpdateAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());            

            _propagateToRoom.Setup(x => x.HandleAsync(It.IsAny<BookRoomsNotification>(), It.IsAny<CancellationToken>()));
            _propagateToUser.Setup(x => x.HandleAsync(It.IsAny<BookRoomsNotification>(), It.IsAny<CancellationToken>()));

            await _useCase.HandleAsync(notification, new CancellationToken());

            _repository.Verify(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()), Times.Once);
            _propagateToRoom.Verify(x => x.HandleAsync(It.IsAny<BookRoomsNotification>(), It.IsAny<CancellationToken>()), Times.Never);
            _propagateToUser.Verify(x => x.HandleAsync(It.IsAny<BookRoomsNotification>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
