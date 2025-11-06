using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class OrganizationCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public int DistrictId { get; set; }
        public string Pincode { get; set; }
        public string StorageContainerName { get; set; }
    }
    public class OrganizationUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public string? LogoUrl { get; set; }
        public int? DistrictId { get; set; }
        public string? Pincode { get; set; }
        public string? StorageContainerName { get; set; }
    }
}
