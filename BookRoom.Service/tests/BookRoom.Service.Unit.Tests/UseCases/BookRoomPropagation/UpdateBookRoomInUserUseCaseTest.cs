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
    public class UpdateBookRoomInUserUseCaseTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> _repository;
        private readonly Mock<ILogger<UpdateBookRoomInUserUseCase>> _logger;
        private readonly IUpdateBookRoomInUserUseCase _useCase;

        public UpdateBookRoomInUserUseCaseTest()
        {
            _logger = new Mock<ILogger<UpdateBookRoomInUserUseCase>>();
            _mapper = MapperCreate.CreateMappers();
            _repository = new Mock<IUserRepository>();

            _useCase = new UpdateBookRoomInUserUseCase(
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

            var User = new AutoFaker<User>().Generate();
            User.Books = new List<BookRooms>();

            _repository.Setup(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(User);

            _repository.Setup(x => x.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()));

            await _useCase.HandleAsync(notification, new CancellationToken());

            _repository.Verify(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "ShouldUpdateUpdatingBook")]
        public async Task ShouldUpdateUpdatingBook()
        {
            var notification = new AutoFaker<BookRoomsNotification>().Generate();
            notification.Id = 0;

            var User = new AutoFaker<User>().Generate();
            User.Books = new AutoFaker<BookRooms>().Generate(3);
            for (int i = 0; i < User.Books.Count(); i++)
            {
                User.Books.ElementAt(i).User = User;
                if (i == 0)
                    User.Books.ElementAt(i).Id = 0;

            }

            _repository.Setup(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(User);

            _repository.Setup(x => x.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()));

            await _useCase.HandleAsync(notification, new CancellationToken());

            _repository.Verify(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "ShouldUpdateInsertingNewBook")]
        public async Task ShouldUpdateInsertingNewBook()
        {
            var notification = new AutoFaker<BookRoomsNotification>().Generate();
            notification.Id = 15;

            var user = new AutoFaker<User>().Generate();
            user.Books = new AutoFaker<BookRooms>().Generate(3);
            for (int i = 0; i < user.Books.Count(); i++)
            {
                user.Books.ElementAt(i).User = user;
                user.Books.ElementAt(i).Id = i;
            }

            _repository.Setup(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _repository.Setup(x => x.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()));

            await _useCase.HandleAsync(notification, new CancellationToken());

            _repository.Verify(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "ShouldException")]
        public async Task ShouldException()
        {
            var notification = new AutoFaker<BookRoomsNotification>().Generate();
            notification.Id = 15;

            var user = new AutoFaker<User>().Generate();
            user.Books = new AutoFaker<BookRooms>().Generate(3);

            _repository.Setup(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            _repository.Setup(x => x.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()));

            await _useCase.HandleAsync(notification, new CancellationToken());

            _repository.Verify(x => x.FindOneAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
