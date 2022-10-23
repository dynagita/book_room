using BookRoom.Service.Domain.Contract.Notifications;
using MediatR;

namespace BookRoom.Service.Domain.Contract.Handlers.Notifications
{
    public interface IPropagateRoomNotificationHandler : INotificationHandler<RoomNotification>
    {
    }
}
