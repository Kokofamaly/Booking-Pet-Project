namespace BookingPetProject.Models
{
    public class MasterClass
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime DateTimeEvent { get; set; }
        public int Capacity { get; set; }

        public List<Booking> Bookings { get; set; } = new();
    }
}