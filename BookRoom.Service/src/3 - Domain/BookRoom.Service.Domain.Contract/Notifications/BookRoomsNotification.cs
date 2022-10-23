using BookRoom.Service.Domain.Contract.Enums;
using MediatR;

namespace BookRoom.Service.Domain.Contract.Notifications
{
    public class BookRoomsNotification : INotification
    {
        public DateTime DatInc { get; set; }
        public DateTime DatAlt { get; set; }
        public bool Active { get; set; }
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public BookStatusRoom Status { get; set; }
        public UserNotification User { get; set; }
        public RoomNotification Room { get; set; }
    }
}
