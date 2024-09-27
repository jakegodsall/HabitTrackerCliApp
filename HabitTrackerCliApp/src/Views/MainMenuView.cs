using System.Runtime.InteropServices.JavaScript;
using HabitTrackerCliApp.Controllers;
using HabitTrackerCliApp.Repositories;
using HabitTrackerCliApp.Utils;

namespace HabitTrackerCliApp.Views;

public class MainMenuView
{
    public void Run()
    {
        var dataString = "Data Source=habits.db";
        var habitController = new HabitController(new HabitLogController(new HabitLogRepository(dataString)), new HabitRepository(dataString));

        var value = -1;
        do
        {
            Console.Clear();
            ConsoleUtils.DisplayHeader("Habit Tracker Main Menu");
            
            value = UserInteractionUtils.GetIntFromUser(DisplayOptions);

            switch (value)
            {
                case 1:
                    habitController.ViewAllHabits();
                    break;
                case 2:
                    habitController.LogHabits();
                    break;
                case 3:
                    habitController.CreateHabit();
                    break;
                case 6:
                    Console.WriteLine("Goodbye.");
                    break;
            }
        } while (value != 6);

    }

    private static void DisplayOptions()
    {
        Console.WriteLine(ConsoleUtils.CreateListItem(1, "View all habits"));
        Console.WriteLine(ConsoleUtils.CreateListItem(2, "Log daily habits"));
        Console.WriteLine(ConsoleUtils.CreateListItem(3, "Add a new habit"));
        Console.WriteLine(ConsoleUtils.CreateListItem(6, "Exit"));
        Console.Write("\nPlease select an option: ");
    }
}