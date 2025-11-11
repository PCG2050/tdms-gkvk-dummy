using Application.Interface.Repository.DataTables;
using Application.Models;
using Domain.Entities.GenericTables;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.DataTables
{
    public class NominationRewardRepository : INominationRewardRepository
    {
        private readonly TdmsDbContext _context;

        public NominationRewardRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public IQueryable<NominationReward> GetQueryable()
        {
            return _context.NominationRewards.AsQueryable();
        }

        public async Task<NominationReward?> GetByIdAsync(int id)
        {
            return await _context.NominationRewards.FindAsync(id);
        }

        public async Task<NominationReward?> GetWithDetailsAsync(int id)
        {
            return await _context.NominationRewards
                .Include(n => n.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Include(n => n.Organization)
                .Include(n => n.Type)
                .Include(n => n.Region)
                .Include(n => n.Contribution)
                .Include(n => n.Mode)
                .Include(n => n.NominationCategory)
                .Include(n => n.Position)
                .Include(n => n.ApprovedBy)
                .Include(n => n.NominationRewardIFSFarmers)
                .Include(n => n.NominationRewardIFSEntrepreneurs)
                .Include(n => n.NominationRewardFarmerInnovations)
                .Include(n => n.NominationRewardEntrepreneurInnovations)
                .Include(n => n.NominationRewardOrganicFarmers)
                .Include(n => n.NominationRewardOrganicEntrepreneurs)
                .AsSplitQuery()
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<NominationReward> AddAsync(NominationReward entity)
        {
            _context.NominationRewards.Add(entity);
            await _context.SaveChangesAsync();
            return await GetWithDetailsAsync(entity.Id) ?? entity;
        }
        public async Task<NominationReward> UpdateAsync(NominationReward entity)
        {
            // Explicitly mark old children as deleted
            var existingIFSFarmers = _context.NominationRewardIFSFarmers
                .Where(x => x.NominationRewardId == entity.Id);
            _context.NominationRewardIFSFarmers.RemoveRange(existingIFSFarmers);

            var existingIFSEntrepreneurs = _context.NominationRewardIFSEntrepreneurs
                .Where(x => x.NominationRewardId == entity.Id);
            _context.NominationRewardIFSEntrepreneurs.RemoveRange(existingIFSEntrepreneurs);

            var existingFarmerInnovations = _context.NominationRewardFarmerInnovations
                .Where(x => x.NominationRewardId == entity.Id);
            _context.NominationRewardFarmerInnovations.RemoveRange(existingFarmerInnovations);

            var existingEntrepreneurInnovations = _context.NominationRewardEntrepreneurInnovations
                .Where(x => x.NominationRewardId == entity.Id);
            _context.NominationRewardEntrepreneurInnovations.RemoveRange(existingEntrepreneurInnovations);

            var existingOrganicFarmers = _context.NominationRewardOrganicFarmers
                .Where(x => x.NominationRewardId == entity.Id);
            _context.NominationRewardOrganicFarmers.RemoveRange(existingOrganicFarmers);

            var existingOrganicEntrepreneurs = _context.NominationRewardOrganicEntrepreneurs
                .Where(x => x.NominationRewardId == entity.Id);
            _context.NominationRewardOrganicEntrepreneurs.RemoveRange(existingOrganicEntrepreneurs);

            // Now save - will delete old and insert new
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var Nominationreward = await _context.NominationRewards.FindAsync(id);
            if (Nominationreward != null)
            {
                _context.NominationRewards.Remove(Nominationreward);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<NominationReward>> GetAllAsync()
        {
            return await _context.NominationRewards
                .Include(n => n.Type)
                .Include(n => n.NominationCategory)
                .Include(n => n.NominationRewardIFSFarmers)
                .Include(n => n.NominationRewardIFSEntrepreneurs)
                .Include(n => n.NominationRewardFarmerInnovations)
                .Include(n => n.NominationRewardEntrepreneurInnovations)
                .Include(n => n.NominationRewardOrganicFarmers)
                .Include(n => n.NominationRewardOrganicEntrepreneurs)
                .AsSplitQuery()                
                .ToListAsync();
        }

        public async Task<PaginatedResult<NominationReward>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? typeId = null,
            string? searchTerm = null)
        {
            var query = _context.NominationRewards
                .Where(n => unitLocationIds.Contains(n.UnitLocationId))
                .AsQueryable();

            if (startDate.HasValue)
                query = query.Where(n => n.StartDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(n => n.EndDate <= endDate.Value);

            if (typeId.HasValue)
                query = query.Where(n => n.TypeId == typeId.Value);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(n =>
                    (n.AwardName != null && n.AwardName.Contains(searchTerm)) ||
                    (n.SpecificContributionTitle != null && n.SpecificContributionTitle.Contains(searchTerm)));
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(n => n.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(n => n.NominationRewardIFSFarmers)
                .Include(n => n.NominationRewardIFSEntrepreneurs)
                .Include(n => n.NominationRewardFarmerInnovations)
                .Include(n => n.NominationRewardEntrepreneurInnovations)
                .Include(n => n.NominationRewardOrganicFarmers)
                .Include(n => n.NominationRewardOrganicEntrepreneurs)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();

            return new PaginatedResult<NominationReward>(items, totalCount, pageNumber, pageSize);
        }

        public async Task<PaginatedResult<NominationReward>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var query = _context.NominationRewards
                .Where(n => unitLocationIds.Contains(n.UnitLocationId) &&
                            n.FormStatus == status)
                            .AsQueryable();

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(n => n.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(n => n.NominationRewardIFSFarmers)
                .Include(n => n.NominationRewardIFSEntrepreneurs)
                .Include(n => n.NominationRewardFarmerInnovations)
                .Include(n => n.NominationRewardEntrepreneurInnovations)
                .Include(n => n.NominationRewardOrganicFarmers)
                .Include(n => n.NominationRewardOrganicEntrepreneurs)
                .AsSplitQuery()
                .ToListAsync();

            return new PaginatedResult<NominationReward>(
                items, 
                totalCount, 
                pageNumber, 
                pageSize);
        }

        public async Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds)
        {
            var summary = await _context.NominationRewards
                .Where(n => unitLocationIds.Contains(n.UnitLocationId))
                .GroupBy(n => n.FormStatus)
                .Select(g => new { FormStatus = g.Key, Count = g.Count() })
                .ToListAsync();
            return summary.ToDictionary(x => x.FormStatus, x => x.Count);
           
        }
    }
}
