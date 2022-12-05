using StarWarsAPI.Core.Entities;

namespace StarWarsAPI.Core.Repositories
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        Task<Vehicle> GetById(int id);
        Task<int> Create(Vehicle vehicle);
        Task<int> Update(Vehicle vehicle);
        Task<int> Delete(int id);
    }
}