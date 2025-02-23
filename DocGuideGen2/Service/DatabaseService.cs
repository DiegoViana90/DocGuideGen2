using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using DocGuideGen2.Models;

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

        // ✅ Check if a guide already exists
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

        // ✅ Add a new guide
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

        // ✅ Get guides based on filter
        public async Task<List<Guide>> GetGuidesByFilterAsync(string filter)
        {
            var guides = new List<Guide>();

            using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM guide";
                if (filter == "Guia")
                {
                    query += " WHERE type = 1";
                }
                else if (filter == "Assistente de Guia")
                {
                    query += " WHERE type = 2";
                }

                using (var command = new SqliteCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        guides.Add(new Guide
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Registry = reader.GetString(2),
                            Rut = reader.GetString(3),
                            Type = reader.GetInt32(4)
                        });
                    }
                }
            }

            return guides;
        }

        // ✅ Atualizar guia existente
        public async Task<bool> UpdateGuideAsync(Guide guide)
        {
            using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
            {
                await connection.OpenAsync();

                string updateQuery = @"
            UPDATE guide
            SET name = @name,
                registry = @registry,
                rut = @rut,
                type = @type
            WHERE id = @id";

                using (var command = new SqliteCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@name", guide.Name);
                    command.Parameters.AddWithValue("@registry", guide.Registry);
                    command.Parameters.AddWithValue("@rut", guide.Rut);
                    command.Parameters.AddWithValue("@type", guide.Type);
                    command.Parameters.AddWithValue("@id", guide.Id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

    }
}
