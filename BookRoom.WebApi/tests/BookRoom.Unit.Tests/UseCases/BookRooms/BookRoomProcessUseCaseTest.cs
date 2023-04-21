using AutoBogus;
using AutoMapper;
using BookRoom.Application.UseCases.BookRoomUseCases;
using BookRoom.Domain.Contract.Notification.BookRooms;
using BookRoom.Domain.Contract.UseCases.BookRooms;
using BookRoom.Domain.Entities;
using BookRoom.Domain.Queue;
using BookRoom.Domain.Repositories.EntityFramework;
using BookRoom.Unit.Tests.Utils;
using Moq;
using Serilog;

namespace BookRoom.Unit.Tests.UseCases.BookRoomUseCases
{
    public class BookRoomProcessUseCaseTest
    {
        private readonly Mock<IBookRoomsRepository> _repository;
        private readonly Mock<ILogger> _logger;
        private readonly IMapper _mapper;
        private readonly Mock<IBookRoomProducer> _producer;
        private readonly IBookRoomProcessUseCase _useCase;
        public BookRoomProcessUseCaseTest()
        {
            _repository = new Mock<IBookRoomsRepository>();
            _logger = new Mock<ILogger>();
            _mapper = MapperCreate.CreateMappers();
            _producer = new Mock<IBookRoomProducer>();
            _useCase = new BookRoomProcessUseCase(
                _repository.Object,
                _logger.Object,
                _mapper,
                _producer.Object
                );
        }

        [Fact(DisplayName = "ShouldProcessUseCase")]
        public async Task ShouldProcessUseCase()
        {
            var notification = new AutoFaker<BookRoomNotification>().Generate();
            var book = _mapper.Map<BookRooms>(notification);

            _repository.Setup(x => x.GetBookRoomByPeriod(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()));

            _repository.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(book);

            _producer.Setup(x => x.SendAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()));

            await _useCase.HandleAsync(notification, new CancellationToken());

            _repository.Verify(x => x.GetBookRoomByPeriod(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()), Times.Once);
            _producer.Verify(x => x.SendAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()));
        }

        [Fact(DisplayName = "ShouldProcessSameExistBookUseCase")]
        public async Task ShouldProcessSameExistBookUseCase()
        {
            var notification = new AutoFaker<BookRoomNotification>().Generate();
            var book = _mapper.Map<BookRooms>(notification);
            book.Status = Domain.Contract.Enums.BookStatusRoom.Confirmed;

            _repository.Setup(x => x.GetBookRoomByPeriod(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(book);

            _repository.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(book);

            _producer.Setup(x => x.SendAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()));

            await _useCase.HandleAsync(notification, new CancellationToken());

            _repository.Verify(x => x.GetBookRoomByPeriod(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()), Times.Once);
            _producer.Verify(x => x.SendAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "ShouldProcessDifferentExistBookUseCase")]
        public async Task ShouldProcessDifferentExistBookUseCase()
        {
            var notification = new AutoFaker<BookRoomNotification>().Generate();
            notification.Reference = 1;
            var book = _mapper.Map<BookRooms>(notification);
            book.Status = Domain.Contract.Enums.BookStatusRoom.Confirmed;
            book.Id = 15;

            _repository.Setup(x => x.GetBookRoomByPeriod(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(book);

            _repository.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(book);

            _producer.Setup(x => x.SendAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()));

            await _useCase.HandleAsync(notification, new CancellationToken());

            _repository.Verify(x => x.GetBookRoomByPeriod(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()), Times.Once);
            _producer.Verify(x => x.SendAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "ShouldException")]
        public async Task ShouldException()
        {
            var notification = new AutoFaker<BookRoomNotification>().Generate();
            notification.Reference = 1;
            var book = _mapper.Map<BookRooms>(notification);
            book.Status = Domain.Contract.Enums.BookStatusRoom.Confirmed;
            book.Id = 15;

            _repository.Setup(x => x.GetBookRoomByPeriod(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            _repository.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(book);

            _producer.Setup(x => x.SendAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()));

            await _useCase.HandleAsync(notification, new CancellationToken());

            _repository.Verify(x => x.GetBookRoomByPeriod(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()), Times.Never);
            _producer.Verify(x => x.SendAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
