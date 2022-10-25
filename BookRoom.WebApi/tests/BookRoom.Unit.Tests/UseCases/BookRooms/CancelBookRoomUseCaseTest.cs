using AutoBogus;
using AutoMapper;
using BookRoom.Domain.Contract.Constants;
using BookRoom.Domain.Contract.UseCases.BookRooms;
using BookRoom.Domain.Entities;
using BookRoom.Domain.Queue;
using BookRoom.Domain.Repositories.EntityFramework;
using BookRoom.Unit.Tests.Utils;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookRoom.Application.UseCases.BookRoomUseCases
{
    public class CancelBookRoomUseCaseTest
    {
        private readonly ICancelBookRoomUseCase _useCase;
        private readonly Mock<IBookRoomsRepository> _repository;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<CancelBookRoomUseCase>> _logger;
        private readonly Mock<IBookRoomProducer> _producer;

        public CancelBookRoomUseCaseTest()
        {
            _repository = new Mock<IBookRoomsRepository>();
            _mapper = MapperCreate.CreateMappers();
            _logger = new Mock<ILogger<CancelBookRoomUseCase>>();
            _producer = new Mock<IBookRoomProducer>();
            _useCase = new CancelBookRoomUseCase(
                _repository.Object,
                _mapper,
                _logger.Object,
                _producer.Object
                );
        }

        [Fact(DisplayName = "ShouldCancelBook")]
        public async Task ShouldCancelBook()
        {

            var bookId = new Random().Next(1, 50);

            var bookRoom = new AutoFaker<BookRooms>().Generate();
            bookRoom.Id = bookId;
            bookRoom.Status = Domain.Contract.Enums.BookStatusRoom.Canceled;

            _repository.Setup(x => x.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(bookRoom);

            _repository.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(bookRoom);

            _producer.Setup(x => x.SendAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()));

            var response = await _useCase.HandleAsync(bookId, new CancellationToken());

            response.Status
                .Should()
                .Be(200);
            response.Data
                .Status
                .Should()
                .Be(Domain.Contract.Enums.BookStatusRoom.Canceled);
            _repository.Verify(x => x.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);

            _repository.Verify(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()), Times.Once);

            _producer.Verify(x => x.SendAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "ShouldNotCancelBook")]
        public async Task ShouldNotCancelBook()
        {

            var bookId = new Random().Next(1, 50);

            var bookRoom = new AutoFaker<BookRooms>().Generate();
            bookRoom.Id = bookId;
            bookRoom.Status = Domain.Contract.Enums.BookStatusRoom.Canceled;

            _repository.Setup(x => x.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(bookRoom);

            _repository.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            _producer.Setup(x => x.SendAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()));

            var response = await _useCase.HandleAsync(bookId, new CancellationToken());

            response.Status
                .Should()
                .Be(400);
            response.Data
                .Should()
                .BeNull();
            response.Error
                .Should()
                .Be(ErrorMessages.EXCEPTION_ERROR);

            _repository.Verify(x => x.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);

            _repository.Verify(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()), Times.Once);

            _producer.Verify(x => x.SendAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
