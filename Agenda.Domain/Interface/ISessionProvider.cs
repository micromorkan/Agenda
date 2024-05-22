using Agenda.Domain.Entity;
using Agenda.Domain.Models;
using Agenda.Domain.Util;

namespace Agenda.Domain.Interface
{
    public interface ISessionProvider
    {
        void Set(int userId, string userName, string profile);

        UserSession Get();
    }
}
