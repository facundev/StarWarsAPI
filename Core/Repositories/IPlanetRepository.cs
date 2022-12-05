using StarWarsAPI.Core.Entities;

namespace StarWarsAPI.Core.Repositories
{
    public interface IPlanetRepository : IRepository<Planet>
    {
        Task<Planet> GetById(int id);
        Task<int> Create(Planet planet);
        Task<int> Update(Planet planet);
        Task<int> Delete(int id);
    }
}