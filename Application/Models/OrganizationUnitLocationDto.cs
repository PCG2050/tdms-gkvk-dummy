using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class OrganizationUnitLocationDto
    {
        public int UnitId { get; set; }
        public int DistrictId { get; set; }
    }

    public class OrganizationUnitLocationUpdateDto
    {
        public int Id { get; set; }

       
        public int? UnitId { get; set; }

       
        public int? DistrictId { get; set; }      
    }
    //public class OrganizationUnitLocationResponseDto
    //{
    //    public int Id { get; set; }
    //    public int OrganizationId { get; set; }
    //    public int UnitId { get; set; }
    //    public string UnitName { get; set; } = string.Empty;
    //    public int DistrictId { get; set; }
    //    public string DistrictName { get; set; } = string.Empty;
    //    public int StateId { get; set; }
    //    public string StateName { get; set; } = string.Empty;
    //    public bool IsActive { get; set; } = true;
    //    public DateTime CreatedAt { get; set; }
    //    public DateTime? UpdatedAt { get; set; }
    //    public int CreatedById { get; set; }
    //    public string CreatedByName { get; set; } = string.Empty;
    //}
}
