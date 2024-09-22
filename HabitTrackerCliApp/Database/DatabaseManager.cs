namespace HabitTrackerCliApp.Database
{
    using Microsoft.Data.Sqlite;
    using System;

    public class DatabaseManager
    {
        private readonly string _connectionString = "Data Source=habits.db";

        public void InitializeDatabase()
        {
            try
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();

                    var foreignKeyCommand = connection.CreateCommand();
                    foreignKeyCommand.CommandText = "PRAGMA foreign_keys = ON;";
                    foreignKeyCommand.ExecuteNonQuery();

                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Habits (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,
                            Description TEXT,
                            CreationDate TEXT DEFAULT CURRENT_TIMESTAMP
                        )";
                    command.ExecuteNonQuery();
                    
                    Console.WriteLine("FIRST TABLE CREATION EXECUTED");

                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS HabitLogs (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            HabitId INTEGER NOT NULL,
                            DATE TEXT NOT NULL,
                            DidComplete INTEGER NOT NULL,
                            FOREIGN KEY (HabitId) REFERENCES Habits (Id)
                        );";
                    command.ExecuteNonQuery();
                    
                    Console.WriteLine("SECOND TABLE CREATION EXECUTED");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while initializing the database: {ex.Message}");
            }
        }
    }
}