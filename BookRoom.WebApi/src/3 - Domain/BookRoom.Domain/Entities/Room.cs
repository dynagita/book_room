namespace BookRoom.Domain.Entities
{
    public class Room : EntityBase
    {
        public string Description { get; set; }

        public string Title { get; set; }

        public IEnumerable<BookRooms> Books { get; set; }
    }
}
