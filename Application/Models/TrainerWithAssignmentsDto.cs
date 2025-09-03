using Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class BulkTrainerAssignmentByLocationDto
    {
        public int TrainerId { get; set; }
        public List<int> OrganizationUnitLocationIds { get; set; } = new List<int>();
    }

    // DTO for trainer with their current assignments
    public class TrainerWithAssignmentsDto
    {
        public int TrainerId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public EmployementType EmployementType { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public DateOnly DateOfJoining { get; set; }
        public bool IsDeactivated { get; set; }

        // List of currently assigned location IDs
        public List<int> AssignedLocationIds { get; set; } = new List<int>();

        // Detailed location information
        public List<UnitLocationDetailsDto> UnitLocationDetails { get; set; } = new List<UnitLocationDetailsDto>();
    }

 
}
