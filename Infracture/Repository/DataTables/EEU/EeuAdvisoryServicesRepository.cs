// EeuAdvisoryServicesRepository.cs
using Application.Interface.Repository.DataTables.EEU;
using Domain.Entities.EEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.EEU
{
    public class EeuAdvisoryServicesRepository : IEeuAdvisoryServicesRepository
    {
        private readonly TdmsDbContext _context;

        public EeuAdvisoryServicesRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public IQueryable<EeuAdvisoryServices> GetQueryable()
        {
            return _context.EeuAdvisoryServices.AsQueryable();
        }

        public async Task<EeuAdvisoryServices?> GetByIdAsync(int id)
        {
            return await _context.EeuAdvisoryServices.FindAsync(id);
        }

        public async Task<EeuAdvisoryServices?> GetWithDetailsAsync(int id)
        {
            return await _context.EeuAdvisoryServices
                .Include(a => a.CriticalInputsDistributed)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<EeuAdvisoryServices?> GetByProgramIdAsync(int programId)
        {
            return await _context.EeuAdvisoryServices
                .Include(a => a.CriticalInputsDistributed)
                .FirstOrDefaultAsync(a => a.EeuProgramDetailsId == programId);
        }

        public async Task<EeuAdvisoryServices> CreateAsync(EeuAdvisoryServices entity)
        {
            _context.EeuAdvisoryServices.Add(entity);
            await _context.SaveChangesAsync();
            return await GetWithDetailsAsync(entity.Id) ?? entity;
        }

        public async Task<EeuAdvisoryServices> CreateWithChildrenAsync(
            EeuAdvisoryServices parent,
            List<EeuCriticalInputsDistributed>? criticalInputs)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Create parent entity
                _context.EeuAdvisoryServices.Add(parent);
                await _context.SaveChangesAsync();

                var advisoryServiceId = parent.Id;

                // Create child entities
                if (criticalInputs != null && criticalInputs.Any())
                {
                    foreach (var input in criticalInputs)
                    {
                        input.EeuAdvisoryServicesId = advisoryServiceId;
                        _context.Set<EeuCriticalInputsDistributed>().Add(input);
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return await GetWithDetailsAsync(advisoryServiceId) ?? parent;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<EeuAdvisoryServices> UpdateAsync(EeuAdvisoryServices entity)
        {
            _context.EeuAdvisoryServices.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<EeuAdvisoryServices> UpdateWithChildrenAsync(
            EeuAdvisoryServices parent,
            List<EeuCriticalInputsDistributed>? criticalInputs)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var advisoryServiceId = parent.Id;

                // Load existing entity with all children
                var existing = await GetWithDetailsAsync(advisoryServiceId);
                if (existing == null)
                    throw new InvalidOperationException($"Advisory service with ID {advisoryServiceId} not found");

                // Update parent entity
                existing.NoOfFacebookSMS = parent.NoOfFacebookSMS;
                existing.NoOfSMSSentToRegisteredFarmers = parent.NoOfSMSSentToRegisteredFarmers;
                existing.NoOfWhatsappGroups = parent.NoOfWhatsappGroups;
                existing.NoOfWhatsappSMS = parent.NoOfWhatsappSMS;
                existing.NoOfAnsweredWhatsappQueries = parent.NoOfAnsweredWhatsappQueries;
                existing.NoOfPhoneCalls = parent.NoOfPhoneCalls;
                existing.NoOfFaceToFaceDiscussions = parent.NoOfFaceToFaceDiscussions;
                existing.NoOfGroupDiscussions = parent.NoOfGroupDiscussions;
                existing.NoOfEmailsSent = parent.NoOfEmailsSent;
                existing.NoOfNewspaperCoverage = parent.NoOfNewspaperCoverage;
                existing.NoOfBeneficiaries = parent.NoOfBeneficiaries;
                existing.UpdatedById = parent.UpdatedById;
                existing.UpdatedAt = parent.UpdatedAt;
                _context.EeuAdvisoryServices.Update(existing);

                // Process Critical Inputs (Hybrid Pattern)
                if (criticalInputs != null)
                {
                    var existingInputs = existing.CriticalInputsDistributed?.ToList() ?? new List<EeuCriticalInputsDistributed>();
                    var incomingIds = criticalInputs.Where(i => i.Id > 0).Select(i => i.Id).ToList();

                    // DELETE: Items in DB but not in incoming array
                    var inputsToDelete = existingInputs.Where(i => !incomingIds.Contains(i.Id)).ToList();
                    foreach (var input in inputsToDelete)
                    {
                        _context.Set<EeuCriticalInputsDistributed>().Remove(input);
                    }

                    // CREATE or UPDATE
                    foreach (var input in criticalInputs)
                    {
                        if (input.Id > 0)
                        {
                            // UPDATE existing
                            var existingInput = existingInputs.FirstOrDefault(i => i.Id == input.Id);
                            if (existingInput != null)
                            {
                                existingInput.InputName = input.InputName;
                                existingInput.QuantityDistributed = input.QuantityDistributed;
                                existingInput.NoOfRecipients = input.NoOfRecipients;
                                existingInput.UpdatedById = input.UpdatedById;
                                existingInput.UpdatedAt = input.UpdatedAt;
                                _context.Set<EeuCriticalInputsDistributed>().Update(existingInput);
                            }
                        }
                        else
                        {
                            // CREATE new
                            input.EeuAdvisoryServicesId = advisoryServiceId;
                            _context.Set<EeuCriticalInputsDistributed>().Add(input);
                        }
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return await GetWithDetailsAsync(advisoryServiceId) ?? existing;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.EeuAdvisoryServices.FindAsync(id);
            if (entity != null)
            {
                _context.EeuAdvisoryServices.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}