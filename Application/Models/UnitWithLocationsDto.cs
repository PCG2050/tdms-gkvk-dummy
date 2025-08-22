namespace Application.Models
{
    public class UnitWithLocationsDto
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public List<UnitLocationDto> Locations { get; set; }
    }
}