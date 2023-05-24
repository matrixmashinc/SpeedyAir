using Newtonsoft.Json;

namespace SpeedyAir
{
    public class Order
    {
        public string OrderId { get; set; }
        public string Destination { get; set; }
        public Flight? Flight { get; set; }
    }
}
