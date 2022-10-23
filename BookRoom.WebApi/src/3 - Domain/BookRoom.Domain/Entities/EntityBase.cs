namespace BookRoom.Domain.Entities
{
    public class EntityBase
    {
        public EntityBase()
        {
            DatInc = DatAlt = DateTime.Now;
        }

        public int Id { get; set; }

        public DateTime DatInc { get; set; }

        public DateTime DatAlt { get; set; }

        public bool Active { get; set; }
    }
}
