// EeuRecommendationRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.EEU
{
    public class EeuRecommendationRepository : IEeuRecommendationRepository
    {
        private readonly TdmsDbContext _context;

        public EeuRecommendationRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<EeuRecommendation?> GetByIdAsync(int id)
        {
            return await _context.EeuRecommendations.FindAsync(id);
        }

        public async Task<EeuRecommendation?> GetByProgramIdAsync(int programId)
        {
            return await _context.EeuRecommendations
                .FirstOrDefaultAsync(r => r.EeuProgramDetailsId == programId);
        }

        public async Task<EeuRecommendation> CreateAsync(EeuRecommendation entity)
        {
            _context.EeuRecommendations.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<EeuRecommendation> UpdateAsync(EeuRecommendation entity)
        {
            _context.EeuRecommendations.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.EeuRecommendations.FindAsync(id);
            if (entity != null)
            {
                _context.EeuRecommendations.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}