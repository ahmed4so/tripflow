namespace UsersApp.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int FlightId { get; set; } = 1; // Default FlightId
        public int PassengerId { get; set; } = 1; // Default PassengerId
        public DateTime BookingDate { get; set; } = DateTime.Now; // Default BookingDate
        public decimal TotalPrice { get; set; }

        public Flight Flight { get; set; }
        public Passenger Passenger { get; set; }
    }
}
