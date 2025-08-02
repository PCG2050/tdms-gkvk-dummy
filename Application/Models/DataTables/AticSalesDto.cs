using Domain.Entities.ATIC;

namespace Application.Models.DataTables
{
    public class AticSalesDto : AticSalesCreateDto, IBaseEntryDto
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
    public class AticSalesCreateDto:BaseEntryCreateDto
    {
        public required string Details { get; set; }
        public AticSalesQuantityType QuantityType { get; set; }
        public double Quantity { get; set; }
    }
    public class AticSalesUpdateDto : BaseEntryUpdateDto
    {
        public string? Details { get; set; }
        public AticSalesQuantityType? QuantityType { get; set; }
        public double? Quantity { get; set; }
    }
}
