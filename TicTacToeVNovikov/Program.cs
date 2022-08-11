
namespace TicTacToeVNovikov
{
    internal class Program
    {   
        static void Main(string[] args)
        {
            while (true)
            {
                Game game = new Game();
                if (game.AskForNewGame())
                {
                    game.Startgame();
                }
                else { break; }
            }
        }
    }
}