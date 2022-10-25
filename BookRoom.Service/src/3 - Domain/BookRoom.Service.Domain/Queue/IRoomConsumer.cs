using BookRoom.Service.Domain.Contract.Notifications;
using BookRoom.Services.Domain.Entities;

namespace BookRoom.Service.Domain.Queue
{
    public interface IRoomConsumer : IConsumer<Contract.Notifications.RoomNotification>
    {
    }
}
