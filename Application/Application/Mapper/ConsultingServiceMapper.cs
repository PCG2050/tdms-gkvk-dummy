
namespace Application.Mapper
{
    [Mapper]
    public partial class ConsultingServiceMapper
    {    
       
        public partial ConsultingServiceDto MapToDto(ConsultingAndSocialMediaService entity);
        public partial CompleteConsultingServiceDto MapToCompleteDto(ConsultingAndSocialMediaService entity);
        public partial ModeAndOutreachDto MapToDto(TableModeAndOutreach entity);

        public partial ConsultingAndSocialMediaService MapToEntity(ConsultingServiceCreateDto dto);
        public partial TableModeAndOutreach MapToEntity(ModeAndOutreachCreateDto dto);

        // Helper methods for nested mappings
        private string? GetUnitName(OrganizationUnitLocation? location)
            => location?.Unit?.Name;

        private string? GetDistrictName(OrganizationUnitLocation? location)
            => location?.District?.Name;

        private string? GetStateName(OrganizationUnitLocation? location)
            => location?.District?.State?.Name;

        // Mapping with Navigation Details
        [MapProperty(nameof(ConsultingAndSocialMediaService.UnitLocation), nameof(ConsultingServiceDto.UnitName), Use = nameof(GetUnitName))]
        [MapProperty(nameof(ConsultingAndSocialMediaService.UnitLocation), nameof(ConsultingServiceDto.DistrictName), Use = nameof(GetDistrictName))]
        [MapProperty(nameof(ConsultingAndSocialMediaService.UnitLocation), nameof(ConsultingServiceDto.StateName), Use = nameof(GetStateName))]
        [MapProperty(nameof(ConsultingAndSocialMediaService.Organization.Name), nameof(ConsultingServiceDto.OrganizationName))]
        [MapProperty(nameof(ConsultingAndSocialMediaService.Category.Name), nameof(ConsultingServiceDto.CategoryName))]
        [MapProperty(nameof(ConsultingAndSocialMediaService.RelatedTo.Name), nameof(ConsultingServiceDto.RelatedToName))]
        [MapProperty(nameof(ConsultingAndSocialMediaService.ExtensionActivity.Name), nameof(ConsultingServiceDto.ExtensionActivityName))]
        [MapProperty(nameof(ConsultingAndSocialMediaService.Particulars.Name), nameof(ConsultingServiceDto.ParticularsName))]
        [MapProperty(nameof(ConsultingAndSocialMediaService.ModeOutreach.Name), nameof(ConsultingServiceDto.ModeOutreachName))]
        [MapProperty(nameof(ConsultingAndSocialMediaService.ApprovedBy.FirstName ), nameof(ConsultingServiceDto.ApprovedByName))]
        [MapProperty(nameof(ConsultingAndSocialMediaService.CreatedBy.FirstName), nameof(ConsultingServiceDto.CreatedByName))]
        public partial ConsultingServiceDto MapToDtoWithDetails(ConsultingAndSocialMediaService entity);


        //public partial ConsultingServiceDto MapToDtoWithDetails(ConsultingAndSocialMediaService entity)
        //{
        //    // Use the existing MapToDto method to map base properties,
        //    // then manually set the navigation properties as per the MapProperty attributes.
        //    var dto = MapToDto(entity);

        //    dto.UnitName = GetUnitName(entity.UnitLocation);
        //    dto.DistrictName = GetDistrictName(entity.UnitLocation);
        //    dto.StateName = GetStateName(entity.UnitLocation);
        //    dto.OrganizationName = entity.Organization?.Name;
        //    dto.CategoryName = entity.Category?.Name;
        //    dto.RelatedToName = entity.RelatedTo?.Name;
        //    dto.ExtensionActivityName = entity.ExtensionActivity?.Name;
        //    dto.ParticularsName = entity.Particulars?.Name;
        //    dto.ModeOutreachName = entity.ModeOutreach?.Name;
        //    dto.ApprovedByName = entity.ApprovedBy?.FirstName;
        //    dto.CreatedByName = entity.CreatedBy?.FirstName;

        //    return dto;
        //}


        // Manual Mapping for Update DTO

        public void MapUpdateDtoToEntity(ConsultingServiceUpdateDto dto, ConsultingAndSocialMediaService entity)
        {
            if (dto.StartDate.HasValue) entity.StartDate = dto.StartDate;
            if (dto.EndDate.HasValue) entity.EndDate = dto.EndDate;
            if (dto.CategoryId.HasValue) entity.CategoryId = dto.CategoryId;
            if (dto.RelatedToId.HasValue) entity.RelatedToId = dto.RelatedToId;
            if (dto.RelatedToDisciplineOther != null) entity.RelatedToDisciplineOther = dto.RelatedToDisciplineOther;
            if (dto.ExtensionActivityId.HasValue) entity.ExtensionActivityId = dto.ExtensionActivityId;
            if (dto.ParticularsId.HasValue) entity.ParticularsId = dto.ParticularsId;
            if (dto.ParticularsOthers != null) entity.ParticularsOthers = dto.ParticularsOthers;
            if (dto.ModeOrOutreachId.HasValue) entity.ModeOrOutreachId = dto.ModeOrOutreachId;
            if (dto.Title != null) entity.Title = dto.Title;
            if (dto.Location != null) entity.Location = dto.Location;
            if (dto.Date.HasValue) entity.Date = dto.Date.Value;
            if (dto.Publisher != null) entity.Publisher = dto.Publisher;
            if (dto.WeblinkOrApplink != null) entity.WeblinkOrApplink = dto.WeblinkOrApplink;
            if (dto.PublishedDocUrl != null) entity.PublishedDocUrl = dto.PublishedDocUrl;
            if (dto.PhoneCalls.HasValue) entity.PhoneCalls = dto.PhoneCalls.Value;
            if (dto.Male_SC.HasValue) entity.Male_SC = dto.Male_SC.Value;
            if (dto.Male_ST.HasValue) entity.Male_ST = dto.Male_ST.Value;
            if (dto.Male_OBC.HasValue) entity.Male_OBC = dto.Male_OBC.Value;
            if (dto.Male_GEN.HasValue) entity.Male_GEN = dto.Male_GEN.Value;
            if (dto.Female_SC.HasValue) entity.Female_SC = dto.Female_SC.Value;
            if (dto.Female_ST.HasValue) entity.Female_ST = dto.Female_ST.Value;
            if (dto.Female_OBC.HasValue) entity.Female_OBC = dto.Female_OBC.Value;
            if (dto.Female_GEN.HasValue) entity.Female_GEN = dto.Female_GEN.Value;
            if (dto.FacebookPostCount.HasValue) entity.FacebookPostCount = dto.FacebookPostCount.Value;
            if (dto.NoOfSms.HasValue) entity.NoOfSms = dto.NoOfSms.Value;
            if (dto.NoOfWhatsappGroup.HasValue) entity.NoOfWhatsappGroup = dto.NoOfWhatsappGroup.Value;
            if (dto.NoOfWhatsappMessage.HasValue) entity.NoOfWhatsappMessage = dto.NoOfWhatsappMessage.Value;
            if (dto.CountOfAnsweredQueries.HasValue) entity.CountOfAnsweredQueries = dto.CountOfAnsweredQueries.Value;
            if (dto.FaceToFace.HasValue) entity.FaceToFace = dto.FaceToFace.Value;
            if (dto.GroupDiscussion.HasValue) entity.GroupDiscussion = dto.GroupDiscussion.Value;
            if (dto.NoOfEmailSent.HasValue) entity.NoOfEmailSent = dto.NoOfEmailSent.Value;
            if (dto.NoOfNewspaperCoverage.HasValue) entity.NoOfNewspaperCoverage = dto.NoOfNewspaperCoverage.Value;
            if (dto.NoOfBeneficiaries.HasValue) entity.NoOfBeneficiaries = dto.NoOfBeneficiaries.Value;
            if (dto.NoOfPressVisit.HasValue) entity.NoOfPressVisit = dto.NoOfPressVisit.Value;
            if (dto.NoOfPressMeet.HasValue) entity.NoOfPressMeet = dto.NoOfPressMeet.Value;
            if (dto.NoOfPressCoverage.HasValue) entity.NoOfPressCoverage = dto.NoOfPressCoverage.Value;
            if (dto.NoOfTweets.HasValue) entity.NoOfTweets = dto.NoOfTweets.Value;
        }
    }
}