using Application.Models;
using Domain.Entities;
using Domain.Entities.Junction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface ITrainerAssignmentRepository
    {
        Task<TrainerAssignment?> GetByTrainerLocationAsync(int unitLocationId, int trainerId);
        Task<TrainerAssignment> AddAsync(TrainerAssignment unitLocationTrainer);
        Task<TrainerAssignment> UpdateAsync(TrainerAssignment unitLocationTrainer);
        Task DeleteAsync(int id);
        Task DeleteAsync(TrainerAssignment unitLocationTrainer);
        Task<bool> AssignmentExistsAsync(ExistingTrainerAssignmentDto trainerAssignment);
        Task<List<TrainerUnitWithLocationsDto>> GetAssignmentsDetailsByTrainerAsync(int trainerId);

        // New method for checking assignments before deletion
        Task<bool> HasAssignmentsForUnitLocationAsync(int unitLocationId);

        Task<List<TrainerAssignment>> GetByTrainerIdAsync(int trainerId);
    }
}
