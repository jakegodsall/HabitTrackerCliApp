using HabitTrackerCliApp.Models;
using HabitTrackerCliApp.Repositories;
using HabitTrackerCliApp.Utils;

namespace HabitTrackerCliApp.Controllers;

public class HabitController
{
    private readonly HabitRepository _habitRepository;

    public HabitController(HabitRepository habitRepository)
    {
        _habitRepository = habitRepository;
    }

    public void ViewAllHabits()
    {
        ConsoleUtils.DisplayHeader("HABITS");
        PrintHabitList();
    }

    public void CreateHabit()
    {
        ConsoleUtils.DisplayHeader("CREATE HEADER");

        var name = UserInteractionUtils.GetTextualInputFromUser("name");
        var description = UserInteractionUtils.GetTextualInputFromUser("description");
        
        var habit = new Habit
        {
            Name = name,
            Description = description,
            CreationDate = DateTime.Now
        };
        
        Console.WriteLine(habit);

        _habitRepository.CreateHabit(habit);
        Console.WriteLine("Habit persisted");
    }

    public void DeleteHabit()
    {
        ConsoleUtils.DisplayHeader("DELETE A HABIT");

        var habits = _habitRepository.GetAllHabits();

        var selectedValue = UserInteractionUtils.GetIntFromUser(PrintHabitList) - 1;

        if (selectedValue > 0 && selectedValue <= habits.Count)
        {
            var selectedHabit = habits[selectedValue - 1];
            _habitRepository.DeleteHabitById(selectedHabit.Id);
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }
    }

    private void PrintHabitList()
    {
        var habits = _habitRepository.GetAllHabits();
        for (var i = 0; i < habits.Count; i++)
        {
            Console.WriteLine(ConsoleUtils.CreateListItem(i + 1, habits[i].Name));
        }
    }
}