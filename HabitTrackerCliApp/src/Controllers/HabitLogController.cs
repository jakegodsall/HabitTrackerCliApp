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
}