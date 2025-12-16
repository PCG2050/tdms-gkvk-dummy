// SametiRecommendationRepository.cs
using Application.Interface.Repository.DataTables.SAMETI;
using Domain.Entities.SAMETI;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.SAMETI
{
    public class SametiRecommendationRepository : ISametiRecommendationRepository
    {
        private readonly TdmsDbContext _context;

        public SametiRecommendationRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<SametiRecommendation?> GetByIdAsync(int id)
        {
            return await _context.SametiRecommendations.FindAsync(id);
        }

        public async Task<SametiRecommendation?> GetByProgramIdAsync(int programId)
        {
            return await _context.SametiRecommendations
                .FirstOrDefaultAsync(r => r.SametiProgramDetailsId == programId);
        }

        public async Task<SametiRecommendation> CreateAsync(SametiRecommendation entity)
        {
            _context.SametiRecommendations.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<SametiRecommendation> UpdateAsync(SametiRecommendation entity)
        {
            _context.SametiRecommendations.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.SametiRecommendations.FindAsync(id);
            if (entity != null)
            {
                _context.SametiRecommendations.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}