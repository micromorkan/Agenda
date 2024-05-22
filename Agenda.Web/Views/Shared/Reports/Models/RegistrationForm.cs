using Agenda.Domain.Entity;

namespace Agenda.Web.Views.Shared.Reports.Models
{
    public class RegistrationForm
    {
        public RegistrationForm(User user)
        {
            User = user;
        }

        public User User { get; set; }
    }
}
