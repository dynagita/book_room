using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BookRoom.Services.Domain.Entities
{
    public class EntityBase : INotification
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public object Id { get; set; }

        [Required]
        [BsonElement("datInc")]
        public DateTime DatInc { get; set; }

        [Required]
        [BsonElement("datAlt")]
        public DateTime DatAlt { get; set; }

        [Required]
        [BsonElement("active")]
        public bool Active { get; set; }

        [Required]
        [BsonElement("reference")]
        public int Reference { get; set; }
    }
}
