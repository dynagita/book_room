using AutoBogus;
using AutoMapper;
using BookRoom.Application.UseCases.RoomUseCases;
using BookRoom.Application.UseCases.RooUseCases;
using BookRoom.Domain.Contract.Constants;
using BookRoom.Domain.Contract.Requests.Commands.RoomCommands;
using BookRoom.Domain.Contract.UseCases.Rooms;
using BookRoom.Domain.Repositories.EntityFramework;
using BookRoom.Unit.Tests.Utils;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookRoom.Unit.Tests.UseCases.Room
{
    public class UpdateRoomUseCaseTest
    {
        private readonly Mock<IRoomRepository> _repository;
        private readonly IUpdateRoomUseCase _useCase;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<CreateRoomUseCase>> _logger;
        public UpdateRoomUseCaseTest()
        {
            _repository = new Mock<IRoomRepository>();
            _mapper = MapperCreate.CreateMappers();
            _logger = new Mock<ILogger<CreateRoomUseCase>>();
            _useCase = new UpdateRoomUseCase(_repository.Object, _mapper, _logger.Object);
        }

        [Fact(DisplayName = "ShouldUpdateRoom")]
        public async Task ShouldUpdateRoom()
        {
            var roomRequest = new AutoFaker<UpdateRoomRequest>().Generate();
            var room = _mapper.Map<Domain.Entities.Room>(roomRequest);

            _repository.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<Domain.Entities.Room>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(room);

            var response = await _useCase.HandleAsync(roomRequest.Reference, roomRequest, new CancellationToken());

            response.Status
                .Should()
                .Be(200);
            response.
                Data.Number
                .Should()
                .Be(room.Number);
            _repository.Verify(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<Domain.Entities.Room>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "ShouldNotUpdateRoom")]
        public async Task ShouldNotUpdateRoom()
        {
            var roomRequest = new AutoFaker<UpdateRoomRequest>().Generate();

            _repository.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<Domain.Entities.Room>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            var response = await _useCase.HandleAsync(roomRequest.Reference, roomRequest, new CancellationToken());

            response.Status
                .Should()
                .Be(400);
            response.Error
                .Should()
                .Be(ErrorMessages.EXCEPTION_ERROR);
            _repository.Verify(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<Domain.Entities.Room>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
