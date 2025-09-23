using StackTrack.ConsoleApp.UI;

namespace StackTrack.ConsoleApp.UI.Implementations;

public class ConsoleService : IConsoleService
{
    public void WriteLine(string message) => Console.WriteLine(message);
    public void WriteLine() => Console.WriteLine();
    public void Write(string message) => Console.Write(message);
    public string ReadLine() => Console.ReadLine() ?? string.Empty;
    public ConsoleKeyInfo ReadKey() => Console.ReadKey();
    public void Clear() => Console.Clear();

    public void WriteError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"ERROR: {message}");
        Console.ResetColor();
    }

    public void WriteSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"SUCCESS: {message}");
        Console.ResetColor();
    }

    public void WriteWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"WARNING: {message}");
        Console.ResetColor();
    }

    public void WriteTitle(string title)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        WriteLine($"\n=== {title} ===");
        Console.ResetColor();
    }

    public void WriteMenu(IEnumerable<string> options)
    {
        var optionList = options.ToList();
        for (int i = 0; i < optionList.Count; i++)
        {
            WriteLine($"{i + 1}. {optionList[i]}");
        }
        WriteLine();
    }

    public int GetMenuChoice(int maxOption)
    {
        while (true)
        {
            Write($"Enter your choice (1-{maxOption}): ");
            if (int.TryParse(ReadLine(), out int choice) && choice >= 1 && choice <= maxOption)
            {
                return choice;
            }
            WriteError($"Please enter a number between 1 and {maxOption}");
        }
    }

    public bool GetYesNo(string question)
    {
        while (true)
        {
            Write($"{question} (y/n): ");
            var input = ReadLine().ToLower().Trim();
            if (input == "y" || input == "yes")
                return true;
            if (input == "n" || input == "no")
                return false;
            WriteError("Please enter 'y' for yes or 'n' for no");
        }
    }

    public string GetInput(string prompt, bool required = true)
    {
        while (true)
        {
            Write($"{prompt}: ");
            var input = ReadLine().Trim();
            
            if (!required || !string.IsNullOrWhiteSpace(input))
                return input;
                
            WriteError("This field is required");
        }
    }

    public string GetPassword(string prompt = "Password: ")
    {
        Write(prompt);
        var password = "";
        while (true)
        {
            var key = ReadKey();
            if (key.Key == ConsoleKey.Enter)
            {
                WriteLine();
                break;
            }
            else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password[..^1];
                Write("\b \b");
            }
            else if (!char.IsControl(key.KeyChar))
            {
                password += key.KeyChar;
                Write("*");
            }
        }
        return password;
    }
}