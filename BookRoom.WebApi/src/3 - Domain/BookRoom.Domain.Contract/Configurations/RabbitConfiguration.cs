namespace BookRoom.Domain.Contract.Configurations
{
    public class RabbitConfiguration
    {
        public string Connection { get; set; }

        public bool Durable { get; set; }

        public bool Exclusive { get; set; }

        public bool AutoDelete { get; set; }
    }
}
