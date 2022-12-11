using Dapper;
using MySql.Data.MySqlClient;
using StarWarsAPI.Core.Entities;
using StarWarsAPI.Core.Repositories;
using System.Data;
using System.Xml.Linq;

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

        public async Task<int> InsertAll(PeopleList[] peopleList)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO PeopleFilms (People, Film) VALUES(@Name, @Film)", connection);
            var sql = "";

            // Code block used to loop through the list of People
            for (int i = 0; i < peopleList.Length; i++)
            {
                sql = @"INSERT INTO People (Name, Height, Mass, HairColor, SkinColor, EyeColor, BirthYear, Gender, Homeworld, Created, Edited, Url) 
                            VALUES (@Name, @Height, @Mass, @HairColor, @SkinColor, @EyeColor, @BirthYear, @Gender, @Homeworld, @Created, @Edited, @Url)";

                // Code block used to loop through the list of Films
                foreach (string film in peopleList[i].Films)
                {
                    cmd.CommandText = "INSERT INTO PeopleFilms (People, Film) VALUES(@Name, @Film)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Name", peopleList[i].Name);
                    cmd.Parameters.AddWithValue("@Film", film);
                    cmd.ExecuteNonQuery();
                }

                // Code block used to loop through the list of Species
                foreach (string specie in peopleList[i].Species)
                {
                    cmd.CommandText = "INSERT INTO PeopleSpecies (People, Specie) VALUES(@Name, @Specie)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Name", peopleList[i].Name);
                    cmd.Parameters.AddWithValue("@Specie", specie);
                    cmd.ExecuteNonQuery();
                }

                // Code block used to loop through the list of Vehicles
                foreach (string vehicle in peopleList[i].Vehicles)
                {
                    cmd.CommandText = "INSERT INTO PeopleVehicles (People, Vehicle) VALUES(@Name, @Vehicle)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Name", peopleList[i].Name);
                    cmd.Parameters.AddWithValue("@Vehicle", vehicle);
                    cmd.ExecuteNonQuery();
                }

                // Code block used to loop through the list of Starships
                foreach (string starship in peopleList[i].Starships)
                {
                    cmd.CommandText = "INSERT INTO PeopleStarships (People, Starship) VALUES(@Name, @Starship)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Name", peopleList[i].Name);
                    cmd.Parameters.AddWithValue("@Starship", starship);
                    cmd.ExecuteNonQuery();
                }
            }

            var affectedRows = await connection.ExecuteAsync(sql, peopleList);
            connection.Close();

            return affectedRows;
        }

        public async Task<int> UpdateAll(PeopleList[] peopleList)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE PeopleFilms SET Name=@Name, Film=@Film WHERE People=@Name", connection);
            var sql = "";

            // Code block used to loop through the list of People
            for (int i = 0; i < peopleList.Length; i++)
            {
                sql = @"UPDATE People SET Name=@Name, Height=@Height, Mass=@Mass, HairColor=@HairColor, 
                        SkinColor=@SkinColor, EyeColor=@EyeColor, BirthYear=@BirthYear, Gender=@Gender, 
                        Homeworld=@Homeworld, Created=@Created, Edited=@Edited, Url=@Url
                        WHERE Name=@Name";

                // Code block used to loop through the list of Films
                foreach (string film in peopleList[i].Films)
                {
                    cmd.CommandText = "UPDATE PeopleFilms SET People=@Name, Film=@Film WHERE People=@Name";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Name", peopleList[i].Name);
                    cmd.Parameters.AddWithValue("@Film", film);
                    cmd.ExecuteNonQuery();
                }

                // Code block used to loop through the list of Species
                foreach (string specie in peopleList[i].Species)
                {
                    cmd.CommandText = "UPDATE PeopleSpecies SET People=@Name, Specie=@Specie WHERE People=@Name";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Name", peopleList[i].Name);
                    cmd.Parameters.AddWithValue("@Specie", specie);
                    cmd.ExecuteNonQuery();
                }

                // Code block used to loop through the list of Vehicles
                foreach (string vehicle in peopleList[i].Vehicles)
                {
                    cmd.CommandText = "UPDATE PeopleVehicles SET People=@Name, Vehicle=@Vehicle WHERE People=@Name";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Name", peopleList[i].Name);
                    cmd.Parameters.AddWithValue("@Vehicle", vehicle);
                    cmd.ExecuteNonQuery();
                }

                // Code block used to loop through the list of Starships
                foreach (string starship in peopleList[i].Starships)
                {
                    cmd.CommandText = "UPDATE PeopleStarships SET People=@Name, Starship=@Starship WHERE People=@Name";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Name", peopleList[i].Name);
                    cmd.Parameters.AddWithValue("@Starship", starship);
                    cmd.ExecuteNonQuery();
                }
            }

            var affectedRows = await connection.ExecuteAsync(sql, peopleList);
            return affectedRows;
        }
    }
}