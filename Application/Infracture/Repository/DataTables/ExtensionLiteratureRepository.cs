using Application.Interface.Repository.DataTables;
using Domain.Entities.GenericTables;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables
{
    public class ExtensionLiteratureRepository : IExtensionLiteratureRepository
    {
        private readonly TdmsDbContext _context;

        public ExtensionLiteratureRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<ExtensionLiterature?> GetByIdAsync(int id)
        {
            return await _context.ExtensionLiteratures.FindAsync(id);
        }

        public async Task<List<ExtensionLiterature>> GetByPublicationIdAsync(int publicationId)
        {
            return await _context.ExtensionLiteratures
                .Where(el => el.PublicationId == publicationId)
                .OrderByDescending(el => el.CreatedAt)
                .ToListAsync();
        }

        public async Task<ExtensionLiterature> CreateAsync(ExtensionLiterature extensionLiterature)
        {
            _context.ExtensionLiteratures.Add(extensionLiterature);
            await _context.SaveChangesAsync();
            return extensionLiterature;
        }

        public async Task<ExtensionLiterature> UpdateAsync(ExtensionLiterature extensionLiterature)
        {
            _context.ExtensionLiteratures.Update(extensionLiterature);
            await _context.SaveChangesAsync();
            return extensionLiterature;
        }

        public async Task DeleteAsync(int id)
        {
            var extensionLiterature = await _context.ExtensionLiteratures.FindAsync(id);
            if (extensionLiterature != null)
            {
                _context.ExtensionLiteratures.Remove(extensionLiterature);
                await _context.SaveChangesAsync();
            }
        }
    }
}
