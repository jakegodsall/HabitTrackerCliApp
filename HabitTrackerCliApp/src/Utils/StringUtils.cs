namespace HabitTrackerCliApp.Utils;

public class StringUtils
{
    public static string CapitaliseString(string s)
    {
        return char.ToUpper(s[0]) + s.Substring(1);
    }

    public static bool IsFinalCharacterFullStop(string s)
    {
        if (string.IsNullOrEmpty(s)) return false;
        return s[^1] == '.';
    }

    public static string FormatDoubleToPercentageString(double d)
    {
        return d.ToString("F0") + "%";
    }
}