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
        

        var habitController = new HabitController(new HabitRepository("Data Source=habits.db"));

        var value = -1;
        do
        {
            value = UserInteractionUtils.GetIntFromUser(DisplayOptions);

            switch (value)
            {
                case 1:
                    habitController.ViewAllHabits();
                    break;
                case 2:
                    break;
                // case 3:
                //     habitController.CreateHabit();
                //     break;
                // case 4:
                //     habitController.UpdateHabit();
                //     break;
                // case 5:
                //     habitController.DeleteHabit();
                //     break;
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
        Console.WriteLine(ConsoleUtils.CreateListItem(4, "Update a habit"));
        Console.WriteLine(ConsoleUtils.CreateListItem(5, "Delete a habit"));
        Console.WriteLine(ConsoleUtils.CreateListItem(6, "Exit"));
        Console.Write("\nPlease select an option: ");
    }
}