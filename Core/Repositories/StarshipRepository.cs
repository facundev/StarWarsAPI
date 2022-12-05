using Dapper;
using MySql.Data.MySqlClient;
using StarWarsAPI.Core.Entities;
using StarWarsAPI.Core.Repositories;

namespace StarWarsAPI.Repositories
{
    public class StarshipRepository : IStarshipRepository
    {
        private readonly string _connectionString;
        public StarshipRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AWSDatabase");
        }

        public async Task<IEnumerable<Starship>> GetAll()
        {
            using var connection = new MySqlConnection(_connectionString);

            var sql = "SELECT Id, Name, Model, Manufacturer, CostInCredits, Lenght, MaxAtmospheringSpeed, Crew, Passengers, CargoCapacity, Consumables, HyperdriveRating, MGLT, StarshipClass, Created, HyperdriveRating, Edited, Url FROM Starships";

            var starship = await connection.QueryAsync<Starship>(sql);
            return starship;
        }

        public async Task<Starship> GetById(int id)
        {
            using var connection = new MySqlConnection(_connectionString);

            var sqlStarship = "SELECT * FROM Starships WHERE Id=@Id";

            var starship = await connection.QueryFirstOrDefaultAsync<Starship>(sqlStarship, new { Id = id });

            return starship;
        }

        public async Task<int> Create(Starship starship)
        {
            using var connection = new MySqlConnection(_connectionString);

            var sql = @"INSERT INTO Starships (Name, Model, Manufacturer, CostInCredits, Lenght, MaxAtmospheringSpeed, Crew, Passengers, CargoCapacity, Consumables, HyperdriveRating, MGLT, StarshipClass, Created, Edited, Url) 
                            VALUES (@Name, @Model, @Manufacturer, @CostInCredits, @Lenght, @MaxAtmospheringSpeed, @Crew, @Passengers, @CargoCapacity, @Consumables, @HyperdriveRating, @MGLT, @StarshipClass, @Created, @Edited, @Url)";

            var affectedRows = await connection.ExecuteAsync(sql, starship);
            return affectedRows;
        }

        public async Task<int> Update(Starship starship)
        {
            using var connection = new MySqlConnection(_connectionString);

            var sql = @"UPDATE Starships SET Name=@Name, Model=@Model, Manufacturer=@Manufacturer, CostInCredits=@CostInCredits, 
                        Lenght=@Lenght, MaxAtmospheringSpeed=@MaxAtmospheringSpeed, Crew=@Crew, Passengers=@Passengers, 
                        CargoCapacity=@CargoCapacity, Consumables=@Consumables, HyperdriveRating=@HyperdriveRating, MGLT=@MGLT, StarshipClass=@StarshipClass Created=@Created, Edited=@Edited, Url=@Url
                        WHERE Id=@Id";

            var affectedRows = await connection.ExecuteAsync(sql, starship);
            return affectedRows;
        }

        public async Task<int> Delete(int id)
        {
            using var connection = new MySqlConnection(_connectionString);

            var sql = @"DELETE FROM Starships WHERE Id=@Id";

            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows;
        }
    }
}