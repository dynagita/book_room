using AutoBogus;
using BookRoom.Readness.Application.Handlers.Queries.BookRoomsHandlers;
using BookRoom.Readness.Domain.Contract.Handlers.Queries.BookRoomsHandlers;
using BookRoom.Readness.Domain.Contract.Requests.Queries.BookRoomsQueries;
using BookRoom.Readness.Domain.Contract.Responses;
using BookRoom.Readness.Domain.Contract.Responses.BookRoomsResponses;
using BookRoom.Readness.Domain.Contract.UseCases.BookRoomsUseCases;
using FluentAssertions;
using Moq;

namespace BookRoom.Readness.Unit.Tests.Handlers.Queries.BookRoomsHandlers
{
    public class ListBooksByUserRequestHandlerTest
    {
        private readonly Mock<IListBooksByUserUseCase> _useCase;
        private readonly IListBooksByUserRequestHandler _handler;
        public ListBooksByUserRequestHandlerTest()
        {
            _useCase = new Mock<IListBooksByUserUseCase>();
            _handler = new ListBooksByUserRequestHandler(_useCase.Object);
        }

        [Fact(DisplayName = "ShouldGetList")]
        public async Task ShouldGetList()
        {
            var list = new AutoFaker<BookRoomResponse>().Generate(5);

            _useCase.Setup(x => x.HandleAsync(It.IsAny<ListBooksByUserRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(CommonResponse<List<BookRoomResponse>>.Ok(list));

            var request = new AutoFaker<ListBooksByUserRequest>().Generate();

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
