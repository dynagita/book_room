using AutoBogus;
using BookRoom.Readness.Application.Handlers.Queries.BookRoomsHandlers;
using BookRoom.Readness.Domain.Contract.Handlers.Queries.BookRoomsHandlers;
using BookRoom.Readness.Domain.Contract.Requests.Queries.BookRoomsQueries;
using BookRoom.Readness.Domain.Contract.Responses;
using BookRoom.Readness.Domain.Contract.Responses.BookRoomsResponses;
using BookRoom.Readness.Domain.Contract.UseCases.BookRoomsUseCases;
using FluentAssertions;
using Moq;
using System.Reflection.Metadata;

namespace BookRoom.Readness.Unit.Tests.Handlers.Queries.BookRoomsHandlers
{
    public class CheckAvailabilityRequestHandlerTest
    {
        private readonly Mock<ICheckAvailabilityUseCase> _useCase;
        private readonly ICheckAvailabilityRequestHandler _handler;

        public CheckAvailabilityRequestHandlerTest()
        {
            _useCase = new Mock<ICheckAvailabilityUseCase>();

            _handler = new CheckAvailabilityRequestHandler(_useCase.Object);
        }

        [Fact(DisplayName = "ShouldCheckAvailability")]
        public async Task ShouldCheckAvailability()
        {
            var availability = new AutoFaker<CheckAvailabilityResponse>().Generate();

            _useCase.Setup(x => x.HandleAsync(It.IsAny<CheckAvailabilityRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(CommonResponse<CheckAvailabilityResponse>.Ok(availability));

            var request = new AutoFaker<CheckAvailabilityRequest>().Generate();

            var response = await _handler.Handle(request, new CancellationToken());

            response.Status
                .Should()
                .Be(200);
        }
    }
}
