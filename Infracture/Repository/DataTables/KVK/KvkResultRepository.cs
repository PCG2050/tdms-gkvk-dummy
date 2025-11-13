using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.DataTables.KVK
{
    public class KvkResultRepository : IKvkResultRepository
    {
        private readonly TdmsDbContext _context;

        public KvkResultRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<KvkResult?> GetByIdAsync(int id)
        {
            return await _context.KvkResults
                .Include(r => r.ProgramDetails)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<KvkResult?> GetByProgramIdAsync(int programId)
        {
            return await _context.KvkResults
                .FirstOrDefaultAsync(r => r.KvkProgramDetailsId == programId);
        }

        public async Task<KvkResult?> GetWithDetailsAsync(int id)
        {
            return await _context.KvkResults
                .Include(r => r.ProgramDetails)
                .Include(r => r.FldResults!)
                    .ThenInclude(f => f.DetailsOfDemo)
                .Include(r => r.OftResults!)
                    .ThenInclude(o => o.DetailsOfDemo)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<KvkResult> CreateAsync(KvkResult entity)
        {
            _context.KvkResults.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<KvkResult> UpdateAsync(KvkResult entity)
        {
            _context.KvkResults.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.KvkResults.FindAsync(id);
            if (entity != null)
            {
                _context.KvkResults.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Creates KvkResult with all child entities in a single transaction
        /// This solves the parent-child ID dependency issue by:
        /// 1. Creating the parent KvkResult first to get auto-generated ID
        /// 2. Using that ID to create all child FldResults and OftResults
        /// 3. Committing all changes in one transaction or rolling back on failure
        /// </summary>
        public async Task<KvkResult> CreateWithChildrenAsync(
            KvkResult parent,
            List<KvkFldResult>? fldResults,
            List<KvkOftResult>? oftResults)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Step 1: Create parent entity - gets auto-generated ID
                _context.KvkResults.Add(parent);
                await _context.SaveChangesAsync();

                var resultId = parent.Id;

                // Step 2: Create FldResults using parent ID
                if (fldResults != null && fldResults.Any())
                {
                    foreach (var fldResult in fldResults)
                    {
                        fldResult.KvkResultId = resultId;
                        _context.KvkFLDResults.Add(fldResult);
                    }
                }

                // Step 3: Create OftResults using parent ID
                if (oftResults != null && oftResults.Any())
                {
                    foreach (var oftResult in oftResults)
                    {
                        oftResult.KvkResultId = resultId;
                        _context.KvkOFTResults.Add(oftResult);
                    }
                }

                // Step 4: Save all child entities
                await _context.SaveChangesAsync();

                // Step 5: Commit transaction
                await transaction.CommitAsync();

                // Return the complete entity with all relationships loaded
                return (await GetWithDetailsAsync(resultId))!;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Updates KvkResult with all child entities using Hybrid Pattern in a single transaction
        /// Hybrid Pattern:
        /// - Items WITH Id > 0: UPDATE existing
        /// - Items WITHOUT Id (null or 0): CREATE new
        /// - Items in DB but NOT in incoming arrays: DELETE
        /// All operations are atomic - either all succeed or all fail
        /// </summary>
        public async Task<KvkResult> UpdateWithChildrenAsync(
            KvkResult parent,
            List<KvkFldResult>? fldResults,
            List<KvkOftResult>? oftResults)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var resultId = parent.Id;

                // Step 1: Get existing entity with all children
                var existing = await GetWithDetailsAsync(resultId);
                if (existing == null)
                {
                    throw new KeyNotFoundException($"KvkResult with Id {resultId} not found");
                }

                // Step 2: Update parent entity
                existing.UploadExcelUrl = parent.UploadExcelUrl;
                existing.UpdatedById = parent.UpdatedById;
                existing.UpdatedAt = parent.UpdatedAt;
                _context.KvkResults.Update(existing);

                // Step 3: Handle FldResults - Hybrid Pattern (CREATE/UPDATE/DELETE)
                var existingFldResults = existing.FldResults?.ToList() ?? new List<KvkFldResult>();

                if (fldResults != null)
                {
                    // Get IDs of incoming items (items user wants to keep/update)
                    var incomingFldIds = fldResults
                        .Where(f => f.Id > 0)
                        .Select(f => f.Id)
                        .ToList();

                    // DELETE: Items in DB but NOT in incoming array
                    var fldToDelete = existingFldResults
                        .Where(f => !incomingFldIds.Contains(f.Id))
                        .ToList();

                    foreach (var fld in fldToDelete)
                    {
                        _context.KvkFLDResults.Remove(fld);
                    }

                    // CREATE or UPDATE
                    foreach (var fld in fldResults)
                    {
                        fld.KvkResultId = resultId;

                        if (fld.Id > 0)
                        {
                            // UPDATE: Item has ID, update existing
                            var existingFld = existingFldResults.FirstOrDefault(f => f.Id == fld.Id);
                            if (existingFld != null)
                            {
                                existingFld.DetailsOfDemoId = fld.DetailsOfDemoId;
                                existingFld.FldNumber = fld.FldNumber;
                                existingFld.Parameter1 = fld.Parameter1;
                                existingFld.Observation1 = fld.Observation1;
                                existingFld.Parameter2 = fld.Parameter2;
                                existingFld.Observation2 = fld.Observation2;
                                existingFld.Parameter3 = fld.Parameter3;
                                existingFld.Observation3 = fld.Observation3;
                                existingFld.Parameter4 = fld.Parameter4;
                                existingFld.Observation4 = fld.Observation4;
                                existingFld.Parameter5 = fld.Parameter5;
                                existingFld.Observation5 = fld.Observation5;
                                existingFld.Yield = fld.Yield;
                                existingFld.GrossCost = fld.GrossCost;
                                existingFld.GrossReturns = fld.GrossReturns;
                                existingFld.NetReturns = fld.NetReturns;
                                existingFld.BC = fld.BC;
                                existingFld.UpdatedById = fld.UpdatedById;
                                existingFld.UpdatedAt = fld.UpdatedAt;
                                _context.KvkFLDResults.Update(existingFld);
                            }
                        }
                        else
                        {
                            // CREATE: Item has no ID, create new
                            _context.KvkFLDResults.Add(fld);
                        }
                    }
                }
                else
                {
                    // If fldResults is null, delete all existing
                    foreach (var fld in existingFldResults)
                    {
                        _context.KvkFLDResults.Remove(fld);
                    }
                }

                // Step 4: Handle OftResults - Hybrid Pattern (CREATE/UPDATE/DELETE)
                var existingOftResults = existing.OftResults?.ToList() ?? new List<KvkOftResult>();

                if (oftResults != null)
                {
                    // Get IDs of incoming items
                    var incomingOftIds = oftResults
                        .Where(o => o.Id > 0)
                        .Select(o => o.Id)
                        .ToList();

                    // DELETE: Items in DB but NOT in incoming array
                    var oftToDelete = existingOftResults
                        .Where(o => !incomingOftIds.Contains(o.Id))
                        .ToList();

                    foreach (var oft in oftToDelete)
                    {
                        _context.KvkOFTResults.Remove(oft);
                    }

                    // CREATE or UPDATE
                    foreach (var oft in oftResults)
                    {
                        oft.KvkResultId = resultId;

                        if (oft.Id > 0)
                        {
                            // UPDATE: Item has ID, update existing
                            var existingOft = existingOftResults.FirstOrDefault(o => o.Id == oft.Id);
                            if (existingOft != null)
                            {
                                existingOft.DetailsOfDemoId = oft.DetailsOfDemoId;
                                existingOft.Parameter1 = oft.Parameter1;
                                existingOft.Observation1 = oft.Observation1;
                                existingOft.Parameter2 = oft.Parameter2;
                                existingOft.Observation2 = oft.Observation2;
                                existingOft.Parameter3 = oft.Parameter3;
                                existingOft.Observation3 = oft.Observation3;
                                existingOft.Parameter4 = oft.Parameter4;
                                existingOft.Observation4 = oft.Observation4;
                                existingOft.Parameter5 = oft.Parameter5;
                                existingOft.Observation5 = oft.Observation5;
                                existingOft.Yield = oft.Yield;
                                existingOft.GrossCost = oft.GrossCost;
                                existingOft.GrossReturns = oft.GrossReturns;
                                existingOft.NetReturns = oft.NetReturns;
                                existingOft.BC = oft.BC;
                                existingOft.UpdatedById = oft.UpdatedById;
                                existingOft.UpdatedAt = oft.UpdatedAt;
                                _context.KvkOFTResults.Update(existingOft);
                            }
                        }
                        else
                        {
                            // CREATE: Item has no ID, create new
                            _context.KvkOFTResults.Add(oft);
                        }
                    }
                }
                else
                {
                    // If oftResults is null, delete all existing
                    foreach (var oft in existingOftResults)
                    {
                        _context.KvkOFTResults.Remove(oft);
                    }
                }

                // Step 5: Save all changes
                await _context.SaveChangesAsync();

                // Step 6: Commit transaction
                await transaction.CommitAsync();

                // Return the updated entity with all relationships loaded
                return (await GetWithDetailsAsync(resultId))!;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
