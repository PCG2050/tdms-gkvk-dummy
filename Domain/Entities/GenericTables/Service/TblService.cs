using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.GenericTables.Service
{
    public class TblService : AuditableBaseEntity
    {
        // Foreign keys
        public int? CategoryId { get; set; }
        [JsonIgnore]
        public ConsultancyServicesCategory? Category { get; set; }
        [MaxLength(200)]
        public string? OtherCategory { get; set; }

        public int? ThemeId { get; set; }
        [JsonIgnore]
        public ServiceTheme? Theme { get; set; }   
        [MaxLength(200)]
        public string? OtherTheme { get; set; }

        [MaxLength(250)]
        public string? CropPlantProductName { get; set; }

        [MaxLength(150)]
        public string? Variety { get; set; }

        public int? SourceOfFundId { get; set; }
        [JsonIgnore]
        public SourceOfFund? SourceOfFund { get; set; }

        [MaxLength(200)]
        public string? OtherSourceOfFund { get; set; }

        [MaxLength(250)]
        public string? Component { get; set; }
        
        public int? QuantityUnitId { get; set; }
        [JsonIgnore]
        public QuantityUnit? QuantityUnit { get; set; }

        public int Number { get; set; }
        public double AmountGenerated { get; set; }

      

        //Training Hall category
        public DateTime Date { get; set; }

        [MaxLength(250)]
        public string? RentedTo { get; set; }

        [MaxLength(250)]
        public string? TitleOfActivityConducted { get; set; }

        public decimal AmountReleased { get; set; }

        // Navigation
        public ICollection<TableHostel> Hostels { get; set; }
        public ICollection<RevolvingFundStatus> RevolvingFundStatuses { get; set; }

        public ICollection<VisitorDetail> VisitorDetails { get; set; }

        
    }
}
