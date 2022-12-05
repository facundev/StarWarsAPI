using StarWarsAPI.Core.Entities;

namespace StarWarsAPI.Core.Repositories
{
    public interface IFilmRepository : IRepository<Film>
    {
        Task<Film> GetById(int id);
        Task<int> Create(Film film);
        Task<int> Update(Film film);
        Task<int> Delete(int id);
    }
}