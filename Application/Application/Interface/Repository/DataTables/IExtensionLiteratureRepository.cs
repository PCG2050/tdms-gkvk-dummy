using Domain.Entities.GenericTables;

namespace Application.Interface.Repository.DataTables
{
    public interface IExtensionLiteratureRepository
    {
        Task<ExtensionLiterature?> GetByIdAsync(int id);
        Task<List<ExtensionLiterature>> GetByPublicationIdAsync(int publicationId);
        Task<ExtensionLiterature> CreateAsync(ExtensionLiterature extensionLiterature);
        Task<ExtensionLiterature> UpdateAsync(ExtensionLiterature extensionLiterature);
        Task DeleteAsync(int id);
    }
}