using StarWarsAPI.Core.Entities;

namespace StarWarsAPI.Core.Repositories
{
    public interface IPeopleRepository : IRepository<People>
    {
        Task<People> GetById(int id);
        Task<int> Create(People people);
        Task<int> Update(People people);
        Task<int> Delete(int id);
    }
}