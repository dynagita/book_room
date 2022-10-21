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
    public class UpdateRoomHandlerTest
    {
        private readonly Mock<IUpdateRoomUseCase> _useCase;
        private readonly IUpdateRoomHandler _handler;

        public UpdateRoomHandlerTest()
        {
            _useCase = new Mock<IUpdateRoomUseCase>();
            _handler = new UpdateRoomHandler(_useCase.Object);
        }

        [Fact(DisplayName = "ShouldSuccess")]
        public async Task ShouldSuccess()
        {
            var commonResponse = CommonResponse<RoomResponse>.Ok(new AutoFaker<RoomResponse>().Generate());

            _useCase.Setup(x => x.HandleAsync(It.IsAny<int>(), It.IsAny<UpdateRoomRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(commonResponse);

            var response = await _handler.Handle(new AutoFaker<UpdateRoomRequest>().Generate(), new CancellationToken());

            response.Status
                .Should()
                .Be(200);
            response.Error
                .Should()
                .Be(string.Empty);
            response.Data
                .Title
                .Should()
                .Be(commonResponse.Data.Title);
            response.Data
                .Description
                .Should()
                .Be(commonResponse.Data.Description);
        }

        [Fact(DisplayName = "ShouldNotSuccess")]
        public async Task ShouldNotSuccess()
        {
            string error = "Fail test";
            var commonResponse = CommonResponse<RoomResponse>.BadRequest(error);

            _useCase.Setup(x => x.HandleAsync(It.IsAny<int>(), It.IsAny<UpdateRoomRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(commonResponse);

            var response = await _handler.Handle(new AutoFaker<UpdateRoomRequest>().Generate(), new CancellationToken());

            response.Status
                .Should()
                .Be(400);
            response.Data
                .Should()
                .BeNull();
            response.Error
                .Should()
                .Be(error);
        }
    }
}
