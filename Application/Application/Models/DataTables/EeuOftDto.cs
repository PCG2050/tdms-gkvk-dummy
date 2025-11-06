namespace Application.Models.DataTables
{
    public class EeuOftDto : EeuOftCreateDto, IBaseEntryDto
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }

    public class EeuOftCreateDto : BaseEntryCreateDto
    {
        public required string Title { get; set; }
        public required string Crop { get; set; }
        public double Area { get; set; }
        public int TrialMaleScStCount { get; set; }
        public int TrialMaleGenCount { get; set; }
        public int TrialFemaleScStCount { get; set; }
        public int TrailFemalGenCount { get; set; }
        public double YieldT1 { get; set; }
        public double YieldT2 { get; set; }
        public double PercentIncreaseInYield { get; set; }
    }

    public class EeuOftUpdateDto : BaseEntryUpdateDto
    {
        public string Title { get; set; }
        public string Crop { get; set; }
        public double? Area { get; set; }
        public int? TrialMaleScStCount { get; set; }
        public int? TrialMaleGenCount { get; set; }
        public int? TrialFemaleScStCount { get; set; }
        public int? TrailFemalGenCount { get; set; }
        public double? YieldT1 { get; set; }
        public double? YieldT2 { get; set; }
        public double? PercentIncreaseInYield { get; set; }
    }
}
