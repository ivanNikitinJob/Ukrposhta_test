using Ukrposhta.Entities;

namespace Ukrposhta.Servises.Interfaces
{
    public interface IDepartmenService
    {
        Task<IEnumerable<Department>> GetAllAsync();
    }
}
