using BookRoom.Domain.Contract.Enums;

namespace BookRoom.Domain.Entities
{
    public class BookRooms : EntityBase
    {
        public BookRooms() : base() 
        { 
        }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public BookStatusRoom Status { get; set; }

        public User User { get; set; }

        public Room Room { get; set; }
    }
}
