using AutoBogus;
using BookRoom.Application.Handlers.Commands.BookRoomHandlers;
using BookRoom.Domain.Contract.Constants;
using BookRoom.Domain.Contract.Handlers.Commands.BookRoomHandlers;
using BookRoom.Domain.Contract.Requests.Commands.BookRooms;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.BookRoomsResponses;
using BookRoom.Domain.Contract.UseCases.BookRooms;
using FluentAssertions;
using Moq;

namespace BookRoom.Unit.Tests.Handlers.Commands.BookRoomHandlers
{
    public class CreateBookRoomHandlerTest
    {
        private readonly Mock<ICreateBookRoomUseCase> _useCase;
        private readonly ICreateBookRoomHandler _handler;

        public CreateBookRoomHandlerTest()
        {
            _useCase = new Mock<ICreateBookRoomUseCase>();
            _handler = new CreateBookRoomHandler(_useCase.Object);
        }

        [Fact(DisplayName = "ShouldSuccess")]
        public async Task ShouldSuccess()
        {
            var request = new AutoFaker<CreateBookRoomRequest>().Generate();

            var expected = new AutoFaker<BookRoomResponse>().Generate();

            _useCase.Setup(x => x.HandleAsync(It.IsAny<CreateBookRoomRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(CommonResponse<BookRoomResponse>.Ok(expected));

            var response = await _handler.Handle(request, new CancellationToken());

            response.Status
                .Should()
                .Be(200);
            response.Data
                .Reference
                .Should()
                .Be(expected.Reference);
            response.Error
                .Should()
                .BeNullOrEmpty();
        }

        [Fact(DisplayName = "ShouldNotSuccessExpected")]
        public async Task ShouldNotSuccessExpected()
        {
            var request = new AutoFaker<CreateBookRoomRequest>().Generate();

            var expected = new AutoFaker<BookRoomResponse>().Generate();

            _useCase.Setup(x => x.HandleAsync(It.IsAny<CreateBookRoomRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(CommonResponse<BookRoomResponse>.BadRequest(ErrorMessages.BookRoomMessages.BOOK_CANT_START_30_DAYS_ADVANCED));

            var response = await _handler.Handle(request, new CancellationToken());

            response.Status
                .Should()
                .Be(400);
            response.Data
                .Should()
                .BeNull();
            response.Error
                .Should()
                .Be(ErrorMessages.BookRoomMessages.BOOK_CANT_START_30_DAYS_ADVANCED);
        }

        [Fact(DisplayName = "ShouldNotSuccess")]
        public async Task ShouldNotSuccess()
        {
            var request = new AutoFaker<CreateBookRoomRequest>().Generate();

            var expected = new AutoFaker<BookRoomResponse>().Generate();

            _useCase.Setup(x => x.HandleAsync(It.IsAny<CreateBookRoomRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(CommonResponse<BookRoomResponse>.BadRequest(ErrorMessages.EXCEPTION_ERROR));

            var response = await _handler.Handle(request, new CancellationToken());

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
