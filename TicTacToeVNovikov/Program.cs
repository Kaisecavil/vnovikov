
using System.Reflection;
using TicTacToeVNovikov.Resources;
using System.Resources;
using System.Globalization;
using System.Text;

namespace TicTacToeVNovikov;
internal class Program
{
    
    
    static void Main(string[] args)
    {
        while (true)
        {
            if (Game.AskForNewGame())
            {
                Game game = new Game();
                game.Startgame();
            }
            else { break; }
        }
    }
}