using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DataTables
{
    public class NaepDetailsDto : NaepDetailsCreateDto, IBaseEntryDto
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
    public class NaepDetailsCreateDto : BaseEntryCreateDto
    {
        public required string Particulars { get; set; }
        public required string Place { get; set; }
        public int ProgrammeCount { get; set; }
        public int ParticipantCount { get; set; }
    }
    public class NaepDetailsUpdateDto : BaseEntryUpdateDto
    {
        public string? Particulars { get; set; }
        public string? Place { get; set; }
        public int? ProgrammeCount { get; set; }
        public int? ParticipantCount { get; set; }
    }
}
