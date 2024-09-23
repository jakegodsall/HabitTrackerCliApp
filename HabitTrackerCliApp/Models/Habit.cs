namespace HabitTrackerCliApp.Models;

public class Habit
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
    
    public Habit() {}

    public Habit(string name, string description, DateTime creationDate)
    {
        Name = name;
        Description = description;
        CreationDate = creationDate;
    }

    public Habit(string name, string description, int day, int month, int year)
    {
        Name = name;
        Description = description;
        CreationDate = new DateTime(year, month, day);
    }

    public override string ToString()
    {
        return Name;
    }
}