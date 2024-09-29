using HabitTrackerCliApp.Models;
using HabitTrackerCliApp.Repositories;
using HabitTrackerCliApp.Utils;

namespace HabitTrackerCliApp.Controllers;

public class HabitLogController
{
    private readonly HabitLogRepository _habitLogRepository;

    public HabitLogController(HabitLogRepository habitLogRepository)
    {
        _habitLogRepository = habitLogRepository;
    }
    
    public void CreateHabitLog(Habit habit)
    {
        Console.WriteLine($"Habit: {habit.Name}");
        var completed = UserInteractionUtils.GetBoolInputFromUser("Did you complete this today?");
        
        _habitLogRepository.CreateOrUpdateHabitLog(new HabitLog()
        {
            HabitId = habit.Id,
            Date = DateTime.Now,
            DidComplete = completed
        });
    }

    public int CountHabitLogsByHabitId(int habitId)
    {
        return _habitLogRepository.CountHabitLogsByHabitId(habitId);
    }
    
    public int CalculateCurrentStreakByHabitId(int habitId)
    {
        var habitLogs = _habitLogRepository.GetAllHabitLogsByHabitId(habitId);
    
        // Check if there are no logs
        if (habitLogs.Count == 0) return 0;
    
        // Sort the logs by date in descending order
        var sortedLogs = habitLogs.OrderByDescending(log => log.Date).ToList();
    
        // Ensure the most recent log is today and DidComplete is true
        var mostRecent = sortedLogs.First();
        if (mostRecent.Date.Date != DateTime.Today || !mostRecent.DidComplete) return 0;

        // Calculate streak
        var streak = 1;  // Starting with 1 because today is completed
        var previousDate = mostRecent.Date.Date;

        // Loop through the logs from second most recent to oldest
        for (int i = 1; i < sortedLogs.Count; i++)
        {
            var log = sortedLogs[i];
        
            // Check if the habit was completed on this day
            if (log.DidComplete)
            {
                // Check if this date is exactly one day before the previous date
                if (previousDate.AddDays(-1) == log.Date.Date)
                {
                    streak++;
                    previousDate = log.Date.Date;  // Update previous date to this log's date
                }
                else
                {
                    // Break if there is a gap in the streak
                    break;
                }
            }
            else
            {
                // Break if the habit was not completed
                break;
            }
        }

        return streak;
    }

    public int CalculateLongestStreakByHabitId(int habitId)
    {
        var habitLogs = _habitLogRepository.GetAllHabitLogsByHabitId(habitId);

        // If no logs exist, return 0
        if (habitLogs.Count == 0) return 0;
    
        // Sort the logs by date in ascending order
        var sortedLogs = habitLogs.OrderBy(log => log.Date).ToList();

        var longestStreak = 0;
        var currentStreak = 0;
        DateTime? previousDate = null;

        // Iterate through the logs
        foreach (var log in sortedLogs)
        {
            if (log.DidComplete)
            {
                if (previousDate == null)
                {
                    // If this is the first log, start the streak
                    currentStreak = 1;
                }
                else
                {
                    // Check if this date is exactly one day after the previous log
                    if (log.Date.Date == previousDate.Value.AddDays(1))
                    {
                        currentStreak++;
                    }
                    else
                    {
                        // If there's a gap, compare and reset the streak
                        longestStreak = Math.Max(longestStreak, currentStreak);
                        currentStreak = 1; // Start a new streak
                    }
                }
            
                // Update the previous date
                previousDate = log.Date.Date;
            }
            else
            {
                // If DidComplete is false, compare and reset the streak
                longestStreak = Math.Max(longestStreak, currentStreak);
                currentStreak = 0; // Reset streak
                previousDate = null;  // Reset previous date
            }
        }

        // Check the last streak
        longestStreak = Math.Max(longestStreak, currentStreak);

        return longestStreak;
    }
    
    public DateTime GetDateOfFirstHabitLogByHabitId(int habitId)
    {
        var logs = _habitLogRepository.GetAllHabitLogsByHabitId(habitId);
        var sortedLogs = logs.OrderBy(log => log.Date).ToList();
        return sortedLogs.First().Date;
    }

    public double CalculateProportionOfSuccessByHabitId(int habitId)
    {
        var logs = _habitLogRepository.GetAllHabitLogsByHabitId(habitId);
        
        // account for empty logs
        if (logs.Count == 0) return 0;
        
        var success = logs.Count(log => log.DidComplete);
        var total = logs.Count;

        return (double)success / total * 100;
    }
}