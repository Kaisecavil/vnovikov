using System.Text.RegularExpressions;

namespace TicTacToeVNovikov;

internal class Player
{
    public int Id { get; private set; }
    public char Mark { get; private set; }
    public int PlayerSequenceNumber { get; private set; }
    
    static private int _maxNameLeangth;
    static private int _minNameLeangth;
    static private int _maxPlayerAge;
    static private int _minPlayerAge;
    private int _age;
    public int Age { 
        get 
        { 
            return _age;
        }
        private set
        {
            if(value < _maxPlayerAge && value > _minPlayerAge)
            {
                _age = value;
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

    public Player(int id,string? playerName, int age, char mark, int playerSequenceNumber)//, int maxNameLeangth = GameConstants.MaxNameLeangth, int minNameLeangth = GameConstants.MinNameLeangth,int maxPlayerAge = GameConstants.MaxPlayerAge,int minPlayerAge = GameConstants.MinPlayerAge
    {
        try
        {
            
            PlayerName = playerName;
            Age = age;
            Mark = mark;
            PlayerSequenceNumber = playerSequenceNumber;

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
        Console.WriteLine($"Player #{playerNumber} Input your Id Name Age");
        return Console.ReadLine();
    }

    public static void ParsePlayerInfo(string? playerInfo,out int id,out string name,out int age)
    {
        if (playerInfo != null)
        {
            string temp = Regex.Replace(playerInfo, @"\s+", " ").Trim();
            Regex regex = new Regex(@"^\d+\s\w+\s\d{1,3}$");
            bool isValid = regex.IsMatch(temp);

            if (isValid)
            {
                string[] nums = temp.Split(' ');
                id = Convert.ToInt32(nums[0]);
                name = nums[1];
                age = Convert.ToInt32(nums[2]);
            }
            else
            {
                throw new Exception($"Wrong format of Info,must be id name age, for example: 13 Vlad 19");
            }
        }
        else
        {
            throw new NullReferenceException("Information about Player can't be null");
        }
    }
}

