using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BookRoom.Services.Domain.Entities
{
    public class User : EntityBase
    {
        [Required]
        [BsonElement("name")]
        public string Name { get; set; }
        [Required]
        [BsonElement("lastName")]
        public string LastName { get; set; }
        [Required]
        [BsonElement("email")]
        public string Email { get; set; }
        [Required]
        [BsonElement("bornDate")]
        public DateTime? BornDate { get; set; }
        [Required]
        [BsonElement("password")]
        public string Password { get; set; }

        [Required]
        [BsonElement("books")]
        public IEnumerable<BookRooms> Books { get; set; }
    }
}
