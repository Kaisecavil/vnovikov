using ConsoleApp2.Constants;

namespace ConsoleApp2
{
    public class GameField
    {
        private int Rows;
        private int Columns;
        private char[,] Field;
        private int AmountOfSkippedTurns;
        private int mistakesInRow; 
        public int turnCounter { get; private set; }


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
        public void DisplayField()
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


        public void MakeTurn(int x, int y)
        {
            char mark = GameConstants.GAME_MARKS[turnCounter % 2];

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
                    turnCounter++;//skip turn if player done to much mistakes
                    
                    AmountOfSkippedTurns++;
                    throw new Exception($"is's yours {mistakesInRow}-th mistake in a row, your turn is skipped");
                }
                throw new Exception($"Invalid spot: {e.Message}", e);

            }
        }
        public int CheckVictory(out int res)
        {
            if (turnCounter - AmountOfSkippedTurns < 5)//until that moment none of players can't win
                                                       
            {
                res = 0;
                return 0;
            }
            else
            {
                char mark = turnCounter % 2 == 0 ? GameConstants.GAME_MARKS[1] : GameConstants.GAME_MARKS[0];//loking only for necessary player mark
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
                    if (turnCounter - AmountOfSkippedTurns == Rows * Columns)//Draw catcher
                    {
                        res = -1;
                        return -1;
                    }
                    res = 0;
                    return 0;
                }
            }

        }

        private void SetStartField()// Initializing game field with dots 
        {
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                {
                    Field[i, j] = GameConstants.GAME_MARKS[2];
                }
        }

        private bool ValidSpot(int x, int y)
        {
            if (x >= 1 && y >= 1 && x <= Rows && y <= Columns)
            {
                if (Field[x - 1, y - 1] == GameConstants.GAME_MARKS[2])
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
    }
}