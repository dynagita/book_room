using AutoBogus;
using BookRoom.Application.Handlers.Commands.UserHandlers;
using BookRoom.Domain.Contract.Handlers.Commands.User;
using BookRoom.Domain.Contract.Requests.Commands.UserCommands;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.UserResponses;
using BookRoom.Domain.Contract.UseCases.User;
using FluentAssertions;
using Moq;

namespace BookRoom.Unit.Tests.Handlers.Commands.User
{    
    public class UserCreateHandlerTest
    {
        private readonly IUserCreateHandler _handler;
        private readonly Mock<ICreateUserUseCase> _useCase;
        public UserCreateHandlerTest()
        {
            _useCase = new Mock<ICreateUserUseCase>();
            _handler = new UserCreateHandler(_useCase.Object);
        }
        
        [Fact(DisplayName = "ShouldSuccess")]
        public async Task ShouldSuccess()
        {
            var commonResponse = CommonResponse<UserResponse>.Ok(new AutoFaker<UserResponse>().Generate());

            _useCase.Setup(x => x.HandleAsync(It.IsAny<UserCreateRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(commonResponse);

            var response = await _handler.Handle(new AutoFaker<UserCreateRequest>().Generate(), new CancellationToken());

            response.Status
                .Should()
                .Be(200);
            response.Error
                .Should()
                .Be(string.Empty);
            response.Data
                .FirstName
                .Should()
                .Be(commonResponse.Data.FirstName);
            response.Data
                .LastName
                .Should()
                .Be(commonResponse.Data.LastName);
        }

        [Fact(DisplayName = "ShouldNotSuccess")]
        public async Task ShouldNotSuccess()
        {
            string error = "Fail test";
            var commonResponse = CommonResponse<UserResponse>.BadRequest(error);

            _useCase.Setup(x => x.HandleAsync(It.IsAny<UserCreateRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(commonResponse);

            var response = await _handler.Handle(new AutoFaker<UserCreateRequest>().Generate(), new CancellationToken());

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
