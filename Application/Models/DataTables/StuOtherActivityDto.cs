using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DataTables
{
    public class StuOtherActivityDto:StuOtherActivityCreateDto
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdateAt { get; set; }
    }
    public class StuOtherActivityCreateDto
    {
        public int UnitLocationId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string ActivityDetails { get; set; } = string.Empty;
        public string Attachements { get; set; }
    }
    public class StuOtherActivityUpdateDto
    {
        public int Id { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? ActivityDetails { get; set; }
        public string? Attachements { get; set; }
    }
}
