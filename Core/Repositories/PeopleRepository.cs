using Dapper;
using MySql.Data.MySqlClient;
using StarWarsAPI.Core.Entities;
using StarWarsAPI.Core.Repositories;

namespace StarWarsAPI.Repositories
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly string _connectionString;
        public PeopleRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AWSDatabase");
        }

        public async Task<IEnumerable<People>> GetAll()
        {
            using var connection = new MySqlConnection(_connectionString);

            var sql = "SELECT Id, Name, Height, Mass, HairColor, SkinColor, EyeColor, BirthYear, Gender, Homeworld, Created, Edited, Url FROM People";

            var people = await connection.QueryAsync<People>(sql);
            return people;
        }

        public async Task<People> GetById(int id)
        {
            using var connection = new MySqlConnection(_connectionString);

            var sqlPeople = "SELECT * FROM People WHERE Id=@Id";

            var people = await connection.QueryFirstOrDefaultAsync<People>(sqlPeople, new { Id = id });

            return people;
        }

        public async Task<int> Create(People people)
        {
            using var connection = new MySqlConnection(_connectionString);

            var sql = @"INSERT INTO People (Name, Height, Mass, HairColor, SkinColor, EyeColor, BirthYear, Gender, Homeworld, Created, Edited, Url) 
                            VALUES (@Name, @Height, @Mass, @HairColor, @SkinColor, @EyeColor, @BirthYear, @Gender, @Homeworld, @Created, @Edited, @Url)";

            var affectedRows = await connection.ExecuteAsync(sql, people);
            return affectedRows;
        }

        public async Task<int> Update(People people)
        {
            using var connection = new MySqlConnection(_connectionString);

            var sql = @"UPDATE People SET Name=@Name, Height=@Height, Mass=@Mass, HairColor=@HairColor, 
                        SkinColor=@SkinColor, EyeColor=@EyeColor, BirthYear=@BirthYear, Gender=@Gender, 
                        Homeworld=@Homeworld, Created=@Created, Edited=@Edited, Url=@Url
                        WHERE Id=@Id";

            var affectedRows = await connection.ExecuteAsync(sql, people);
            return affectedRows;
        }

        public async Task<int> Delete(int id)
        {
            using var connection = new MySqlConnection(_connectionString);

            var sql = @"DELETE FROM People WHERE Id=@Id";

            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows;
        }
    }
}