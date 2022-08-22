using TicTacToeVNovikov.GameConstants;
using TicTacToeVNovikov.Resources;

namespace TicTacToeVNovikov;
/// <summary>
/// This class is responsible for all actions related to game field 
/// </summary>
internal class GameField
{
    private char[,] _Field;
    private int _Rows;
    private int _Columns;
    private int _fieldSize;
    private string _gameMarks;

    public GameField(int rows, int cols, int fieldSize = Constants.FieldSize, string gameMarks = Constants.GameMarks)
    {
        _Field = new char[rows, cols];
        _Rows = rows;
        _Columns = cols;
        _fieldSize = fieldSize;
        _gameMarks = gameMarks;

        SetStartField();
    }
    /// <summary>
    /// Method that outputs game field to console.
    /// </summary>
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
    /// <summary>
    /// Method that checks the field for the victory of one of the players.
    /// </summary>
    /// <param name="mark">Mark of Player that did last move</param>
    /// <param name="turnNumber">Turn Number in game</param>
    /// <param name="skippedTurnCount">Amount of skipped turns during the game</param>
    /// <param name="result">A sign of the victory of one of the players</param>
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
    /// <summary>
    /// Method that puts the mark on the game field.
    /// </summary>
    /// <param name="x">First coordinate</param>
    /// <param name="y">Second cordinate</param>
    /// <param name="mark">Current player's mark</param>
    public void PutMark(int x, int y, char mark)
    {
        if (ValidSpot(x, y))
        {
            _Field[x - 1, y - 1] = mark;
        }
    }
    /// <summary>
    /// Method that validate the spot on the game field.
    /// </summary>
    /// <param name="x">First coordinate</param>
    /// <param name="y">Second cordinate</param>
    /// <returns>
    /// <para>True, if spot is valid</para>
    /// </returns>
    /// <exception cref="Exception">Spot is invlaid</exception>
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
    /// <summary>
    /// Method that initilize game field with empty game marks.
    /// </summary>
    private void SetStartField()
    {
        for (int i = 0; i < _Rows; i++)
            for (int j = 0; j < _Columns; j++)
            {
                _Field[i, j] = _gameMarks[0];
            }
    }



}

