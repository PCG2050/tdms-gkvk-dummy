// AticRecommendationRepository.cs
using Application.Interface.Repository.DataTables.ATIC;
using Domain.Entities.ATIC;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.ATIC
{
    public class AticRecommendationRepository : IAticRecommendationRepository
    {
        private readonly TdmsDbContext _context;

        public AticRecommendationRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<AticRecommendation?> GetByIdAsync(int id)
        {
            return await _context.AticRecommendations.FindAsync(id);
        }

        public async Task<AticRecommendation?> GetByProgramIdAsync(int programId)
        {
            return await _context.AticRecommendations
                .FirstOrDefaultAsync(r => r.AticProgramDetailsId == programId);
        }

        public async Task<AticRecommendation> CreateAsync(AticRecommendation entity)
        {
            _context.AticRecommendations.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<AticRecommendation> UpdateAsync(AticRecommendation entity)
        {
            _context.AticRecommendations.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.AticRecommendations.FindAsync(id);
            if (entity != null)
            {
                _context.AticRecommendations.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}