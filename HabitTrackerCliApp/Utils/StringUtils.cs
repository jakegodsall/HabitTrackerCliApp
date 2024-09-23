namespace HabitTrackerCliApp.Utils;

public class StringUtils
{
    public static string CapitaliseString(string s)
    {
        return char.ToUpper(s[0]) + s.Substring(1);
    }
}