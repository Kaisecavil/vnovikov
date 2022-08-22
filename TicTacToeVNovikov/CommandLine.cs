using System.Text.Json;
using TicTacToeVNovikov.Resources;
using TicTacToeVNovikov.Models;
using TicTacToeVNovikov.Services;
using TicTacToeVNovikov.GameConstants;

namespace TicTacToeVNovikov
{
    /// <summary>
    /// That class is responsible for all actions with commandline.
    /// </summary>
    static internal class CommandLine
    {

        static private Dictionary<string, Action> _commandAction = new Dictionary<string, Action>()
        {
            {"/help", Help},
            {"/generatelastgameresult", GenerateLastGameResult},
            {"/generateresultsforcurrentplayers", GenerateResultsForCurrentPlayers},
            {"/generateallresults", GenerateAllResults}
        };

        static private Dictionary<string, string> _commandDescription = new Dictionary<string, string>()
        {
            {"/help",Strings.HelpDescription},
            {"/generatelastgameresult",Strings.GenerateLastGameResultDescription},
            {"/generateresultsforcurrentplayers",Strings.GenerateResultsForCurrentPlayersDescription},
            {"/generateallresults",Strings.GenerateAllResultsDescription},
            {"/skip",Strings.SkipDescription}
        };
        /// <summary>
        /// A method that prompts the user to enter a command.
        /// </summary>
        static public void AskForCommand()
        {
            Help();
            Console.WriteLine(Strings.AskForCommand);
            string? command = Console.ReadLine();
            while (command != "/skip")
            {
                try
                {
                    _commandAction[command].Invoke();
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine(Strings.UnknownCommand, command);
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine(Strings.NullCommand);
                }
                Console.Write(Strings.AskForCommand);
                command = Console.ReadLine();

            }
        }
        /// <summary>
        /// show all available command and theirs description
        /// </summary>
        private static void Help()
        {
            foreach (var pair in _commandDescription)
            {
                Console.WriteLine($"{pair.Key} - {pair.Value}");
            }
        }
        /// <summary>
        /// generate file-report in json format with last game result
        /// </summary>
        private static void GenerateLastGameResult()
        {
            GameResult gameResult = DbService.Unit.GameResults.GetLast();
            string json = JsonSerializer.Serialize(gameResult);
            try
            {
                var directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                string folderPath = Path.Combine(directory.Parent.Parent.Parent.ToString(), Constants.DownloadedFilesDirectoryName);
                string dateTime = DateTime.Now.ToString().Replace(':', '_').Replace('.', '_');
                string path = Path.Combine(folderPath, dateTime + '_' + string.Format(Constants.LastGameResultFileName, gameResult.GameResultId));
                FileStream fileStream = File.Open(path, FileMode.Create, FileAccess.Write);
                StreamWriter fileWriter = new StreamWriter(fileStream);
                fileWriter.WriteLine(json);
                fileWriter.Flush();
                fileWriter.Close();
                Console.WriteLine(Strings.CommandExecuted, dateTime + '_' + string.Format(Constants.LastGameResultFileName, gameResult.GameResultId), path);
            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe);
            }

        }
        /// <summary>
        /// show all games where two current players take part together
        /// </summary>
        private static void GenerateResultsForCurrentPlayers()
        {

            string json = "";
            GameResult gameResult = DbService.Unit.GameResults.GetLast();
            int id1 = gameResult.FirstPlayerId < gameResult.SecondPlayerId ? gameResult.FirstPlayerId : gameResult.SecondPlayerId;//In order to avoid situation where we have two identical filese but with names like "GameResultsBetween1and2.json" and "GameResultsBetween2and1.json";
            int id2 = gameResult.FirstPlayerId < gameResult.SecondPlayerId ? gameResult.SecondPlayerId : gameResult.FirstPlayerId;
            var results = DbService.Unit.GameResults.GetAll().ToList();
            for (int i = 0; i < results.Count(); i++)
            {
                if (results[i].FirstPlayerId == gameResult.FirstPlayerId && results[i].SecondPlayerId == gameResult.SecondPlayerId || results[i].FirstPlayerId == gameResult.SecondPlayerId && results[i].SecondPlayerId == gameResult.FirstPlayerId)
                {
                    json += JsonSerializer.Serialize(results[i]);
                    json += "\n";
                }
            }
            try
            {
                var directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                string folderPath = Path.Combine(directory.Parent.Parent.Parent.ToString(), Constants.DownloadedFilesDirectoryName);
                string dateTime = DateTime.Now.ToString().Replace(':', '_').Replace('.', '_');
                string path = Path.Combine(folderPath, dateTime + '_' + string.Format(Constants.GameResultsForCurrentPlayersFileName, id1, id2));
                FileStream fileStream = File.Open(path, FileMode.Create, FileAccess.Write);
                StreamWriter fileWriter = new StreamWriter(fileStream);
                fileWriter.WriteLine(json);
                fileWriter.Flush();
                fileWriter.Close();
                Console.WriteLine(Strings.CommandExecuted, dateTime + '_' + string.Format(Constants.GameResultsForCurrentPlayersFileName, id1, id2), path);
            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe);
            }


        }
        /// <summary>
        /// generate file-report in json format with ALL game results
        /// </summary>
        private static void GenerateAllResults()
        {

            string json = "";
            var results = DbService.Unit.GameResults.GetAll();
            foreach (var result in results)
            {
                json += JsonSerializer.Serialize(result);
                json += "\n";
            }
            try
            {
                var directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                string folderPath = Path.Combine(directory.Parent.Parent.Parent.ToString(), Constants.DownloadedFilesDirectoryName);
                string dateTime = DateTime.Now.ToString().Replace(':', '_').Replace('.', '_');
                string path = Path.Combine(folderPath, dateTime + '_' + string.Format(Constants.AllGameResultsFileName));
                FileStream fileStream = File.Open(path, FileMode.Create, FileAccess.Write);
                StreamWriter fileWriter = new StreamWriter(fileStream);
                fileWriter.WriteLine(json);
                fileWriter.Flush();
                fileWriter.Close();
                Console.WriteLine(Strings.CommandExecuted, dateTime + '_' + string.Format(Constants.AllGameResultsFileName), path);

            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe);
            }

        }
    }
}
