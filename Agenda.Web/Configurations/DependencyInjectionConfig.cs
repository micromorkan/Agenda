using FluentValidation;
using Agenda.Data.Repository;
using Agenda.Domain.Interface;
using Agenda.Domain.Models;
using Agenda.Services.FluentValidation;
using Agenda.Services.Interface;
using Agenda.Services.Service;
using Agenda.Web.Helpers;

namespace Agenda.Web.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccessService, AccessService>();

            services.AddScoped<ISessionProvider, SessionProvider>();
            services.AddValidatorsFromAssemblyContaining<UserValidator>();
            services.AddTransient<SecurityAttribute>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ILogService, LogService>();

            return services;
        }
    }
}
