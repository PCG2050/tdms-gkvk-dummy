using Application.Interface;
using Application.Interface.Repository;
using Application.Models;
using Domain.Entities.Enum;
using Domain.Entities.Junction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class OrganizationUnitService : IOrganizationUnitService
    {
        private readonly IOrganizationUnitRepository _organizationUnit;
        private readonly ICurrentUserService _currentUser;
        private readonly IUserService _userService;
        private readonly ITrainerAssignmentRepository _trainerAssignment;
     

        public OrganizationUnitService(IOrganizationUnitRepository organizationUnit, ICurrentUserService currentUser,
                                       IUserService userService, ITrainerAssignmentRepository trainerAssignment)
        {
            _organizationUnit = organizationUnit;
            _currentUser = currentUser;
            _userService = userService;
            _trainerAssignment = trainerAssignment;         
        }

        

        public async Task<OrganizationUnitLocation> AddUnitToOrganization(OrganizationUnitLocationDto addUnitLocationDto)
        {
            var role = _currentUser.Role;
            if (role != Role.ADMIN) throw new UnauthorizedAccessException($"{role} does not have access to the action {nameof(this.AddUnitToOrganization)} at {this.GetType()}");
            var newOrgUnit = new OrganizationUnitLocation()
            {
                OrganizationId = _currentUser.OrganizationId,
                CreatedAt = DateTime.UtcNow,
                CreatedById = _currentUser.UserId,
                DistrictId = addUnitLocationDto.DistrictId,
                UnitId = addUnitLocationDto.UnitId,
            };
            if (await _organizationUnit.ExistsAsync(newOrgUnit.OrganizationId, newOrgUnit.UnitId, newOrgUnit.DistrictId))
                throw new InvalidOperationException("Cannot add duplicate unit");
            await _organizationUnit.SaveAsync(newOrgUnit);
            return newOrgUnit;
        }
        public async Task<IEnumerable<OrganizationUnitLocation>> GetOrganizationUnits()
        {
            // Based on the OrgId in user token
            var role = _currentUser.Role;
            if (role == Role.UNDEFINED) throw new UnauthorizedAccessException($"{role} does not have access to the action {nameof(this.AddUnitToOrganization)} at {this.GetType()}");
            var orgUnits = await _organizationUnit.GetByOrganizationIdAsync(_currentUser.OrganizationId);
            if (orgUnits == null || !orgUnits.Any()) return [];
            else return orgUnits;
        }

        public async Task<IEnumerable<OrgUnitLocationDetailsDto>> GetOrganizationUnitsDetails()
        {
            return await _organizationUnit.GetQueryable()
                .Where(x => x.OrganizationId == _currentUser.OrganizationId)
                .Include(x => x.Unit)
                .Include(x => x.District)
                .ThenInclude(x => x.State)
                .GroupBy(x => new { x.Unit.Id, x.Unit.Name })
                    .Select(g => new OrgUnitLocationDetailsDto
                    {
                        UnitId = g.Key.Id,
                        UnitName = g.Key.Name,
                        Location = g.Select(x => new LocationDto
                        {
                            StateId = x.District.State.Id,
                            StateName = x.District.State.Name,
                            DistrictId = x.District.Id,
                            DistrictName = x.District.Name
                        }).Distinct().ToList()
                    })
                .ToListAsync();
        }


        public async Task RemoveUnitFromOrganization(OrganizationUnitLocationDto organizationUnitLocationDto)
        {
            var role = _currentUser.Role;
            if (role != Role.ADMIN) throw new UnauthorizedAccessException($"{role} does not have access to the action {nameof(this.AddUnitToOrganization)} at {this.GetType()}");
            var mapping = await _organizationUnit.GetByOrganizationUnitDistrictAsync(_currentUser.OrganizationId, organizationUnitLocationDto.UnitId, organizationUnitLocationDto.DistrictId);
            if(mapping != null)
                await _organizationUnit.DeleteAsync(mapping);
        }
        public async Task<ServiceResult> MapExistingTrainersAsync(ExistingTrainerAssignmentDto trainerAssignment)
        {
            var unitLocation =  await _organizationUnit.GetByOrganizationUnitDistrictAsync(_currentUser.OrganizationId, trainerAssignment.UnitId, trainerAssignment.DistrictId);
            if (unitLocation is null)
                return ServiceResult.Failure("This Unit mapping does not exist");
            var trainer = await _userService.GetUserByIdAsync(trainerAssignment.TrainerId);
            if (trainer is null)
                return ServiceResult.Failure("Trainer does not exist");
            var alreadyExists = await _trainerAssignment.AssignmentExistsAsync(trainerAssignment);
            if (alreadyExists)
                return ServiceResult.Failure("The trainer is already assigned to this Unit");
            var newAssignment = new TrainerAssignment
            {
                TrainerId = trainerAssignment.TrainerId,
                UnitLocationId = unitLocation.Id,
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedById = _currentUser.UserId
            };
            await _trainerAssignment.AddAsync(newAssignment);
            return ServiceResult.Success();
        }
        public async Task<ServiceResult> UnMapTrainerFromUnitLocationAsync(ExistingTrainerAssignmentDto trainerAssignment)
        {
            var unitLocation = await _organizationUnit.GetByOrganizationUnitDistrictAsync(_currentUser.OrganizationId, trainerAssignment.UnitId, trainerAssignment.DistrictId);
            if (unitLocation is null)
                return ServiceResult.Failure("This Unit mapping does not exist");
            var existingAssignment = await _trainerAssignment.GetByTrainerLocationAsync(unitLocation.Id, trainerAssignment.TrainerId);
            if (existingAssignment is null)
                return ServiceResult.Failure("This assignment does not exist");
            await _trainerAssignment.DeleteAsync(existingAssignment);
            return ServiceResult.Success();
        }
       
    }
}
