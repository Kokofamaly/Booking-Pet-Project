namespace BookingPetProject.Models.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int MasterClassId { get; set; }
        public string MasterClassName { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public int BookingStatus{ get; set; }
    }
}