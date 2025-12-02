using Domain.Entities;

namespace Application.Interface.Mapper
{
    /// <summary>
    /// Generic mapper interface for program entities and their DTOs
    /// Implement this interface for each unit (FTI, STU, ATIC, etc.)
    /// </summary>
    public interface IGenericProgramMapper<TProgram, TProgramDto, TProgramCreateDto, TProgramUpdateDto, TCompleteDto,
        TDemographics, TDemographicsDto, TDemographicsCreateDto, TDemographicsUpdateDto,
        TContent, TContentDto,
        TAdvisory, TAdvisoryDto, TAdvisoryCreateDto, TAdvisoryUpdateDto,
        TReport, TReportDto, TReportCreateDto, TReportUpdateDto,
        TRecommendation, TRecommendationDto, TRecommendationCreateDto, TRecommendationUpdateDto>
        where TProgram : ReportEntryBaseEntity
        where TDemographics : class
        where TContent : class
        where TAdvisory : class
        where TReport : class
        where TRecommendation : class
    {
        // Program Details Mappings
        TProgramDto MapToDto(TProgram entity);
        TProgram MapToEntity(TProgramCreateDto dto);
        void MapUpdateDtoToEntity(TProgramUpdateDto dto, TProgram entity);
        TCompleteDto MapToCompleteDto(TProgram entity);

        // Demographics Mappings
        TDemographicsDto MapToDto(TDemographics entity);
        TDemographics MapToEntity(TDemographicsCreateDto dto);
        void MapUpdateDtoToEntity(TDemographicsUpdateDto dto, TDemographics entity);

        // Content Mappings
        TContentDto MapToDto(TContent entity);

        // Advisory Services Mappings
        TAdvisoryDto MapToDto(TAdvisory entity);
        TAdvisory MapToEntity(TAdvisoryCreateDto dto);
        void MapUpdateDtoToEntity(TAdvisoryUpdateDto dto, TAdvisory entity);

        // Report Mappings
        TReportDto MapToDto(TReport entity);
        TReport MapToEntity(TReportCreateDto dto);
        void MapUpdateDtoToEntity(TReportUpdateDto dto, TReport entity);

        // Recommendation Mappings
        TRecommendationDto MapToDto(TRecommendation entity);
        TRecommendation MapToEntity(TRecommendationCreateDto dto);
        void MapUpdateDtoToEntity(TRecommendationUpdateDto dto, TRecommendation entity);
    }
}
