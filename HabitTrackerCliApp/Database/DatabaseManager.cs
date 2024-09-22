namespace HabitTrackerCliApp.Database;
using Microsoft.Data.Sqlite;
public class DatabaseManager
{
    private string _connectionString = "Data Source=habits.db";

    public void InitializeDatabase()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            var createHabitsTable = @"
                CREATE TABLE IF NOT EXISTS Habits (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Description TEXT,
                    CreationDate TEXT
                )";

            using (var command = new SqliteCommand(createHabitsTable, connection))
            {
                command.ExecuteNonQuery();
            }

            var createHabitLogsTable = @"
                CREATE TABLE IF NOT EXISTS HabitLogs (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    HabitId INTEGER NOT NULL,
                    DATE TEXT NOT NULL,
                    DidComplete BOOLEAN NOT NULL,
                    FOREIGN KEY (HabitId) REFERENCES Habits (Id)
                );";
            
            using (var command = new SqliteCommand(createHabitLogsTable, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}