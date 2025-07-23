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
                              select trainer).AnyAsync();
        }

        public Task DeleteAsync(int id)
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
            return await _context.UnitTrainers.FirstOrDefaultAsync(x => x.UnitLocationId == unitLocationId
            && x.TrainerId == trainerId);
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
                .Where(x => x.TrainerId == trainerId)
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
    }
}
