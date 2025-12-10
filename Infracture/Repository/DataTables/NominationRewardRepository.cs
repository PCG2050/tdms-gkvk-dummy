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

        // ===== HYBRID UPDATE METHOD =====
        /// <summary>
        /// Updates parent and manages all children (create/update/delete) in one transaction
        /// Follows the same pattern as FinancialBudgetRepository.UpdateWithChildrenAsync
        /// </summary>
        public async Task<NominationReward> UpdateWithChildrenAsync(
            NominationReward parent,
            List<NominationRewardIFSFarmer>? ifsFarmers,
            List<NominationRewardFarmerInnovation>? farmerInnovations,
            List<NominationRewardOrganicFarmer>? organicFarmers,
            List<NominationRewardIFSEnterpreneur>? ifsEntrepreneurs,
            List<NominationRewardEntrepreneurInnovation>? entrepreneurInnovations,
            List<NominationRewardOrganicEntrepreneur>? organicEntrepreneurs)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var nominationRewardId = parent.Id;

                // Load existing entity with all children
                var existing = await GetWithDetailsAsync(nominationRewardId);
                if (existing == null)
                    throw new InvalidOperationException($"NominationReward with ID {nominationRewardId} not found");

                // 1. Update parent entity
                existing.StartDate = parent.StartDate;
                existing.EndDate = parent.EndDate;
                existing.TypeId = parent.TypeId;
                existing.RegionId = parent.RegionId;
                existing.ContributionId = parent.ContributionId;
                existing.ModeId = parent.ModeId;
                existing.NominationCategoryId = parent.NominationCategoryId;
                existing.OtherRegion = parent.OtherRegion;
                existing.AwardName = parent.AwardName;
                existing.OtherContribution = parent.OtherContribution;
                existing.AwardingAgency = parent.AwardingAgency;
                existing.SpecificContributionTitle = parent.SpecificContributionTitle;
                existing.OrganizerInstitutionName = parent.OrganizerInstitutionName;
                existing.OrganizerInstituteAddress = parent.OrganizerInstituteAddress;
                existing.AwardApplicationDate = parent.AwardApplicationDate;
                existing.AwardFilePath = parent.AwardFilePath;
                existing.AwardEventTitle = parent.AwardEventTitle;
                existing.AwardEventDate = parent.AwardEventDate;
                existing.SanctionLetterDate = parent.SanctionLetterDate;
                existing.SanctionLetterFilePath = parent.SanctionLetterFilePath;
                existing.PaperDate = parent.PaperDate;
                existing.PaperFilePath = parent.PaperFilePath;
                existing.AwardReceivingPhoto = parent.AwardReceivingPhoto;
                existing.AwardReceivingCertificate = parent.AwardReceivingCertificate;
                existing.InstitutionBoardName = parent.InstitutionBoardName;
                existing.InstitutionName = parent.InstitutionName;
                existing.InstitutionDesignation = parent.InstitutionDesignation;
                existing.InstitutionAddress = parent.InstitutionAddress;
                existing.PositionId = parent.PositionId;
                existing.PositionFrom = parent.PositionFrom;
                existing.PositionTo = parent.PositionTo;
                existing.DurationDays = parent.DurationDays;
                existing.NominationDate = parent.NominationDate;
                existing.NominationLetterPath = parent.NominationLetterPath;
                existing.FormStatus = parent.FormStatus;
                existing.UpdatedById = parent.UpdatedById;
                existing.UpdatedAt = parent.UpdatedAt;
                _context.NominationRewards.Update(existing);

                // 2. Process IFSFarmers (Hybrid Pattern: Create/Update/Delete)
                if (ifsFarmers != null)
                {
                    var existingFarmers = existing.NominationRewardIFSFarmers?.ToList() ?? new List<NominationRewardIFSFarmer>();
                    var incomingIds = ifsFarmers.Where(f => f.Id > 0).Select(f => f.Id).ToList();

                    // DELETE: Items in DB but not in incoming array
                    var farmersToDelete = existingFarmers.Where(f => !incomingIds.Contains(f.Id)).ToList();
                    foreach (var farmer in farmersToDelete)
                    {
                        _context.NominationRewardIFSFarmers.Remove(farmer);
                    }

                    // CREATE or UPDATE
                    foreach (var farmer in ifsFarmers)
                    {
                        if (farmer.Id > 0)
                        {
                            // UPDATE existing
                            var existingFarmer = existingFarmers.FirstOrDefault(f => f.Id == farmer.Id);
                            if (existingFarmer != null)
                            {
                                existingFarmer.NameAddress = farmer.NameAddress;
                                existingFarmer.Phone = farmer.Phone;
                                existingFarmer.ComponentOfIFS = farmer.ComponentOfIFS;
                                existingFarmer.UpdatedById = farmer.UpdatedById;
                                existingFarmer.UpdatedAt = farmer.UpdatedAt;
                                _context.NominationRewardIFSFarmers.Update(existingFarmer);
                            }
                        }
                        else
                        {
                            // CREATE new
                            farmer.NominationRewardId = nominationRewardId;
                            _context.NominationRewardIFSFarmers.Add(farmer);
                        }
                    }
                }

                // 3. Process FarmerInnovations (Hybrid Pattern)
                if (farmerInnovations != null)
                {
                    var existingInnovations = existing.NominationRewardFarmerInnovations?.ToList() ?? new List<NominationRewardFarmerInnovation>();
                    var incomingIds = farmerInnovations.Where(i => i.Id > 0).Select(i => i.Id).ToList();

                    // DELETE: Items in DB but not in incoming array
                    var innovationsToDelete = existingInnovations.Where(i => !incomingIds.Contains(i.Id)).ToList();
                    foreach (var innovation in innovationsToDelete)
                    {
                        _context.NominationRewardFarmerInnovations.Remove(innovation);
                    }

                    // CREATE or UPDATE
                    foreach (var innovation in farmerInnovations)
                    {
                        if (innovation.Id > 0)
                        {
                            // UPDATE existing
                            var existingInnovation = existingInnovations.FirstOrDefault(i => i.Id == innovation.Id);
                            if (existingInnovation != null)
                            {
                                existingInnovation.Type = innovation.Type;
                                existingInnovation.NameAddress = innovation.NameAddress;
                                existingInnovation.PhoneNumber = innovation.PhoneNumber;
                                existingInnovation.DetailsOfInnovation = innovation.DetailsOfInnovation;
                                existingInnovation.UpdatedById = innovation.UpdatedById;
                                existingInnovation.UpdatedAt = innovation.UpdatedAt;
                                _context.NominationRewardFarmerInnovations.Update(existingInnovation);
                            }
                        }
                        else
                        {
                            // CREATE new
                            innovation.NominationRewardId = nominationRewardId;
                            _context.NominationRewardFarmerInnovations.Add(innovation);
                        }
                    }
                }

                // 4. Process OrganicFarmers (Hybrid Pattern)
                if (organicFarmers != null)
                {
                    var existingOrganic = existing.NominationRewardOrganicFarmers?.ToList() ?? new List<NominationRewardOrganicFarmer>();
                    var incomingIds = organicFarmers.Where(o => o.Id > 0).Select(o => o.Id).ToList();

                    // DELETE: Items in DB but not in incoming array
                    var organicToDelete = existingOrganic.Where(o => !incomingIds.Contains(o.Id)).ToList();
                    foreach (var organic in organicToDelete)
                    {
                        _context.NominationRewardOrganicFarmers.Remove(organic);
                    }

                    // CREATE or UPDATE
                    foreach (var organic in organicFarmers)
                    {
                        if (organic.Id > 0)
                        {
                            // UPDATE existing
                            var existingOrganicFarmer = existingOrganic.FirstOrDefault(o => o.Id == organic.Id);
                            if (existingOrganicFarmer != null)
                            {
                                existingOrganicFarmer.NameAddress = organic.NameAddress;
                                existingOrganicFarmer.PhoneNumber = organic.PhoneNumber;
                                existingOrganicFarmer.CropsGrown = organic.CropsGrown;
                                existingOrganicFarmer.UpdatedById = organic.UpdatedById;
                                existingOrganicFarmer.UpdatedAt = organic.UpdatedAt;
                                _context.NominationRewardOrganicFarmers.Update(existingOrganicFarmer);
                            }
                        }
                        else
                        {
                            // CREATE new
                            organic.NominationRewardId = nominationRewardId;
                            _context.NominationRewardOrganicFarmers.Add(organic);
                        }
                    }
                }

                // 5. Process IFSEntrepreneurs (Hybrid Pattern)
                if (ifsEntrepreneurs != null)
                {
                    var existingEntrepreneurs = existing.NominationRewardIFSEntrepreneurs?.ToList() ?? new List<NominationRewardIFSEnterpreneur>();
                    var incomingIds = ifsEntrepreneurs.Where(e => e.Id > 0).Select(e => e.Id).ToList();

                    // DELETE: Items in DB but not in incoming array
                    var entrepreneursToDelete = existingEntrepreneurs.Where(e => !incomingIds.Contains(e.Id)).ToList();
                    foreach (var entrepreneur in entrepreneursToDelete)
                    {
                        _context.NominationRewardIFSEntrepreneurs.Remove(entrepreneur);
                    }

                    // CREATE or UPDATE
                    foreach (var entrepreneur in ifsEntrepreneurs)
                    {
                        if (entrepreneur.Id > 0)
                        {
                            // UPDATE existing
                            var existingEntrepreneur = existingEntrepreneurs.FirstOrDefault(e => e.Id == entrepreneur.Id);
                            if (existingEntrepreneur != null)
                            {
                                existingEntrepreneur.NameAddress = entrepreneur.NameAddress;
                                existingEntrepreneur.Phone = entrepreneur.Phone;
                                existingEntrepreneur.ComponentOfIFS = entrepreneur.ComponentOfIFS;
                                existingEntrepreneur.UpdatedById = entrepreneur.UpdatedById;
                                existingEntrepreneur.UpdatedAt = entrepreneur.UpdatedAt;
                                _context.NominationRewardIFSEntrepreneurs.Update(existingEntrepreneur);
                            }
                        }
                        else
                        {
                            // CREATE new
                            entrepreneur.NominationRewardId = nominationRewardId;
                            _context.NominationRewardIFSEntrepreneurs.Add(entrepreneur);
                        }
                    }
                }

                // 6. Process EntrepreneurInnovations (Hybrid Pattern)
                if (entrepreneurInnovations != null)
                {
                    var existingInnovations = existing.NominationRewardEntrepreneurInnovations?.ToList() ?? new List<NominationRewardEntrepreneurInnovation>();
                    var incomingIds = entrepreneurInnovations.Where(i => i.Id > 0).Select(i => i.Id).ToList();

                    // DELETE: Items in DB but not in incoming array
                    var innovationsToDelete = existingInnovations.Where(i => !incomingIds.Contains(i.Id)).ToList();
                    foreach (var innovation in innovationsToDelete)
                    {
                        _context.NominationRewardEntrepreneurInnovations.Remove(innovation);
                    }

                    // CREATE or UPDATE
                    foreach (var innovation in entrepreneurInnovations)
                    {
                        if (innovation.Id > 0)
                        {
                            // UPDATE existing
                            var existingInnovation = existingInnovations.FirstOrDefault(i => i.Id == innovation.Id);
                            if (existingInnovation != null)
                            {
                                existingInnovation.Type = innovation.Type;
                                existingInnovation.NameAddress = innovation.NameAddress;
                                existingInnovation.PhoneNumber = innovation.PhoneNumber;
                                existingInnovation.DetailsOfInnovation = innovation.DetailsOfInnovation;
                                existingInnovation.UpdatedById = innovation.UpdatedById;
                                existingInnovation.UpdatedAt = innovation.UpdatedAt;
                                _context.NominationRewardEntrepreneurInnovations.Update(existingInnovation);
                            }
                        }
                        else
                        {
                            // CREATE new
                            innovation.NominationRewardId = nominationRewardId;
                            _context.NominationRewardEntrepreneurInnovations.Add(innovation);
                        }
                    }
                }

                // 7. Process OrganicEntrepreneurs (Hybrid Pattern)
                if (organicEntrepreneurs != null)
                {
                    var existingOrganic = existing.NominationRewardOrganicEntrepreneurs?.ToList() ?? new List<NominationRewardOrganicEntrepreneur>();
                    var incomingIds = organicEntrepreneurs.Where(o => o.Id > 0).Select(o => o.Id).ToList();

                    // DELETE: Items in DB but not in incoming array
                    var organicToDelete = existingOrganic.Where(o => !incomingIds.Contains(o.Id)).ToList();
                    foreach (var organic in organicToDelete)
                    {
                        _context.NominationRewardOrganicEntrepreneurs.Remove(organic);
                    }

                    // CREATE or UPDATE
                    foreach (var organic in organicEntrepreneurs)
                    {
                        if (organic.Id > 0)
                        {
                            // UPDATE existing
                            var existingOrganicEntrepreneur = existingOrganic.FirstOrDefault(o => o.Id == organic.Id);
                            if (existingOrganicEntrepreneur != null)
                            {
                                existingOrganicEntrepreneur.NameAddress = organic.NameAddress;
                                existingOrganicEntrepreneur.PhoneNumber = organic.PhoneNumber;
                                existingOrganicEntrepreneur.CropsGrown = organic.CropsGrown;
                                existingOrganicEntrepreneur.UpdatedById = organic.UpdatedById;
                                existingOrganicEntrepreneur.UpdatedAt = organic.UpdatedAt;
                                _context.NominationRewardOrganicEntrepreneurs.Update(existingOrganicEntrepreneur);
                            }
                        }
                        else
                        {
                            // CREATE new
                            organic.NominationRewardId = nominationRewardId;
                            _context.NominationRewardOrganicEntrepreneurs.Add(organic);
                        }
                    }
                }

                // Save all changes
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // Return updated entity with all children
                return await GetWithDetailsAsync(nominationRewardId) ?? existing;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
