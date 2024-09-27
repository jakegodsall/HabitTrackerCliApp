namespace HabitTrackerCliApp.Utils;

public static class ConsoleUtils
{
    public static string CreateListItem(int index, string value)
    {
        if (!StringUtils.IsFinalCharacterFullStop(value)) value += ".";
        return $"\u2192 {index}. {value}";
    }
    
    public static void DisplayHeader(string title)
    {
        var result = title.PadLeft(title.Length + 2).PadRight(title.Length + 4);
        var bar = new string('=', result.Length);
        Console.WriteLine(bar);
        Console.WriteLine(result);
        Console.WriteLine(bar);
    }
    
   
}