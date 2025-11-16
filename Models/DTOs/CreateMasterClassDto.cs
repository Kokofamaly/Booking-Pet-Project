namespace BookingPetProject.Models.DTOs
{
    public class CreateMasterClassDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime DateTimeEvent { get; set; }
        public int Capacity { get; set; }
    }
}