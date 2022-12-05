using StarWarsAPI.Core.Entities;

namespace StarWarsAPI.Core.Repositories
{
    public interface IStarshipRepository : IRepository<Starship>
    {
        Task<Starship> GetById(int id);
        Task<int> Create(Starship starship);
        Task<int> Update(Starship starship);
        Task<int> Delete(int id);
    }
}