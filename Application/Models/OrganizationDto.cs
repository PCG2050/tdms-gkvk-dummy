using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class OrganizationDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DistrictName { get; set; }
        public string StateName { get; set; }
        public string LogoUrl { get; set; }
        public string StorageContainerName { get; set; }
        public string StorageContainerNamePublic { get => StorageContainerName is null ? string.Empty : $"{StorageContainerName}-public"; }
    }
}
