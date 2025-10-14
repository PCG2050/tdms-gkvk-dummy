using Application.Interface.Repository.DataTables;
using Domain.Entities.GenericTables;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables
{
    public class PublisherDetailsRepository : IPublisherDetailsRepository
    {
        private readonly TdmsDbContext _context;

        public PublisherDetailsRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<PublisherDetails?> GetByIdAsync(int id)
        {
            return await _context.PublisherDetails.FindAsync(id);
        }

        public async Task<PublisherDetails?> GetByPublicationIdAsync(int publicationId)
        {
            return await _context.PublisherDetails
                .FirstOrDefaultAsync(pd => pd.PublicationId == publicationId);
        }

        public async Task<PublisherDetails> CreateAsync(PublisherDetails publisherDetails)
        {
            _context.PublisherDetails.Add(publisherDetails);
            await _context.SaveChangesAsync();
            return publisherDetails;
        }

        public async Task<PublisherDetails> UpdateAsync(PublisherDetails publisherDetails)
        {
            _context.PublisherDetails.Update(publisherDetails);
            await _context.SaveChangesAsync();
            return publisherDetails;
        }

        public async Task DeleteAsync(int id)
        {
            var publisherDetails = await _context.PublisherDetails.FindAsync(id);
            if (publisherDetails != null)
            {
                _context.PublisherDetails.Remove(publisherDetails);
                await _context.SaveChangesAsync();
            }
        }
    }
}
