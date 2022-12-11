using Dapper;
using MySql.Data.MySqlClient;
using StarWarsAPI.Core.Entities;
using StarWarsAPI.Core.Repositories;

namespace StarWarsAPI.Repositories
{
    public class PlanetRepository : IPlanetRepository
    {
        private readonly string _connectionString;
        public PlanetRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AWSDatabase");
        }

        public async Task<IEnumerable<Planet>> GetAll()
        {
            using var connection = new MySqlConnection(_connectionString);

            var sql = "SELECT Id, Name, RotationPeriod, OrbitalPeriod, Diameter, Climate, Gravity, Terrain, SurfaceWater, Population, Created, Edited, Url FROM Planets";

            var planet = await connection.QueryAsync<Planet>(sql);
            return planet;
        }

        public async Task<Planet> GetById(int id)
        {
            using var connection = new MySqlConnection(_connectionString);

            var sqlPlanet = "SELECT * FROM Planets WHERE Id=@Id";

            var planet = await connection.QueryFirstOrDefaultAsync<Planet>(sqlPlanet, new { Id = id });

            return planet;
        }

        public async Task<int> Create(Planet planet)
        {
            using var connection = new MySqlConnection(_connectionString);

            var sql = @"INSERT INTO Planets (Name, RotationPeriod, OrbitalPeriod, Diameter, Climate, Gravity, Terrain, SurfaceWater, Population, Created, Edited, Url) 
                            VALUES (@Name, @RotationPeriod, @OrbitalPeriod, @Diameter, @Climate, @Gravity, @Terrain, @SurfaceWater, @Population, @Created, @Edited, @Url)";

            var affectedRows = await connection.ExecuteAsync(sql, planet);
            return affectedRows;
        }

        public async Task<int> Update(Planet planet)
        {
            using var connection = new MySqlConnection(_connectionString);

            var sql = @"UPDATE Planets SET Name=@Name, RotationPeriod=@RotationPeriod, OrbitalPeriod=@OrbitalPeriod, Diameter=@Diameter, 
                        Climate=@Climate, Climate=@Climate, Gravity=@Gravity, Gravity=@Gravity, 
                        Terrain=@Terrain, Terrain=@Terrain, SurfaceWater=@SurfaceWater, Population=@Population, Edited=@Edited, Url=@Url
                        WHERE Id=@Id";

            var affectedRows = await connection.ExecuteAsync(sql, planet);
            return affectedRows;
        }

        public async Task<int> Delete(int id)
        {
            using var connection = new MySqlConnection(_connectionString);

            var sql = @"DELETE FROM Planets WHERE Id=@Id";

            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows;
        }

        public async Task<int> InsertAll(PlanetsList[] planetsList)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO PlanetsFilms (Planet, Film) VALUES(@Name, @Film)", connection);
            var sql = "";

            // Code block used to loop through the list of Planets
            for (int i = 0; i < planetsList.Length; i++)
            {
                sql = @"INSERT INTO Planets (Name, RotationPeriod, OrbitalPeriod, Diameter, Climate, Gravity, Terrain, SurfaceWater, Population, Created, Edited, Url) 
                            VALUES (@Name, @RotationPeriod, @OrbitalPeriod, @Diameter, @Climate, @Gravity, @Terrain, @SurfaceWater, @Population, @Created, @Edited, @Url)";

                // Code block used to loop through the list of Films
                foreach (string film in planetsList[i].Films)
                {
                    cmd.CommandText = "INSERT INTO PlanetsFilms (Planet, Film) VALUES(@Name, @Film)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Name", planetsList[i].Name);
                    cmd.Parameters.AddWithValue("@Film", film);
                    cmd.ExecuteNonQuery();
                }

                // Code block used to loop through the list of Residents
                foreach (string resident in planetsList[i].Residents)
                {
                    cmd.CommandText = "INSERT INTO PlanetsPeople (Planet, People) VALUES(@Name, @Resident)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Name", planetsList[i].Name);
                    cmd.Parameters.AddWithValue("@Resident", resident);
                    cmd.ExecuteNonQuery();
                }
            }

            var affectedRows = await connection.ExecuteAsync(sql, planetsList);
            connection.Close();

            return affectedRows;
        }

        public async Task<int> UpdateAll(PlanetsList[] planetsList)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE PlanetsFilms SET Planet=@Name, Film=@Film WHERE Planet=@Name", connection);
            var sql = "";

            // Code block used to loop through the list of Planets
            for (int i = 0; i < planetsList.Length; i++)
            {
                sql = @"UPDATE Planets SET Name=@Name, RotationPeriod=@RotationPeriod, OrbitalPeriod=@OrbitalPeriod, Diameter=@Diameter, 
                        Climate=@Climate, Climate=@Climate, Gravity=@Gravity, Gravity=@Gravity, 
                        Terrain=@Terrain, Terrain=@Terrain, SurfaceWater=@SurfaceWater, Population=@Population, Edited=@Edited, Url=@Url
                        WHERE Name=@Name";

                // Code block used to loop through the list of Films
                foreach (string film in planetsList[i].Films)
                {
                    cmd.CommandText = "UPDATE PlanetsFilms SET Planet=@Name, Film=@Film WHERE Planet=@Name";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Name", planetsList[i].Name);
                    cmd.Parameters.AddWithValue("@Film", film);
                    cmd.ExecuteNonQuery();
                }

                // Code block used to loop through the list of Resident
                foreach (string resident in planetsList[i].Residents)
                {
                    cmd.CommandText = "UPDATE PlanetsPeople SET Planet=@Name, People=@Resident WHERE Planet=@Name";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Name", planetsList[i].Name);
                    cmd.Parameters.AddWithValue("@Resident", resident);
                    cmd.ExecuteNonQuery();
                }
            }

            var affectedRows = await connection.ExecuteAsync(sql, planetsList);
            return affectedRows;
        }
    }
}