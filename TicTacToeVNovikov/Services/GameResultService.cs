using TicTacToeVNovikov.Models;

namespace TicTacToeVNovikov.Services
{
    internal static class GameResultService
    {
        /// <summary>
        /// Method that commits game result
        /// </summary>
        /// <param name="gameStartTime">Parameter that is necessary for commiting game result and contains game start time</param>
        /// <param name="indexOfWinner">Parameter that is necessary for commiting game result and contains sequence number of player that wins the game</param>
        /// <param name="playerList">List of players who take part in the game</param>
        public static void CommitGameResult(DateTime gameStartTime, int indexOfWinner, List<Player> playerList)
        {
            DbService.Unit.GameResults.Insert(new GameResult(gameStartTime.ToString("O"), DateTime.Now.ToString("O"), playerList[0].Id, playerList[1].Id, 'X', 'O', playerList[indexOfWinner - 1].Id));
            DbService.Unit.Commit();
        }
    }
}
