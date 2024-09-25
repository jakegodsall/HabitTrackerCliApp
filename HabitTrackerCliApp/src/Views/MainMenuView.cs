using System.Runtime.InteropServices.JavaScript;
using HabitTrackerCliApp.Controllers;
using HabitTrackerCliApp.Repositories;
using HabitTrackerCliApp.Utils;

namespace HabitTrackerCliApp.Views;

public class MainMenuView
{
    public void Run()
    {
        ConsoleUtils.DisplayHeader("Habit Tracker Main Menu");
        var value = UserInteractionUtils.GetIntFromUser(DisplayOptions);

        var habitController = new HabitController(new HabitRepository("Data Source=habits.db"));
        
        switch (value)
        {
            case 1:
                habitController.ViewAllHabits();
                break;
            case 2:
                break;
            case 3:
                habitController.CreateHabit();
                break;
            case 4:
                habitController.DeleteHabit();
                break;
            case 5:
                break;
        }
    }

    private static void DisplayOptions()
    {
        Console.WriteLine(ConsoleUtils.CreateListItem(1, "View all habits"));
        Console.WriteLine(ConsoleUtils.CreateListItem(2, "Log daily habits"));
        Console.WriteLine(ConsoleUtils.CreateListItem(3, "Add a new habit"));
        Console.WriteLine(ConsoleUtils.CreateListItem(4, "Delete a habit"));
        Console.WriteLine(ConsoleUtils.CreateListItem(5, "Exit"));
        Console.Write("\nPlease select an option: ");
    }
}