using StarWarsAPI.Core.Entities;

namespace StarWarsAPI.Core.Repositories
{
    public interface ISpecieRepository : IRepository<Specie>
    {
        Task<Specie> GetById(int id);
        Task<int> Create(Specie specie);
        Task<int> Update(Specie specie);
        Task<int> Delete(int id);
    }
}