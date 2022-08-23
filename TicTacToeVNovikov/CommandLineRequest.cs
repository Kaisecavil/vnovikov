using System.Text.Json;
using TicTacToeVNovikov.Resources;
using TicTacToeVNovikov.Models;
using TicTacToeVNovikov.Services;
using TicTacToeVNovikov.GameConstants;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TicTacToeVNovikov
{
    /// <summary>
    /// That class is responsible for all actions with commandline.
    /// </summary>
    static internal class CommandLineRequest
    {

        static private Dictionary<string, Action> _commandAction = new Dictionary<string, Action>()
        {
            {Constants.TextOfCommand.Help, Help},
            {Constants.TextOfCommand.GenerateLastGameResult, GenerateLastGameResult},
            {Constants.TextOfCommand.GenerateResultsForCurrentPlayers, GenerateResultsForCurrentPlayers},
            {Constants.TextOfCommand.GenerateAllResults, GenerateAllResults}
        };

        static private Dictionary<string, string> _commandDescription = new Dictionary<string, string>()
        {
            {Constants.TextOfCommand.Help,Strings.HelpDescription},
            {Constants.TextOfCommand.GenerateLastGameResult,Strings.GenerateLastGameResultDescription},
            {Constants.TextOfCommand.GenerateResultsForCurrentPlayers,Strings.GenerateResultsForCurrentPlayersDescription},
            {Constants.TextOfCommand.GenerateAllResults,Strings.GenerateAllResultsDescription},
            {Constants.TextOfCommand.Skip,Strings.SkipDescription}
        };

        /// <summary>
        /// A method that prompts the user to enter a command.
        /// </summary>
        static public void AskForCommand()
        {
            Help();
            Console.WriteLine(Strings.AskForCommand);
            string? command = Console.ReadLine();
            while (command != Constants.TextOfCommand.Skip)
            {
                CommandInvocation(command);
                Console.Write(Strings.AskForCommand);
                command = Console.ReadLine();

            }
        }

        /// <summary>
        /// Method that invokes necessary command
        /// </summary>
        /// <param name="commandToInvoke">Command that must be invoked</param>
        static private void CommandInvocation(string? commandToInvoke)
        {
            try
            {
                _commandAction[commandToInvoke].Invoke();
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine(Strings.UnknownCommand, commandToInvoke);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine(Strings.NullCommand);
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
                WriteJsonFile(json,Constants.FileName.LastGameResultFileName,new string[]{ gameResult.GameResultId.ToString()});
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
                WriteJsonFile(json, Constants.FileName.GameResultsForCurrentPlayersFileName, new string[] { id1.ToString(), id2.ToString() });
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
                WriteJsonFile(json, Constants.FileName.AllGameResultsFileName, new string[] {});
            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe);
            }

        }

        /// <summary>
        /// generate file-report in json format
        /// </summary>
        /// <param name="json">json serialized sting</param>
        /// <param name="fileName">file of file</param>
        /// <param name="args">arguments which specify the file name</param>
        private static void WriteJsonFile(string json,string fileName, string?[] args)
        {
            
            var directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            string dateTime = DateTime.Now.ToString().Replace(':', '_').Replace('.', '_');
            string folderPath = Path.Combine(directory.Parent.Parent.Parent.ToString(), Constants.FileName.DownloadedFilesDirectoryName);
            string path = Path.Combine(folderPath, dateTime + '_' + string.Format(fileName,args));
            Directory.CreateDirectory(folderPath);
            FileStream fileStream = File.Open(path, FileMode.Create, FileAccess.Write);
            StreamWriter fileWriter = new StreamWriter(fileStream);
            fileWriter.WriteLine(json);
            fileWriter.Flush();
            fileWriter.Close();
            Console.WriteLine(Strings.CommandExecuted, dateTime + '_' + string.Format(fileName, args), path);
        }
    }
}
