// NaepRecommendationRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.NAEP
{
    public class NaepRecommendationRepository : INaepRecommendationRepository
    {
        private readonly TdmsDbContext _context;

        public NaepRecommendationRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<NaepRecommendation?> GetByIdAsync(int id)
        {
            return await _context.NaepRecommendations.FindAsync(id);
        }

        public async Task<NaepRecommendation?> GetByProgramIdAsync(int programId)
        {
            return await _context.NaepRecommendations
                .FirstOrDefaultAsync(r => r.NaepProgramDetailsId == programId);
        }

        public async Task<NaepRecommendation> CreateAsync(NaepRecommendation entity)
        {
            _context.NaepRecommendations.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<NaepRecommendation> UpdateAsync(NaepRecommendation entity)
        {
            _context.NaepRecommendations.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.NaepRecommendations.FindAsync(id);
            if (entity != null)
            {
                _context.NaepRecommendations.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}