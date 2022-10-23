using BookRoom.Service.Domain.Contract.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BookRoom.Services.Domain.Entities
{
    public class BookRooms : EntityBase
    {
        [BsonElement("startDate")]
        public DateTime StartDate { get; set; }

        [BsonElement("endDate")]
        public DateTime EndDate { get; set; }

        [BsonElement("status")]
        public BookStatusRoom Status { get; set; }

        [BsonElement("user")]
        public User User { get; set; }

        [BsonElement("room")]
        public Room Room { get; set; }
    }
}
