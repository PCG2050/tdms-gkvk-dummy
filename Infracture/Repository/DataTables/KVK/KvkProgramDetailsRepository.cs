// KvkProgramDetailsRepository.cs
using Application.Interface.Repository.DataTables.KVK;
using Application.Models;
using Domain.Entities.KVK;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.KVK
{
    public class KvkProgramDetailsRepository : IKvkProgramDetailsRepository
    {
        private readonly TdmsDbContext _context;

        public KvkProgramDetailsRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public IQueryable<KvkProgramDetails> GetQueryable()
        {
            return _context.KvkProgramDetails.AsQueryable();
        }

        public async Task<List<KvkProgramDetails>> GetAllAsync()
        {
            return await _context.KvkProgramDetails
                .Include(p=>p.ProgramType)
                .Include(p => p.Status)
                .Include(p => p.ParticipantDemographics!)
                    .ThenInclude(pd => pd.Participant)
                .Include(p => p.ProgramContent!)
                    .ThenInclude(pc => pc.ResourcePersons!)
                .Include(p => p.ProgramContent!)
                    .ThenInclude(pc => pc.TopicsCovered!)
                .Include(p => p.ProgramContent!)
                    .ThenInclude(pc => pc.TeachingAids!)
                        .ThenInclude(ta => ta.TypeOfAid)
                .Include(p => p.ProgramContent!)
                     .ThenInclude(pc => pc.FieldVisits!)
                  .Include(p => p.ProgramContent!)
                     .ThenInclude(pc => pc.FieldDays!)
                 .Include(p => p.ProgramContent!)
                     .ThenInclude(pc => pc.FarmerScientistInteractions!)
                .Include(p => p.AdvisoryServices)
                .Include(p => p.Results)
                    .ThenInclude(r => r.FldResults!)
                        .ThenInclude(f => f.DetailsOfDemo)
                .Include(p => p.Results)
                    .ThenInclude(r => r.OftResults!)
                        .ThenInclude(o => o.DetailsOfDemo)
                .Include(p => p.Reports)
                .Include(p => p.Recommendations)
                .AsSplitQuery()
                .ToListAsync();
        }
        public async Task<KvkProgramDetails?> GetByIdAsync(int id)
        {
            return await _context.KvkProgramDetails
                .Include(p => p.ProgramType)
                .Include(p => p.Category)
                .Include(p => p.Type)
                .Include(p => p.Theme)
                .Include(p => p.ThematicArea)
                .Include(p => p.SourceOfFund)
                .Include(p => p.Region)
                .Include(p => p.Status)
                .Include(p => p.Source)
                .Include(p => p.Mode)
                .Include(p => p.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Include(p => p.UnitLocation)
                    .ThenInclude(ul => ul.District)
                        .ThenInclude(d => d.State)
                .Include(p => p.ApprovedBy)
                .Include(p => p.Collaborator)
                .Include(p => p.CollaborativeProgramOption)
                .Include(p => p.ParticipatedAs)
                .Include(p => p.EventNames)
                .Include(p => p.VAPOptions)
                .Include(p => p.TargetFarmers)
                .Include(p => p.ZoneOptions)
                .Include(p => p.Place)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<KvkProgramDetails?> GetWithDetailsAsync(int id)
        {
            return await _context.KvkProgramDetails
                .Include(p => p.ProgramType)
                .Include(p => p.Category)
                .Include(p => p.Type)
                .Include(p => p.Theme)
                .Include(p => p.ThematicArea)
                .Include(p => p.SourceOfFund)
                .Include(p => p.Region)
                .Include(p => p.Status)
                .Include(p => p.Source)
                .Include(p => p.Mode)
                .Include(p => p.Collaborator)
                .Include(p => p.CollaborativeProgramOption)
                .Include(p => p.ParticipatedAs)
                .Include(p => p.EventNames)
                .Include(p => p.VAPOptions)
                .Include(p => p.TargetFarmers)
                .Include(p => p.ZoneOptions)
                .Include(p => p.Place)
                .Include(p => p.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Include(p => p.ParticipantDemographics!)
                    .ThenInclude(pd => pd.Participant)
                .Include(p => p.ProgramContent!)
                    .ThenInclude(pc => pc.ResourcePersons!)
                .Include(p => p.ProgramContent!)
                    .ThenInclude(pc => pc.TopicsCovered!)
                .Include(p => p.ProgramContent!)
                    .ThenInclude(pc => pc.TeachingAids!)
                        .ThenInclude(ta => ta.TypeOfAid)
                .Include(p => p.ProgramContent!)
                        .ThenInclude(pc => pc.FieldVisits!)
                    .Include(p => p.ProgramContent!)
                        .ThenInclude(pc => pc.FieldDays!)
                    .Include(p => p.ProgramContent!)
                        .ThenInclude(pc => pc.FarmerScientistInteractions!)
                .Include(p => p.AdvisoryServices)
                .Include(p => p.Results)
                    .ThenInclude(r => r.FldResults!)
                        .ThenInclude(f => f.DetailsOfDemo)
                .Include(p => p.Results)
                    .ThenInclude(r => r.OftResults!)
                        .ThenInclude(o => o.DetailsOfDemo)
                .Include(p => p.Reports)
                .Include(p => p.Recommendations)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<KvkProgramDetails> CreateAsync(KvkProgramDetails entity)
        {
            _context.KvkProgramDetails.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<KvkProgramDetails> UpdateAsync(KvkProgramDetails entity)
        {
            _context.KvkProgramDetails.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.KvkProgramDetails.FindAsync(id);
            if (entity != null)
            {
                _context.KvkProgramDetails.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<KvkProgramDetails>> GetByCreatedByIdAsync(int createdById)
        {
            return await _context.KvkProgramDetails
                .Include(p => p.ProgramType)
                .Include(p => p.Category)
                .Include(p => p.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Where(p => p.CreatedById == createdById)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<KvkProgramDetails>> GetByUnitLocationIdsAsync(List<int> unitLocationIds)
        {
            return await _context.KvkProgramDetails
                .Include(p => p.ProgramType)
                .Include(p => p.Category)
                .Include(p => p.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Where(p => unitLocationIds.Contains(p.UnitLocationId))
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds)
        {
            return await _context.DeuProgramDetails
                .Where(p => unitLocationIds.Contains(p.UnitLocationId))
                .GroupBy(p => p.FormStatus)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Status, x => x.Count);
        }
    }

}
