using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.DataTables.EEU
{
    public class EeuResultRepository : IEeuResultRepository
    {
        private readonly TdmsDbContext _context;

        public EeuResultRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<EeuResult?> GetByIdAsync(int id)
        {
            return await _context.EeuResults
                .Include(r => r.ProgramDetails)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<EeuResult?> GetByProgramIdAsync(int programId)
        {
            return await _context.EeuResults
                .FirstOrDefaultAsync(r => r.EeuProgramDetailsId == programId);
        }

        public async Task<EeuResult?> GetWithDetailsAsync(int id)
        {
            return await _context.EeuResults
                .Include(r => r.ProgramDetails)
                .Include(r => r.FldResults!)
                    .ThenInclude(f => f.DetailsOfDemo)
                .Include(r => r.OftResults!)
                    .ThenInclude(o => o.DetailsOfDemo)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<EeuResult> CreateAsync(EeuResult entity)
        {
            _context.EeuResults.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<EeuResult> UpdateAsync(EeuResult entity)
        {
            _context.EeuResults.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.EeuResults.FindAsync(id);
            if (entity != null)
            {
                _context.EeuResults.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Creates EeuResult with all child entities in a single transaction
        /// This solves the parent-child ID dependency issue by:
        /// 1. Creating the parent EeuResult first to get auto-generated ID
        /// 2. Using that ID to create all child FldResults and OftResults
        /// 3. Committing all changes in one transaction or rolling back on failure
        /// </summary>
        public async Task<EeuResult> CreateWithChildrenAsync(
            EeuResult parent,
            List<EeuFldResult>? fldResults,
            List<EeuOftResult>? oftResults)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Step 1: Create parent entity - gets auto-generated ID
                _context.EeuResults.Add(parent);
                await _context.SaveChangesAsync();

                var resultId = parent.Id;

                // Step 2: Create FldResults using parent ID
                if (fldResults != null && fldResults.Any())
                {
                    foreach (var fldResult in fldResults)
                    {
                        fldResult.EeuResultId = resultId;
                        _context.EeuFLDResults.Add(fldResult);
                    }
                }

                // Step 3: Create OftResults using parent ID
                if (oftResults != null && oftResults.Any())
                {
                    foreach (var oftResult in oftResults)
                    {
                        oftResult.EeuResultId = resultId;
                        _context.EeuOFTResults.Add(oftResult);
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
        /// Updates EeuResult with all child entities using Hybrid Pattern in a single transaction
        /// Hybrid Pattern:
        /// - Items WITH Id > 0: UPDATE existing
        /// - Items WITHOUT Id (null or 0): CREATE new
        /// - Items in DB but NOT in incoming arrays: DELETE
        /// All operations are atomic - either all succeed or all fail
        /// </summary>
        public async Task<EeuResult> UpdateWithChildrenAsync(
            EeuResult parent,
            List<EeuFldResult>? fldResults,
            List<EeuOftResult>? oftResults)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var resultId = parent.Id;

                // Step 1: Get existing entity with all children
                var existing = await GetWithDetailsAsync(resultId);
                if (existing == null)
                {
                    throw new KeyNotFoundException($"EeuResult with Id {resultId} not found");
                }

                // Step 2: Update parent entity
                existing.UploadExcelUrl = parent.UploadExcelUrl;
                existing.UpdatedById = parent.UpdatedById;
                existing.UpdatedAt = parent.UpdatedAt;
                _context.EeuResults.Update(existing);

                // Step 3: Handle FldResults - Hybrid Pattern (CREATE/UPDATE/DELETE)
                var existingFldResults = existing.FldResults?.ToList() ?? new List<EeuFldResult>();

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
                        _context.EeuFLDResults.Remove(fld);
                    }

                    // CREATE or UPDATE
                    foreach (var fld in fldResults)
                    {
                        fld.EeuResultId = resultId;

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
                                _context.EeuFLDResults.Update(existingFld);
                            }
                        }
                        else
                        {
                            // CREATE: Item has no ID, create new
                            _context.EeuFLDResults.Add(fld);
                        }
                    }
                }
                else
                {
                    // If fldResults is null, delete all existing
                    foreach (var fld in existingFldResults)
                    {
                        _context.EeuFLDResults.Remove(fld);
                    }
                }

                // Step 4: Handle OftResults - Hybrid Pattern (CREATE/UPDATE/DELETE)
                var existingOftResults = existing.OftResults?.ToList() ?? new List<EeuOftResult>();

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
                        _context.EeuOFTResults.Remove(oft);
                    }

                    // CREATE or UPDATE
                    foreach (var oft in oftResults)
                    {
                        oft.EeuResultId = resultId;

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
                                _context.EeuOFTResults.Update(existingOft);
                            }
                        }
                        else
                        {
                            // CREATE: Item has no ID, create new
                            _context.EeuOFTResults.Add(oft);
                        }
                    }
                }
                else
                {
                    // If oftResults is null, delete all existing
                    foreach (var oft in existingOftResults)
                    {
                        _context.EeuOFTResults.Remove(oft);
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
