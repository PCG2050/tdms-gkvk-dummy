// DeuRecommendationRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.DEU
{
    public class DeuRecommendationRepository : IDeuRecommendationRepository
    {
        private readonly TdmsDbContext _context;

        public DeuRecommendationRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<DeuRecommendation?> GetByIdAsync(int id)
        {
            return await _context.DeuRecommendations.FindAsync(id);
        }

        public async Task<DeuRecommendation?> GetByProgramIdAsync(int programId)
        {
            return await _context.DeuRecommendations
                .FirstOrDefaultAsync(r => r.DeuProgramDetailsId == programId);
        }

        public async Task<DeuRecommendation> CreateAsync(DeuRecommendation entity)
        {
            _context.DeuRecommendations.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<DeuRecommendation> UpdateAsync(DeuRecommendation entity)
        {
            _context.DeuRecommendations.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.DeuRecommendations.FindAsync(id);
            if (entity != null)
            {
                _context.DeuRecommendations.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}