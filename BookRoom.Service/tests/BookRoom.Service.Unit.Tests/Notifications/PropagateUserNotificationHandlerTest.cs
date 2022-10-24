using AutoBogus;
using BookRoom.Service.Application.Handlers.Notifications;
using BookRoom.Service.Domain.Contract.Handlers.Notifications;
using BookRoom.Service.Domain.Contract.Notifications;
using BookRoom.Service.Domain.Contract.UseCases;
using Moq;

namespace BookRoom.Service.Unit.Test.Handlers.Notifications
{
    public class PropagateUserNotificationHandlerTest
    {
        private readonly Mock<IPropagateUserUseCase> _useCase;
        private readonly IPropagateUserNotificationHandler _handler;

        public PropagateUserNotificationHandlerTest()
        {
            _useCase = new Mock<IPropagateUserUseCase>();
            _handler = new PropagateUserNotificationHandler(_useCase.Object);
        }

        [Fact(DisplayName = "ShouldSuccess")]
        public async Task ShouldSuccess()
        {
            var notification = new AutoFaker<UserNotification>().Generate();

            _useCase.Setup(x => x.HandleAsync(It.IsAny<UserNotification>(), It.IsAny<CancellationToken>()));

            await _handler.Handle(notification, new CancellationToken());

            _useCase.Verify(x => x.HandleAsync(It.IsAny<UserNotification>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
