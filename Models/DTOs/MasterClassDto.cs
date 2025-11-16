namespace BookingPetProject.Models.DTOs
{
    public class MasterClassDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime DateTimeEvent { get; set; }
        public int Capacity { get; set; }
    }
}