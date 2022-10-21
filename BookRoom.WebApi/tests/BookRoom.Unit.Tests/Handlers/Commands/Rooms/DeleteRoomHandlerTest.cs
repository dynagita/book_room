using AutoBogus;
using BookRoom.Domain.Contract.Handlers.Commands.RoomHandlers;
using BookRoom.Domain.Contract.Requests.Commands.RoomCommands;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.RoomResponses;
using BookRoom.Domain.Contract.UseCases.Rooms;
using FluentAssertions;
using Moq;

namespace BookRoom.Application.Handlers.Commands.RoomHandlers
{
    public class DeleteRoomHandlerTest
    {
        private readonly Mock<IDeleteRoomUseCase> _useCase;
        private readonly IDeleteRoomHandler _handler;

        public DeleteRoomHandlerTest()
        {
            _useCase = new Mock<IDeleteRoomUseCase>();
            _handler = new DeleteRoomHandler(_useCase.Object);
        }

        [Fact(DisplayName = "ShouldSuccess")]
        public async Task ShouldSuccess()
        {
            var commonResponse = CommonResponse<bool>.Ok(true);

            _useCase.Setup(x => x.HandleAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(commonResponse);

            var response = await _handler.Handle(new AutoFaker<DeleteRoomRequest>().Generate(), new CancellationToken());

            response.Status
                .Should()
                .Be(200);
            response.Error
                .Should()
                .Be(string.Empty);
            response.Data
                .Should()
                .BeTrue();            
        }

        [Fact(DisplayName = "ShouldNotSuccess")]
        public async Task ShouldNotSuccess()
        {
            string error = "Fail test";
            var commonResponse = CommonResponse<bool>.BadRequest(error);

            _useCase.Setup(x => x.HandleAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(commonResponse);

            var response = await _handler.Handle(new AutoFaker<DeleteRoomRequest>().Generate(), new CancellationToken());

            response.Status
                .Should()
                .Be(400);
            response.Data
                .Should()
                .BeFalse();
            response.Error
                .Should()
                .Be(error);
        }
    }
}
