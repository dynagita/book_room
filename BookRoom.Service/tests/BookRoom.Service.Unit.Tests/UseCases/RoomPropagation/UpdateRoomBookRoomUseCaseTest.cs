using AutoBogus;
using AutoMapper;
using BookRoom.Service.Application.UseCases.BookRoomPropagation;
using BookRoom.Service.Application.UseCases.RoomPropagation;
using BookRoom.Service.Domain.Contract.Notifications;
using BookRoom.Service.Domain.Contract.UseCases;
using BookRoom.Service.Domain.Repositories;
using BookRoom.Service.Unit.Tests.Utils;
using BookRoom.Services.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookRoom.Service.Unit.Tests.UseCases.RoomPropagation
{
    public class UpdateRoomBookRoomUseCaseTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IBookRoomRepository> _repository;
        private readonly Mock<ILogger<UpdateRoomBookRoomUseCase>> _logger;
        private readonly IUpdateRoomBookRoomUseCase _useCase;
        public UpdateRoomBookRoomUseCaseTest()
        {
            _mapper = MapperCreate.CreateMappers();
            _repository = new Mock<IBookRoomRepository>();
            _logger = new Mock<ILogger<UpdateRoomBookRoomUseCase>>();

            _useCase = new UpdateRoomBookRoomUseCase(
                _logger.Object,
                _mapper,
                _repository.Object
                );
        }

        [Fact(DisplayName = "ShouldNotUpdate")]
        public async Task ShouldNotUpdate()
        {
            var notification = new AutoFaker<RoomNotification>().Generate();

            _repository.Setup(x => x.GetAllByRoomAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()));
            _repository.Setup(x => x.UpdateAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()));

            await _useCase.HandleAsync(notification, new CancellationToken());

            _repository.Verify(x => x.GetAllByRoomAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact(DisplayName = "ShouldUpdate")]
        public async Task ShouldUpdate()
        {
            var notification = new AutoFaker<RoomNotification>().Generate();
            var books = new AutoFaker<BookRooms>().Generate(2);

            _repository.Setup(x => x.GetAllByRoomAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(books);
            _repository.Setup(x => x.UpdateAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()));

            await _useCase.HandleAsync(notification, new CancellationToken());

            _repository.Verify(x => x.GetAllByRoomAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()), Times.Exactly(books.Count));
        }

        [Fact(DisplayName = "ShouldException")]
        public async Task ShouldException()
        {
            var notification = new AutoFaker<RoomNotification>().Generate();
            var books = new AutoFaker<BookRooms>().Generate(2);

            _repository.Setup(x => x.GetAllByRoomAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());
            _repository.Setup(x => x.UpdateAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()));

            await _useCase.HandleAsync(notification, new CancellationToken());

            _repository.Verify(x => x.GetAllByRoomAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
