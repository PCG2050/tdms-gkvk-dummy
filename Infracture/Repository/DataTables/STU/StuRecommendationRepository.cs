// StuRecommendationRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.STU
{
    public class StuRecommendationRepository : IStuRecommendationRepository
    {
        private readonly TdmsDbContext _context;

        public StuRecommendationRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<StuRecommendation?> GetByIdAsync(int id)
        {
            return await _context.StuRecommendations.FindAsync(id);
        }

        public async Task<StuRecommendation?> GetByProgramIdAsync(int programId)
        {
            return await _context.StuRecommendations
                .FirstOrDefaultAsync(r => r.StuProgramDetailsId == programId);
        }

        public async Task<StuRecommendation> CreateAsync(StuRecommendation entity)
        {
            _context.StuRecommendations.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<StuRecommendation> UpdateAsync(StuRecommendation entity)
        {
            _context.StuRecommendations.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.StuRecommendations.FindAsync(id);
            if (entity != null)
            {
                _context.StuRecommendations.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}