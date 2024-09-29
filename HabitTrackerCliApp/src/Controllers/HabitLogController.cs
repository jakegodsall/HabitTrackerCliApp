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
}