using HabitTrackerCliApp.Database;
using HabitTrackerCliApp.Models;
using HabitTrackerCliApp.Repositories;
using HabitTrackerCliApp.Views;

namespace HabitTrackerCliApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
        
            var databaseManager = new DatabaseManager();
            databaseManager.InitializeDatabase();
            Console.WriteLine("Created the database");

            var habit1 = new Habit("Press-ups", "Do 10 press-ups per day", 23, 09, 2024);
            var habit2 = new Habit("Spanish Vocabulary", "Learn 10 new Spanish words per day", 20, 09, 2024);

            var habitRepository = new HabitRepository("Data Source=habits.db");
            
            habitRepository.CreateHabit(habit1);
            habitRepository.CreateHabit(habit2);
            
            var habits = habitRepository.GetAllHabits();

            habitRepository.DeleteHabitById(2);
            
            foreach (var habit in habits)
            {
                Console.WriteLine("Habit: " + habit);
            }

            var mainMenuView = new MainMenuView();
            
            mainMenuView.Run();
        }
    }
}