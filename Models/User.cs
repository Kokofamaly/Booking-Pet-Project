namespace BookingPetProject.Models
{
    public class User
    {
        public string? Id { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }

        public List<Booking> Bookings { get; set; } = new();
    }
}