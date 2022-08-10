using System;
using System.Collections.Generic;
using System.Text;
using ConsoleApp2.Constants;

namespace ConsoleApp2
{
    public class GameField
    {
        private int Rows;
        private int Columns;
        private char[,] Field;
        private int AmountOfSkippedTurns;
        private int mistakesInRow; //счетчик текущей серии ошибок пользователя при вводе, который обнуляется если пользователь ввёл всё правильно
        public int turnCounter { get; private set; }//счёкик ходов позволяюший понять завершена и чёй сейчас ход Х или О


        public GameField(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Field = new char[rows, columns];
            turnCounter = 0;
            mistakesInRow = 0;
            AmountOfSkippedTurns = 0;
            SetStartField();
        }

        public void SetStartField()//заполнение поля точками
        {
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                {
                    Field[i, j] = '.';
                }
        }

        public void DisplayField()// отрисовка поля
        {
            for (int i = 0; i < Rows; i++)
            {
          
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
          
        }


        public void MakeTurn(int x, int y)//метод совершения хода
        {
            char mark = GameConstants.GAME_MARKS[turnCounter % 2];//из счётчика хода можем понять какую метку нужно ставить

            try
            {
                if (ValidSpot(x, y))
                {
                    Field[x - 1, y - 1] = mark;
                    turnCounter++;
                    mistakesInRow = 0;
                }
            }
            catch (Exception e)
            {
                mistakesInRow++;
                if (mistakesInRow == GameConstants.MISTAKES_MAX_COUNT)
                {
                    turnCounter++;// если пользователь превысил лимит ошибок бросаем исключение и пропускаем ход
                    AmountOfSkippedTurns++;
                    throw new Exception($"is's yours {mistakesInRow}-th mistake in a row, your turn is skipped");
                }
                throw new Exception($"Invalid spot: {e.Message}", e);

            }
        }

        private bool ValidSpot(int x, int y)
        {
            if (x >= 1 && y >= 1 && x <= Rows && y <= Columns)
            {
                if (Field[x - 1, y - 1] == '.')
                {
                    return true;
                }
                else
                {
                    throw new Exception("this plase is alredy marked,try another");
                }
            }
            else
            {
                throw new Exception($"Coordinates out of range of game field, must be beetwen 1 and {Rows}");
            }
        }

        public int CheckVictory(out int res)
        {
            if (turnCounter - AmountOfSkippedTurns < 5)// до этого момента ни одна из сторон выйграть не может . нет смысла проверять победные условия
            {
                res = 0;
                return 0;
            }
            else
            {
                char mark = turnCounter % 2 == 0 ? 'O' : 'X';// проверяем только метки игрока совершившего последний ход... довольно логично
                int playernum = turnCounter % 2 == 0 ? 2 : 1;
                bool ldiag = true;
                bool rdiag = true;
                for (int i = 0; i < Rows; i++)
                {
                    bool col = true;
                    bool row = true;
                    for (int j = 0; j < Columns; j++)
                    {
                        col &= Field[i, j] == mark;
                        row &= Field[j, i] == mark;
                    }
                    if (col || row)
                    {
                        res = playernum;
                        return playernum;

                    }
                    ldiag &= Field[i, i] == mark;
                    rdiag &= Field[i, Rows - i - 1] == mark;
                }
                if (ldiag || rdiag)
                {
                    res = playernum;
                    return playernum;
                }
                else
                {
                    if (turnCounter - AmountOfSkippedTurns == Rows * Columns)//отслеживатель ничьей
                    {
                        res = -1;
                        return -1;
                    }
                    res = 0;
                    return 0;
                }
            }

        }

       
    }
}