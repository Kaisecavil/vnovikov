﻿using System.Text.RegularExpressions;
using ConsoleApp2.Constants;

namespace ConsoleApp2
{
    public class GameEngine
    {
        public void GameStart()
        {
            
            Player player1 = new Player(InputName(1), 1);
            Player player2 = new Player(InputName(2), 2);
            List<Player> players = new List<Player>();
            players.Add(player1);
            players.Add(player2);
            GameField gameField = new GameField(3, 3);
            gameField.DisplayField();
            int x = -1; //variables for remembering coordinates
            int y = -1;
            int res = 0;
            while (gameField.CheckVictory(out res) == 0)
            {
                while (true)
                {

                    try 
                    {
                        ParseTurnInfo(AskForTurnInfo(gameField.turnCounter, players), out x, out y);
                        gameField.MakeTurn(x, y);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }
                    break;
                }
                gameField.DisplayField();
            }
            if (res == -1)
            {
                Console.WriteLine("DRAW");
            }
            else
            {
                Console.WriteLine($"Player №{res} ({players[res-1].PlayerName}) won");
            }
            Console.WriteLine("press Enter to repeate,and any other button to exit");
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                GameStart(); 
            }
            else
            {
                return;
            }

        }

        private static string AskForTurnInfo(int turn, List<Player> players) 
        {
            Console.Write($"Player #{players[turn % 2].PlayerNumber} ({players[turn % 2].PlayerName}) Input two numbers [1-3]:");
            return Console.ReadLine();
        }

        private static string InputName(int playerNum)
        {
            string name = "";
            do
            {
                if (name.Length >= GameConstants.MAX_NAME_LENGTH)
                {
                    Console.WriteLine($"invalid name length,length must be between {GameConstants.MIN_NAME_LENGTH} and {GameConstants.MAX_NAME_LENGTH}");
                }
                Console.WriteLine($"Player#{playerNum} Input your name [{GameConstants.MIN_NAME_LENGTH}-{GameConstants.MAX_NAME_LENGTH}] characters:");
                name = Console.ReadLine();
                if(name.Length < GameConstants.MIN_NAME_LENGTH)
                {
                    Console.WriteLine($"invalid name length,length must be between {GameConstants.MIN_NAME_LENGTH} and {GameConstants.MAX_NAME_LENGTH}");
                }
            } while (name.Length >= GameConstants.MAX_NAME_LENGTH || name.Length == 0);
            
                return name;
        }

        private void ParseTurnInfo(string turnStr, out int x, out int y)//Method for parsing Players input
        {
            string temp = Regex.Replace(turnStr, @"\s+", " ").Trim();
            Regex regex = new Regex(@"^\d \d$");
            bool isValid = regex.IsMatch(temp);

            if (isValid)
            {
                string[] nums = temp.Split(' ', 2);
                x = Convert.ToInt32(nums[0]);
                y = Convert.ToInt32(nums[1]);
            }
            else
            {
                throw new Exception($"Wrong format of Info,must be two numbers from 1 to {GameConstants.FIELD_SIZE}");
            }

        }
    }
}
