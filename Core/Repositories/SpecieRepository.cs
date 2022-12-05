using Dapper;
using MySql.Data.MySqlClient;
using StarWarsAPI.Core.Entities;
using StarWarsAPI.Core.Repositories;

namespace StarWarsAPI.Repositories
{
    public class SpecieRepository : ISpecieRepository
    {
        private readonly string _connectionString;
        public SpecieRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AWSDatabase");
        }

        public async Task<IEnumerable<Specie>> GetAll()
        {
            using var connection = new MySqlConnection(_connectionString);

            var sql = "SELECT Id, Name, Classification, Designation, AverageHeight, SkinColors, HairColors, EyeColors, AverageLifespan, Homeworld, Language, Created, Edited, Url FROM Species";

            var specie = await connection.QueryAsync<Specie>(sql);
            return specie;
        }

        public async Task<Specie> GetById(int id)
        {
            using var connection = new MySqlConnection(_connectionString);

            var sqlSpecie = "SELECT * FROM Species WHERE Id=@Id";

            var specie = await connection.QueryFirstOrDefaultAsync<Specie>(sqlSpecie, new { Id = id });

            return specie;
        }

        public async Task<int> Create(Specie specie)
        {
            using var connection = new MySqlConnection(_connectionString);

            var sql = @"INSERT INTO Species (Name, Classification, Designation, AverageHeight, SkinColors, HairColors, EyeColors, AverageLifespan, Homeworld, Language, Created, Edited, Url) 
                            VALUES (@Name, @Classification, @Designation, @AverageHeight, @SkinColors, @HairColors, @EyeColors, @AverageLifespan, @Homeworld, @Language, @Created, @Edited, @Url)";

            var affectedRows = await connection.ExecuteAsync(sql, specie);
            return affectedRows;
        }

        public async Task<int> Update(Specie specie)
        {
            using var connection = new MySqlConnection(_connectionString);

            var sql = @"UPDATE Species SET Name=@Name, Classification=@Classification, Designation=@Designation, AverageHeight=@AverageHeight, 
                        HairColors=@HairColors, SkinColors=@SkinColors, HairColors=@HairColors, EyeColors=@EyeColors, 
                        AverageLifespan=@AverageLifespan, Homeworld=@Homeworld, Language=@Language, Created=@Created,
                        Edited=@Edited, Url=@Url
                        WHERE Id=@Id";

            var affectedRows = await connection.ExecuteAsync(sql, specie);
            return affectedRows;
        }

        public async Task<int> Delete(int id)
        {
            using var connection = new MySqlConnection(_connectionString);

            var sql = @"DELETE FROM Species WHERE Id=@Id";

            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows;
        }
    }
}