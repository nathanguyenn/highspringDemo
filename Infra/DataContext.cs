using Microsoft.Data.Sqlite;
using System.Data;

namespace WebApplication1.Infra
{
    public class DataContext
    {
        private readonly string _connectionString;

        public DataContext(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Default")!;
            InitDatabase();
        }

        public IDbConnection CreateConnection() => new SqliteConnection(_connectionString);

        private void InitDatabase()
        {
            using var connection = CreateConnection();
            connection.Open();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = @"
            CREATE TABLE IF NOT EXISTS Products (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Price REAL NOT NULL
            );";
            cmd.ExecuteNonQuery();
        }
    }
}
