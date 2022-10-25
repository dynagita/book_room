using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookRoom.Readness.Domain.Entities
{
    public class EntityBase
    {                
        [BsonElement("datInc")]
        public DateTime DatInc { get; set; }

        [BsonElement("datAlt")]
        public DateTime DatAlt { get; set; }

        [BsonElement("active")]
        public bool Active { get; set; }

        [BsonId]
        [BsonElement("_id")]
        public int Id { get; set; }
    }
}
