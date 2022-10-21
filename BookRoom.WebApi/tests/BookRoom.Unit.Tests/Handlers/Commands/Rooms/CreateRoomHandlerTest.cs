using AutoBogus;
using BookRoom.Domain.Contract.Handlers.Commands.RoomHandlers;
using BookRoom.Domain.Contract.Requests.Commands.RoomCommands;
using BookRoom.Domain.Contract.Requests.Commands.UserCommands;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.RoomResponses;
using BookRoom.Domain.Contract.Responses.UserResponses;
using BookRoom.Domain.Contract.UseCases.Rooms;
using FluentAssertions;
using Moq;

namespace BookRoom.Application.Handlers.Commands.RoomHandlers
{
    public class CreateRoomHandlerTest
    {
        private readonly Mock<ICreateRoomUseCase> _useCase;
        private readonly ICreateRoomHandler _handler;

        public CreateRoomHandlerTest()
        {
            _useCase = new Mock<ICreateRoomUseCase>();
            _handler = new CreateRoomHandler(_useCase.Object);
        }

        [Fact(DisplayName = "ShouldSuccess")]
        public async Task ShouldSuccess()
        {
            var commonResponse = CommonResponse<RoomResponse>.Ok(new AutoFaker<RoomResponse>().Generate());

            _useCase.Setup(x => x.HandleAsync(It.IsAny<CreateRoomRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(commonResponse);

            var response = await _handler.Handle(new AutoFaker<CreateRoomRequest>().Generate(), new CancellationToken());

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

            _useCase.Setup(x => x.HandleAsync(It.IsAny<CreateRoomRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(commonResponse);

            var response = await _handler.Handle(new AutoFaker<CreateRoomRequest>().Generate(), new CancellationToken());

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
