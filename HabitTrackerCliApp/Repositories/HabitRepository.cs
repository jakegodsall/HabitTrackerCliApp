using HabitTrackerCliApp.Models;
using Microsoft.Data.Sqlite;

namespace HabitTrackerCliApp.Repositories;

public class HabitRepository
{
    private readonly string _connectionString;

    public HabitRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void GetAllHabits()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Name, Description, CreationDate FROM Habits";
            
            
        }
    }

    public void CreateHabit(Habit habit)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Habits (Name, Description, CreationDate)
                VALUES (@name, @description, @creationDate);";

            command.Parameters.AddWithValue("@name", habit.Name);
            command.Parameters.AddWithValue("@description", habit.Description);
            command.Parameters.AddWithValue("@creationDate", habit.CreationDate);

            command.ExecuteNonQuery();
        }
    }
}