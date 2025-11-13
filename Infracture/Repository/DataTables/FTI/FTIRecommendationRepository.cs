// FTIRecommendationRepository.cs
using Application.Interface.Repository.DataTables.FTI;
using Domain.Entities.FTI;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.FTI
{
    public class FTIRecommendationRepository : IFTIRecommendationRepository
    {
        private readonly TdmsDbContext _context;

        public FTIRecommendationRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<FTIRecommendation?> GetByIdAsync(int id)
        {
            return await _context.FTIRecommendations.FindAsync(id);
        }

        public async Task<FTIRecommendation?> GetByProgramIdAsync(int programId)
        {
            return await _context.FTIRecommendations
                .FirstOrDefaultAsync(r => r.FTIProgramDetailsId == programId);
        }

        public async Task<FTIRecommendation> CreateAsync(FTIRecommendation entity)
        {
            _context.FTIRecommendations.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<FTIRecommendation> UpdateAsync(FTIRecommendation entity)
        {
            _context.FTIRecommendations.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.FTIRecommendations.FindAsync(id);
            if (entity != null)
            {
                _context.FTIRecommendations.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
