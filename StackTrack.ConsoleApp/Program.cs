using Accounts;
using ConsoleUI;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography.X509Certificates;

namespace Program;

class Program
{
    static MainMenu mainMenu = new MainMenu();

    static void Main(string[] Args)
    {
        mainMenu.MainMenuDisplay();
    }
}