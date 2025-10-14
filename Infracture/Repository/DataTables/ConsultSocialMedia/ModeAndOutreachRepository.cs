using Application.Interface.Repository.DataTables;
using Application.Interface.Repository.DataTables.ConsultSocialMedia;
using Domain.Entities.GenericTables.ConsultingAndSocialMediaService;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.ConsultSocialMedia
{
    public class ModeAndOutreachRepository : IModeAndOutreachRepository
    {
        private readonly TdmsDbContext _context;

        public ModeAndOutreachRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<TableModeAndOutreach?> GetByIdAsync(int id)
        {
            if (_context.TableModeAndOutreaches == null)
                return null;
            return await _context.TableModeAndOutreaches.FindAsync(id);
        }

        public async Task<List<TableModeAndOutreach>> GetByConsultingServiceIdAsync(int consultingServiceId)
        {
            if (_context.TableModeAndOutreaches == null)
                return new List<TableModeAndOutreach>();
            return await _context.TableModeAndOutreaches
                .Where(m => m.ConsultingServiceId == consultingServiceId)
                .ToListAsync();
        }

        public async Task<TableModeAndOutreach> CreateAsync(TableModeAndOutreach entity)
        {
            if (_context.TableModeAndOutreaches == null)
                throw new InvalidOperationException("TableModeAndOutreaches DbSet is not initialized.");
            _context.TableModeAndOutreaches.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TableModeAndOutreach> UpdateAsync(TableModeAndOutreach entity)
        {
            if (_context.TableModeAndOutreaches == null)
                throw new InvalidOperationException("TableModeAndOutreaches DbSet is not initialized.");
            _context.TableModeAndOutreaches.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            if (_context.TableModeAndOutreaches == null)
                return;
            var entity = await _context.TableModeAndOutreaches.FindAsync(id);
            if (entity != null)
            {
                _context.TableModeAndOutreaches.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}