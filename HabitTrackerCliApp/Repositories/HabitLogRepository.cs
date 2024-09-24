using HabitTrackerCliApp.Models;
using Microsoft.Data.Sqlite;

namespace HabitTrackerCliApp.Repositories;

public class HabitLogRepository
{
    private readonly string _connectionString;

    public HabitLogRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<HabitLog> GetAllHabitLogsByHabitId(int habitId)
    {
        var habitLogs = new List<HabitLog>();
        
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT Id, HabitId, DidComplete, Date
            FROM HabitLogs
            WHERE HabitId = @habitId";

        command.Parameters.AddWithValue("@habitId", habitId);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            habitLogs.Add(new HabitLog(
                reader.GetInt32(0),
                reader.GetInt32(1),
                reader.GetBoolean(2),
                reader.GetDateTime(3)
            ));
        }

        return habitLogs;
    }

    public void CreateHabitLog(HabitLog habitLog)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
            
        var command = connection.CreateCommand();
        command.CommandText = @"
                INSERT INTO HabitLogs (Id, HabitId, DidComplete, Date)
                VALUES (@id, @habitId, @didComplete, @date);";

        command.Parameters.AddWithValue("@id", habitLog.Id);
        command.Parameters.AddWithValue("@habitId", habitLog.HabitId);
        command.Parameters.AddWithValue("@didComplete", habitLog.DidComplete);
        command.Parameters.AddWithValue("@date", habitLog.Date);

        command.ExecuteNonQuery();
    }

    public HabitLog? GetHabitLogById(int id)
    {
        using var connection = new SqliteConnection(_connectionString)
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT Id, HabitId, DidComplete, Date
            FROM HabitLogs
            WHERE id = @id";

        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new HabitLog(
                reader.GetInt32(0),
                reader.GetInt32(1),
                reader.GetBoolean(2),
                reader.GetDateTime(3)
            );
        }
        else
        {
            return null;
        }
    }
}