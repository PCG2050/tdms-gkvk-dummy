using Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DataTables
{
    public interface IBaseEntryDto
    {
        int Id { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset? UpdatedAt { get; set; }
    }
    public abstract class BaseEntryCreateDto
    {
        public int UnitLocationId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string? Attachements { get; set; }
    }

    public abstract class BaseEntryUpdateDto:IUpdateDto
    {
        public required int Id { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Attachements { get; set; }
    }
}
