using AgileStudioServer.Data.Entities;

namespace AgileStudioServer.Data.Repositories
{
    public abstract class AbstractDBRepository : IDisposable
    {
        protected readonly DBContext _context;

        public AbstractDBRepository(DBContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
