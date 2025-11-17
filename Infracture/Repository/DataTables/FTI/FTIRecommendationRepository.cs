// FtiRecommendationRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Application.Interface.Repository.DataTables.FTI;
using Domain.Entities.DEU;
using Domain.Entities.FTI;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.FTI
{
    public class FtiRecommendationRepository : IFtiRecommendationRepository
    {
        private readonly TdmsDbContext _context;

        public FtiRecommendationRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<FtiRecommendation?> GetByIdAsync(int id)
        {
            return await _context.FtiRecommendations.FindAsync(id);
        }

        public async Task<FtiRecommendation?> GetByProgramIdAsync(int programId)
        {
            return await _context.FtiRecommendations
                .FirstOrDefaultAsync(r => r.FtiProgramDetailsId == programId);
        }

        public async Task<FtiRecommendation> CreateAsync(FtiRecommendation entity)
        {
            _context.FtiRecommendations.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<FtiRecommendation> UpdateAsync(FtiRecommendation entity)
        {
            _context.FtiRecommendations.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.FtiRecommendations.FindAsync(id);
            if (entity != null)
            {
                _context.FtiRecommendations.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
