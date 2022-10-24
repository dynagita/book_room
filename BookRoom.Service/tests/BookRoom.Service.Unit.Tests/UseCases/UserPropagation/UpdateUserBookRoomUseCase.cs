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

namespace BookRoom.Service.Unit.Test.UseCases.UserPropagation
{
    public class UpdateUserBookRoomUseCaseTest
    {
        private readonly Mock<IBookRoomRepository> _repository;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<UpdateUserBookRoomUseCase>> _logger;
        private readonly IUpdateUserBookRoomUseCase _useCase;

        public UpdateUserBookRoomUseCaseTest()
        {
            _repository = new Mock<IBookRoomRepository>();
            _mapper = MapperCreate.CreateMappers();
            _logger = new Mock<ILogger<UpdateUserBookRoomUseCase>>();

            _useCase = new UpdateUserBookRoomUseCase(
                _repository.Object,
                _mapper,
                _logger.Object
                );
        }

        [Fact(DisplayName = "ShouldNotUpdate")]
        public async Task ShouldNotUpdate()
        {
            var notification = new AutoFaker<UserNotification>().Generate();

            _repository.Setup(x => x.GetAllByUserAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()));
            _repository.Setup(x => x.UpdateAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()));

            await _useCase.HandleAsync(notification, new CancellationToken());

            _repository.Verify(x => x.GetAllByUserAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact(DisplayName = "ShouldUpdate")]
        public async Task ShouldUpdate()
        {
            var notification = new AutoFaker<UserNotification>().Generate();
            var books = new AutoFaker<BookRooms>().Generate(2);

            _repository.Setup(x => x.GetAllByUserAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(books);
            _repository.Setup(x => x.UpdateAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()));

            await _useCase.HandleAsync(notification, new CancellationToken());

            _repository.Verify(x => x.GetAllByUserAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()), Times.Exactly(books.Count));
        }

        [Fact(DisplayName = "ShouldException")]
        public async Task ShouldException()
        {
            var notification = new AutoFaker<UserNotification>().Generate();
            var books = new AutoFaker<BookRooms>().Generate(2);

            _repository.Setup(x => x.GetAllByUserAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());
            _repository.Setup(x => x.UpdateAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()));

            await _useCase.HandleAsync(notification, new CancellationToken());

            _repository.Verify(x => x.GetAllByUserAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
