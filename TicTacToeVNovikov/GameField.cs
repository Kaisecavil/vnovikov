using TicTacToeVNovikov.Constants;

namespace TicTacToeVNovikov
{
    internal class GameField //class that responsible for action with gamefield
    {
        private char[,] Field;
        private int Rows;
        private int Columns;

        public GameField(int rows,int cols)
        {
            Field = new char[rows,cols];
            Rows = rows;
            Columns = cols;
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
        public void CheckVictory(Player currentPlayer , int turnNumber, int skippedTurnCount, out int res)
        {
            
            char mark = currentPlayer.Mark;//loking only for necessary player mark
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
                    res = currentPlayer.PlayerSequenceNumber;
                    return;


                }
                ldiag &= Field[i, i] == mark;
                rdiag &= Field[i, Rows - i - 1] == mark;
            }
            if (ldiag || rdiag)
            {
                res = currentPlayer.PlayerSequenceNumber;
                return;
            }
            else
            {
                if (turnNumber - skippedTurnCount == Rows * Columns-1)//Draw catcher
                {
                    res = -1;
                    return;
                }
                res = 0;
                return;
            }
        }

        public void PutMark(int x, int y, char mark)
        {
            if (ValidSpot(x, y))
            {
                Field[x - 1, y - 1] = mark;
            }
        }

        private bool ValidSpot(int x,int y)
        {
            if(x<=Rows && y<=Columns && x>=1 && y >= 1)
            {
                if(Field[x-1, y-1] == (char)GameConstants.Marks.EmptyFieldMark)
                {
                    return true;
                }
                else
                {
                    throw new Exception("This spot is alredy marked, try another");
                }
            }
            else
            {
                throw new Exception($"Cordinates out of range of gamefield, must be two numbers from 1 to {GameConstants.FieldSize}");
            }
        }
        private void SetStartField()
        {
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                {
                    Field[i, j] = (char)GameConstants.Marks.EmptyFieldMark;
                }
        }


        
    }
}
