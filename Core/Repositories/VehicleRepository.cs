using Dapper;
using MySql.Data.MySqlClient;
using StarWarsAPI.Core.Entities;
using StarWarsAPI.Core.Repositories;

namespace StarWarsAPI.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly string _connectionString;
        public VehicleRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AWSDatabase");
        }

        public async Task<IEnumerable<Vehicle>> GetAll()
        {
            using var connection = new MySqlConnection(_connectionString);

            var sql = "SELECT Id, Name, Model, Manufacturer, CostInCredits, Lenght, MaxAtmospheringSpeed, Crew, Passengers, CargoCapacity, Consumables, Created, VehicleClass, Edited, Url FROM Vehicles";

            var vehicle = await connection.QueryAsync<Vehicle>(sql);
            return vehicle;
        }

        public async Task<Vehicle> GetById(int id)
        {
            using var connection = new MySqlConnection(_connectionString);

            var sqlVehicle = "SELECT * FROM Vehicles WHERE Id=@Id";

            var vehicle = await connection.QueryFirstOrDefaultAsync<Vehicle>(sqlVehicle, new { Id = id });

            return vehicle;
        }

        public async Task<int> Create(Vehicle vehicle)
        {
            using var connection = new MySqlConnection(_connectionString);

            var sql = @"INSERT INTO Vehicles (Name, Model, Manufacturer, CostInCredits, Lenght, MaxAtmospheringSpeed, Crew, Passengers, CargoCapacity, Consumables, VehicleClass, Created, Edited, Url) 
                            VALUES (@Name, @Model, @Manufacturer, @CostInCredits, @Lenght, @MaxAtmospheringSpeed, @Crew, @Passengers, @CargoCapacity, @Consumables, @VehicleClass, @Created, @Edited, @Url)";

            var affectedRows = await connection.ExecuteAsync(sql, vehicle);
            return affectedRows;
        }

        public async Task<int> Update(Vehicle vehicle)
        {
            using var connection = new MySqlConnection(_connectionString);

            var sql = @"UPDATE Vehicles SET Name=@Name, Model=@Model, Manufacturer=@Manufacturer, CostInCredits=@CostInCredits, 
                        Lenght=@Lenght, MaxAtmospheringSpeed=@MaxAtmospheringSpeed, Crew=@Crew, Passengers=@Passengers, 
                        CargoCapacity=@CargoCapacity, Consumables=@Consumables, VehicleClass=@VehicleClass, Created=@Created, Edited=@Edited, Url=@Url
                        WHERE Id=@Id";

            var affectedRows = await connection.ExecuteAsync(sql, vehicle);
            return affectedRows;
        }

        public async Task<int> Delete(int id)
        {
            using var connection = new MySqlConnection(_connectionString);

            var sql = @"DELETE FROM Vehicles WHERE Id=@Id";

            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows;
        }
    }
}