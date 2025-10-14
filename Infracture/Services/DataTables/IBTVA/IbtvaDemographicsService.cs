using Application.Interface.Repository.DataTables;
using Application.Interface.Repository.DataTables.IBTVA;
using Application.Interface.Services.DataTables;
using Application.Interface.Services.DataTables.IBTVA;
using Application.Mappers;
using Application.Mappers.IBTVA;
using Application.Models.DataTables;
using Application.Models.DataTables.IBTVA;

namespace Infrastructure.Services.DataTables.IBTVA
{
    public class IbtvaDemographicsService : IIbtvaDemographicsService
    {
        private readonly IIbtvaDemographicsRepository _repository;
        private readonly IbtvaProgramMapper _mapper;
        private readonly IIbtvaProgramRepository _programRepository;

        public IbtvaDemographicsService(
            IIbtvaDemographicsRepository repository,
            IbtvaProgramMapper mapper,
            IIbtvaProgramRepository programRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _programRepository = programRepository;
        }

        public async Task<DemographicsDto> CreateAsync(int programId, DemographicsCreateDto dto, int trainerId)
        {
            var demographic = _mapper.MapToEntity(dto);
            demographic.IbtvaProgramDetailsId = programId;
            demographic.CreatedById = trainerId;
            demographic.CreatedAt = DateTimeOffset.UtcNow;

            var created = await _repository.CreateAsync(demographic);

            // Update program phase if needed
            var program = await _programRepository.GetByIdAsync(programId, trainerId);
            if (program != null && program.CurrentPhase < 2)
            {
                program.CurrentPhase = 2;
                await _programRepository.UpdateAsync(program);
            }

            return _mapper.MapToDto(created);
        }

        public async Task<DemographicsDto?> GetByIdAsync(int id, int programId)
        {
            var demographic = await _repository.GetByIdAsync(id, programId);
            return demographic == null ? null : _mapper.MapToDto(demographic);
        }

        public async Task<IEnumerable<DemographicsDto>> GetAllByProgramAsync(int programId)
        {
            var demographics = await _repository.GetAllByProgramAsync(programId);
            return demographics.Select(_mapper.MapToDto);
        }

        public async Task<DemographicsDto?> UpdateAsync(int id, int programId, DemographicsCreateDto dto, int trainerId)
        {
            var existing = await _repository.GetByIdAsync(id, programId);
            if (existing == null) return null;

            var updated = _mapper.MapToEntity(dto);
            updated.Id = id;
            updated.IbtvaProgramDetailsId = programId;
            updated.CreatedById = existing.CreatedById;
            updated.CreatedAt = existing.CreatedAt;
            updated.UpdatedById = trainerId;

            var result = await _repository.UpdateAsync(updated);
            return result == null ? null : _mapper.MapToDto(result);
        }

        public async Task<bool> DeleteAsync(int id, int programId)
        {
            return await _repository.DeleteAsync(id, programId);
        }
    }
}