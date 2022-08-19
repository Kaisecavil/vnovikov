using TicTacToeVNovikov.Resources;

namespace TicTacToeVNovikov;
internal class GameField
{
    private char[,] _Field;
    private int _Rows;
    private int _Columns;
    private int _fieldSize;
    private string _gameMarks;

    public GameField(int rows, int cols, int fieldSize = GameConstants.FieldSize, string gameMarks = GameConstants.GameMarks)
    {
        _Field = new char[rows, cols];
        _Rows = rows;
        _Columns = cols;
        _fieldSize = fieldSize;
        _gameMarks = gameMarks;

        SetStartField();
    }

    public void DisplayField()
    {
        for (int i = 0; i < _Rows; i++)
        {
            for (int j = 0; j < _Columns; j++)
            {
                if (j != _Columns - 1)
                    Console.Write($"{_Field[i, j]}|");
                else
                    Console.Write($"{_Field[i, j]}");
            }

            Console.WriteLine();
            if (i != _Rows - 1)
                Console.WriteLine("-----");
        }
    }
    public void CheckVictory(char mark, int turnNumber, int skippedTurnCount, out int result)
    {

        bool leftDiagonal = true;
        bool rightDiagonal = true;
        for (int i = 0; i < _Rows; i++)
        {
            bool column = true;
            bool row = true;
            for (int j = 0; j < _Columns; j++)
            {
                column &= _Field[i, j] == mark;
                row &= _Field[j, i] == mark;
            }
            if (column || row)
            {
                result = turnNumber % 2 + 1;
                return;


            }
            leftDiagonal &= _Field[i, i] == mark;
            rightDiagonal &= _Field[i, _Rows - i - 1] == mark;
        }
        if (leftDiagonal || rightDiagonal)
        {
            result = turnNumber % 2 + 1;
            return;
        }
        else
        {
            if (turnNumber - skippedTurnCount == _Rows * _Columns - 1)
            {
                result = -1;
                return;
            }
            result = 0;
            return;
        }
    }

    public void PutMark(int x, int y, char mark)
    {
        if (ValidSpot(x, y))
        {
            _Field[x - 1, y - 1] = mark;
        }
    }

    private bool ValidSpot(int x, int y)
    {
        if (x <= _Rows && y <= _Columns && x >= 1 && y >= 1)
        {
            if (_Field[x - 1, y - 1] == _gameMarks[0])
            {
                return true;
            }
            else
            {
                throw new Exception(Strings.MarkedSpot);
            }
        }
        else
        {
            throw new Exception(string.Format(Strings.CordinatesOutOfRange,_fieldSize));
        }
    }
    private void SetStartField()
    {
        for (int i = 0; i < _Rows; i++)
            for (int j = 0; j < _Columns; j++)
            {
                _Field[i, j] = _gameMarks[0];
            }
    }



}

