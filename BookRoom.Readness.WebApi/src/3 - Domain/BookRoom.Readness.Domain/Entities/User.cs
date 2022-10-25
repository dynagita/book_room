using MongoDB.Bson.Serialization.Attributes;

namespace BookRoom.Readness.Domain.Entities
{
    public class User : EntityBase
    {
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("lastName")]
        public string LastName { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("bornDate")]
        public DateTime? BornDate { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("books")]
        public IEnumerable<BookRooms> Books { get; set; }
    }
}
