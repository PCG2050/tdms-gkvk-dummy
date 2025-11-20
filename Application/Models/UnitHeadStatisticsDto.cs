namespace Application.Models
{
    /// <summary>
    /// Statistics for Unit Head dashboard
    /// </summary>
    public class UnitHeadStatisticsDto
    {
        public int AssignedUnitsCount { get; set; }
        public int TrainersCount { get; set; }
        public int PendingApprovalsCount { get; set; }
        public int ApprovedThisMonthCount { get; set; }
    }
}
