using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BookRoom.Services.Domain.Entities
{
    public class Room : EntityBase
    {
        [Required]
        [BsonElement("description")]
        public string Description { get; set; }

        [Required]
        [BsonElement("title")]
        public string Title { get; set; }

        [Required]
        [BsonElement("number")]
        public int Number { get; set; }

        [Required]
        [BsonElement("books")]
        public IEnumerable<BookRooms> Books { get; set; }
    }
}
