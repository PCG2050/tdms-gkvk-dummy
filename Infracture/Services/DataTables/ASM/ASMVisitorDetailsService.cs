using Application.Interface.Repository.DataTables.ASM;
using Application.Interface.Services.DataTables.ASM;
using Application.Mapper.DataTable.ASM;
using Application.Models.DataTables.ASM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.DataTables.ASM
{
    public class ASMVisitorDetailsService : IASMVisitorDetailsService
    {
        private readonly IASMVisitorDetailsRepository _repository;
        private readonly ASMVisitorDetailsMapper _mapper;

        public ASMVisitorDetailsService(IASMVisitorDetailsRepository repository, ASMVisitorDetailsMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ASMVisitorDetailsDto> AddAsync(ASMVisitorDetailsDto dto)
        {
            var entity = _mapper.MapToEntity(dto);
            var saved = await _repository.AddAsync(entity);
            return _mapper.MapToDto(saved);
        }

        public async Task<IEnumerable<ASMVisitorDetailsDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(_mapper.MapToDto);
        }

        public async Task<ASMVisitorDetailsDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.MapToDto(entity);
        }

        public async Task<ASMVisitorDetailsDto> UpdateAsync(int id, ASMVisitorDetailsDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) throw new Exception("Record not found");

            existing.InstituteName = dto.InstituteName;
            existing.StartDate = dto.StartDate;
            existing.EndDate = dto.EndDate;
            existing.FarmersCount = dto.FarmersCount;
            existing.StudentsCount = dto.StudentsCount;
            existing.PublicCount = dto.PublicCount;
            existing.SubmittedDate = dto.SubmittedDate;
           

            var updated = await _repository.UpdateAsync(existing);
            return _mapper.MapToDto(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
