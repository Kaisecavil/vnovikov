﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using TicTacToeVNovikov.Resources;

namespace TicTacToeVNovikov
{
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
            {"/skip,",Strings.SkipDescription}
        };

        static public void AskForCommand()
        {
            Console.WriteLine(Strings.AskForCommand);
            string? command = Console.ReadLine();
            while (command!="/skip")
            {
                try
                {
                    _commandAction[command].Invoke();
                }
                catch(KeyNotFoundException e)
                {
                    Console.WriteLine(Strings.UnknownCommand,command);
                }
                Console.Write(Strings.AskForCommand);
                command = Console.ReadLine();
                
            }
        }

        private static void Help()
        {
           foreach (var pair in _commandDescription)
            {
                Console.WriteLine($"{pair.Key} - {pair.Value}");
            }
        }

        private static void GenerateLastGameResult()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new ApplicationContext()))
            {
                GameResult gameResult = unitOfWork.GameResults.GetLast();
                string json = JsonSerializer.Serialize(gameResult);
                try
                {
                    FileStream fileStream = File.Open($"gameResult{gameResult.GameResultId}.txt", FileMode.Create, FileAccess.Write);
                    StreamWriter fileWriter = new StreamWriter(fileStream);
                    fileWriter.WriteLine(json);
                    fileWriter.Flush();
                    fileWriter.Close();
                }
                catch (IOException ioe)
                {
                    Console.WriteLine(ioe);
                }
            }
        }

        private static void GenerateResultsForCurrentPlayers()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new ApplicationContext()))
            {
                string json = "";
                GameResult gameResult = unitOfWork.GameResults.GetLast();
                int id1 = gameResult.FirstPlayerId < gameResult.SecondPlayerId ? gameResult.FirstPlayerId : gameResult.SecondPlayerId;
                int id2 = gameResult.FirstPlayerId < gameResult.SecondPlayerId ? gameResult.SecondPlayerId : gameResult.FirstPlayerId;
                var results = unitOfWork.GameResults.GetAll().ToList();
                for (int i = 0; i < results.Count(); i++)
                {
                    if(results[i].FirstPlayerId == gameResult.FirstPlayerId && results[i].SecondPlayerId == gameResult.SecondPlayerId || results[i].FirstPlayerId == gameResult.SecondPlayerId && results[i].SecondPlayerId == gameResult.FirstPlayerId)
                    {
                        json += JsonSerializer.Serialize(results[i]);
                        json += "\n";
                    }
                }
                try
                {
                    FileStream fileStream = File.Open($"GameResultsBetween{id1}and{id2}.txt", FileMode.Create, FileAccess.Write);
                    StreamWriter fileWriter = new StreamWriter(fileStream);
                    fileWriter.WriteLine(json);
                    fileWriter.Flush();
                    fileWriter.Close();
                }
                catch (IOException ioe)
                {
                    Console.WriteLine(ioe);
                }

            }
        }

        private static void GenerateAllResults()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new ApplicationContext()))
            {
                string json = "";
                var results = unitOfWork.GameResults.GetAll();
                foreach (var result in results)
                {
                    json+=JsonSerializer.Serialize(result);
                    json += "\n";
                }
                try
                {
                    FileStream fileStream = File.Open("allGameResults.txt", FileMode.Create, FileAccess.Write);
                    StreamWriter fileWriter = new StreamWriter(fileStream);
                    fileWriter.WriteLine(json);
                    fileWriter.Flush();
                    fileWriter.Close();
                }
                catch (IOException ioe)
                {
                    Console.WriteLine(ioe);
                }
            }
        }
    }
}
