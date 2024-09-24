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

    public List<Habit> GetAllHabits()
    {
        var habits = new List<Habit>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Name, Description, CreationDate FROM Habits";


        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var habit = new Habit
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Description = reader.GetString(2),
                CreationDate = reader.GetDateTime(3)
            };
                
            habits.Add(habit);
        }

        return habits;
    }

    public void CreateHabit(Habit habit)
    {
        using var connection = new SqliteConnection(_connectionString);
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

    public Habit? GetHabitById(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Name, Description, Habits.CreationDate FROM Habits WHERE Id = @id";

        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Habit
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Description = reader.GetString(2),
                CreationDate = reader.GetDateTime(3)
            };
        }
        else
        {
            return null;
        }
    }

    public void UpdateHabitById(int id, Habit habit)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
                UPDATE Habits
                SET Name = @Name,
                    Description = @Description,
                    CreationDate = @CreationDate
                WHERE Id = @Id";

        command.Parameters.AddWithValue("@Id", id);
        command.Parameters.AddWithValue("@Name", habit.Name);
        command.Parameters.AddWithValue("@Description", habit.Description);
        command.Parameters.AddWithValue("@CreationDate", habit.CreationDate);

        command.ExecuteNonQuery();
    }
    
    public bool DeleteHabitById(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Habits WHERE id = @id";

        command.Parameters.AddWithValue("@id", id);

        var rowsEffected = command.ExecuteNonQuery();

        return rowsEffected > 0;
    }
}