﻿namespace BookRoom.Domain.Entities
{
    public class Room : EntityBase
    {
        public Room() : base()
        {

        }
        public string Description { get; set; }

        public string Title { get; set; }

        public int Number { get; set; }

        public IEnumerable<BookRooms> Books { get; set; }
    }
}
