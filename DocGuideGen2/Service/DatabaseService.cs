using Microsoft.Data.Sqlite;
using System.IO;
using System.Threading.Tasks;

namespace DocGuideGen2.Services
{
    public class DatabaseService
    {
        private readonly string _dbPath;

        public DatabaseService()
        {
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _dbPath = Path.Combine(folderPath, "DocGuideGen.db");
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
            {
                connection.Open();

                string createGuideTableQuery = @"
                CREATE TABLE IF NOT EXISTS guide (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL,
                    registry TEXT NOT NULL,
                    rut TEXT NOT NULL,
                    type INTEGER NOT NULL
                );";

                using (var command = new SqliteCommand(createGuideTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        // Method to check if a guide already exists
        public async Task<bool> GuideExistsAsync(string name)
        {
            using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
            {
                await connection.OpenAsync();
                string query = "SELECT COUNT(*) FROM guide WHERE name = @name";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", name);

                    var result = await command.ExecuteScalarAsync();
                    int count = Convert.ToInt32(result);

                    return count > 0;
                }
            }
        }

        // Method to add a new guide
        public async Task AddGuideAsync(string name, string registry, string rut, int type)
        {
            using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
            {
                await connection.OpenAsync();
                string insertQuery = "INSERT INTO guide (name, registry, rut, type) VALUES (@name, @registry, @rut, @type)";

                using (var command = new SqliteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@registry", registry);
                    command.Parameters.AddWithValue("@rut", rut);
                    command.Parameters.AddWithValue("@type", type);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
