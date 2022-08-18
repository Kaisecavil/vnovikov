using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using TicTacToeVNovikov.Resources;

namespace TicTacToeVNovikov;

internal class Player
{
    static private int _maxNameLeangth;
    static private int _minNameLeangth;
    static private int _maxPlayerAge;
    static private int _minPlayerAge;
    public int Id { get; private set; }
    private int _age;
    public int Age { 
        get 
        { 
            return _age;
        }
         set
        {
            if(value < _maxPlayerAge && value > _minPlayerAge)
            {
                if(value > Age)
                {
                    _age = value;
                }
                else
                {
                    throw new Exception(Exceptions.LesserAge);
                }
                
            }
            else
            {
                throw new Exception((Game.resourceManager.GetString("AgeIsOutOfLimits"),_minPlayerAge,_maxPlayerAge).ToString());
            }
        } 
    }
    private string? _playerName;
    public string? PlayerName
    {
        get
        {
            return _playerName;
        }
        private set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                if (_minNameLeangth <= value.Length && value.Length <= _maxNameLeangth)
                {
                    _playerName = value;
                }
                else
                {
                    throw new ArgumentException((Exceptions.PlayerNameLengthIsOutOfLimits,_minNameLeangth,_maxNameLeangth).ToString());
                }
            }
            else if (value == null)
            {
                throw new ArgumentNullException(Exceptions.NullPlayerName);
            }
            else
            {
                throw new ArgumentException(Exceptions.EmptyPlayerName);
            }
        }
    }

    public Player(int id, string? playerName, int age)//, int maxNameLeangth = GameConstants.MaxNameLeangth, int minNameLeangth = GameConstants.MinNameLeangth,int maxPlayerAge = GameConstants.MaxPlayerAge,int minPlayerAge = GameConstants.MinPlayerAge
    {
        try
        {
            Id = id;
            PlayerName = playerName;
            Age = age;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Player(string playerName,int age)
    {
        try
        {
            PlayerName = playerName;
            Age = age;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    static Player()
    {
        _maxPlayerAge = GameConstants.MaxPlayerAge;
        _minPlayerAge = GameConstants.MinPlayerAge;
        _maxNameLeangth = GameConstants.MaxNameLeangth;
        _minNameLeangth = GameConstants.MinNameLeangth;
    }

    public static string? AskForPlayerInfo(int playerNumber)
    {
        Console.WriteLine(Resource1.AskForPlayerInfo,playerNumber);
        return Console.ReadLine();
    }

    public static void ParsePlayerInfo(string? playerInfo,out int id,out string name,out int age,out bool isRegistered)
    {
        if (playerInfo != null)
        {
            string temp = Regex.Replace(playerInfo, @"\s+", " ").Trim();
            Regex regexRegistered = new Regex(@"^\d+\s\w+\s\d{1,3}$");
            Regex regexUnregistered = new Regex(@"^\w+\s\d{1,3}$");
            if (regexRegistered.IsMatch(temp))
            {
                string[] nums = temp.Split(' ');
                id = Convert.ToInt32(nums[0]);
                name = nums[1];
                age = Convert.ToInt32(nums[2]);
                isRegistered = true;
            }
            else if (regexUnregistered.IsMatch(temp))
            {
                id = 0;
                string[] nums = temp.Split(' ');
                name = nums[0];
                age = Convert.ToInt32(nums[1]);
                isRegistered=false;
            }
            else
            {
                throw new Exception(Exceptions.WrongFormatOfPlayerInfo);
            }
        }
        else
        {
            throw new NullReferenceException(Exceptions.NullPlayerInfo);
        }
    }
}

