using TicTacToeVNovikov.GameConstants;
using TicTacToeVNovikov.Resources;

namespace TicTacToeVNovikov;

/// <summary>
/// This class is responsible for all actions related to game field 
/// </summary>
internal class GameField
{
    private char[,] _field;
    private int _fieldSize;
    private string _gameMarks;

    public GameField(int fieldsize, string gameMarks)
    {
        _fieldSize = fieldsize;
        _field = new char[fieldsize, fieldsize];
        _gameMarks = gameMarks;
        SetStartField();
    }

    /// <summary>
    /// Method that outputs game field to console.
    /// </summary>
    public void DisplayField()
    {
        for (int i = 0; i < _fieldSize; i++)
        {
            for (int j = 0; j < _fieldSize; j++)
            {
                if (j != _fieldSize - 1)
                    Console.Write(_field[i, j]+Constants.GameStrings.FieldColumnDelimeter);
                else
                    Console.Write($"{_field[i, j]}");
            }

            Console.WriteLine();
            if (i != _fieldSize - 1)
                Console.WriteLine(Constants.GameStrings.FieldRowDelimeter);
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
        for (int i = 0; i < _fieldSize; i++)
        {
            bool column = true;
            bool row = true;
            for (int j = 0; j < _fieldSize; j++)
            {
                column &= _field[i, j] == mark;
                row &= _field[j, i] == mark;
            }
            if (column || row)
            {
                result = turnNumber % 2 + 1;
                return;


            }
            leftDiagonal &= _field[i, i] == mark;
            rightDiagonal &= _field[i, _fieldSize - i - 1] == mark;
        }
        if (leftDiagonal || rightDiagonal)
        {
            result = turnNumber % 2 + 1;
            return;
        }
        else
        {
            if (turnNumber - skippedTurnCount == _fieldSize * _fieldSize - 1)
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
            _field[x - 1, y - 1] = mark;
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
        if (x <= _fieldSize && y <= _fieldSize && x >= 1 && y >= 1)
        {
            if (_field[x - 1, y - 1] == _gameMarks[0])
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
        for (int i = 0; i < _fieldSize; i++)
            for (int j = 0; j < _fieldSize; j++)
            {
                _field[i, j] = _gameMarks[0];
            }
    }

}

