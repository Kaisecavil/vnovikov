
using System.Reflection;
using TicTacToeVNovikov.Resources;
using System.Resources;
using System.Globalization;

namespace TicTacToeVNovikov;
internal class Program
{
    
    
    static void Main(string[] args)
    {
        while (true)
        {
            CultureInfo.CurrentUICulture = new CultureInfo("en-US");
            Console.WriteLine(CultureInfo.CurrentUICulture.Name);
            
            if (Game.AskForNewGame())
            {
                Game game = new Game();
                game.Startgame();
            }
            else { break; }
        }
    }
}