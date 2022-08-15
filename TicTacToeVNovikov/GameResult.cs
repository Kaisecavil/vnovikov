namespace TicTacToeVNovikov
{
    internal class GameResult
    {
        private int GameResultId { get; set; }
        private string GameStartTimestamp { get; set; }
        private string GameFinishTimestamp { get; set; }
        private int FirstPlayerId { get; set; }
        private int SecondPlayerId { get; set; }
        private char FirstPlayerMark { get; set; }
        private char SecondPlayerMark { get; set; }
        private int WinnerId { get; set; }

        public GameResult(int gameResultId, string gameStartTimestamp, string gameFinishTimestamp, int firstPlayerId, int secondPlayerId, char firstPlayerMark, char secondPlayerMark, int winnerId)
        {
            GameResultId = gameResultId;
            GameStartTimestamp = gameStartTimestamp;
            GameFinishTimestamp = gameFinishTimestamp;
            FirstPlayerId = firstPlayerId;
            SecondPlayerId = secondPlayerId;
            FirstPlayerMark = firstPlayerMark;
            SecondPlayerMark = secondPlayerMark;
            WinnerId = winnerId;
        }

        private void SubmitGameRusult()
        {

        }
    }
}
