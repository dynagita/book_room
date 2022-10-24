using AutoBogus;
using BookRoom.Application.Handlers.Notifications;
using BookRoom.Domain.Contract.Handlers.Notification;
using BookRoom.Domain.Contract.Notification.BookRooms;
using BookRoom.Domain.Contract.Requests.Commands.BookRooms;
using BookRoom.Domain.Contract.UseCases.BookRooms;
using Moq;

namespace BookRoom.Unit.Tests.Handlers.Notifications
{
    public class BookRoomNotificationHandlerTest
    {
        private readonly Mock<IBookRoomProcessUseCase> _useCase;
        private readonly IBookRoomNotificationHandler _handler;

        public BookRoomNotificationHandlerTest()
        {
            _useCase = new Mock<IBookRoomProcessUseCase>();
            _handler = new BookRoomNotificationHandler(_useCase.Object);
        }

        [Fact(DisplayName = "ShouldSuccess")]
        public async Task ShouldSuccess()
        {
            var notification = new AutoFaker<BookRoomNotification>().Generate();

            _useCase.Setup(x => x.HandleAsync(It.IsAny<BookRoomNotification>(), It.IsAny<CancellationToken>()));

            await _handler.Handle(notification, new CancellationToken());

            _useCase.Verify(x => x.HandleAsync(It.IsAny<BookRoomNotification>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
