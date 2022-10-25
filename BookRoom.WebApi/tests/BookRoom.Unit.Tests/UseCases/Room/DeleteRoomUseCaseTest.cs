using AutoBogus;
using BookRoom.Application.UseCases.RoomUseCases;
using BookRoom.Application.UseCases.RooUseCases;
using BookRoom.Domain.Contract.Constants;
using BookRoom.Domain.Contract.UseCases.Rooms;
using BookRoom.Domain.Queue;
using BookRoom.Domain.Repositories.EntityFramework;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookRoom.Unit.Tests.UseCases.Rooms
{
    public class DeleteRoomUseCaseTest
    {
        private readonly Mock<IRoomRepository> _repository;
        private readonly IDeleteRoomUseCase _useCase;
        private readonly Mock<ILogger<CreateRoomUseCase>> _logger;
        private readonly Mock<IRoomProducer> _producer;
        public DeleteRoomUseCaseTest()
        {
            _repository = new Mock<IRoomRepository>();
            _logger = new Mock<ILogger<CreateRoomUseCase>>();
            _producer = new Mock<IRoomProducer>();
            _useCase = new DeleteRoomUseCase(_repository.Object, _logger.Object, _producer.Object);

        }

        [Fact(DisplayName = "ShouldDeleteRoom")]
        public async Task ShouldDeleteRoom()
        {
            _repository.Setup(x => x.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()));

            var response = await _useCase.HandleAsync(new Random().Next(50), new CancellationToken());

            response.Status
                .Should()
                .Be(200);
            response.Data
                .Should()
                .BeTrue();
            _repository.Verify(x => x.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "ShouldNotDeleteRoom")]
        public async Task ShouldNotDeleteRoom()
        {
            _repository.Setup(x => x.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            var response = await _useCase.HandleAsync(new Random().Next(50), new CancellationToken());

            response.Status
                .Should()
                .Be(400);
            response.Error
                .Should()
                .Be(ErrorMessages.EXCEPTION_ERROR);
        }
    }
}
