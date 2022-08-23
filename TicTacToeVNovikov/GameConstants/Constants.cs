namespace TicTacToeVNovikov.GameConstants;
/// <summary>
/// This class contains game constants.
/// </summary>
internal static class Constants
{
    public static class GameLimits
    {
        public const int FieldSize = 3;
        public const int MaxNameLeangth = 25;
        public const int MinNameLeangth = 3;
        public const int PlayersCount = 2;
        public const int MaxMistakesCount = 3;
        public const string GameMarks = ".XO";
        public const int MaxPlayerAge = 90;
        public const int MinPlayerAge = 10;
    }

    public static class TextOfCommand
    {
        public const string Help = "/help";
        public const string GenerateLastGameResult = "/generatelastgameresult";
        public const string GenerateResultsForCurrentPlayers = "/generateresultsforcurrentplayers";
        public const string GenerateAllResults = "/generateallresults";
        public const string Skip = "/skip";
    }

    public static class FileName
    {
        public const string DownloadedFilesDirectoryName = "DownloadedFiles";
        public const string AllGameResultsFileName = "allGameResults.json";
        public const string LastGameResultFileName = "lastGameResult{0}.json";
        public const string GameResultsForCurrentPlayersFileName = "gameResultsForPlayers{0}and{1}.json";
    }

}

