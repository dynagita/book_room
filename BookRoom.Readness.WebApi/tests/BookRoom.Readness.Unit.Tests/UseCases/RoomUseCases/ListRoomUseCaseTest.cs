using AutoBogus;
using AutoMapper;
using BookRoom.Readness.Application.UseCases.RoomUseCases;
using BookRoom.Readness.Domain.Contract.Constants;
using BookRoom.Readness.Domain.Contract.Requests.Queries.BookRoomsQueries;
using BookRoom.Readness.Domain.Contract.Requests.Queries.RoomQueries;
using BookRoom.Readness.Domain.Contract.UseCases.RomUseCases;
using BookRoom.Readness.Domain.Entities;
using BookRoom.Readness.Domain.Repositories;
using BookRoom.Readness.Unit.Tests.Utils;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookRoom.Readness.Unit.Tests.UseCases.RoomUseCases
{
    public class ListRoomUseCaseTest
    {
        private readonly Mock<IRoomRepository> _repository;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<ListRoomUseCase>> _logger;
        private readonly IListRoomUseCase _useCase;

        public ListRoomUseCaseTest()
        {
            _repository = new Mock<IRoomRepository>();
            _mapper = MapperCreate.CreateMappers();
            _logger = new Mock<ILogger<ListRoomUseCase>>();

            _useCase = new ListRoomUseCase(
                _logger.Object,
                _mapper,
                _repository.Object
                );
        }

        [Fact(DisplayName = "ShouldList")]
        public async Task ListRoomUseCase()
        {
            var rooms = new AutoFaker<Room>().Generate(15);

            _repository.Setup(x => x.ListAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(rooms);

            var response = await _useCase.HandleAsync(new CancellationToken());

            response.Status
                .Should()
                .Be(200);
            response.Data
                .Should()
                .HaveCount(15);
        }

        [Fact(DisplayName = "ShouldException")]
        public async Task ShouldException()
        {
            var request = new ListBooksByUserRequest()
            {
                Email = "fool@fool.com"
            };

            var books = new AutoFaker<BookRooms>().Generate(15);

            _repository.Setup(x => x.ListAllAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            var response = await _useCase.HandleAsync(new CancellationToken());

            response.Status
                .Should()
                .Be(400);
            response.Data
                .Should()
                .BeNull();
            response.Error
                .Should()
                .Be(ErrorMessages.EXCEPTION_ERROR);
        }
    }
}
