using Application.Interface.Repository;
using Application.Models;
using Domain.Entities.Junction;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class TrainerAssignmentRepository : ITrainerAssignmentRepository
    {
        private readonly TdmsDbContext _context;

        public TrainerAssignmentRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<List<TrainerAssignment>> GetAllAsync()
        {
            return await _context.UnitTrainers
                .Where(x => !x.IsDeactivated)
                .ToListAsync();
        }

        public async Task<List<int>> GetUnitLocationIdsByTrainerIdAsync(int trainerId)
        {
            return await _context.UnitTrainers
                .Where(ta => ta.TrainerId == trainerId && !ta.IsDeactivated)
                .Select(ta => ta.UnitLocationId)
                .Distinct()
                .ToListAsync();
        }

        public async Task<bool> IsTrainerAssignedToLocationAsync(int trainerId, int unitLocationId)
        {
            return await _context.UnitTrainers
                .Where(a => !a.IsDeactivated)
                .AnyAsync(a => a.TrainerId == trainerId && a.UnitLocationId == unitLocationId);
        }

        public async Task<TrainerAssignment> AddAsync(TrainerAssignment unitLocationTrainer)
        {
            await _context.AddAsync(unitLocationTrainer);
            await _context.SaveChangesAsync();
            return unitLocationTrainer;
        }

        public async Task<bool> AssignmentExistsAsync(ExistingTrainerAssignmentDto trainerAssignment)
        {

            return await (from unitLocation in _context.OrganizationUnitLocations
                              join trainer in _context.UnitTrainers on unitLocation.Id equals trainer.UnitLocationId
                              where unitLocation.UnitId == trainerAssignment.UnitId
                              && unitLocation.DistrictId == trainerAssignment.DistrictId
                              && trainer.TrainerId == trainerAssignment.TrainerId
                              && !trainer.IsDeactivated
                              select trainer).AnyAsync();
        }

        public async Task<List<TrainerAssignment>> GetByTrainerIdAsync(int trainerId)
        {
            return await _context.UnitTrainers
                                    .Include(ut => ut.UnitLocation)
                                        .ThenInclude(ul => ul.Unit)
                                    .Include(ut => ut.UnitLocation)
                                        .ThenInclude(ul => ul.District)
                                            .ThenInclude(d => d.State)
                                    .Where(ut => ut.TrainerId == trainerId && !ut.IsDeactivated)
                                    .ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
          
        }

        public async Task DeleteAsync(TrainerAssignment unitLocationTrainer)
        {
            _context.UnitTrainers.Remove(unitLocationTrainer);
            await _context.SaveChangesAsync();
        }



        public async Task<TrainerAssignment?> GetByTrainerLocationAsync(int unitLocationId, int trainerId)
        {
            return await _context.UnitTrainers
                .Where(x => !x.IsDeactivated)
                .FirstOrDefaultAsync(x => x.UnitLocationId == unitLocationId && x.TrainerId == trainerId);
        }

        public Task<TrainerAssignment> UpdateAsync(TrainerAssignment unitLocationTrainer)
        {
            throw new NotImplementedException();
        }
           
        
        public async Task<List<TrainerUnitWithLocationsDto>> GetAssignmentsDetailsByTrainerAsync(int trainerId)
        {
           var trainerAssignments = await _context.UnitTrainers
                .Include(x => x.UnitLocation)
                    .ThenInclude(l => l.Unit)
                .Include(x => x.UnitLocation)
                    .ThenInclude(l => l.District)
                    .ThenInclude(d => d.State)
                .Where(x => x.TrainerId == trainerId && !x.IsDeactivated)
                .GroupBy(ut => new { ut.UnitLocation.Unit.Id, ut.UnitLocation.Unit.Name })
            .Select(g => new TrainerUnitWithLocationsDto
            {
                UnitId = g.Key.Id,
                UnitName = g.Key.Name,
                Locations = g.Select(ut => new UnitLocationDto
                {
                    UnitLocationId = ut.UnitLocation.Id,
                    DistrictId = ut.UnitLocation.District.Id,
                    DistrictName = ut.UnitLocation.District.Name,
                    StateId = ut.UnitLocation.District.State.Id,
                    StateName = ut.UnitLocation.District.State.Name
                }).ToList()
            }).ToListAsync();
            return trainerAssignments;
        }


        public async Task<bool> HasAssignmentsForUnitLocationAsync(int unitLocationId)
        {
            return await _context.UnitTrainers
                .Where(ta => !ta.IsDeactivated)
                .AnyAsync(ta => ta.UnitLocationId == unitLocationId);
        }

        // New methods to TrainerAssignmentRepository class       

        public async Task AddRangeAsync(IEnumerable<TrainerAssignment> assignments)
        {
            await _context.UnitTrainers.AddRangeAsync(assignments);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<TrainerAssignment> assignments)
        {
            _context.UnitTrainers.RemoveRange(assignments);
            await _context.SaveChangesAsync();
        }

        public async Task<List<int>> GetTrainerIdsByUnitLocationIdAsync(int unitLocationId)
        {
            return await _context.UnitTrainers
                .Where(ta => ta.UnitLocationId == unitLocationId && !ta.IsDeactivated)
                .Select(ta => ta.TrainerId)
                .Distinct()
                .ToListAsync();
        }

        public async Task<TrainerAssignment?> GetDeactivatedAssignmentAsync(int trainerId, int unitLocationId)
        {
            return await _context.UnitTrainers
                .FirstOrDefaultAsync(x => x.TrainerId == trainerId
                    && x.UnitLocationId == unitLocationId
                    && x.IsDeactivated);
        }

        public async Task<TrainerAssignment> SaveAsync(TrainerAssignment assignment)
        {
            _context.UnitTrainers.Update(assignment);
            await _context.SaveChangesAsync();
            return assignment;
        }
    }

}
