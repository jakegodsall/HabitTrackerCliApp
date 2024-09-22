using HabitTrackerCliApp.Database;
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
        

            var mainMenuView = new MainMenuView();
            
            mainMenuView.Run();
        }
    }
}