using MediatR;

namespace BookRoom.Service.Domain.Contract.Notifications
{
    public class PropagateUserNotification : INotification
    {
        public DateTime DatInc { get; set; }
        public DateTime DatAlt { get; set; }
        public bool Active { get; set; }
        public int Reference { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? BornDate { get; set; }
        public string Password { get; set; }
    }
}
