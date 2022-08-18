using System.Numerics;
using System.Text.RegularExpressions;
using TicTacToeVNovikov.Resources;
using System.Reflection;
using System.Globalization;
using System.Resources;

namespace TicTacToeVNovikov;

internal class Game
{
    //public static CultureInfo cultureInfo = new CultureInfo("en-US");
    //public static ResourceManager resourceManager = new($"TicTacToeVNovikov.Resources.en.Strings", Assembly.GetExecutingAssembly());
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

    public Game(int fieldSize = GameConstants.FieldSize, int playersCount = GameConstants.PlayersCount, int maxMistakesCount = GameConstants.MaxMistakesCount, string gameMarks = GameConstants.GameMarks)
    {
        _playerList = new List<Player>();
        _gameField = new GameField(fieldSize, fieldSize);
        _turnCounter = 0;
        _mistakesInRow = 0;
        _fieldSize = fieldSize;
        _playersCount = playersCount;
        _maxMistakesCount = maxMistakesCount;
        _gameMarks = gameMarks;
        SelectLocalization();
        PlayersRegister();
        _currentPlayer = _playerList[0];
        

    }

    public void SelectLocalization()
    {
        var localizations = new Dictionary<string, string>()
        {
            {"en","en-US"},
            {"ru","ru-RU"},
            {"de","de-DE"}
        };
        Console.WriteLine(Strings.NecessaryAbbreviation);
        foreach (var item in localizations)
        {
            Console.WriteLine(Strings.KeyIsForValue,item.Key,item.Value);
        }
        var key = Console.ReadLine();
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(localizations[key]);

    }

    public static bool AskForNewGame()
    {
        Console.WriteLine(Strings.AskForNewGame,"\n");
        return Console.ReadKey().Key == ConsoleKey.Enter;
    }
    public void Startgame()
    {
        _gameStartTime = DateTime.Now;
        _gameField.DisplayField();
        int winnerIndex = 0;
        while (winnerIndex == 0)
        {
            MakeTurn(_currentPlayer);
            _gameField.CheckVictory(_gameMarks[_turnCounter % 2 + 1], _turnCounter, _amountOfSkippedTurns, out winnerIndex);
            PassTurn();
        }
        if (winnerIndex != -1)
        {
            Console.WriteLine(Strings.AnnouncementOfWinner,winnerIndex, _playerList[winnerIndex - 1].PlayerName);
        }
        else
        {
            Console.WriteLine(Strings.Draw);
        }
        EndGame(_gameStartTime,winnerIndex);
        CommandLine.AskForCommand();
    }
    private void EndGame(DateTime gameStartTime,int indexOfWinner)
    {
        using (UnitOfWork unitOfWork = new UnitOfWork(new ApplicationContext()))
        {
            unitOfWork.GameResults.Insert(new GameResult(_gameStartTime.ToString("O"), DateTime.Now.ToString("O"), _playerList[0].Id, _playerList[1].Id, 'X', 'O', _playerList[indexOfWinner - 1].Id));
            unitOfWork.Commit();
        }
    }
    private void MakeTurn(Player player)
    {
        int x = 0;
        int y = 0;
        while (true)
        {
            try
            {
                ParseTurnInfo(AskForPlayerTurn(player), out x, out y);
                _gameField.PutMark(x, y, _gameMarks[_turnCounter%2+1]);
                _gameField.DisplayField();
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _mistakesInRow++;
                if (_mistakesInRow >= _maxMistakesCount)
                {
                    Console.WriteLine(Strings.SkippedTurn,_mistakesInRow);
                    _amountOfSkippedTurns++;
                    _mistakesInRow = 0;
                    return;
                }
                continue;
            }
        }
    }

    private void PassTurn()
    {
        _turnCounter++;
        int necesseryPlayerSequenceNum = _turnCounter % _playersCount;
        _currentPlayer = _playerList[necesseryPlayerSequenceNum];
        _mistakesInRow = 0;
    }

    private void PlayersRegister()
    {
        using(UnitOfWork unitOfWork =  new UnitOfWork(new ApplicationContext()))
        {
            for (int i = 1; i <= _playersCount; i++)
            {
                while (true)
                {
                    try
                    {
                        int id;
                        string? name = null;
                        int age;
                        bool isRegistered = false;
                        Player.ParsePlayerInfo(Player.AskForPlayerInfo(i), out id, out name, out age,out isRegistered);
                        if (isRegistered)
                        {
                            Player findedPlayer = unitOfWork.Players.GetById(id);
                            if (findedPlayer != null)
                            {
                                if (findedPlayer.PlayerName == name)
                                {
                                    Player player = new Player(id, name, age);
                                    _playerList.Add(player);
                                    if (findedPlayer.Age != age)
                                    {
                                        findedPlayer.Age = age;
                                        unitOfWork.Commit();
                                    }
                                }
                                else
                                {
                                    throw new Exception((Strings.IdIsOccupied, id).ToString());
                                }
                            }
                            else
                            {
                                Player player = new Player(id, name, age);
                                _playerList.Add(player);
                                unitOfWork.Players.Insert(player);
                                unitOfWork.Commit();
                            }
                        }
                        else
                        {
                            Player player = new Player(name, age);
                            unitOfWork.Players.Insert(player);
                            unitOfWork.Commit();
                            Player playerWithId = new Player(unitOfWork.Players.GetLast().Id, name, age);
                            _playerList.Add(playerWithId);
                            Console.WriteLine(Strings.SuccessfullRegistation,playerWithId.PlayerName,playerWithId.Id);
                        }
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(Strings.InvalidPlayerInfo + e.Message);
                        continue;
                    }
                }
            }
        }
    }

    private string? AskForPlayerTurn(Player player)
    {
        Console.WriteLine(Strings.AskForPlayerTurn,_turnCounter % 2 + 1,player.PlayerName,_fieldSize);
        return Console.ReadLine();

    }

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
                throw new Exception((Strings.WrongFormatOfTurnInfo,_fieldSize).ToString());
            }
        }
        else
        {
            throw new NullReferenceException(Strings.NullPlayerInfo);
        }

    }
}

