namespace BookingPetProject.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int MasterClassId { get; set; }
        public Status BookingStatus { get; set; }

        public User? User { get; set; }
        public MasterClass? MasterClass { get; set; }

        public enum Status
        {
            Pending,
            Confirmed,
            Cancelled
        }
    }
}