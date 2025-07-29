using Application.Interface;
using Application.Interface.Repository;
using Application.Models;
using Domain.Entities;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TdmsDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public UserRepository(TdmsDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task<User> GetByActivationTokenAsync(string token)
        {
            //return await _context.Users.FirstOrDefaultAsync(u => u.ActivationToken == token);
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task SaveAsync(User user)
        {
            if (_context.Entry(user).State == EntityState.Detached)
                _context.Users.Add(user);
            else
                _context.Users.Update(user);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedResult<TrainerDetailsDto>> GetPaginatedItemsAsync(int organizationId, int pageNumber = 1, QueryFilter? queryFilter = null, int pageSize = 10)
        {
            var query = _context.Users.Where(x => x.OrganizationId == organizationId && x.Role == Domain.Entities.Enum.Role.TRAINER);
            if (queryFilter?.Filters?.Count > 0)
            {
                var filters = queryFilter.Filters;
            }
            var result = new PaginatedResult<TrainerDetailsDto>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
            };
            result.TotalItems = await query.AsNoTracking().CountAsync();
            int offset = (pageNumber - 1) * pageSize;
            var trainersDtoQuery = await query
                                    .OrderBy(u => u.Id) // Essential for consistent pagination results
                                    .Skip(offset)
                                    .Take(pageSize)
                                    .Select(u => new TrainerDetailsDto
                                    {
                                        UserId = u.Id,
                                        FirstName = u.FirstName,
                                        LastName = u.LastName,
                                        Units = u.TrainerAssignments
                                            .Select(ta => ta.UnitLocation.Unit) // Select the Unit entity first
                                            .Distinct() // Get unique units if a trainer is assigned to multiple locations within the same unit
                                            .Select(unit => new TrainerUnitDto
                                            {
                                                UnitId = unit.Id,
                                                Name = unit.Name,
                                                Locations = u.TrainerAssignments
                                                    .Where(ta => ta.UnitLocation.UnitId == unit.Id) // Filter assignments for this specific unit
                                                    .Select(ta => ta.UnitLocation)
                                                    .Distinct() // Get unique locations within this unit
                                                    .Select(ul => new TrainerLocationDto
                                                    {
                                                        StateId = ul.District.State.Id, // Access State via District
                                                        StateName = ul.District.State.Name,
                                                        Districts = u.TrainerAssignments
                                                            .Where(ta => ta.UnitLocation.Id == ul.Id) // Filter assignments for this specific location
                                                            .Select(ta => ta.UnitLocation.District)
                                                            .Distinct()
                                                            .Select(d => new TrainerDistrictDto
                                                            {
                                                                DistrictId = d.Id,
                                                                DistrictName = d.Name
                                                            })
                                                            .ToList()
                                                    })
                                                    .ToList()
                                            })
                                            .ToList()
                                    }).ToListAsync();
            result.Items = trainersDtoQuery;
            return result;
        }
    }
}
