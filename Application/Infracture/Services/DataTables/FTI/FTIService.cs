using Application.Interface.Repository.DataTables.FTI;
using Application.Interface.Services.DataTables.FTI;
using Application.Mapper.DataTable.FTI;
using Application.Models.DataTables.FTI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.DataTables.FTI
{
    public class FTIService : IFTIService
    {
        private readonly IFTIRepository _repository;
        private readonly FTIMapper _mapper;

        public FTIService(IFTIRepository repository, FTIMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FTIProgramDetailsDto>> GetAllProgramsAsync()
        {
            var entities = await _repository.GetAllProgramsAsync();
            return entities.ConvertAll(e => _mapper.MapToDto(e));
        }

        public async Task<FTIProgramDetailsDto?> GetProgramByIdAsync(int id)
        {
            var entity = await _repository.GetProgramByIdAsync(id);
            return entity == null ? null : _mapper.MapToDto(entity);
        }

        public async Task<FTIProgramDetailsDto> AddProgramAsync(FTIProgramDetailsDto dto)
        {
            var entity = _mapper.MapToEntity(dto);
            var added = await _repository.AddProgramAsync(entity);
            return _mapper.MapToDto(added);
        }

        public async Task<FTIProgramDetailsDto> UpdateProgramAsync(FTIProgramDetailsDto dto)
        {
            var entity = _mapper.MapToEntity(dto);
            var updated = await _repository.UpdateProgramAsync(entity);
            return _mapper.MapToDto(updated);
        }

        public async Task<bool> DeleteProgramAsync(int id)
        {
            return await _repository.DeleteProgramAsync(id);
        }
    }
}
