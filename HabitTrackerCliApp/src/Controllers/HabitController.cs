using HabitTrackerCliApp.Models;
using HabitTrackerCliApp.Repositories;
using HabitTrackerCliApp.Utils;

namespace HabitTrackerCliApp.Controllers;

public class HabitController
{
    private readonly HabitRepository _habitRepository;
    private const string ERROR_MESSAGE = "Invalid selection. Please choose a valid habit";

    public HabitController(HabitRepository habitRepository)
    {
        _habitRepository = habitRepository;
    }

    public void ViewAllHabits()
    {
        ConsoleUtils.DisplayHeader("HABITS");
        PrintHabitList();

        var selectedHabit = SelectHabit("Select a habit number: ");
        
        Console.WriteLine(Environment.NewLine);
        
        PerformOperationOnHabit(selectedHabit);
        
    }

    public void CreateHabit()
    {
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
        Console.WriteLine("Select an option");
        int operation;
        do
        {
            operation = UserInteractionUtils.GetIntFromUser(PrintHabitOptionsList);

            switch (operation)
            {
                case 1:
                    Console.WriteLine("Showing details for habit");
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

    private Habit? GetHabitFromList()
    {
        var habits = _habitRepository.GetAllHabits();

        var selectedValue = UserInteractionUtils.GetIntFromUser(PrintHabitList);

        if (selectedValue > 0 && selectedValue <= habits.Count)
        {
            var selectedHabit = habits[selectedValue - 1];
            return selectedHabit;
        }
        else
        {
            Console.WriteLine("Invalid selection.");
            return null;
        }
    }

    private Habit? SelectHabit(string headerMessage)
    {
        ConsoleUtils.DisplayHeader(headerMessage);

        Habit? selectedHabit;
        do
        {
            selectedHabit = GetHabitFromList();

            if (selectedHabit == null)
            {
                Console.WriteLine(ERROR_MESSAGE);
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