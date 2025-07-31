using Application.Interface;
using Application.Interface.Services.Common;
using Application.Models;
using Domain.Entities;
using Domain.Entities.Enum;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public abstract class GenericTableService<TEntity, TCreateDto, TUpdateDto, TDto> : IGenericTableService<TEntity, TCreateDto, TUpdateDto, TDto>
    where TEntity : ReportEntryBaseEntity
    where TCreateDto : class
    where TUpdateDto : IUpdateDto
    where TDto : class
    {
        protected readonly IGenericRepository<TEntity, TDto> _repository;
        protected readonly ICurrentUserService _currentUserService;
        protected readonly IEntityPermissionService _entityPermissionService;

        protected GenericTableService(
            IGenericRepository<TEntity, TDto> repository,
            ICurrentUserService currentUserService,
            IEntityPermissionService entityPermissionService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _entityPermissionService = entityPermissionService;
        }

        public virtual async Task<ServiceResult<TEntity>> AddAsync(TCreateDto createDto)
        {
            // Validate permissions before creation
            if (!await CanCreateAsync(createDto))
                return ServiceResult<TEntity>.Failure("User does not have permission to create this item", ServiceErrorStatus.FORBIDDEN);

            var entity = await MapCreateDtoToEntityAsync(createDto);
            SetAuditFields(entity, isCreate: true);

            await _repository.AddAsync(entity);
            return ServiceResult<TEntity>.Success(entity);
        }

        public virtual async Task<ServiceResult> DeleteAsync(int id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity is null)
                return ServiceResult.Failure($"{typeof(TEntity).Name} not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModify(entity))
                return ServiceResult.Failure("User does not have permission to delete this item", ServiceErrorStatus.FORBIDDEN);

            await _repository.DeleteAsync(entity);
            return ServiceResult.Success();
        }

        public virtual async Task<ServiceResult<TEntity>> UpdateAsync(TUpdateDto updateDto)
        {
            var entity = await _repository.GetAsync(updateDto.Id);
            if (entity is null)
                return ServiceResult<TEntity>.Failure($"{typeof(TEntity).Name} not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModify(entity))
                return ServiceResult<TEntity>.Failure("User does not have permission to update this item", ServiceErrorStatus.FORBIDDEN);

            await MapUpdateDtoToEntityAsync(updateDto, entity);
            SetAuditFields(entity, isCreate: false);

            await _repository.UpdateAsync(entity);
            return ServiceResult<TEntity>.Success(entity);
        }

        public virtual async Task<PaginatedResult<TDto>> GetPaginatedItemsAsync(int pageNumber = 1, int pageSize = 10)
        {
            var filter = await BuildQueryFilterAsync();
            return await _repository.GetPaginatedItemsAsync(_currentUserService.OrganizationId, pageNumber, filter, pageSize);
        }

        protected abstract Task<TEntity> MapCreateDtoToEntityAsync(TCreateDto createDto);
        protected abstract Task MapUpdateDtoToEntityAsync(TUpdateDto updateDto, TEntity entity);

        protected virtual async Task<bool> CanCreateAsync(TCreateDto createDto)
        {
            // Default: allow creation (override for specific permission logic)
            return true;
        }

        protected virtual async Task<QueryFilter> BuildQueryFilterAsync()
        {
            var filter = new QueryFilter();

            // Default role-based filtering
            if (_currentUserService.Role == Role.TRAINER)
                filter.Filters["CreatedById"] = _currentUserService.UserId;

            return filter;
        }

        protected virtual void SetAuditFields(TEntity entity, bool isCreate)
        {
            if (isCreate)
            {
                entity.CreatedById = _currentUserService.UserId;
                entity.CreatedAt = DateTimeOffset.UtcNow;
                entity.OrganizationId = _currentUserService.OrganizationId;
            }
            else
            {
                entity.UpdatedById = _currentUserService.UserId;
                entity.UpdatedAt = DateTimeOffset.UtcNow;
            }
        }
    }
}
