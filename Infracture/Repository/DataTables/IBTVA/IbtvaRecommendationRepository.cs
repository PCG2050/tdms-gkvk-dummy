// IbtvaRecommendationRepository.cs
using Application.Interface.Repository.DataTables.IBTVA;
using Domain.Entities.IBTVA;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.IBTVA
{
    public class IbtvaRecommendationRepository : IIbtvaRecommendationRepository
    {
        private readonly TdmsDbContext _context;

        public IbtvaRecommendationRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<IbtvaRecommendation?> GetByIdAsync(int id)
        {
            return await _context.IbtvaRecommendations.FindAsync(id);
        }

        public async Task<IbtvaRecommendation?> GetByProgramIdAsync(int programId)
        {
            return await _context.IbtvaRecommendations
                .FirstOrDefaultAsync(r => r.IbtvaProgramDetailsId == programId);
        }

        public async Task<IbtvaRecommendation> CreateAsync(IbtvaRecommendation entity)
        {
            _context.IbtvaRecommendations.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IbtvaRecommendation> UpdateAsync(IbtvaRecommendation entity)
        {
            _context.IbtvaRecommendations.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.IbtvaRecommendations.FindAsync(id);
            if (entity != null)
            {
                _context.IbtvaRecommendations.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}