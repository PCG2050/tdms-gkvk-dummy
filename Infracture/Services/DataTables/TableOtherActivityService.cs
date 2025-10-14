using Application.Interface.Repository.DataTables;
using Application.Interface.Services.DataTables;
using Application.Mapper;
using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.DataTables
{
    public class TableOtherActivityService : ITableOtherActivityService
    {
        private readonly ITableOtherActivityRepository _repo;
        private readonly TableOtherActivityMapper _mapper;

        public TableOtherActivityService(ITableOtherActivityRepository repo, TableOtherActivityMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<TableOtherActivityDto> CreateAsync(TableOtherActivityCreateDto dto)
        {
            var entity = _mapper.MapToEntity(dto);
            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
            return _mapper.MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;

            await _repo.DeleteAsync(entity);
            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<List<TableOtherActivityDto>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return entities.ConvertAll(e => _mapper.MapToDto(e));
        }

        public async Task<TableOtherActivityDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : _mapper.MapToDto(entity);
        }

        public async Task<bool> UpdateAsync(int id, TableOtherActivityUpdateDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;

            _mapper.MapToExistingEntity(dto, entity);
            await _repo.UpdateAsync(entity);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}
