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
}