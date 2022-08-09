using System;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    public class Program
    {

        static void Main(string[] args)
        {
            int rows = 3;
            int columns = 3;
            string fPlayerName = InputName();
            string sPlayerName = InputName();
            char[,] field = new char[rows, columns];

            int turnCounter = 0;
            SetStartField(rows, columns, field);
            while (CheckVictory(field, turnCounter) == 0)
            {
                DisplayField(rows, columns, field);
                Turn(ref field,ref turnCounter);

            }
        }

        public static int CheckVictory(char[,] fld, int turn)
        {
            return 0;
        }

        public static void ParseTurn(string turnStr, out int x, out int y)
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

        public static string InputName()
        {
            string name = "";
            while (name.Length >= 25 || name.Length == 0)
            {
                Console.WriteLine("Input Player Name [1-25] characters:");
                name = Console.ReadLine();
            }
            return name;
        }

        public static void Turn(ref char[,] fld,ref int numOfTurn)
        {
            Random random = new Random();
            if (numOfTurn % 2 == 0)
            {
                Console.Write("First Player turn, input two numbers [1-3]: ");
            }
            else
            {
                Console.Write("Second turn, input two numbers [1-3]: ");
            }
            string turn;
            int x = -1;
            int y = -1;
            for (int i = 0; i < 3; i++)
            {
                turn = Console.ReadLine();
                ParseTurn(turn, out x, out y);
                if (!IsValidCoords(x, y, ref fld))
                {
                    Console.WriteLine($"Wrong cords, try again {i + 1} attempt out of 3");
                }
                else
                {
                    break;
                }
                if (i == 2)
                {
                    Console.WriteLine("3 of 3");
                    numOfTurn++;
                    return;
                }

            }
            if (numOfTurn % 2 == 0)
            {
                PutMark(ref fld, x, y, 'X');
            }
            else
            {
                PutMark(ref fld, x, y, 'O');
            }
            numOfTurn++;

        }

        public static void PutMark(ref char[,] fld, int x, int y, char mark)
        {
            fld[x - 1, y - 1] = mark;
        }

        public static void SetStartField(int Rows, int Columns, char[,] Field)
        {
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                {
                    Field[i, j] = '.';
                }
        }

        public static void DisplayField(int Rows, int Columns, char[,] Field)
        {
            for (int i = 0; i < Rows; i++)
            {
                //Console.WriteLine();
                for (int j = 0; j < Columns; j++)
                {
                    if (j != 1)
                        Console.Write($"{Field[i, j]}");
                    else
                        Console.Write($"|{Field[i, j]}|");
                }

                Console.WriteLine();
                if (i != 2)
                    Console.WriteLine("-----");
            }
            Console.ReadKey();
        }

        public static bool IsValidCoords(int x, int y, ref char[,] fld)
        {
            return (x >= 1 && y >= 1 && x < 4 && y < 4) && fld[x - 1, y - 1] == '.';
        }


    }
}
