namespace Application.Models.DataTables
{
    public class AsmVisitDto : AsmVisitCreateDto, IBaseEntryDto
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }

    public class AsmVisitCreateDto : BaseEntryCreateDto
    {
        public required string OrganizationName { get; set; }
        public int VisitorCount { get; set; }
    }

    public class AsmVisitUpdateDto : BaseEntryUpdateDto
    {
        public string? OrganizationName { get; set; }
        public int? VisitorCount { get; set; }
    }
}
