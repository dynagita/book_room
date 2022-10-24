using MongoDB.Bson.Serialization.Attributes;

namespace BookRoom.Readness.Domain.Entities
{
    public class Room : EntityBase
    {
        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("number")]
        public int Number { get; set; }

        [BsonElement("books")]
        public IEnumerable<BookRooms> Books { get; set; }
    }
}
