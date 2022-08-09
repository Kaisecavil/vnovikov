using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    public class Program
    {
       

        static void Main(string[] args)
        {
            GameField gameField = new GameField(3, 3, 2);
            List<string> players = new List<string> { InputName(), InputName() }; //лист тут необходим ,так как много где используется вдинамическиъ сообщениях типо: Console.Write($"Player №{turn%2+1} ({players[turn % 2]}) Input two numbers [1-3]:");

            int x = -1; //переменные для запоминания координат и состояния игры
            int y = -1;
            int res = 0;
            while (gameField.CheckVictory(out res) == 0)//игра идет до того момента пока метод определения победы не выдаст результат отличный от нуля или пока не закочится поле
            {
                while(true)
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
                Console.WriteLine($"Player №{res} ({players[res-1]}) won");
            }
            Console.WriteLine("Do you wanna to repeat? type 1 or 2\n1.yes\n2.no");
            if (Console.Read() == '1')
            {
                Main(args); //не уверен насчёт этого способа повтора игры , при нём во первых почему-то вылетает две просьбы ввести пользователя ник . и ещё возможно переполнение стека вызова в теории
            }
            
        }

        public static string AskForTurnInfo(int turn, List<string> players) //Метод запрашиваюший у нужного игрока строку на ввод
        {
            Console.Write($"Player №{turn%2+1} ({players[turn % 2]}) Input two numbers [1-3]:");
            return Console.ReadLine();
        }

        

        public static void ParseTurnInfo(string turnStr, out int x, out int y)//Метод проверяюший формат ввода и выуживающий из него две координаты
        {
            Regex regex = new Regex(@"^\d \d$");
            bool isValid = regex.IsMatch(turnStr);

            if (isValid)
            {
                string[] nums = turnStr.Split(' ', 2);
                x = Convert.ToInt32(nums[0]);
                y = Convert.ToInt32(nums[1]);
            }
            else
            {
                
                x = -1;
                y = -1;

            }

        }

        public static string InputName()//Метод для ввода и валидации имени игрока
        {
            string name = "";
            while (name.Length >= 25 || name.Length==0)
            {
                if (name.Length >= 25)
                {
                    Console.WriteLine("invalid name length");
                }
                Console.WriteLine("Input Player Name [1-25] characters:");
                name = Console.ReadLine();
            }
            return name;
        }

        

        


    }
}
