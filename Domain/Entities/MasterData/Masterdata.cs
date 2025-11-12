using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.MasterData
{
  
        // The classes you provided initially
        public class ProgramType : BaseEntity
        {
            public string Name { get; set; } = default!;
        }
        public class ProgramCategory : BaseEntity
        {
            public string Name { get; set; } = default!;
        }
        public class ProjectCategory : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        // New classes based on your list
        public class EventName : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

       
        public class InfoType : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class Collaborator : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class CollaborativeProgramOption : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class VillageAdoptiveProgram : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class TargetFarmer : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class Theme : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class ThematicArea : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class SponsoredOrganization : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class Mode : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class Region : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class SourceOfFund : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class Status : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class Participant : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class ParticipatedSource : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class ParticipantDealer : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class ResourceType : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class Responsibility : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class TypeOfAid : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class OFTResult : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class FLDResult : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class PublicationCategory : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class KannadaMagazine : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class EnglishMagazine : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class KannadaNewsPaper : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class EnglishNewsPaper : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class ExtensionWork : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class NominationType : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class NominationCategory : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class Position : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class Contribution : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class ConsultancyServicesCategory : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class RelatedTo : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class Particular : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class ModeOutreach : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class ServiceCategory : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class ServiceTheme : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class QuantityUnit : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

        public class Visitor : BaseEntity
        {
            public string Name { get; set; } = default!;
        }

      
}
