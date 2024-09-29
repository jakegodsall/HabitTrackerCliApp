using System.Data;
using HabitTrackerCliApp.Models;
using HabitTrackerCliApp.Repositories;
using HabitTrackerCliApp.Utils;

namespace HabitTrackerCliApp.Controllers;

public class HabitController
{
    private readonly HabitLogController _habitLogController;
    private readonly HabitRepository _habitRepository;
    private const string ErrorMessage = "Invalid selection. Please choose a valid habit";

    public HabitController(
        HabitLogController habitLogController,
        HabitRepository habitRepository)
    {
        _habitLogController = habitLogController;
        _habitRepository = habitRepository;
    }

    public void ViewAllHabits()
    {
        Console.Clear();
        ConsoleUtils.DisplayHeader("HABITS");

        var selectedHabit = SelectHabit("");
        
        Console.WriteLine(Environment.NewLine); 
        
        PerformOperationOnHabit(selectedHabit);
        
    }

    public void ShowHabitDetails(Habit habit)
    {
        
        
        Console.Clear();
        ConsoleUtils.DisplayHeader($"HABIT: {habit.Name}");
        Console.WriteLine(Environment.NewLine);
            
        Console.WriteLine("Description:");
        Console.WriteLine(habit.Description);
        
        Console.WriteLine("Total Days Tracked: ");
        Console.WriteLine(_habitLogController.CountHabitLogsByHabitId(habit.Id));
        
        Console.WriteLine("Current Streak: ");
        Console.WriteLine(_habitLogController.CalculateCurrentStreakByHabitId(habit.Id));
        
        Console.WriteLine("Longest Streak: ");
        
        Console.WriteLine("Tracking since: ");

        Console.WriteLine("Success %: ");

        Console.WriteLine(Environment.NewLine);
        Console.WriteLine("Press any key to go back to the previous menu.");
        Console.ReadKey();
    }

    public void CreateHabit()
    {
        Console.Clear();
        ConsoleUtils.DisplayHeader("CREATE HABIT");

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
    }

    public void UpdateHabit(Habit habit)
    {
        var newName = UserInteractionUtils.GetTextualInputFromUser("new name");
        var newDescription = UserInteractionUtils.GetTextualInputFromUser("new description");

        _habitRepository.UpdateHabitById(habit.Id, new Habit()
        {
            Name = newName,
            Description = newDescription
        });
    }

    public void DeleteHabit(Habit habit)
    {
        _habitRepository.DeleteHabitById(habit.Id);
    }

    private void PerformOperationOnHabit(Habit habit)
    {
        Console.Clear();
        ConsoleUtils.DisplayHeader($"HABIT: {habit.Name}");
        Console.WriteLine(Environment.NewLine + "Select an option");
        int operation;
        do
        {
            operation = UserInteractionUtils.GetIntFromUser(PrintHabitOptionsList);

            switch (operation)
            {
                case 1:
                    Console.WriteLine("Showing details for habit");
                    ShowHabitDetails(habit);
                    break;
                case 2:
                    UpdateHabit(habit);
                    return;
                case 3:
                    DeleteHabit(habit);
                    return;
                case 4:
                    return;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
           
        } while (operation != 4);
    }

    public void LogHabits()
    {
        Console.Clear();

        var habits = _habitRepository.GetAllHabits();

        foreach (var habit in habits)
        {
            _habitLogController.CreateHabitLog(habit);
        }
    }

    private Habit? SelectHabit(string headerMessage)
    {
        Habit? selectedHabit;
        do
        {
            var habits = _habitRepository.GetAllHabits();
            
            Console.WriteLine(Environment.NewLine + "Select a habit to view options:");

            var selectedValue = UserInteractionUtils.GetIntFromUser(PrintHabitList);

            if (selectedValue > 0 && selectedValue <= habits.Count)
            {
                selectedHabit = habits[selectedValue - 1];
            }
            else
            {
                Console.WriteLine("Invalid selection.");
                selectedHabit = null;
            }

            if (selectedHabit == null)
            {
                Console.WriteLine(ErrorMessage);
            }
        } while (selectedHabit == null);

        return selectedHabit;
    }

    private void PrintHabitList()
    {
        var habits = _habitRepository.GetAllHabits();
        for (var i = 0; i < habits.Count; i++)
        {
            Console.WriteLine(ConsoleUtils.CreateListItem(i + 1, habits[i].Name));
        }
    }

    private void PrintHabitOptionsList()
    {
        string[] options = ["Show details", "Update", "Delete", "Go back to main menu"];
        for (var i = 0; i < options.Length; i++)
        {
            Console.WriteLine(ConsoleUtils.CreateListItem(i + 1, options[i]));
        }
    }
}