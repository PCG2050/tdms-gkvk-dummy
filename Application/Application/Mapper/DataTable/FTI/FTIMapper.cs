using Application.Models.DataTables.FTI;
using Domain.Entities.FTI;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper.DataTable.FTI
{
    [Mapper]
    public partial class FTIMapper
    {
        public partial FTIProgramDetailsDto MapToDto(FTIProgramDetails entity);
        public partial FTIProgramDetails MapToEntity(FTIProgramDetailsDto dto);

        public partial FTIParticipantDemographicsDto MapToDto(FTIParticipantDemographics entity);
        public partial FTIParticipantDemographics MapToEntity(FTIParticipantDemographicsDto dto);

        public partial FTIProgramContentAndResourcesDto MapToDto(FTIProgramContentAndResources entity);
        public partial FTIProgramContentAndResources MapToEntity(FTIProgramContentAndResourcesDto dto);

        public partial FTIResourcePersonDto MapToDto(FTIResourcePerson entity);
        public partial FTIResourcePerson MapToEntity(FTIResourcePersonDto dto);

        public partial FTITopicsCoveredInClassDto MapToDto(FTITopicsCoveredInClass entity);
        public partial FTITopicsCoveredInClass MapToEntity(FTITopicsCoveredInClassDto dto);

        public partial FTITeachingAidsDevelopedDto MapToDto(FTITeachingAidsDeveloped entity);
        public partial FTITeachingAidsDeveloped MapToEntity(FTITeachingAidsDevelopedDto dto);

        public partial FTIAdvisoryServicesDto MapToDto(FTIAdvisoryServices entity);
        public partial FTIAdvisoryServices MapToEntity(FTIAdvisoryServicesDto dto);

        public partial FTIReportDto MapToDto(FTIReport entity);
        public partial FTIReport MapToEntity(FTIReportDto dto);

        public partial FTIRecommendationDto MapToDto(FTIRecommendation entity);
        public partial FTIRecommendation MapToEntity(FTIRecommendationDto dto);
    }
}
