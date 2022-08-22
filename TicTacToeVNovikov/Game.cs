using System.Text.RegularExpressions;
using TicTacToeVNovikov.Resources;
using System.Globalization;
using TicTacToeVNovikov.Models;
using TicTacToeVNovikov.Services;
using TicTacToeVNovikov.GameConstants;

namespace TicTacToeVNovikov;
/// <summary>
/// This class is responsible for game logic and players registation.
/// </summary>
/// <remarks>
/// If you want to use this class, you should use construction like that
/// <code>
///      while (true)
///      {
///         if (Game.AskForNewGame())
///             {
///                 Game game = new Game();
///                 game.Startgame();
///         }
///         else { break; }
///      }    
/// </code>
/// </remarks>
internal class Game
{
    private int _turnCounter;
    private int _amountOfSkippedTurns;
    private List<Player> _playerList;
    private GameField _gameField;
    private Player _currentPlayer;
    private int _mistakesInRow;
    private int _fieldSize;
    private int _playersCount;
    private int _maxMistakesCount;
    private string _gameMarks;
    private DateTime _gameStartTime;
    /// <summary>
    /// Main constructor that implements fields and calls some methods
    /// </summary>
    /// <param name="fieldSize">Gamefield size</param>
    /// <param name="playersCount">Players count</param>
    /// <param name="maxMistakesCount">Max count of mistakes that user allowed to do until his turn would be skipped </param>
    /// <param name="gameMarks">String that will be represent all game marks, where first symbol is for wightspace, others is for player marks in order</param>
    public Game(int fieldSize = Constants.FieldSize, int playersCount = Constants.PlayersCount, int maxMistakesCount = Constants.MaxMistakesCount, string gameMarks = Constants.GameMarks)
    {
        SelectLocalization();
        _playerList = PlayerService.PlayersGeristration(playersCount);
        _gameField = new GameField(fieldSize, fieldSize);
        _turnCounter = 0;
        _mistakesInRow = 0;
        _fieldSize = fieldSize;
        _playersCount = playersCount;
        _maxMistakesCount = maxMistakesCount;
        _gameMarks = gameMarks;
        _currentPlayer = _playerList[0];
    }
    /// <summary>
    /// Method that allows you to ask a user to choose necessary localization
    /// </summary>
    public void SelectLocalization()
    {
        var localizations = new Dictionary<string, string>()
        {
            {"en","en-US"},
            {"ru","ru-RU"},
            {"de","de-DE"}
        };
        while (true)
        {
            string? key = "";
            try
            {
                Console.WriteLine(Strings.NecessaryAbbreviation);
                foreach (var item in localizations)
                {
                    Console.WriteLine(Strings.KeyIsForValue, item.Key, item.Value);
                }
                key = Console.ReadLine();
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(localizations[key]);
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine(string.Format(Strings.UnknownAbbreviation, key));
                continue;
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine(Strings.NullAbbreviation);
                continue;
            }
            break;
        }

    }
    /// <summary>
    /// A method that prompt the user to start a new game.
    /// </summary>
    /// <returns>
    /// <para> <see cref="System.Boolean"/> True, if user wants to start new game</para>
    /// <para> <see cref="System.Boolean"/> False, if user doesn't want to start new game</para>
    /// </returns>
    public static bool AskForNewGame()
    {
        Console.WriteLine(Strings.AskForNewGame, "\n");
        return Console.ReadKey().Key == ConsoleKey.Enter;

    }
    /// <summary>
    /// Method that starts game algorithm and turn sequence.
    /// </summary>
    public void Startgame()
    {
        _gameStartTime = DateTime.Now;
        _gameField.DisplayField();
        int winnerIndex = 0;
        while (winnerIndex == 0)
        {
            MakeTurn(_currentPlayer);
            _gameField.CheckVictory(_gameMarks[_turnCounter % 2 + 1], _turnCounter, _amountOfSkippedTurns, out winnerIndex); // _turnCounter % 2 + 1 That expression allows us to determine what mark we should to check
            PassTurn();
        }
        if (winnerIndex != -1)
        {
            Console.WriteLine(Strings.AnnouncementOfWinner, winnerIndex, _playerList[winnerIndex - 1].PlayerName);
        }
        else
        {
            Console.WriteLine(Strings.Draw);
        }
        GameResultService.CommitGameResult(_gameStartTime, winnerIndex, _playerList);
        CommandLine.AskForCommand();
    }

    /// <summary>
    /// Method that realize all actions contained in one turn.
    /// </summary>
    /// <param name="player">The player who is making turn now</param>
    private void MakeTurn(Player player)
    {
        int x = 0;
        int y = 0;
        while (true)
        {
            try
            {
                ParseTurnInfo(AskForPlayerTurn(player), out x, out y);
                _gameField.PutMark(x, y, _gameMarks[_turnCounter % 2 + 1]);
                _gameField.DisplayField();
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _mistakesInRow++;
                if (_mistakesInRow >= _maxMistakesCount)
                {
                    Console.WriteLine(Strings.SkippedTurn, _mistakesInRow);
                    _amountOfSkippedTurns++;
                    _mistakesInRow = 0;
                    return;
                }
                continue;
            }
        }
    }
    /// <summary>
    /// Method that pass the turn.
    /// </summary>
    private void PassTurn()
    {
        _turnCounter++;
        int necesseryPlayerSequenceNum = _turnCounter % _playersCount;
        _currentPlayer = _playerList[necesseryPlayerSequenceNum];
        _mistakesInRow = 0;
    }

    /// <summary>
    /// A method that prompt the user to enter a turn information.
    /// </summary>
    /// <param name="player">The player who is making turn now</param>
    /// <returns>Any string expression or null</returns>
    private string? AskForPlayerTurn(Player player)
    {
        Console.WriteLine(Strings.AskForPlayerTurn, _turnCounter % 2 + 1, player.PlayerName, _fieldSize);
        return Console.ReadLine();

    }
    /// <summary>
    /// Method that parse string in certain way to get information about player's turn
    /// </summary>
    /// <param name="turnStr">String that probably containt information about player's turn or null</param>
    /// <param name="x">First coordinate of player's turn</param>
    /// <param name="y">Second coordinate of player's turn</param>
    /// <exception cref="Exception">Invalid format of player's turn information</exception>
    /// <exception cref="NullReferenceException">Turn information is null</exception>
    private void ParseTurnInfo(string? turnStr, out int x, out int y)
    {
        if (turnStr != null)
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
                throw new Exception(string.Format(Strings.WrongFormatOfTurnInfo, _fieldSize));
            }
        }
        else
        {
            throw new NullReferenceException(Strings.NullPlayerInfo);
        }

    }
}

