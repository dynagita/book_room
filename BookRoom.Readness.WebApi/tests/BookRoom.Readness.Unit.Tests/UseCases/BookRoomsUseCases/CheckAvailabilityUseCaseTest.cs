using AutoBogus;
using AutoMapper;
using BookRoom.Readness.Application.UseCases.BookRoomsUseCases;
using BookRoom.Readness.Domain.Contract.Constants;
using BookRoom.Readness.Domain.Contract.Requests.Queries.BookRoomsQueries;
using BookRoom.Readness.Domain.Contract.UseCases.BookRoomsUseCases;
using BookRoom.Readness.Domain.Entities;
using BookRoom.Readness.Domain.Repositories;
using BookRoom.Readness.Unit.Tests.Utils;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookRoom.Readness.Unit.Tests.UseCases.BookRoomsUseCases
{
    public class CheckAvailabilityUseCaseTest
    {
        private readonly Mock<IBookRoomRepository> _repository;
        private readonly Mock<IRoomRepository> _roomRepository;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<ListBooksByUserUseCase>> _logger;
        private readonly ICheckAvailabilityUseCase _useCase;

        public CheckAvailabilityUseCaseTest()
        {
            _repository = new Mock<IBookRoomRepository>();
            _roomRepository = new Mock<IRoomRepository>();
            _mapper = MapperCreate.CreateMappers();
            _logger = new Mock<ILogger<ListBooksByUserUseCase>>();

            _useCase = new CheckAvailabilityUseCase(
                _logger.Object,
                _mapper,
                _repository.Object,
                _roomRepository.Object
                );
        }

        [Fact(DisplayName = "ShouldBeAvailable")]
        public async Task ShouldBeAvailable()
        {
            var checkRequest = new CheckAvailabilityRequest()
            {
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(4),
                RoomNumber = 1
            };
            
            _repository.Setup(x => x.CheckAvailabilityAsync(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            
            var result = await _useCase.HandleAsync(checkRequest, new CancellationToken());

            result.Status
                .Should()
                .Be(200);
            result.Data
                .Available
                .Should()
                .BeTrue();
        }

        [Fact(DisplayName = "ShouldNotBeAvailable")]
        public async Task ShouldNotBeAvailable()
        {
            var checkRequest = new CheckAvailabilityRequest()
            {
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(4),
                RoomNumber = 1
            };

            var room = new Room()
            {
                Id = 1,
                Description = "Teste",
                Title = "Teste",
                Number = 1,
                Books = new AutoFaker<BookRooms>().Generate(3)
            };
            
            _repository.Setup(x => x.CheckAvailabilityAsync(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _roomRepository.Setup(x => x.GetByRoomNumberAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(room);

            var result = await _useCase.HandleAsync(checkRequest, new CancellationToken());

            result.Status
                .Should()
                .Be(200);
            result.Data
                .Available
                .Should()
                .BeFalse();
            result.Data
                .UnavailableMessage
                .Should()
                .Be(ErrorMessages.BookRoomMessages.ROOM_UNAVAILABLE);
        }

        [Fact(DisplayName = "ShouldExcept")]
        public async Task ShouldExcept()
        {
            var checkRequest = new CheckAvailabilityRequest()
            {
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(4),
                RoomNumber = 1
            };

            _repository.Setup(x => x.CheckAvailabilityAsync(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            _roomRepository.Setup(x => x.GetByRoomNumberAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()));

            var result = await _useCase.HandleAsync(checkRequest, new CancellationToken());

            _repository.Verify(x => x.CheckAvailabilityAsync(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()), Times.Once);
            _roomRepository.Verify(x => x.GetByRoomNumberAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);


        }

    }
}
