namespace Application.Models
{
    public class UnitWithLocationsDto
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public List<UnitLocationDto> Locations { get; set; }
    }

    public class UnitHeadFlatDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int OrgUnitLocationId { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }

        public int StateId { get; set; }
        public string StateName { get; set; }

        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
    }

}