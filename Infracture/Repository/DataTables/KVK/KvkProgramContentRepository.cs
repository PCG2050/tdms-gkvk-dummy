// KvkProgramContentRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.KVK
{
    public class KvkProgramContentRepository : IKvkProgramContentRepository
    {
        private readonly TdmsDbContext _context;

        public KvkProgramContentRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<KvkProgramContentAndResources> CreateAsync(KvkProgramContentAndResources entity)
        {
            _context.KvkProgramContentAndResources.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<KvkProgramContentAndResources?> GetByIdAsync(int id)
        {
            return await _context.KvkProgramContentAndResources
                .Include(c => c.ProgramDetails)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<KvkProgramContentAndResources?> GetWithDetailsAsync(int id)
        {
            return await _context.KvkProgramContentAndResources
                .Include(c => c.ProgramDetails)
                .Include(c => c.ResourcePersons!)
                .Include(c => c.TopicsCovered!)
                .Include(c => c.TeachingAids!)
                    .ThenInclude(ta => ta.TypeOfAid)
                .Include(c => c.FieldVisits!)
                .Include(c => c.FieldDays!)
                .Include(c => c.FarmerScientistInteractions!)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<KvkProgramContentAndResources>> GetByProgramIdAsync(int programId)
        {
            return await _context.KvkProgramContentAndResources
                .Include(c => c.ResourcePersons!)
                .Include(c => c.TopicsCovered!)
                .Include(c => c.TeachingAids!)
                    .ThenInclude(ta => ta.TypeOfAid)
                .Include(c => c.FieldVisits!)
                .Include(c => c.FieldDays!)
                .Include(c => c.FarmerScientistInteractions!)
                .Where(c => c.KvkProgramDetailsId == programId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<KvkProgramContentAndResources> UpdateAsync(KvkProgramContentAndResources entity)
        {
            _context.KvkProgramContentAndResources.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.KvkProgramContentAndResources.FindAsync(id);
            if (entity != null)
            {
                _context.KvkProgramContentAndResources.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Create parent with all child entities in a single transaction
        /// </summary>
        public async Task<KvkProgramContentAndResources> CreateWithChildrenAsync(
            KvkProgramContentAndResources parent,
            List<KvkResourcePerson>? resourcePersons,
            List<KvkTopicsCoveredInClass>? topicsCovered,
            List<KvkTeachingAidsDeveloped>? teachingAids,
            List<KvkFieldVisit>? fieldVisits,
            List<KvkFieldDay>? fieldDays,
            List<KvkFarmerScientistInteraction>? farmerScientistInteractions)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Create parent entity
                _context.KvkProgramContentAndResources.Add(parent);
                await _context.SaveChangesAsync(); // Generates parent ID

                var contentId = parent.Id;

                // 2. Create child entities with generated parent ID
                if (resourcePersons != null && resourcePersons.Any())
                {
                    foreach (var person in resourcePersons)
                    {
                        person.KvkProgramContentAndResourcesId = contentId;
                        _context.KvkResourcePersons.Add(person);
                    }
                }

                if (topicsCovered != null && topicsCovered.Any())
                {
                    foreach (var topic in topicsCovered)
                    {
                        topic.KvkProgramContentAndResourcesId = contentId;
                        _context.KvkTopicsCoveredInClass.Add(topic);
                    }
                }

                if (teachingAids != null && teachingAids.Any())
                {
                    foreach (var aid in teachingAids)
                    {
                        aid.KvkProgramContentAndResourcesId = contentId;
                        _context.KvkTeachingAidsDeveloped.Add(aid);
                    }
                }

                if (fieldVisits != null && fieldVisits.Any())
                {
                    foreach (var visit in fieldVisits)
                    {
                        visit.KvkProgramContentAndResourcesId = contentId;
                        _context.Set<KvkFieldVisit>().Add(visit);
                    }
                }

                if (fieldDays != null && fieldDays.Any())
                {
                    foreach (var day in fieldDays)
                    {
                        day.KvkProgramContentAndResourcesId = contentId;
                        _context.Set<KvkFieldDay>().Add(day);
                    }
                }

                if (farmerScientistInteractions != null && farmerScientistInteractions.Any())
                {
                    foreach (var interaction in farmerScientistInteractions)
                    {
                        interaction.KvkProgramContentAndResourcesId = contentId;
                        _context.Set<KvkFarmerScientistInteraction>().Add(interaction);
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
        public async Task<KvkProgramContentAndResources> UpdateWithChildrenAsync(
            KvkProgramContentAndResources parent,
            List<KvkResourcePerson>? resourcePersons,
            List<KvkTopicsCoveredInClass>? topicsCovered,
            List<KvkTeachingAidsDeveloped>? teachingAids,
            List<KvkFieldVisit>? fieldVisits,
            List<KvkFieldDay>? fieldDays,
            List<KvkFarmerScientistInteraction>? farmerScientistInteractions)
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
                _context.KvkProgramContentAndResources.Update(existing);

                // 2. Process Resource Persons (Hybrid Pattern)
                if (resourcePersons != null)
                {
                    var existingPersons = existing.ResourcePersons?.ToList() ?? new List<KvkResourcePerson>();
                    var incomingIds = resourcePersons.Where(p => p.Id > 0).Select(p => p.Id).ToList();

                    // DELETE: Items in DB but not in incoming array
                    var personsToDelete = existingPersons.Where(p => !incomingIds.Contains(p.Id)).ToList();
                    foreach (var person in personsToDelete)
                    {
                        _context.KvkResourcePersons.Remove(person);
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
                                _context.KvkResourcePersons.Update(existingPerson);
                            }
                        }
                        else
                        {
                            // CREATE new
                            person.KvkProgramContentAndResourcesId = contentId;
                            _context.KvkResourcePersons.Add(person);
                        }
                    }
                }

                // 3. Process Topics Covered (Hybrid Pattern)
                if (topicsCovered != null)
                {
                    var existingTopics = existing.TopicsCovered?.ToList() ?? new List<KvkTopicsCoveredInClass>();
                    var incomingIds = topicsCovered.Where(t => t.Id > 0).Select(t => t.Id).ToList();

                    // DELETE: Items in DB but not in incoming array
                    var topicsToDelete = existingTopics.Where(t => !incomingIds.Contains(t.Id)).ToList();
                    foreach (var topic in topicsToDelete)
                    {
                        _context.KvkTopicsCoveredInClass.Remove(topic);
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
                                _context.KvkTopicsCoveredInClass.Update(existingTopic);
                            }
                        }
                        else
                        {
                            // CREATE new
                            topic.KvkProgramContentAndResourcesId = contentId;
                            _context.KvkTopicsCoveredInClass.Add(topic);
                        }
                    }
                }

                // 4. Process Teaching Aids (Hybrid Pattern)
                if (teachingAids != null)
                {
                    var existingAids = existing.TeachingAids?.ToList() ?? new List<KvkTeachingAidsDeveloped>();
                    var incomingIds = teachingAids.Where(a => a.Id > 0).Select(a => a.Id).ToList();

                    // DELETE: Items in DB but not in incoming array
                    var aidsToDelete = existingAids.Where(a => !incomingIds.Contains(a.Id)).ToList();
                    foreach (var aid in aidsToDelete)
                    {
                        _context.KvkTeachingAidsDeveloped.Remove(aid);
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
                                _context.KvkTeachingAidsDeveloped.Update(existingAid);
                            }
                        }
                        else
                        {
                            // CREATE new
                            aid.KvkProgramContentAndResourcesId = contentId;
                            _context.KvkTeachingAidsDeveloped.Add(aid);
                        }
                    }
                }

                // 5. Process Field Visits (Hybrid Pattern)
                if (fieldVisits != null)
                {
                    var existingVisits = existing.FieldVisits?.ToList() ?? new List<KvkFieldVisit>();
                    var incomingIds = fieldVisits.Where(v => v.Id > 0).Select(v => v.Id).ToList();

                    // DELETE: Items in DB but not in incoming array
                    var visitsToDelete = existingVisits.Where(v => !incomingIds.Contains(v.Id)).ToList();
                    foreach (var visit in visitsToDelete)
                    {
                        _context.Set<KvkFieldVisit>().Remove(visit);
                    }

                    // CREATE or UPDATE
                    foreach (var visit in fieldVisits)
                    {
                        if (visit.Id > 0)
                        {
                            // UPDATE existing
                            var existingVisit = existingVisits.FirstOrDefault(v => v.Id == visit.Id);
                            if (existingVisit != null)
                            {
                                existingVisit.Date = visit.Date;
                                existingVisit.ScientistOfficerVisitedName = visit.ScientistOfficerVisitedName;
                                existingVisit.Purpose = visit.Purpose;
                                existingVisit.NoOfFieldsCovered = visit.NoOfFieldsCovered;
                                existingVisit.NoOfFarmerCovered = visit.NoOfFarmerCovered;
                                existingVisit.PhotoUpload = visit.PhotoUpload;
                                existingVisit.UpdatedById = visit.UpdatedById;
                                existingVisit.UpdatedAt = visit.UpdatedAt;
                                _context.Set<KvkFieldVisit>().Update(existingVisit);
                            }
                        }
                        else
                        {
                            // CREATE new
                            visit.KvkProgramContentAndResourcesId = contentId;
                            _context.Set<KvkFieldVisit>().Add(visit);
                        }
                    }
                }

                // 6. Process Field Days (Hybrid Pattern)
                if (fieldDays != null)
                {
                    var existingDays = existing.FieldDays?.ToList() ?? new List<KvkFieldDay>();
                    var incomingIds = fieldDays.Where(d => d.Id > 0).Select(d => d.Id).ToList();

                    // DELETE: Items in DB but not in incoming array
                    var daysToDelete = existingDays.Where(d => !incomingIds.Contains(d.Id)).ToList();
                    foreach (var day in daysToDelete)
                    {
                        _context.Set<KvkFieldDay>().Remove(day);
                    }

                    // CREATE or UPDATE
                    foreach (var day in fieldDays)
                    {
                        if (day.Id > 0)
                        {
                            // UPDATE existing
                            var existingDay = existingDays.FirstOrDefault(d => d.Id == day.Id);
                            if (existingDay != null)
                            {
                                existingDay.Date = day.Date;
                                existingDay.FarmerName = day.FarmerName;
                                existingDay.Place = day.Place;
                                existingDay.NoOfBeneficieries = day.NoOfBeneficieries;
                                existingDay.UnitLocationId = day.UnitLocationId;
                                existingDay.OrganizationId = day.OrganizationId;
                                existingDay.UpdatedById = day.UpdatedById;
                                existingDay.UpdatedAt = day.UpdatedAt;
                                _context.Set<KvkFieldDay>().Update(existingDay);
                            }
                        }
                        else
                        {
                            // CREATE new
                            day.KvkProgramContentAndResourcesId = contentId;
                            _context.Set<KvkFieldDay>().Add(day);
                        }
                    }
                }

                // 7. Process Farmer Scientist Interactions (Hybrid Pattern)
                if (farmerScientistInteractions != null)
                {
                    var existingInteractions = existing.FarmerScientistInteractions?.ToList() ?? new List<KvkFarmerScientistInteraction>();
                    var incomingIds = farmerScientistInteractions.Where(i => i.Id > 0).Select(i => i.Id).ToList();

                    // DELETE: Items in DB but not in incoming array
                    var interactionsToDelete = existingInteractions.Where(i => !incomingIds.Contains(i.Id)).ToList();
                    foreach (var interaction in interactionsToDelete)
                    {
                        _context.Set<KvkFarmerScientistInteraction>().Remove(interaction);
                    }

                    // CREATE or UPDATE
                    foreach (var interaction in farmerScientistInteractions)
                    {
                        if (interaction.Id > 0)
                        {
                            // UPDATE existing
                            var existingInteraction = existingInteractions.FirstOrDefault(i => i.Id == interaction.Id);
                            if (existingInteraction != null)
                            {
                                existingInteraction.Date = interaction.Date;
                                existingInteraction.ScientistOfficerName = interaction.ScientistOfficerName;
                                existingInteraction.TopicDiscussed = interaction.TopicDiscussed;
                                existingInteraction.NoOfFarmersParticipated = interaction.NoOfFarmersParticipated;
                                existingInteraction.PhotoUpload = interaction.PhotoUpload;
                                existingInteraction.UpdatedById = interaction.UpdatedById;
                                existingInteraction.UpdatedAt = interaction.UpdatedAt;
                                _context.Set<KvkFarmerScientistInteraction>().Update(existingInteraction);
                            }
                        }
                        else
                        {
                            // CREATE new
                            interaction.KvkProgramContentAndResourcesId = contentId;
                            _context.Set<KvkFarmerScientistInteraction>().Add(interaction);
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