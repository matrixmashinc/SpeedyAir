namespace SpeedyAir
{
    public class Flight
    {
        public int FlightNumber { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public int Day { get; set; }        

        public Flight(int flightNumber, string departure, string arrival, int day)
        {
            FlightNumber = flightNumber;
            Departure = departure;
            Arrival = arrival;
            Day = day;            
        }
    }
}
