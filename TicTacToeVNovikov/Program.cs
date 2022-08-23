using TicTacToeVNovikov.GameConstants;

namespace TicTacToeVNovikov;

internal class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            if (Game.AskForNewGame())
            {
                Game game = new Game(Constants.GameLimits.FieldSize,Constants.GameLimits.PlayersCount,Constants.GameLimits.MaxMistakesCount,Constants.GameStrings.GameMarks);
                game.Startgame();
            }
            else { break; }
        }
    }
}