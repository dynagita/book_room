using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BookRoom.Service.Domain.Contract.Notifications
{
    public class PropagateRoomNotification : INotification
    {
        public DateTime DatInc { get; set; }
        public DateTime DatAlt { get; set; }
        public bool Active { get; set; }
        public int Reference { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int Number { get; set; }
    }
}
