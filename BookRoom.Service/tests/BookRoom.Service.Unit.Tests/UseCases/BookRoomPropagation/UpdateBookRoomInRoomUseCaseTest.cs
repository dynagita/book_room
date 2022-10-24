using AutoBogus;
using AutoMapper;
using BookRoom.Service.Application.UseCases.RoomPropagation;
using BookRoom.Service.Domain.Contract.Notifications;
using BookRoom.Service.Domain.Contract.UseCases;
using BookRoom.Service.Domain.Repositories;
using BookRoom.Service.Unit.Tests.Utils;
using BookRoom.Services.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookRoom.Service.Application.UseCases.BookRoomPropagation
{
    public class UpdateBookRoomInRoomUseCaseTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRoomRepository> _repository;
        private readonly Mock<ILogger<UpdateBookRoomInRoomUseCase>> _logger;
        private readonly IUpdateBookRoomInRoomUseCase _useCase;
        public UpdateBookRoomInRoomUseCaseTest()
        {
            _mapper = MapperCreate.CreateMappers();
            _repository = new Mock<IRoomRepository>();
            _logger = new Mock<ILogger<UpdateBookRoomInRoomUseCase>>();

            _useCase = new UpdateBookRoomInRoomUseCase(
                _logger.Object,
                _repository.Object,
                _mapper
                );
        }

        [Fact(DisplayName = "ShouldUpdateCreatingBook")]
        public async Task ShouldUpdateCreatingBook()
        {
            var notification = new AutoFaker<BookRoomsNotification>().Generate();
            notification.Id = 0;

            var room = new AutoFaker<Room>().Generate();
            room.Books = new List<BookRooms>();            

            _repository.Setup(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(room);

            _repository.Setup(x => x.UpdateAsync(It.IsAny<Room>(), It.IsAny<CancellationToken>()));

            await _useCase.HandleAsync(notification, new CancellationToken());

            _repository.Verify(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateAsync(It.IsAny<Room>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "ShouldUpdateUpdatingBook")]
        public async Task ShouldUpdateUpdatingBook()
        {
            var notification = new AutoFaker<BookRoomsNotification>().Generate();
            notification.Id = 0;

            var room = new AutoFaker<Room>().Generate();
            room.Books = new AutoFaker<BookRooms>().Generate(3);
            for (int i = 0; i < room.Books.Count(); i++)
            {
                room.Books.ElementAt(i).Room = room;
                if (i == 0)
                    room.Books.ElementAt(i).Id = 0;

            }

            _repository.Setup(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(room);

            _repository.Setup(x => x.UpdateAsync(It.IsAny<Room>(), It.IsAny<CancellationToken>()));

            await _useCase.HandleAsync(notification, new CancellationToken());

            _repository.Verify(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateAsync(It.IsAny<Room>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "ShouldUpdateInsertingNewBook")]
        public async Task ShouldUpdateInsertingNewBook()
        {
            var notification = new AutoFaker<BookRoomsNotification>().Generate();
            notification.Id = 15;

            var room = new AutoFaker<Room>().Generate();
            room.Books = new AutoFaker<BookRooms>().Generate(3);
            for (int i = 0; i < room.Books.Count(); i++)
            {
                room.Books.ElementAt(i).Room = room;
                room.Books.ElementAt(i).Id = i;
            }

            _repository.Setup(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(room);

            _repository.Setup(x => x.UpdateAsync(It.IsAny<Room>(), It.IsAny<CancellationToken>()));

            await _useCase.HandleAsync(notification, new CancellationToken());

            _repository.Verify(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateAsync(It.IsAny<Room>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "ShouldException")]
        public async Task ShouldException()
        {
            var notification = new AutoFaker<BookRoomsNotification>().Generate();
            notification.Id = 15;

            var room = new AutoFaker<Room>().Generate();
            room.Books = new AutoFaker<BookRooms>().Generate(3);            

            _repository.Setup(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            _repository.Setup(x => x.UpdateAsync(It.IsAny<Room>(), It.IsAny<CancellationToken>()));

            await _useCase.HandleAsync(notification, new CancellationToken());

            _repository.Verify(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateAsync(It.IsAny<Room>(), It.IsAny<CancellationToken>()), Times.Never);
        }

    }
}
