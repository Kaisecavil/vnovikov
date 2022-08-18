using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace TicTacToeVNovikov;

internal class Player
{
    static private int _maxNameLeangth;
    static private int _minNameLeangth;
    static private int _maxPlayerAge;
    static private int _minPlayerAge;
    public int Id { get; private set; }
    //public char Mark { get; private set; }
    //public int PlayerSequenceNumber { get; private set; }
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
                    throw new Exception("You can't grow vice versa");
                }
                
            }
            else
            {
                throw new Exception($"Player Age must be beetwen {_minPlayerAge} and {_maxPlayerAge} ages");
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
                    throw new ArgumentException($"Player name length must be beetwen {_minNameLeangth} and {_maxNameLeangth} characters");
                }
            }
            else if (value == null)
            {
                throw new ArgumentNullException("Player name can't be null");
            }
            else
            {
                throw new ArgumentException("Player name can't be empty or contain only whitespaces");
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
        Console.WriteLine($"Player #{playerNumber} Input your Id Name Age if you played before,or Input your Name Age if you are a new player");
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
                throw new Exception($"Wrong format of Info,must be Id Name Age if you are registered or Name Age if you aren't");
            }
        }
        else
        {
            throw new NullReferenceException("Information about Player can't be null");
        }
    }
}

