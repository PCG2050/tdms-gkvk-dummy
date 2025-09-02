using Application.Interface.Repository;

namespace Application.Models
{
    public class UnitLocationDto
    {
        public int UnitLocationId { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
    }

    public class TrainerWithAssignmentsDto
    {

        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;

        // All the trainer’s assignments
        public List<TrainerWithUnitDto> Assignments { get; set; } = new();
    }
    public class TrainerWithUnitDto
    {
        public int UnitId { get; set; }

        public string UnitName { get; set; }

        public List<UnitLocationDto> Locations { get; set; } = new();
    }


}