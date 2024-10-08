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

    public Habit(string name, string description, int year, int month, int day)
    {
        Name = name;
        Description = description;
        CreationDate = new DateTime(year, month, day);
    }

    public override string ToString()
    {
        return $"{{\n  \"Id\": {Id},\n  \"Name\": \"{Name}\",\n  \"Description\": \"{Description}\",\n  \"CreationDate\": \"{CreationDate:yyyy-MM-ddTHH:mm:ss}\" \n}}";
    }
}