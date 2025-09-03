using Application.Interface.Repository;
using Application.Models;
using Domain.Entities;
using Domain.Entities.Enum;
using Domain.Entities.Junction;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TdmsDbContext _context;

        public UserRepository(TdmsDbContext context)
        {
            _context = context;
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


        public async Task<List<User>> GetUsersByOrganizationAndRoleAsync(int organizationId, Role role)
        {
            return await _context.Users
                .Where(u => u.OrganizationId == organizationId && u.Role == role)
                .ToListAsync();
        }

        public async Task<PaginatedResult<TrainerDetailsDto>> GetPaginatedItemsAsync(int organizationId, int pageNumber = 1, QueryFilter? queryFilter = null, int pageSize = 10)
        {
            var query = _context.Users.Where(x => x.OrganizationId == organizationId && (x.Role == Domain.Entities.Enum.Role.TRAINER));
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
                                        Email = u.Email,
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



        public async Task<PaginatedResult<FlatTrainerDetailsDto>> GetPaginatedTrainerDetailsWithLocationAsync(int organizationId, int pageNumber = 1, QueryFilter? queryFilter = null, int pageSize = 10)
        {
            var query = _context.Users
                .Where(x => x.OrganizationId == organizationId && x.Role == Role.UNITHEAD);

            var result = new PaginatedResult<FlatTrainerDetailsDto>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = await query.AsNoTracking().CountAsync()
            };

            int offset = (pageNumber - 1) * pageSize;

            var trainersDtoQuery = await query
                .OrderBy(u => u.Id)
                .Skip(offset)
                .Take(pageSize)
                .SelectMany(u => u.TrainerAssignments.Select(assignment => new FlatTrainerDetailsDto
                {
                    UserId = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    
                    UnitLocationDetails = new UnitLocationDetailsDto
                    {
                        UnitLocationId = assignment.UnitLocationId,
                        UnitId = assignment.UnitLocation.UnitId,
                        UnitName = assignment.UnitLocation.Unit.Name,

                        StateId = assignment.UnitLocation.District.State.Id,
                        StateName = assignment.UnitLocation.District.State.Name,

                        DistrictId = assignment.UnitLocation.District.Id,
                        DistrictName = assignment.UnitLocation.District.Name

                    }

                }))
                .ToListAsync();

            result.Items = trainersDtoQuery;
            return result;
        }



        public async Task<PaginatedResult<UnitHeadDetailsDto>> GetPaginatedUnitHeadsAsync(int organizationId, int pageNumber = 1, QueryFilter? queryFilter = null, int pageSize = 10)
        {
            var query = _context.Users.Where(x =>
                x.OrganizationId == organizationId && x.Role == Role.UNITHEAD);


            var result = new PaginatedResult<UnitHeadDetailsDto>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = await query.AsNoTracking().CountAsync()
            };


            int offset = (pageNumber - 1) * pageSize;
            var unitHeadsDtoQuery = await query
                                    .OrderBy(u => u.Id) // Essential for consistent pagination results
                                    .Skip(offset)
                                    .Take(pageSize)
                                    .Select(u => new UnitHeadDetailsDto
                                    {
                                        UserId = u.Id,
                                        FirstName = u.FirstName,
                                        LastName = u.LastName,
                                        Email = u.Email,
                                        Phone = u.Phone,
                                        Units = u.UnitHeadAssignments
                                            .Select(ta => ta.UnitLocation.Unit) // Select the Unit entity first
                                            .Distinct() // Get unique units if a trainer is assigned to multiple locations within the same unit                                        
                                            .Select(unit => new UnitHeadUnitDto
                                            {
                                                UnitId = unit.Id,
                                                Name = unit.Name,
                                                Locations = u.UnitHeadAssignments
                                                    .Where(ta => ta.UnitLocation.UnitId == unit.Id) // Filter assignments for this specific unit
                                                    .Select(ta => ta.UnitLocation)
                                                    .Distinct() // Get unique locations within this unit
                                                    .Select(ul => new UnitHeadLocationDto
                                                    {
                                                        StateId = ul.District.State.Id, // Access State via District
                                                        StateName = ul.District.State.Name,
                                                        Districts = u.UnitHeadAssignments
                                                            .Where(ta => ta.UnitLocation.Id == ul.Id) // Filter assignments for this specific location
                                                            .Select(ta => ta.UnitLocation.District)
                                                            .Distinct()
                                                            .Select(d => new UnitHeadDistrictDto
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
            result.Items = unitHeadsDtoQuery;
            return result;
        }

        public async Task<List<FlatUnitHeadDetailsDto>> GetDetailedPaginatedUnitHeadsAsync(int organizationId, int adminId)
        {
            var unitHeads = await _context.Users
                 .Where(u => u.OrganizationId == organizationId && u.Role == Role.UNITHEAD && u.CreatedById == adminId)
                .OrderBy(u => u.Id)
                .Select(u => new FlatUnitHeadDetailsDto
                {
                    UserId = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Phone = u.Phone,
                    UnitLocationDetails = u.UnitHeadAssignments
                        .Select(a => new UnitLocationDetailsDto
                        {
                            UnitLocationId = a.UnitLocationId,
                            UnitId = a.UnitLocation.UnitId,
                            UnitName = a.UnitLocation.Unit.Name,
                            StateId = a.UnitLocation.District.State.Id,
                            StateName = a.UnitLocation.District.State.Name,
                            DistrictId = a.UnitLocation.District.Id,
                            DistrictName = a.UnitLocation.District.Name
                        })
                        .Distinct()
                        .ToList()
                })
                .ToListAsync();

            return unitHeads;
        }

        public async Task<List<FlatUnitHeadDetailsDto>> GetDetailedPaginatedTrainersAsync(int organizationId, int unitHeadId)
        {
            var trainers = await _context.Users
                 .Where(u => u.OrganizationId == organizationId && u.Role == Role.TRAINER && u.CreatedById == unitHeadId)
                .OrderBy(u => u.Id)
                .Select(u => new FlatUnitHeadDetailsDto
                {
                    UserId = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Phone = u.Phone,
                    Gender = u.Gender,
                    EmployementType = u.EmployementType,
                    DateOfBirth = u.DateOfBirth,                   
                    DateOfJoining = u.DateOfJoining,
                    IsDeactivated = u.IsDeactivated,
                    UnitLocationDetails = u.TrainerAssignments
                        .Select(a => new UnitLocationDetailsDto
                        {
                            UnitLocationId = a.UnitLocationId,
                            UnitId = a.UnitLocation.UnitId,
                            UnitName = a.UnitLocation.Unit.Name,
                            StateId = a.UnitLocation.District.State.Id,
                            StateName = a.UnitLocation.District.State.Name,
                            DistrictId = a.UnitLocation.District.Id,
                            DistrictName = a.UnitLocation.District.Name
                        })
                        .Distinct()
                        .ToList()
                })
                .ToListAsync();

            return trainers;
        }

     

        public DateOnly? DateOfJoining { get; set; }
        public bool? IsPhoneConfirmed { get; set; }
        public bool? IsDeactivated { get; set; }


        public async Task SetPasswordResetTokenAsync(int userId, string token, DateTimeOffset expiresAt)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return;

            user.PasswordResetToken = token;
            user.PasswordResetTokenExpiresAt = expiresAt;
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByPasswordResetTokenAsync(string token)
        {
            var now = DateTimeOffset.UtcNow;
            return await _context.Users.FirstOrDefaultAsync(u =>
            u.PasswordResetToken == token &&
            u.PasswordResetTokenExpiresAt != null &&
            u.PasswordResetTokenExpiresAt > now &&
            !u.IsDeactivated);
        }

        public async Task ClearPasswordResetTokenAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return;
            user.PasswordResetTokenExpiresAt = null;
            user.PasswordResetToken = null;
            await _context.SaveChangesAsync();

        }


        //new method 
        public async Task<List<User>> GetTrainersCreatedByAsync(int createdById)
        {
            return await _context.Users
                .Where(u => u.CreatedById == createdById && u.Role == Role.TRAINER)
                .Include(u => u.TrainerAssignments)
                    .ThenInclude(ta => ta.UnitLocation)
                        .ThenInclude(ul => ul.Unit)
                .Include(u => u.TrainerAssignments)
                    .ThenInclude(ta => ta.UnitLocation)
                        .ThenInclude(ul => ul.District)
                            .ThenInclude(d => d.State)
                .OrderBy(u => u.FirstName)
                .ToListAsync();
        }              

       
    }
}
