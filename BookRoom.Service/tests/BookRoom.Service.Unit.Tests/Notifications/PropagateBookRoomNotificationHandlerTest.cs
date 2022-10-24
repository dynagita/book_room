using AutoBogus;
using BookRoom.Service.Application.Handlers.Notifications;
using BookRoom.Service.Domain.Contract.Handlers.Notifications;
using BookRoom.Service.Domain.Contract.Notifications;
using BookRoom.Service.Domain.Contract.UseCases;
using Moq;

namespace BookRoom.Service.Unit.Test.Handlers.Notifications
{
    public class PropagateBookRoomNotificationHandlerTest
    {
        private readonly Mock<IPropagateBookRoomUseCase> _useCase;
        private readonly IPropagateBookRoomNotificationHandler _handler;

        public PropagateBookRoomNotificationHandlerTest()
        {
            _useCase = new Mock<IPropagateBookRoomUseCase>();
            _handler = new PropagateBookRoomNotificationHandler(_useCase.Object);
        }

        [Fact(DisplayName = "ShouldSuccess")]
        public async Task ShouldSuccess()
        {
            var notification = new AutoFaker<BookRoomsNotification>().Generate();

            _useCase.Setup(x => x.HandleAsync(It.IsAny<BookRoomsNotification>(), It.IsAny<CancellationToken>()));

            await _handler.Handle(notification, new CancellationToken());

            _useCase.Verify(x => x.HandleAsync(It.IsAny<BookRoomsNotification>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
