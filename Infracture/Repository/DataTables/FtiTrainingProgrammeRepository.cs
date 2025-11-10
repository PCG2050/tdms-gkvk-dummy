//using Application.Interface.Repository.DataTables;
//using Domain.Entities.FTI;
//using Infrastructure.DbContext;

//namespace Infrastructure.Repository.DataTables
//{
//    public class FtiTrainingProgrammeRepository : GenericRepository<FtiTrainingProgram, FtiTrainingProgram>, IFtiTrainingProgrammeRepository
//    {
//        private readonly TdmsDbContext _context;

//        public FtiTrainingProgrammeRepository(TdmsDbContext context):base(context)
//        {
//            _context = context;
//        }
//        public async Task DeleteAsync(int id)
//        {
//            var entry = await _context.FtiTrainingPrograms.FindAsync(id);
//            if(entry is not null)_context.FtiTrainingPrograms.Remove(entry);
//            await _context.SaveChangesAsync();
//        }

//        public async Task<FtiTrainingProgram?> GetItemAsync(int id)
//        {
//            return await _context.FtiTrainingPrograms.FindAsync(id);
//        }

//        protected override IQueryable<FtiTrainingProgram> ProjectToDto(IQueryable<FtiTrainingProgram> query)
//        {
//            return query;
//        }
//    }
//}
