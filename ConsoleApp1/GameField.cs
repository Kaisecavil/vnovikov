using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class GameField
    {
        private int Rows;
        private int Columns;
        private char[,] Field;
        public int mistakesMaxCount { get; private set; } //максимальное кол-во ошибок которое мользователь может совершить подряд
        private int mistakesInRow; //счетчик текущей серии ошибок пользователя при вводе, который обнуляется если пользователь ввёл всё правильно
        public int turnCounter { get; private set; }//счёкик ходов позволяюший понять завершена и чёй сейчас ход Х или О
        

        public GameField(int rows, int columns,int maxMistakeCount) 
        {
            Rows = rows;
            Columns = columns;
            Field = new char[rows,columns];
            turnCounter = 0;
            mistakesMaxCount = maxMistakeCount;
            mistakesInRow = 0;
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


        public void MakeTurn(int x, int y)//метод совершения хода
        {
            char mark = turnCounter % 2 == 0 ? 'X' : 'O';//из счётчика хода можем понять какую метку нужно ставить
            if (ValidSpot(x, y))
            {
                Field[x - 1, y - 1] = mark;
                turnCounter++;
                mistakesInRow = 0;
            }
            else
            {
                mistakesInRow++;
                if(mistakesInRow == mistakesMaxCount)
                {
                    turnCounter++;// если пользователь превысил лимит ошибок бросаем исключение ипропускаем ход
                    throw new Exception($"is's yours {mistakesInRow}-th mistake in a row, your turn is skipped");
                }
                throw new Exception("invalid spot");//исключение неправильного места метки
            }
            
        }

        public bool ValidSpot(int x,int y)//валидация позиции на поле и проверка его свободности
        {
            return (x >= 1 && y >= 1 && x <= Rows && y <= Columns) && Field[x - 1, y - 1] == '.';
        }

        public int CheckVictory(out int res)//метод проверки победы одной из сторон
        {
            if (turnCounter < 5)// до этого момента ни одна из сторон выйграть не может . нет смысла проверять победные условия
            {
                res = 0;
                return 0;
            }
            else
            {
                char mark = turnCounter % 2 == 0 ? 'O' : 'X';// проверяем только метки игрока совершившего последний ход... довольно логично
                int playernum =  turnCounter % 2 == 0 ? 2 : 1;
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
                    ldiag &= Field[i,i] == mark;
                    rdiag &= Field[i, Rows - i - 1] == mark;
                }
                if (ldiag || rdiag) 
                {
                    res = playernum;
                    return playernum; 
                }
                else
                {
                    if (turnCounter == Rows*Columns)//отслеживатель ничьей
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
