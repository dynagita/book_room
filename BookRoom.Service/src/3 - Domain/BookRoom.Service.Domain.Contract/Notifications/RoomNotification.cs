using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BookRoom.Service.Domain.Contract.Notifications
{
    public class RoomNotification : INotification
    {
        public DateTime DatInc { get; set; }
        public DateTime DatAlt { get; set; }
        public bool Active { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int Number { get; set; }
    }
}
