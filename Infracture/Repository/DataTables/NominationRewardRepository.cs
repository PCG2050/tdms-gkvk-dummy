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
                // Original 6 child collections
                .Include(n => n.NominationRewardIFSFarmers)
                .Include(n => n.NominationRewardIFSEntrepreneurs)
                .Include(n => n.NominationRewardFarmerInnovations)
                .Include(n => n.NominationRewardEntrepreneurInnovations)
                .Include(n => n.NominationRewardOrganicFarmers)
                .Include(n => n.NominationRewardOrganicEntrepreneurs)
                // NEW 4 child collections
                .Include(n => n.Achievements)
                .Include(n => n.AwardRecognitions)
                    .ThenInclude(a => a.Contribution)
                .Include(n => n.UniversitySanctionLetterPaperPosters)
                .Include(n => n.AwardPhotos)
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
            // Explicitly mark old children as deleted (all 10 child collections)
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

            var existingAchievements = _context.Achievements
                .Where(x => x.NominationRewardId == entity.Id);
            _context.Achievements.RemoveRange(existingAchievements);

            var existingAwardRecognitions = _context.AwardRecognitions
                .Where(x => x.NominationRewardId == entity.Id);
            _context.AwardRecognitions.RemoveRange(existingAwardRecognitions);

            var existingUniversitySanctionLetterPaperPosters = _context.UniversitySanctionLetterPaperPosters
                .Where(x => x.NominationRewardId == entity.Id);
            _context.UniversitySanctionLetterPaperPosters.RemoveRange(existingUniversitySanctionLetterPaperPosters);

            var existingAwardPhotos = _context.AwardPhotos
                .Where(x => x.NominationRewardId == entity.Id);
            _context.AwardPhotos.RemoveRange(existingAwardPhotos);

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
                .Include(n => n.Region)
                // Original 6 child collections
                .Include(n => n.NominationRewardIFSFarmers)
                .Include(n => n.NominationRewardIFSEntrepreneurs)
                .Include(n => n.NominationRewardFarmerInnovations)
                .Include(n => n.NominationRewardEntrepreneurInnovations)
                .Include(n => n.NominationRewardOrganicFarmers)
                .Include(n => n.NominationRewardOrganicEntrepreneurs)
                // NEW 4 child collections
                .Include(n => n.Achievements)
                .Include(n => n.AwardRecognitions)
                .Include(n => n.UniversitySanctionLetterPaperPosters)
                .Include(n => n.AwardPhotos)
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

            // Note: searchTerm filter removed as AwardName and SpecificContributionTitle no longer exist in new model
            // If search is needed, add filter based on new fields (e.g., OtherType, OtherRegion)

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(n => n.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                // Original 6 child collections
                .Include(n => n.NominationRewardIFSFarmers)
                .Include(n => n.NominationRewardIFSEntrepreneurs)
                .Include(n => n.NominationRewardFarmerInnovations)
                .Include(n => n.NominationRewardEntrepreneurInnovations)
                .Include(n => n.NominationRewardOrganicFarmers)
                .Include(n => n.NominationRewardOrganicEntrepreneurs)
                // NEW 4 child collections
                .Include(n => n.Achievements)
                .Include(n => n.AwardRecognitions)
                .Include(n => n.UniversitySanctionLetterPaperPosters)
                .Include(n => n.AwardPhotos)
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
                // Original 6 child collections
                .Include(n => n.NominationRewardIFSFarmers)
                .Include(n => n.NominationRewardIFSEntrepreneurs)
                .Include(n => n.NominationRewardFarmerInnovations)
                .Include(n => n.NominationRewardEntrepreneurInnovations)
                .Include(n => n.NominationRewardOrganicFarmers)
                .Include(n => n.NominationRewardOrganicEntrepreneurs)
                // NEW 4 child collections
                .Include(n => n.Achievements)
                .Include(n => n.AwardRecognitions)
                .Include(n => n.UniversitySanctionLetterPaperPosters)
                .Include(n => n.AwardPhotos)
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
        /// Updates parent and manages all 10 children (create/update/delete) in one transaction
        /// Follows the same pattern as FinancialBudgetRepository.UpdateWithChildrenAsync
        /// </summary>
        public async Task<NominationReward> UpdateWithChildrenAsync(
            NominationReward parent,
            List<NominationRewardIFSFarmer>? ifsFarmers,
            List<NominationRewardFarmerInnovation>? farmerInnovations,
            List<NominationRewardOrganicFarmer>? organicFarmers,
            List<NominationRewardIFSEnterpreneur>? ifsEntrepreneurs,
            List<NominationRewardEntrepreneurInnovation>? entrepreneurInnovations,
            List<NominationRewardOrganicEntrepreneur>? organicEntrepreneurs,
            List<Achievement>? achievements,
            List<AwardRecognition>? awardRecognitions,
            List<UniversitySanctionLetterPaperPoster>? universitySanctionLetterPaperPosters,
            List<AwardPhoto>? awardPhotos)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var nominationRewardId = parent.Id;

                // Load existing entity with all children
                var existing = await GetWithDetailsAsync(nominationRewardId);
                if (existing == null)
                    throw new InvalidOperationException($"NominationReward with ID {nominationRewardId} not found");

                // 1. Update parent entity - only fields from new simplified model
                existing.StartDate = parent.StartDate;
                existing.EndDate = parent.EndDate;
                existing.TypeId = parent.TypeId;
                existing.RegionId = parent.RegionId;
                existing.OtherType = parent.OtherType;
                existing.OtherRegion = parent.OtherRegion;
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

                // 8. Process Achievements (Hybrid Pattern) - NEW
                if (achievements != null)
                {
                    var existingAchievements = existing.Achievements?.ToList() ?? new List<Achievement>();
                    var incomingIds = achievements.Where(a => a.Id > 0).Select(a => a.Id).ToList();

                    // DELETE: Items in DB but not in incoming array
                    var achievementsToDelete = existingAchievements.Where(a => !incomingIds.Contains(a.Id)).ToList();
                    foreach (var achievement in achievementsToDelete)
                    {
                        _context.Achievements.Remove(achievement);
                    }

                    // CREATE or UPDATE
                    foreach (var achievement in achievements)
                    {
                        if (achievement.Id > 0)
                        {
                            // UPDATE existing
                            var existingAchievement = existingAchievements.FirstOrDefault(a => a.Id == achievement.Id);
                            if (existingAchievement != null)
                            {
                                existingAchievement.Name = achievement.Name;
                                existingAchievement.Phone = achievement.Phone;
                                existingAchievement.AchievementDetail = achievement.AchievementDetail;
                                existingAchievement.UpdatedById = achievement.UpdatedById;
                                existingAchievement.UpdatedAt = achievement.UpdatedAt;
                                _context.Achievements.Update(existingAchievement);
                            }
                        }
                        else
                        {
                            // CREATE new
                            achievement.NominationRewardId = nominationRewardId;
                            _context.Achievements.Add(achievement);
                        }
                    }
                }

                // 9. Process AwardRecognitions (Hybrid Pattern) - NEW
                if (awardRecognitions != null)
                {
                    var existingAwards = existing.AwardRecognitions?.ToList() ?? new List<AwardRecognition>();
                    var incomingIds = awardRecognitions.Where(a => a.Id > 0).Select(a => a.Id).ToList();

                    // DELETE: Items in DB but not in incoming array
                    var awardsToDelete = existingAwards.Where(a => !incomingIds.Contains(a.Id)).ToList();
                    foreach (var award in awardsToDelete)
                    {
                        _context.AwardRecognitions.Remove(award);
                    }

                    // CREATE or UPDATE
                    foreach (var award in awardRecognitions)
                    {
                        if (award.Id > 0)
                        {
                            // UPDATE existing
                            var existingAward = existingAwards.FirstOrDefault(a => a.Id == award.Id);
                            if (existingAward != null)
                            {
                                existingAward.AwardName = award.AwardName;
                                existingAward.ContributionId = award.ContributionId;
                                existingAward.OtherContribution = award.OtherContribution;
                                existingAward.AwardingAgency = award.AwardingAgency;
                                existingAward.InstitutionName = award.InstitutionName;
                                existingAward.InstitutionAddress = award.InstitutionAddress;
                                existingAward.UpdatedById = award.UpdatedById;
                                existingAward.UpdatedAt = award.UpdatedAt;
                                _context.AwardRecognitions.Update(existingAward);
                            }
                        }
                        else
                        {
                            // CREATE new
                            award.NominationRewardId = nominationRewardId;
                            _context.AwardRecognitions.Add(award);
                        }
                    }
                }

                // 10. Process UniversitySanctionLetterPaperPosters (Hybrid Pattern) - NEW
                if (universitySanctionLetterPaperPosters != null)
                {
                    var existingPosters = existing.UniversitySanctionLetterPaperPosters?.ToList() ?? new List<UniversitySanctionLetterPaperPoster>();
                    var incomingIds = universitySanctionLetterPaperPosters.Where(p => p.Id > 0).Select(p => p.Id).ToList();

                    // DELETE: Items in DB but not in incoming array
                    var postersToDelete = existingPosters.Where(p => !incomingIds.Contains(p.Id)).ToList();
                    foreach (var poster in postersToDelete)
                    {
                        _context.UniversitySanctionLetterPaperPosters.Remove(poster);
                    }

                    // CREATE or UPDATE
                    foreach (var poster in universitySanctionLetterPaperPosters)
                    {
                        if (poster.Id > 0)
                        {
                            // UPDATE existing
                            var existingPoster = existingPosters.FirstOrDefault(p => p.Id == poster.Id);
                            if (existingPoster != null)
                            {
                                existingPoster.SanctionLetterDate = poster.SanctionLetterDate;
                                existingPoster.SanctionLetterFilePath = poster.SanctionLetterFilePath;
                                existingPoster.PaperDate = poster.PaperDate;
                                existingPoster.PaperFilePath = poster.PaperFilePath;
                                existingPoster.UpdatedById = poster.UpdatedById;
                                existingPoster.UpdatedAt = poster.UpdatedAt;
                                _context.UniversitySanctionLetterPaperPosters.Update(existingPoster);
                            }
                        }
                        else
                        {
                            // CREATE new
                            poster.NominationRewardId = nominationRewardId;
                            _context.UniversitySanctionLetterPaperPosters.Add(poster);
                        }
                    }
                }

                // 11. Process AwardPhotos (Hybrid Pattern) - NEW
                if (awardPhotos != null)
                {
                    var existingPhotos = existing.AwardPhotos?.ToList() ?? new List<AwardPhoto>();
                    var incomingIds = awardPhotos.Where(p => p.Id > 0).Select(p => p.Id).ToList();

                    // DELETE: Items in DB but not in incoming array
                    var photosToDelete = existingPhotos.Where(p => !incomingIds.Contains(p.Id)).ToList();
                    foreach (var photo in photosToDelete)
                    {
                        _context.AwardPhotos.Remove(photo);
                    }

                    // CREATE or UPDATE
                    foreach (var photo in awardPhotos)
                    {
                        if (photo.Id > 0)
                        {
                            // UPDATE existing
                            var existingPhoto = existingPhotos.FirstOrDefault(p => p.Id == photo.Id);
                            if (existingPhoto != null)
                            {
                                existingPhoto.AwardReceivingPhoto = photo.AwardReceivingPhoto;
                                existingPhoto.AwardReceivingCertificate = photo.AwardReceivingCertificate;
                                existingPhoto.UpdatedById = photo.UpdatedById;
                                existingPhoto.UpdatedAt = photo.UpdatedAt;
                                _context.AwardPhotos.Update(existingPhoto);
                            }
                        }
                        else
                        {
                            // CREATE new
                            photo.NominationRewardId = nominationRewardId;
                            _context.AwardPhotos.Add(photo);
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
