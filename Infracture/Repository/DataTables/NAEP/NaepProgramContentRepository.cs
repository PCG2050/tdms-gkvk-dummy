// NaepProgramContentRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.NAEP
{
    public class NaepProgramContentRepository : INaepProgramContentRepository
    {
        private readonly TdmsDbContext _context;

        public NaepProgramContentRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<NaepProgramContentAndResources> CreateAsync(NaepProgramContentAndResources entity)
        {
            _context.NaepProgramContentAndResources.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<NaepProgramContentAndResources?> GetByIdAsync(int id)
        {
            return await _context.NaepProgramContentAndResources
                .Include(c => c.ProgramDetails)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<NaepProgramContentAndResources?> GetWithDetailsAsync(int id)
        {
            return await _context.NaepProgramContentAndResources
                .Include(c => c.ProgramDetails)
                .Include(c => c.ResourcePersons!)
                .Include(c => c.TopicsCovered!)
                .Include(c => c.TeachingAids!)
                    .ThenInclude(ta => ta.TypeOfAid)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<NaepProgramContentAndResources>> GetByProgramIdAsync(int programId)
        {
            return await _context.NaepProgramContentAndResources
                .Include(c => c.ResourcePersons!)
                .Include(c => c.TopicsCovered!)
                .Include(c => c.TeachingAids!)
                    .ThenInclude(ta => ta.TypeOfAid)
                .Where(c => c.NaepProgramDetailsId == programId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<NaepProgramContentAndResources> UpdateAsync(NaepProgramContentAndResources entity)
        {
            _context.NaepProgramContentAndResources.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.NaepProgramContentAndResources.FindAsync(id);
            if (entity != null)
            {
                _context.NaepProgramContentAndResources.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Create parent with all child entities in a single transaction
        /// </summary>
        public async Task<NaepProgramContentAndResources> CreateWithChildrenAsync(
            NaepProgramContentAndResources parent,
            List<NaepResourcePerson>? resourcePersons,
            List<NaepTopicsCoveredInClass>? topicsCovered,
            List<NaepTeachingAidsDeveloped>? teachingAids)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Create parent entity
                _context.NaepProgramContentAndResources.Add(parent);
                await _context.SaveChangesAsync(); // Generates parent ID

                var contentId = parent.Id;

                // 2. Create child entities with generated parent ID
                if (resourcePersons != null && resourcePersons.Any())
                {
                    foreach (var person in resourcePersons)
                    {
                        person.NaepProgramContentAndResourcesId = contentId;
                        _context.NaepResourcePersons.Add(person);
                    }
                }

                if (topicsCovered != null && topicsCovered.Any())
                {
                    foreach (var topic in topicsCovered)
                    {
                        topic.NaepProgramContentAndResourcesId = contentId;
                        _context.NaepTopicsCoveredInClass.Add(topic);
                    }
                }

                if (teachingAids != null && teachingAids.Any())
                {
                    foreach (var aid in teachingAids)
                    {
                        aid.NaepProgramContentAndResourcesId = contentId;
                        _context.NaepTeachingAidsDeveloped.Add(aid);
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
        public async Task<NaepProgramContentAndResources> UpdateWithChildrenAsync(
            NaepProgramContentAndResources parent,
            List<NaepResourcePerson>? resourcePersons,
            List<NaepTopicsCoveredInClass>? topicsCovered,
            List<NaepTeachingAidsDeveloped>? teachingAids)
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
                _context.NaepProgramContentAndResources.Update(existing);

                // 2. Process Resource Persons (Hybrid Pattern)
                if (resourcePersons != null)
                {
                    var existingPersons = existing.ResourcePersons?.ToList() ?? new List<NaepResourcePerson>();
                    var incomingIds = resourcePersons.Where(p => p.Id > 0).Select(p => p.Id).ToList();

                    // DELETE: Items in DB but not in incoming array
                    var personsToDelete = existingPersons.Where(p => !incomingIds.Contains(p.Id)).ToList();
                    foreach (var person in personsToDelete)
                    {
                        _context.NaepResourcePersons.Remove(person);
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
                                _context.NaepResourcePersons.Update(existingPerson);
                            }
                        }
                        else
                        {
                            // CREATE new
                            person.NaepProgramContentAndResourcesId = contentId;
                            _context.NaepResourcePersons.Add(person);
                        }
                    }
                }

                // 3. Process Topics Covered (Hybrid Pattern)
                if (topicsCovered != null)
                {
                    var existingTopics = existing.TopicsCovered?.ToList() ?? new List<NaepTopicsCoveredInClass>();
                    var incomingIds = topicsCovered.Where(t => t.Id > 0).Select(t => t.Id).ToList();

                    // DELETE: Items in DB but not in incoming array
                    var topicsToDelete = existingTopics.Where(t => !incomingIds.Contains(t.Id)).ToList();
                    foreach (var topic in topicsToDelete)
                    {
                        _context.NaepTopicsCoveredInClass.Remove(topic);
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
                                _context.NaepTopicsCoveredInClass.Update(existingTopic);
                            }
                        }
                        else
                        {
                            // CREATE new
                            topic.NaepProgramContentAndResourcesId = contentId;
                            _context.NaepTopicsCoveredInClass.Add(topic);
                        }
                    }
                }

                // 4. Process Teaching Aids (Hybrid Pattern)
                if (teachingAids != null)
                {
                    var existingAids = existing.TeachingAids?.ToList() ?? new List<NaepTeachingAidsDeveloped>();
                    var incomingIds = teachingAids.Where(a => a.Id > 0).Select(a => a.Id).ToList();

                    // DELETE: Items in DB but not in incoming array
                    var aidsToDelete = existingAids.Where(a => !incomingIds.Contains(a.Id)).ToList();
                    foreach (var aid in aidsToDelete)
                    {
                        _context.NaepTeachingAidsDeveloped.Remove(aid);
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
                                _context.NaepTeachingAidsDeveloped.Update(existingAid);
                            }
                        }
                        else
                        {
                            // CREATE new
                            aid.NaepProgramContentAndResourcesId = contentId;
                            _context.NaepTeachingAidsDeveloped.Add(aid);
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