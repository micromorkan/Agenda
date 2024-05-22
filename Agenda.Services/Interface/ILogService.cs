using Agenda.Domain.Interface;

namespace Agenda.Services.Interface
{
    public interface ILogService
    {
        void LogException(Exception ex);
    }
}
