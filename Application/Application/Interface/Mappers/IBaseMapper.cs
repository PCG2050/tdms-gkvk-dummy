using Domain.Entities;

namespace Application.Interface.Mappers
{
    /// <summary>
    /// Generic mapper interface for entity-DTO conversions.
    /// Implement this interface in your Mapperly mapper classes.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TDto">Read DTO type</typeparam>
    /// <typeparam name="TCreateDto">Create DTO type</typeparam>
    /// <typeparam name="TUpdateDto">Update DTO type</typeparam>
    public interface IBaseMapper<TEntity, TDto, TCreateDto, TUpdateDto>
        where TEntity : ReportEntryBaseEntity
        where TDto : IBaseDto
        where TCreateDto : ICreateDto
        where TUpdateDto : IUpdateDto
    {
        /// <summary>
        /// Maps a CreateDto to an Entity
        /// </summary>
        TEntity MapToEntity(TCreateDto dto);

        /// <summary>
        /// Maps an Entity to a read DTO (with navigation properties populated)
        /// </summary>
        TDto MapToDtoWithDetails(TEntity entity);

        /// <summary>
        /// Maps an UpdateDto to an existing Entity (partial update)
        /// </summary>
        void MapUpdateDtoToEntity(TUpdateDto dto, TEntity entity);
    }
}
