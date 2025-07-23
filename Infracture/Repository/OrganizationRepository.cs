using Application.Interface.Repository;
using Domain.Entities;
using Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly TdmsDbContext _context;

        public OrganizationRepository(TdmsDbContext context)
        {
            _context = context;
        }
        public async Task<Organization?> GetOrganizationAsync(int id)
        {
            return await _context.Organizations.FindAsync(id);
        }

        public async Task SaveAsync(Organization organization)
        {
            if (_context.Entry<Organization>(organization).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
                await _context.AddAsync(organization);
            else
                _context.Update(organization);
            await _context.SaveChangesAsync();
        }
    }
}
