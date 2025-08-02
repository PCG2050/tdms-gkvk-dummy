namespace Application.Models.DataTables
{
    public class EeuFldDto : EeuFldCreateDto, IBaseEntryDto
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
    public class EeuFldCreateDto: BaseEntryCreateDto
    {
        public required string Title { get; set; }
        public required string Crop { get; set; }
        public double Area { get; set; }
        public int TrialMaleScStCount { get; set; }
        public int TrialMaleGenCount { get; set; }
        public int TrialFemaleScStCount { get; set; }
        public int TrailFemalGenCount { get; set; }
        public double YieldDemo { get; set; }
        public double YieldCheck { get; set; }
        public double PercentIncreaseInYield { get; set; }
    }
    public class EeuFldUpdateDto: BaseEntryUpdateDto
    {
        public string? Title { get; set; }
        public string? Crop { get; set; }
        public double? Area { get; set; }
        public int? TrialMaleScStCount { get; set; }
        public int? TrialMaleGenCount { get; set; }
        public int? TrialFemaleScStCount { get; set; }
        public int? TrailFemalGenCount { get; set; }
        public double? YieldDemo { get; set; }
        public double? YieldCheck { get; set; }
        public double? PercentIncreaseInYield { get; set; }
    }
}
