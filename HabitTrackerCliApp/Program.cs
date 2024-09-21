using HabitTrackerCliApp.Views;

namespace HabitTrackerCliApp
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var mainMenuView = new MainMenuView();
            
            mainMenuView.Run();
        }
    }
}