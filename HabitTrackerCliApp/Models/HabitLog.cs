namespace HabitTrackerCliApp.Models;

public class HabitLog
{
    public int Id { get; set; }
    public int HabitId { get; set; }
    public bool DidComplete { get; set; }
    public DateTime Date { get; set; }
    
    public HabitLog() { }

    public HabitLog(int id, int habitId, bool didComplete, DateTime date)
    {
        Id = id;
        HabitId = habitId;
        DidComplete = didComplete;
        Date = date;
    }

    public HabitLog(int id, int habitId, bool didComplete, int year, int month, int day)
    {
        Id = id;
        HabitId = habitId;
        DidComplete = didComplete;
        Date = new DateTime(year, month, day);
    }
}