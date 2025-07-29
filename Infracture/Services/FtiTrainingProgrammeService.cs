using Application.Interface;
using Application.Interface.Repository.DataTables;
using Application.Models;
using Domain.Entities.Enum;
using Domain.Entities.FTI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class FtiTrainingProgrammeService : IFtiTrainingProgrammeService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IFtiTrainingProgrammeRepository _trainingProgrammeRepository;

        public FtiTrainingProgrammeService(ICurrentUserService currentUserService, IFtiTrainingProgrammeRepository trainingProgrammeRepository)
        {
            _currentUserService = currentUserService;
            _trainingProgrammeRepository = trainingProgrammeRepository;
        }
        public async Task<FtiTrainingProgram> AddAsync(CreateFtiTrainingProgrammeEntryDto createDto)
        {
            if(_currentUserService.Role != Role.TRAINER)throw new UnauthorizedAccessException();
            var entry = new FtiTrainingProgram
            {
                OrganisationName = createDto.OrganisationName,
                TrainingTitle = createDto.TrainingTitle,
                StartDate = createDto.StartDate,
                EndDate = createDto.EndDate,
                Duration = createDto.Duration,
                TrainingCount = createDto.TrainingCount,
                ParticipantCount = createDto.ParticipantCount,
                CreatedById = _currentUserService.UserId,
                CreatedAt = DateTimeOffset.UtcNow,
                OrganizationId = _currentUserService.OrganizationId
            };
            await _trainingProgrammeRepository.AddAsync(entry);
            return entry;
        }

        public async Task DeleteAsync(int id)
        {
            await _trainingProgrammeRepository.DeleteAsync(id);
        }

        public Task DeleteAsync(FtiTrainingProgram entity)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatedResult<FtiTrainingProgram>> GetPaginatedItemsAsync(int pageNumber, int pageSize = 10)
        {
            QueryFilter filter = new QueryFilter();
            if(_currentUserService.Role == Role.TRAINER)filter.Filters["CreatedById"] = _currentUserService.UserId;
            var items = await _trainingProgrammeRepository.GetPaginatedItemsAsync(_currentUserService.OrganizationId,pageNumber,filter,pageSize);
            return items;
        }

        public async Task UpdateAsync(UpdateTrainingProgrammeEntryDto updateDto)
        {
            var entry = await _trainingProgrammeRepository.GetItemAsync(updateDto.Id);
            if (entry != null) {
                if (updateDto.StartDate is not null) entry.StartDate = updateDto.StartDate.Value;
                if (updateDto.EndDate is not null) entry.EndDate = updateDto.EndDate.Value;
                if(updateDto.OrganisationName is not null)entry.OrganisationName = updateDto.OrganisationName;
                if (updateDto.Duration != null) entry.Duration = updateDto.Duration.Value;
                if(updateDto.TrainingCount is not null) entry.TrainingCount = updateDto.TrainingCount.Value;
                if (updateDto.ParticipantCount is not null) entry.ParticipantCount = updateDto.ParticipantCount.Value;
                await _trainingProgrammeRepository.UpdateAsync(entry);
            }
        }
    }
}
