using Agenda.Domain.Entity;

namespace Agenda.Domain.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<User> Users { get; }
        IBaseRepository<LogSystem> SystemLogs { get; }
        IBaseRepository<LogError> ErrorLogs { get; }
        void SaveAllChanges();
    }
}
