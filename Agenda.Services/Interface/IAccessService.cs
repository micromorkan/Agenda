using Agenda.Domain.Entity;
using Agenda.Domain.Models;

namespace Agenda.Services.Interface
{
    public interface IAccessService
    {
        Task<Result<User>> Authenticate(User user);
    }
}
