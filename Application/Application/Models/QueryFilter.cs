using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class QueryFilter
    {
        public Dictionary<string, object> Filters { get; set; } = new();
    }
}
