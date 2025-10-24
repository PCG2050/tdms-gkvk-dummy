using Application.Interface.Repository.DataTables.FIU;
using Application.Interface.Services.DataTables.FIU;
using Application.Mapper.DataTable.FIU;
using Application.Models.DataTables.FIU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.DataTables.FIU
{
    public class FIUProgramActivityService : IFIUProgramActivityService
    {
        private readonly IFIUProgramActivityRepository _repository;
        private readonly FIUProgramActivityMapper _mapper;

        public FIUProgramActivityService(IFIUProgramActivityRepository repository, FIUProgramActivityMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FIUProgramActivityDto> AddAsync(FIUProgramActivityDto dto)
        {
            var entity = _mapper.MapToEntity(dto);
            var result = await _repository.AddAsync(entity);
            return _mapper.MapToDto(result);
        }

        public async Task<IEnumerable<FIUProgramActivityDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(_mapper.MapToDto);
        }

        public async Task<FIUProgramActivityDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.MapToDto(entity);
        }
    }
}
