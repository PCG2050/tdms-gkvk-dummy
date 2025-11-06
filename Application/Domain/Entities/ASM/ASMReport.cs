using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ASM
{
    public class AsmReport : ReportEntryBaseEntity
    {
        public string?  InstituteName { get; set; }

        public int? NoOfFarmers { get; set; }

        public int? NoOfStudents { get; set; }

        public int? NoOfPublic { get; set; }

        //public int Total
        //{
        //    get
        //    {
        //        return (NoOfFarmers ?? 0) + (NoOfStudents ?? 0) + (NoOfPublic ?? 0);
        //    }           
        //}
    }
}
