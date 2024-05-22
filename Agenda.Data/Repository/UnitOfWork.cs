using Agenda.Data.Context;
using Agenda.Domain.Entity;
using Agenda.Domain.Interface;

namespace Agenda.Data.Repository
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private bool _disposed;
        private readonly ADbContext _context;
        private IBaseRepository<LogSystem> _systemLogs;
        private IBaseRepository<LogError> _errorLogs;
        private IBaseRepository<User> _usuarios;

        public UnitOfWork(ADbContext context)
        {
            _context = context;
        }

        public IBaseRepository<LogError> ErrorLogs => _errorLogs ??= new BaseRepository<LogError>(_context);
        public IBaseRepository<LogSystem> SystemLogs => _systemLogs ??= new BaseRepository<LogSystem>(_context);
        public IBaseRepository<User> Users => _usuarios ??= new BaseRepository<User>(_context);

        public void SaveAllChanges()
        {
            _context.SaveChanges();
            Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
