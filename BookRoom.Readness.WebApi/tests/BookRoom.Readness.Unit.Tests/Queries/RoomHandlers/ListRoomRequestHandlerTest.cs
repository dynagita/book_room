using AutoBogus;
using BookRoom.Readness.Application.Handlers.Queries.RoomHandlers;
using BookRoom.Readness.Domain.Contract.Handlers.Queries.RoomHandlers;
using BookRoom.Readness.Domain.Contract.Requests.Queries.BookRoomsQueries;
using BookRoom.Readness.Domain.Contract.Requests.Queries.RoomQueries;
using BookRoom.Readness.Domain.Contract.Responses;
using BookRoom.Readness.Domain.Contract.Responses.BookRoomsResponses;
using BookRoom.Readness.Domain.Contract.Responses.RoomResponses;
using BookRoom.Readness.Domain.Contract.UseCases.RomUseCases;
using FluentAssertions;
using Moq;
using System.Reflection.Metadata;

namespace BookRoom.Readness.Unit.Tests.Handlers.Queries.RoomHandlers
{
    public class ListRoomRequestHandlerTest
    {
        private readonly Mock<IListRoomUseCase> _useCase;
        private readonly IListRoomRequestHandler _handler;

        public ListRoomRequestHandlerTest()
        {
            _useCase = new Mock<IListRoomUseCase>();
            _handler = new ListRoomRequestHandler(_useCase.Object);
        }

        [Fact(DisplayName = "ShouldListRooms")]
        public async Task ShouldCheckAvailability()
        {
            var rooms = new AutoFaker<RoomResponse>().Generate(5);

            _useCase.Setup(x => x.HandleAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(CommonResponse<List<RoomResponse>>.Ok(rooms));

            var request = new AutoFaker<ListRoomsRequest>().Generate();

            var response = await _handler.Handle(request, new CancellationToken());

            response.Status
                .Should()
                .Be(200);
            response.Data
                .Should()
                .HaveCount(5);
        }
    }
}
