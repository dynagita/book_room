using MediatR;

namespace BookRoom.Service.Domain.Contract.Notifications
{
    public class UserNotification : INotification
    {
        public DateTime DatInc { get; set; }
        public DateTime DatAlt { get; set; }
        public bool Active { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? BornDate { get; set; }
        public string Password { get; set; }
    }
}
