// KvkRecommendationRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.NAEP
{
    public class KvkRecommendationRepository : IKvkRecommendationRepository
    {
        private readonly TdmsDbContext _context;

        public KvkRecommendationRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<KvkRecommendation?> GetByIdAsync(int id)
        {
            return await _context.KvkRecommendations.FindAsync(id);
        }

        public async Task<KvkRecommendation?> GetByProgramIdAsync(int programId)
        {
            return await _context.KvkRecommendations
                .FirstOrDefaultAsync(r => r.KvkProgramDetailsId == programId);
        }

        public async Task<KvkRecommendation> CreateAsync(KvkRecommendation entity)
        {
            _context.KvkRecommendations.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<KvkRecommendation> UpdateAsync(KvkRecommendation entity)
        {
            _context.KvkRecommendations.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.KvkRecommendations.FindAsync(id);
            if (entity != null)
            {
                _context.KvkRecommendations.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}