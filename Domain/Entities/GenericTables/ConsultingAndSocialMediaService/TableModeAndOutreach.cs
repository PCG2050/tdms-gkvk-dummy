
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities.GenericTables.ConsultingAndSocialMediaService
{
    public class TableModeAndOutreach : AuditableBaseEntity
    {
        public int ConsultingServiceId { get; set; }
        
        [ForeignKey(nameof(ConsultingServiceId))]
        public ConsultingAndSocialMediaService? ConsultingService { get; set; }       
       

        [MaxLength(150)]
        public string? Name { get; set; }      
        public int? MobileNo { get; set; }       
        public bool Gender { get; set; }        

      
    }
}
