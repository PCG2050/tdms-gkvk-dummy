using Application.Models;
using Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface ITrainerAssignmentRepository
    {
        Task<List<TrainerAssignment>> GetAllAsync();
        Task<List<int>> GetUnitLocationIdsByTrainerIdAsync(int trainerId);

        Task<bool> IsTrainerAssignedToLocationAsync(int trainerID, int unitLocaitonId);
        Task<TrainerAssignment?> GetByTrainerLocationAsync(int unitLocationId, int trainerId);
        Task<TrainerAssignment> AddAsync(TrainerAssignment unitLocationTrainer);

        Task<TrainerAssignment> UpdateAsync(TrainerAssignment unitLocationTrainer);
        Task DeleteAsync(int id);
        Task DeleteAsync(TrainerAssignment unitLocationTrainer);
        Task<bool> AssignmentExistsAsync(ExistingTrainerAssignmentDto trainerAssignment);
        Task<List<TrainerUnitWithLocationsDto>> GetAssignmentsDetailsByTrainerAsync(int trainerId);

        // New method for checking assignments before deletion
        Task<bool> HasAssignmentsForUnitLocationAsync(int unitLocationId);

        //new ones
        Task<List<TrainerAssignment>> GetByTrainerIdAsync(int trainerId);
        Task AddRangeAsync(IEnumerable<TrainerAssignment> assignments);
        Task DeleteRangeAsync(IEnumerable<TrainerAssignment> assignments);

        /// <summary>
        /// Get all trainers assigned to a specific unit location
        /// </summary>
        Task<List<int>> GetTrainerIdsByUnitLocationIdAsync(int unitLocationId);
    }
}
