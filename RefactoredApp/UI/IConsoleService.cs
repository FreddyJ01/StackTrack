namespace StackTrack.RefactoredApp.UI;

public interface IConsoleService
{
    void WriteLine(string message);
    void WriteLine();
    void Write(string message);
    string ReadLine();
    ConsoleKeyInfo ReadKey();
    void Clear();
    void WriteError(string message);
    void WriteSuccess(string message);
    void WriteWarning(string message);
    void WriteTitle(string title);
    void WriteMenu(IEnumerable<string> options);
    int GetMenuChoice(int maxOption);
    bool GetYesNo(string question);
    string GetInput(string prompt, bool required = true);
    string GetPassword(string prompt = "Password: ");
}