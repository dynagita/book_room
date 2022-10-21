using AutoBogus;
using AutoMapper;
using BookRoom.Application.UseCases.RooUseCases;
using BookRoom.Domain.Contract.Constants;
using BookRoom.Domain.Contract.Requests.Commands.RoomCommands;
using BookRoom.Domain.Contract.UseCases.Rooms;
using BookRoom.Domain.Repositories.EntityFramework;
using BookRoom.Unit.Tests.Utils;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookRoom.Unit.Tests.UseCases.Room
{
    public class CreateRoomUseCaseTest
    {
        private readonly Mock<IRoomRepository> _repository;
        private readonly ICreateRoomUseCase _useCase;
        private readonly Mock<ILogger<CreateRoomUseCase>> _logger;
        private readonly IMapper _mapper;
        public CreateRoomUseCaseTest()
        {        
            _repository = new Mock<IRoomRepository>();
            _mapper = MapperCreate.CreateMappers();
            _logger = new Mock<ILogger<CreateRoomUseCase>>();
            _useCase = new CreateRoomUseCase(_repository.Object, _mapper, _logger.Object);
        }

        [Fact(DisplayName = "ShouldCreateUser")]
        public async Task ShouldCreateUser()
        {
            var roomRequest = new AutoFaker<CreateRoomRequest>().Generate();
            roomRequest.Reference = 0;

            _repository.Setup(x => x.GetByNumber(It.IsAny<int>(), It.IsAny<CancellationToken>()));

            _repository.Setup(x => x.InsertAsync(It.IsAny<Domain.Entities.Room>(), It.IsAny<CancellationToken>()));

            var response = await _useCase.HandleAsync(roomRequest, new CancellationToken());

            response.Status
                .Should()
                .Be(200);

            _repository.Verify(x => x.GetByNumber(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.InsertAsync(It.IsAny<Domain.Entities.Room>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "ShouldNotCreateUser")]
        public async Task ShouldNotCreateUser()
        {
            var roomRequest = new AutoFaker<CreateRoomRequest>().Generate();
            var room = _mapper.Map<Domain.Entities.Room>(roomRequest);

            _repository.Setup(x => x.GetByNumber(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(room);

            _repository.Setup(x => x.InsertAsync(It.IsAny<Domain.Entities.Room>(), It.IsAny<CancellationToken>()));

            var response = await _useCase.HandleAsync(roomRequest, new CancellationToken());

            response.Status
                .Should()
                .Be(400);
            response.Error
                .Should()
                .Be(ErrorMessages.RoomMessages.ROOM_EXISTS);

            _repository.Verify(x => x.GetByNumber(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.InsertAsync(It.IsAny<Domain.Entities.Room>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact(DisplayName = "ShouldExceptionCreateUser")]
        public async Task ShouldExceptionCreateUser()
        {
            var roomRequest = new AutoFaker<CreateRoomRequest>().Generate();

            _repository.Setup(x => x.GetByNumber(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            _repository.Setup(x => x.InsertAsync(It.IsAny<Domain.Entities.Room>(), It.IsAny<CancellationToken>()));

            var response = await _useCase.HandleAsync(roomRequest, new CancellationToken());

            response.Status
                .Should()
                .Be(400);
            response.Error
                .Should()
                .Be(ErrorMessages.EXCEPTION_ERROR);

            _repository.Verify(x => x.GetByNumber(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.InsertAsync(It.IsAny<Domain.Entities.Room>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
