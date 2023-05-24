using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SpeedyAir
{
    public class FlightScheduler
    {
        private List<Flight> flights;
        private List<Order> orders;

        private int MaxOrders = 20;

        public FlightScheduler()
        {
            flights = new List<Flight>();
            orders = new List<Order>();
        }

        public void DisplayFlightSchedule()
        {
            foreach (var flight in flights)
            {
                Console.WriteLine($"Flight: {flight.FlightNumber}, departure: {flight.Departure}, arrival: {flight.Arrival}, day: {flight.Day}");
            }
        }

        public void LoadFlightSchedule()
        {
            // Day 1 - Flights
            flights.Add(new Flight(1, "YUL", "YYZ", 1));
            flights.Add(new Flight(2, "YUL", "YYC", 1));
            flights.Add(new Flight(3, "YUL", "YVR", 1));

            // Day 2 - Flights
            flights.Add(new Flight(4, "YUL", "YYZ", 2));
            flights.Add(new Flight(5, "YUL", "YYC", 2));
            flights.Add(new Flight(6, "YUL", "YVR", 2));
        }

        public void LoadOrdersFromFile(string filePath)
        {
            string json = File.ReadAllText(filePath);
            if (json != null)
            {
                var orderData = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

                if (orderData != null)
                {
                    // Load Orders
                    foreach (var item in orderData)
                    {
                        orders.Add(
                            new Order
                            {
                                OrderId = item.Key,
                                Destination = Convert.ToString(JObject.Parse(JsonConvert.SerializeObject(item.Value))["destination"])
                            });
                    }
                }
            }
        }

        public void ScheduleOrders()
        {
            // Set Flight Details for Each Order
            foreach (var flight in flights.OrderBy(f => f.Arrival))
            {
                orders.Where(o => o.Destination == flight.Arrival)
                    .Skip((flight.Day - 1) * MaxOrders)
                    .Take(MaxOrders).ToList()
                    .ForEach(f => f.Flight = flight);
            }

            // Iterate Orders for Output
            foreach (var order in orders)
            {
                bool scheduled = false;
                if (order.Flight != null)
                {
                    Console.WriteLine($"order: {order.OrderId}, flightNumber: {order.Flight.FlightNumber}, departure: {order.Flight.Departure}, arrival: {order.Flight.Arrival}, day: {order.Flight.Day}");
                    scheduled = true;
                }

                if (!scheduled)
                {
                    Console.WriteLine($"order: {order.OrderId}, flightNumber: not scheduled");
                }
            }
        }
    }
}
