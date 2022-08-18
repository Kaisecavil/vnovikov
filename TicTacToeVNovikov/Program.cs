
namespace TicTacToeVNovikov;
internal class Program
{
    static void Main(string[] args)
    {
        //CommandLine.AskForCommand();
        //return;
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