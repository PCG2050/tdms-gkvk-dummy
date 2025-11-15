// FTIProgramContentRepository.cs
using Application.Interface.Repository.DataTables.FTI;
using Domain.Entities.FTI;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.FTI
{
    public class FTIProgramContentRepository : IFTIProgramContentRepository
    {
        private readonly TdmsDbContext _context;

        public FTIProgramContentRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<FTIProgramContentAndResources?> GetByIdAsync(int id)
        {
            return await _context.FTIProgramContentAndResources.FindAsync(id);
        }

        public async Task<FTIProgramContentAndResources?> GetWithDetailsAsync(int id)
        {
            return await _context.FTIProgramContentAndResources
                .Include(c => c.ResourcePersons!)
                .Include(c => c.TopicsCovered!)
                .Include(c => c.TeachingAids!)
                    .ThenInclude(ta => ta.TypeOfAid)
                .Include(c => c.ProgramDetails)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<FTIProgramContentAndResources>> GetByProgramIdAsync(int programId)
        {
            return await _context.FTIProgramContentAndResources
                .Include(c => c.ResourcePersons!)
                .Include(c => c.TopicsCovered!)
                .Include(c => c.TeachingAids!)
                    .ThenInclude(ta => ta.TypeOfAid)
                .Where(c => c.FTIProgramDetailsId == programId)
                .ToListAsync();
        }

        public async Task<FTIProgramContentAndResources> CreateAsync(FTIProgramContentAndResources entity)
        {
            _context.FTIProgramContentAndResources.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<FTIProgramContentAndResources> UpdateAsync(FTIProgramContentAndResources entity)
        {
            _context.FTIProgramContentAndResources.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.FTIProgramContentAndResources.FindAsync(id);
            if (entity != null)
            {
                _context.FTIProgramContentAndResources.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Create parent with all child entities in a single transaction
        /// </summary>
        public async Task<FTIProgramContentAndResources> CreateWithChildrenAsync(
            FTIProgramContentAndResources parent,
            List<FTIResourcePerson>? resourcePersons,
            List<FTITopicsCoveredInClass>? topicsCovered,
            List<FTITeachingAidsDeveloped>? teachingAids)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Create parent entity
                _context.FTIProgramContentAndResources.Add(parent);
                await _context.SaveChangesAsync(); // Generates parent ID

                var contentId = parent.Id;

                // 2. Create child entities with generated parent ID
                if (resourcePersons != null && resourcePersons.Any())
                {
                    foreach (var person in resourcePersons)
                    {
                        person.FTIProgramContentAndResourcesId = contentId;
                        _context.FTIResourcePersons.Add(person);
                    }
                }

                if (topicsCovered != null && topicsCovered.Any())
                {
                    foreach (var topic in topicsCovered)
                    {
                        topic.FTIProgramContentAndResourcesId = contentId;
                        _context.FTITopicsCoveredInClass.Add(topic);
                    }
                }

                if (teachingAids != null && teachingAids.Any())
                {
                    foreach (var aid in teachingAids)
                    {
                        aid.FTIProgramContentAndResourcesId = contentId;
                        _context.FTITeachingAidsDeveloped.Add(aid);
                    }
                }

                // Save all children
                await _context.SaveChangesAsync();

                // Commit transaction
                await transaction.CommitAsync();

                // Return complete entity with children
                return (await GetWithDetailsAsync(contentId))!;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Update parent with all child entities using hybrid pattern in a single transaction
        /// - Creates new children (Id = 0)
        /// - Updates existing children (Id > 0)
        /// - Deletes children not in lists
        /// </summary>
        public async Task<FTIProgramContentAndResources> UpdateWithChildrenAsync(
            FTIProgramContentAndResources parent,
            List<FTIResourcePerson>? resourcePersons,
            List<FTITopicsCoveredInClass>? topicsCovered,
            List<FTITeachingAidsDeveloped>? teachingAids)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var contentId = parent.Id;

                // Load existing entity with all children
                var existing = await GetWithDetailsAsync(contentId);
                if (existing == null)
                    throw new InvalidOperationException($"Content with ID {contentId} not found");

                // 1. Update parent entity
                existing.UpdatedById = parent.UpdatedById;
                existing.UpdatedAt = parent.UpdatedAt;
                _context.FTIProgramContentAndResources.Update(existing);

                // 2. Process Resource Persons (Hybrid Pattern)
                if (resourcePersons != null)
                {
                    var existingPersons = existing.ResourcePersons?.ToList() ?? new List<FTIResourcePerson>();
                    var incomingIds = resourcePersons.Where(p => p.Id > 0).Select(p => p.Id).ToList();

                    // DELETE: Items in DB but not in incoming array
                    var personsToDelete = existingPersons.Where(p => !incomingIds.Contains(p.Id)).ToList();
                    foreach (var person in personsToDelete)
                    {
                        _context.FTIResourcePersons.Remove(person);
                    }

                    // CREATE or UPDATE
                    foreach (var person in resourcePersons)
                    {
                        if (person.Id > 0)
                        {
                            // UPDATE existing
                            var existingPerson = existingPersons.FirstOrDefault(p => p.Id == person.Id);
                            if (existingPerson != null)
                            {
                                existingPerson.Name = person.Name;
                                existingPerson.Designation = person.Designation;
                                existingPerson.ResourceType = person.ResourceType;
                                existingPerson.Responsibility = person.Responsibility;
                                existingPerson.InstitutionOrDepartment = person.InstitutionOrDepartment;
                                existingPerson.UpdatedById = person.UpdatedById;
                                existingPerson.UpdatedAt = person.UpdatedAt;
                                _context.FTIResourcePersons.Update(existingPerson);
                            }
                        }
                        else
                        {
                            // CREATE new
                            person.FTIProgramContentAndResourcesId = contentId;
                            _context.FTIResourcePersons.Add(person);
                        }
                    }
                }

                // 3. Process Topics Covered (Hybrid Pattern)
                if (topicsCovered != null)
                {
                    var existingTopics = existing.TopicsCovered?.ToList() ?? new List<FTITopicsCoveredInClass>();
                    var incomingIds = topicsCovered.Where(t => t.Id > 0).Select(t => t.Id).ToList();

                    // DELETE: Items in DB but not in incoming array
                    var topicsToDelete = existingTopics.Where(t => !incomingIds.Contains(t.Id)).ToList();
                    foreach (var topic in topicsToDelete)
                    {
                        _context.FTITopicsCoveredInClass.Remove(topic);
                    }

                    // CREATE or UPDATE
                    foreach (var topic in topicsCovered)
                    {
                        if (topic.Id > 0)
                        {
                            // UPDATE existing
                            var existingTopic = existingTopics.FirstOrDefault(t => t.Id == topic.Id);
                            if (existingTopic != null)
                            {
                                existingTopic.Date = topic.Date;
                                existingTopic.Title = topic.Title;
                                existingTopic.PhotoUpload = topic.PhotoUpload;
                                existingTopic.UpdatedById = topic.UpdatedById;
                                existingTopic.UpdatedAt = topic.UpdatedAt;
                                _context.FTITopicsCoveredInClass.Update(existingTopic);
                            }
                        }
                        else
                        {
                            // CREATE new
                            topic.FTIProgramContentAndResourcesId = contentId;
                            _context.FTITopicsCoveredInClass.Add(topic);
                        }
                    }
                }

                // 4. Process Teaching Aids (Hybrid Pattern)
                if (teachingAids != null)
                {
                    var existingAids = existing.TeachingAids?.ToList() ?? new List<FTITeachingAidsDeveloped>();
                    var incomingIds = teachingAids.Where(a => a.Id > 0).Select(a => a.Id).ToList();

                    // DELETE: Items in DB but not in incoming array
                    var aidsToDelete = existingAids.Where(a => !incomingIds.Contains(a.Id)).ToList();
                    foreach (var aid in aidsToDelete)
                    {
                        _context.FTITeachingAidsDeveloped.Remove(aid);
                    }

                    // CREATE or UPDATE
                    foreach (var aid in teachingAids)
                    {
                        if (aid.Id > 0)
                        {
                            // UPDATE existing
                            var existingAid = existingAids.FirstOrDefault(a => a.Id == aid.Id);
                            if (existingAid != null)
                            {
                                existingAid.TypeOfAidId = aid.TypeOfAidId;
                                existingAid.OtherTypeOfAid = aid.OtherTypeOfAid;
                                existingAid.Purpose = aid.Purpose;
                                existingAid.Number = aid.Number;
                                existingAid.UpdatedById = aid.UpdatedById;
                                existingAid.UpdatedAt = aid.UpdatedAt;
                                _context.FTITeachingAidsDeveloped.Update(existingAid);
                            }
                        }
                        else
                        {
                            // CREATE new
                            aid.FTIProgramContentAndResourcesId = contentId;
                            _context.FTITeachingAidsDeveloped.Add(aid);
                        }
                    }
                }

                // Save all changes
                await _context.SaveChangesAsync();

                // Commit transaction
                await transaction.CommitAsync();

                // Return complete entity with children
                return (await GetWithDetailsAsync(contentId))!;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
