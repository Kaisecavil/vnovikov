using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ConsoleApp2.Constants;
using ConsoleApp2;
using static System.Net.Mime.MediaTypeNames;

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
            int x = -1; //переменные для запоминания координат и состояния игры
            int y = -1;
            int res = 0;
            while (gameField.CheckVictory(out res) == 0)//игра идет до того момента пока метод определения победы не выдаст результат отличный от нуля или пока не закочится поле
            {
                while (true)
                {

                    try // запрашиваем на ввод необходимую информацию и если она не подходит по тем или иным причинам запрашиваем ещё раз
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
                GameStart(); //не уверен насчёт этого способа повтора игры , при нём во первых почему-то вылетает две просьбы ввести пользователя ник . и ещё возможно переполнение стека вызова в теории
            }
            else
            {
                return;
            }

        }

        public static string AskForTurnInfo(int turn, List<Player> players) //Метод запрашиваюший у нужного игрока строку на ввод
        {
            Console.Write($"Player #{players[turn % 2].PlayerNumber} ({players[turn % 2].PlayerName}) Input two numbers [1-3]:");
            return Console.ReadLine();
        }

        public static string InputName(int playerNum)//Метод для ввода и валидации имени игрока
        {
            string name = "";
            do
            {
                if (name.Length >= GameConstants.MAX_NAME_LENGTH)
                {
                    Console.WriteLine($"invalid name length,length must be between 1 and {GameConstants.MAX_NAME_LENGTH}");
                }
                Console.WriteLine($"Player#{playerNum} Input your name [1-{GameConstants.MAX_NAME_LENGTH}] characters:");
                name = Console.ReadLine();
            } while (name.Length >= GameConstants.MAX_NAME_LENGTH || name.Length == 0);
            
                return name;
        }

        public void ParseTurnInfo(string turnStr, out int x, out int y)//Метод проверяюший формат ввода и выуживающий из него две координаты
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
