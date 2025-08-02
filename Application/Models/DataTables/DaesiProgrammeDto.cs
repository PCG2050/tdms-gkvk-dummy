using Application.Interface;

namespace Application.Models.DataTables
{
    public class DaesiProgrammeDto:DaesiProgrammeCreateDto
    {
        public required int Id {  get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }

    public class DaesiProgrammeCreateDto
    {
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }
        public required int UnitLocationId { get; set; }
        public required string Place { get; set; }
        public required string NodalTrainingInstitute { get; set; }
        public int BatchCount { get; set; }
        public int DealerCount { get; set; }
        public string? Attachements { get; set; }
    }
    public class DaesiProgrammeUpdateDto:IUpdateDto
    {
        public int Id { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Place { get; set; }
        public string? NodalTrainingInstitute { get; set; }
        public int? BatchCount { get; set; }
        public int? DealerCount { get; set; }
        public string? Attachements { get; set; }
    }
}
