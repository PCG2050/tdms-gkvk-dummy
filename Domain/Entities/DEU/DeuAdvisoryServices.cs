using Domain.Entities.FIU;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DEU
{
    public class DeuAdvisoryServices : AuditableBaseEntity
    {

     
            [Required]
            public int DeuProgramDetailsId { get; set; }

            [ForeignKey(nameof(DeuProgramDetailsId))]
            public DeuProgramDetails ProgramDetails { get; set; }

            // Advisory service metrics
            public int? NoOfFacebookSMS { get; set; }
            public int? NoOfSMSSentToRegisteredFarmers { get; set; }
            public int? NoOfWhatsappGroups { get; set; }
            public int? NoOfWhatsappSMS { get; set; }
            public int? NoOfAnsweredWhatsappQueries { get; set; }
            public int? NoOfPhoneCalls { get; set; }
            public int? NoOfFaceToFaceDiscussions { get; set; }
            public int? NoOfGroupDiscussions { get; set; }
            public int? NoOfEmailsSent { get; set; }
            public int? NoOfNewspaperCoverage { get; set; }
            public int? NoOfBeneficiaries { get; set; }
        
    }

}

