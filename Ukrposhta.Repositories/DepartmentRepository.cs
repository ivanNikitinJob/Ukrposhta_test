using Microsoft.Extensions.Configuration;
using Ukrposhta.Entities;
using Ukrposhta.Repositories.Interfaces;

namespace Ukrposhta.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IConfiguration configuration) : base(configuration)
        { }

    }
}
