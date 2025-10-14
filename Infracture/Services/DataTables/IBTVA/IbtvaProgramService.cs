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
    public class IbtvaProgramService : IIbtvaProgramService
    {
        private readonly IIbtvaProgramRepository _repository;
        private readonly IbtvaProgramMapper _mapper;

        public IbtvaProgramService(
            IIbtvaProgramRepository repository,
            IbtvaProgramMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IbtvaProgramDetailsDto> CreateAsync(
            IbtvaProgramCreateDto dto,
            int trainerId,
            int organizationId)
        {
            var program = _mapper.CreateProgramEntity(dto, trainerId, organizationId);
            var created = await _repository.CreateAsync(program);
            return _mapper.MapToDto(created);
        }

        public async Task<IbtvaProgramDetailsDto?> GetByIdAsync(int id, int trainerId)
        {
            var program = await _repository.GetByIdAsync(id, trainerId);
            return program == null ? null : _mapper.MapToDto(program);
        }

        public async Task<IEnumerable<IbtvaProgramDetailsDto>> GetAllAsync(int trainerId)
        {
            var programs = await _repository.GetAllAsync(trainerId);
            return programs.Select(_mapper.MapToDto);
        }

        public async Task<IbtvaProgramDetailsDto?> UpdateAsync(
            int id,
            IbtvaProgramUpdateDto dto,
            int trainerId)
        {
            var existing = await _repository.GetByIdAsync(id, trainerId);
            if (existing == null)
                return null;

            _mapper.UpdateProgramEntity(dto, existing, trainerId);
            var updated = await _repository.UpdateAsync(existing);
            return updated == null ? null : _mapper.MapToDto(updated);
        }

        public async Task<bool> DeleteAsync(int id, int trainerId)
        {
            return await _repository.DeleteAsync(id, trainerId);
        }

        public async Task<bool> SubmitAsync(int id, int trainerId)
        {
            return await _repository.SubmitAsync(id, trainerId);
        }
    }
}