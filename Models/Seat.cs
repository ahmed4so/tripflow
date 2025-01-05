namespace UsersApp.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public string SeatNumber { get; set; }
        public bool IsAvailable { get; set; }
        public int FlightId { get; set; } // Foreign Key from Flight

        public Flight Flight { get; set; }
    }
}
