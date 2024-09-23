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
        
            // var databaseManager = new DatabaseManager();
            // databaseManager.InitializeDatabase();
            
            var mainMenuView = new MainMenuView();
            
            mainMenuView.Run();
        }
    }
}