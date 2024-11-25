using Ukrposhta.Repositories;
using Ukrposhta.Repositories.Interfaces;
using Ukrposhta.Servises;
using Ukrposhta.Servises.Interfaces;

namespace Ukrposhta.Client
{
    public static class DependencyHelper
    {
        public static void AddServices(IServiceCollection services)
        {
            services.AddTransient<IDepartmenService, DepartmentService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IReportService, ReportService>();
        }

        public static void AddRepositories(IServiceCollection services)
        {
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IEmployeeRepository, EmploeeRepository>();
        }
    }
}
