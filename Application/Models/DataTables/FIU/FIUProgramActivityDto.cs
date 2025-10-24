using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DataTables.FIU
{
    public class FIUProgramActivityDto
    {
        public int UnitLocationId { get; set; }
        public int OrganizationId { get; set; }
        public int? FIUActivitiesId { get; set; }
        public int? Number { get; set; }
        public string? UploadMediaUrl { get; set; }
    }
}
