using System.Text.RegularExpressions;

namespace TicTacToeVNovikov.GameConstants;
/// <summary>
/// This class contains game constants.
/// </summary>
internal static class Constants
{
    /// <summary>
    /// inner class of <see cref="Constants"/> contains game limit constants
    /// </summary>
    public static class GameLimits
    {
        public const int FieldSize = 3;
        public const int MaxNameLeangth = 25;
        public const int MinNameLeangth = 3;
        public const int PlayersCount = 2;
        public const int MaxMistakesCount = 3;
        public const int MaxPlayerAge = 90;
        public const int MinPlayerAge = 10;

    }

    /// <summary>
    /// inner class of <see cref="Constants"/> contains Text Of Commandline commands constants
    /// </summary>
    public static class TextOfCommand
    {
        public const string Help = "/help";
        public const string GenerateLastGameResult = "/generatelastgameresult";
        public const string GenerateResultsForCurrentPlayers = "/generateresultsforcurrentplayers";
        public const string GenerateAllResults = "/generateallresults";
        public const string Skip = "/skip";
    }

    /// <summary>
    /// inner class of <see cref="Constants"/> contains File Name constants
    /// </summary>
    public static class FileName
    {
        public const string DownloadedFilesDirectoryName = "DownloadedFiles";
        public const string AllGameResultsFileName = "allGameResults.json";
        public const string LastGameResultFileName = "lastGameResult{0}.json";
        public const string GameResultsForCurrentPlayersFileName = "gameResultsForPlayers{0}and{1}.json";
    }

    /// <summary>
    /// inner class of <see cref="Constants"/> contains Game String constants
    /// </summary>
    public static class GameStrings
    {
        public const string GameMarks = ".XO";
        public const string FieldRowDelimeter = "-----";
        public const string FieldColumnDelimeter = "|";
        public const string AvailableLocalizations = "en-US,de-DE,ru-RU";
        public const char AvailableLocalizationsDelimeter = ',';
    }

    /// <summary>
    /// inner class of <see cref="Constants"/> contains Table Parameters constants
    /// </summary>
    public static class TableParameters
    {
        public const char PlayersTableRowDelimeter = '-';
        public const string PlayersTableColumnDelimeter = "|";
        public const int PlayersTableAgeColumnWidth = 10;
        public const int PlayersTableIdColumnWidth = 4;
    }

    /// <summary>
    /// inner class of <see cref="Constants"/> contains Regular Expressions constants
    /// </summary>
    public static class GameRegularExpressions
    {
        public const string WightSpaces = @"\s+";
        public const string Registered = @"^\d+\s\w+\s\d{1,3}$";
        public const string Unregistered = @"^\w+\s\d{1,3}$";
        public const string Turn = @"^\d \d$";


    }

}

