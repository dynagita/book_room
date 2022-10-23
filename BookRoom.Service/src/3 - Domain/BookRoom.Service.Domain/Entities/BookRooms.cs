using BookRoom.Service.Domain.Contract.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BookRoom.Services.Domain.Entities
{
    public class BookRooms : EntityBase
    {
        [Required]
        [BsonElement("startDate")]
        public DateTime StartDate { get; set; }

        [Required]
        [BsonElement("endDate")]
        public DateTime EndDate { get; set; }

        [Required]
        [BsonElement("status")]
        public BookStatusRoom Status { get; set; }

        [Required]
        [BsonElement("user")]
        public User User { get; set; }

        [Required]
        [BsonElement("room")]
        public Room Room { get; set; }
    }
}
