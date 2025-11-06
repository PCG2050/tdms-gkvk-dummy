using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DataTables
{
    public class UserHistoryDto
    {
        public int Id { get; set; }
        public string? Title { get; set; } 
        public string? CreatedAt { get; set; }
        public string? FormStatus { get; set; }
    }
}
