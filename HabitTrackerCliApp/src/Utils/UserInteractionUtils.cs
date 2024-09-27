namespace HabitTrackerCliApp.Utils;

public static class UserInteractionUtils
{
    public static int GetIntFromUser(Action displayOptions)
    {
        displayOptions();
        var success = false;
        var number = 1;
        do
        {
            var userInput = Console.ReadLine();

            success = int.TryParse(userInput, out number);

            if (!success)
            {
                Console.WriteLine($"Invalid input. Please choose a valid option.{Environment.NewLine}");
                displayOptions();
            }
        } while (!success);

        return number;
    }

    public static string GetTextualInputFromUser(string field)
    {
        bool success;
        string input;
        do
        {
            Console.Write($"Enter a value for {field}: ");
            input = Console.ReadLine();
            success = !string.IsNullOrEmpty(input);

            if (!success)
            {
                Console.WriteLine($"{StringUtils.CapitaliseString(field)} cannot be empty. Please enter a valid {field}:");
            }
        } while (!success);

        return input;
    }

    public static bool GetBoolInputFromUser(string question)
    {
        var success = false;
        string input;
        do
        {
            Console.Write(question + " (y/n)");

            input = Console.ReadLine();

            switch (input)
            {
                case "y":
                case "Y":
                    return true;
                case "N":
                case "n":
                    return false;
            }
        } while (true);
    }
}