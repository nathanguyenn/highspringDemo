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
            -- Create the Categories table
            CREATE TABLE IF NOT EXISTS Categories (
                Id INTEGER PRIMARY KEY,
                Name TEXT NOT NULL UNIQUE
            );

            -- Seed the Categories table with predefined values
            INSERT OR IGNORE INTO Categories (Id, Name) VALUES
                (1, 'General'),
                (2, 'Technology'),
                (3, 'Random');

            -- Create the BlogPosts table
            CREATE TABLE IF NOT EXISTS BlogPosts (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Title TEXT NOT NULL,
                Contents TEXT NOT NULL,
                Timestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
                CategoryId INTEGER NOT NULL,
                FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
            );

            -- Create dummy data for blogs
            INSERT OR IGNORE INTO BlogPosts (Title, Contents, Timestamp, CategoryId) VALUES
            ('Welcome to the Blog', 'This is the very first post on our brand new blog. Stay tuned for more!', '2025-01-01 10:00:00', 1),
            ('Why .NET 7 is Awesome', 'Let''s explore some of the exciting new features in .NET 7.', '2025-02-15 14:30:00', 2),
            ('A Random Thought', 'Ever wonder why cats knock stuff off tables? Just random musings here.', '2025-03-20 08:45:00', 3),
            ('Tech Trends in 2025', 'AI, WebAssembly, and quantum computing are shaping the future.', '2025-04-05 12:00:00', 2),
            ('General Updates', 'We''ve updated our site layout and improved mobile responsiveness.', '2025-04-10 09:15:00', 1),
            ('The Sandwich Debate', 'Is a hot dog a sandwich? We dive into this vital question.', '2025-05-01 11:00:00', 3);
            ";

            cmd.ExecuteNonQuery();
        }
    }
}
