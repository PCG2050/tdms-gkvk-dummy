using Domain.Entities.GenericTables;

namespace Application.Interface.Repository.DataTables
{
    public interface IPublisherDetailsRepository
    {
        Task<PublisherDetails?> GetByIdAsync(int id);
        Task<PublisherDetails?> GetByPublicationIdAsync(int publicationId);
        Task<PublisherDetails> CreateAsync(PublisherDetails publisherDetails);
        Task<PublisherDetails> UpdateAsync(PublisherDetails publisherDetails);
        Task DeleteAsync(int id);
    }
}