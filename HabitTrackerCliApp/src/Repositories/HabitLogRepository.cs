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

    public List<HabitLog> GetAllHabitLogs()
    {
        var habitLogs = new List<HabitLog>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"SELECT Id, HabitId, DidComplete, Date FROM HabitLogs";

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
                INSERT INTO HabitLogs (HabitId, DidComplete, Date)
                VALUES (@habitId, @didComplete, @date);";

        command.Parameters.AddWithValue("@habitId", habitLog.HabitId);
        command.Parameters.AddWithValue("@didComplete", habitLog.DidComplete);
        command.Parameters.AddWithValue("@date", habitLog.Date);

        command.ExecuteNonQuery();
    }

    public void CreateOrUpdateHabitLog(HabitLog habitLog)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var existingHabitLog = GetHabitLogByHabitIdAndDate(habitLog.HabitId, habitLog.Date);

        if (existingHabitLog == null)
        {
            CreateHabitLog(habitLog);
        }
        else
        {
            UpdateHabitLogById(existingHabitLog.Id, habitLog);

        }
    }

    public HabitLog? GetHabitLogById(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
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

    public HabitLog? GetHabitLogByHabitIdAndDate(int habitId, DateTime dateTime)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        
        using var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT Id, HabitId, DidComplete, Date
            FROM HabitLogs
            WHERE Habitid = @habitId AND DATE(Date) = DATE(@date)";

        command.Parameters.AddWithValue("@habitId", habitId);
        command.Parameters.AddWithValue("@date", dateTime);

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

    public void UpdateHabitLogById(int id, HabitLog habitLog)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        
        var command = connection.CreateCommand();
        command.CommandText = @"
            UPDATE HabitLogs
            SET HabitId = @habitId,
                DidComplete = @didComplete,
                Date = @date
            WHERE id = @id";

        command.Parameters.AddWithValue("@id", habitLog.Id);
        command.Parameters.AddWithValue("@habitId", habitLog.HabitId);
        command.Parameters.AddWithValue("@didComplete", habitLog.DidComplete);
        command.Parameters.AddWithValue("@date", habitLog.Date);

        command.ExecuteNonQuery();
    }

    public bool DeleteHabitLogById(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM HabitLogs WHERE id = @id";

        command.Parameters.AddWithValue("@id", id);

        var linesAffected = command.ExecuteNonQuery();

        return linesAffected > 0;
    }
    
    // Summary methods

    public int CountHabitLogsByHabitId(int habitId)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"SELECT COUNT(*) FROM HabitLogs WHERE HabitId = @habitId";

        command.Parameters.AddWithValue("@habitId", habitId);

        var result = command.ExecuteScalar();

        return result != null ? Convert.ToInt32(result) : 0;
    }
}