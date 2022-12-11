using StarWarsAPI.Core.Entities;

namespace StarWarsAPI.Core.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
    }
}