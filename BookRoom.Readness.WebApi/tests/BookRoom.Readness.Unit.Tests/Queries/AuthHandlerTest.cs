using AutoBogus;
using BookRoom.Readness.Application.Handlers.Queries.AuthQueries;
using BookRoom.Readness.Domain.Contract.Handlers.Queries.Auth;
using BookRoom.Readness.Domain.Contract.Requests.Queries.AuthQueries;
using BookRoom.Readness.Domain.Contract.Responses;
using BookRoom.Readness.Domain.Contract.Responses.AuthResponses;
using BookRoom.Readness.Domain.Contract.UseCases.Auth;
using FluentAssertions;
using Moq;

namespace BookRoom.Unit.Tests.Handlers.Queries
{
    public class AuthHandlerTest
    {
        private readonly Mock<IAuthenticateUseCase> _useCase;
        private readonly IAuthHandler _handler;

        public AuthHandlerTest()
        {
            _useCase = new Mock<IAuthenticateUseCase>();
            _handler = new AuthHandler(_useCase.Object);
        }

        [Fact(DisplayName = "ShouldSuccess")]
        public async Task ShouldSuccess()
        {
            var commonResponse = CommonResponse<AuthResponse>.Ok(new AutoFaker<AuthResponse>().Generate());

            _useCase.Setup(x => x.HandleAsync(It.IsAny<AuthRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(commonResponse);

            var response = await _handler.Handle(new AutoFaker<AuthRequest>().Generate(), new CancellationToken());

            response.Status
                .Should()
                .Be(200);
            response.Error
                .Should()
                .Be(string.Empty);
            response.Data
                .User
                .Should()
                .Be(commonResponse.Data.User);
            response.Data
                .Token
                .Should()
                .Be(commonResponse.Data.Token);
        }

        [Fact(DisplayName = "ShouldNotSuccess")]
        public async Task ShouldNotSuccess()
        {
            string error = "Fail test";
            var commonResponse = CommonResponse<AuthResponse>.BadRequest(error);

            _useCase.Setup(x => x.HandleAsync(It.IsAny<AuthRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(commonResponse);

            var response = await _handler.Handle(new AutoFaker<AuthRequest>().Generate(), new CancellationToken());

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
