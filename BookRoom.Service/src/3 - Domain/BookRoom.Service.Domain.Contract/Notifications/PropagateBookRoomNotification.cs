using BookRoom.Service.Domain.Contract.Enums;
using MediatR;

namespace BookRoom.Service.Domain.Contract.Notifications
{
    public class PropagateBookRoomNotification : INotification
    {
        public DateTime DatInc { get; set; }
        public DateTime DatAlt { get; set; }
        public bool Active { get; set; }
        public int Reference { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public BookStatusRoom Status { get; set; }
        public PropagateUserNotification User { get; set; }
        public PropagateRoomNotification Room { get; set; }
    }
}
