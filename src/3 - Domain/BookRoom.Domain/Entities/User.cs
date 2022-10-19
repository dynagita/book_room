namespace BookRoom.Domain.Entities
{
    public class User : EntityBase
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BornDate { get; set; }
        public string Password { get; set; }
    }
}
