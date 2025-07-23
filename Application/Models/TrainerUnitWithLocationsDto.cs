namespace Application.Models
{
    public class TrainerUnitWithLocationsDto
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public List<UnitLocationDto> Locations { get; set; }
    }
}