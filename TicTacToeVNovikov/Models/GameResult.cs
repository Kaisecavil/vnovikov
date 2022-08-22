namespace TicTacToeVNovikov.Models
{
    /// <summary>
    /// Class that desrcibes entity of Game result
    /// </summary>
    internal class GameResult
    {
        public int GameResultId { get; private set; }
        public string GameStartTimestamp { get; private set; }
        public string GameFinishTimestamp { get; private set; }
        public int FirstPlayerId { get; private set; }
        public int SecondPlayerId { get; private set; }
        public char FirstPlayerMark { get; private set; }
        public char SecondPlayerMark { get; private set; }
        public int WinnerId { get; private set; }

        public GameResult(string gameStartTimestamp, string gameFinishTimestamp, int firstPlayerId, int secondPlayerId, char firstPlayerMark, char secondPlayerMark, int winnerId)
        {
            GameStartTimestamp = gameStartTimestamp;
            GameFinishTimestamp = gameFinishTimestamp;
            FirstPlayerId = firstPlayerId;
            SecondPlayerId = secondPlayerId;
            FirstPlayerMark = firstPlayerMark;
            SecondPlayerMark = secondPlayerMark;
            WinnerId = winnerId;
        }
    }
}
