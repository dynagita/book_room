using AutoBogus;
using BookRoom.Service.Application.Handlers.Notifications;
using BookRoom.Service.Domain.Contract.Handlers.Notifications;
using BookRoom.Service.Domain.Contract.Notifications;
using BookRoom.Service.Domain.Contract.UseCases;
using Moq;

namespace BookRoom.Service.Unit.Test.Handlers.Notifications
{
    public class PropagateRoomNotificationHandlerTest
    {
        private readonly Mock<IPropagateRoomUseCase> _useCase;
        private readonly IPropagateRoomNotificationHandler _handler;

        public PropagateRoomNotificationHandlerTest()
        {
            _useCase = new Mock<IPropagateRoomUseCase>();
            _handler = new PropagateRoomNotificationHandler(_useCase.Object);
        }

        [Fact(DisplayName = "ShouldSuccess")]
        public async Task ShouldSuccess()
        {
            var notification = new AutoFaker<RoomNotification>().Generate();

            _useCase.Setup(x => x.HandleAsync(It.IsAny<RoomNotification>(), It.IsAny<CancellationToken>()));

            await _handler.Handle(notification, new CancellationToken());

            _useCase.Verify(x => x.HandleAsync(It.IsAny<RoomNotification>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
