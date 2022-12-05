using Dapper;
using MySql.Data.MySqlClient;
using StarWarsAPI.Core.Entities;
using StarWarsAPI.Core.Repositories;

namespace StarWarsAPI.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private readonly string _connectionString;
        public FilmRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AWSDatabase");
        }

        public async Task<IEnumerable<Film>> GetAll()
        {
            using var connection = new MySqlConnection(_connectionString);

            var sql = "SELECT Id, Title, EpisodeId, OpeningCrawl, Director, Producer, ReleaseDate, Created, Edited, Url FROM Films";

            var film = await connection.QueryAsync<Film>(sql);
            return film;
        }

        public async Task<Film> GetById(int id)
        {
            using var connection = new MySqlConnection(_connectionString);

            var sqlFilm = "SELECT * FROM Films WHERE Id=@Id";

            var film = await connection.QueryFirstOrDefaultAsync<Film>(sqlFilm, new { Id = id });

            return film;
        }

        public async Task<int> Create(Film film)
        {
            using var connection = new MySqlConnection(_connectionString);

            var sql = @"INSERT INTO Films (Title, EpisodeId, OpeningCrawl, Director, Producer, ReleaseDate, Created, Edited, Url) 
                            VALUES (@Title, @EpisodeId, @OpeningCrawl, @Director, @Producer, @ReleaseDate, @Created, @Edited, @Url)";

            var affectedRows = await connection.ExecuteAsync(sql, film);
            return affectedRows;
        }

        public async Task<int> Update(Film film)
        {
            using var connection = new MySqlConnection(_connectionString);

            var sql = @"UPDATE Films SET Title=@Title, EpisodeId=@EpisodeId, OpeningCrawl=@OpeningCrawl, Director=@Director, 
                        Producer=@Producer, ReleaseDate=@ReleaseDate, Created=@Created, Edited=@Edited, Url=@Url
                        WHERE Id=@Id";

            var affectedRows = await connection.ExecuteAsync(sql, film);
            return affectedRows;
        }

        public async Task<int> Delete(int id)
        {
            using var connection = new MySqlConnection(_connectionString);

            var sql = @"DELETE FROM Films WHERE Id=@Id";

            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows;
        }
    }
}