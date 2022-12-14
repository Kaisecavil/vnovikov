using System.Text.RegularExpressions;
using TicTacToeVNovikov.GameConstants;
using TicTacToeVNovikov.Resources;

namespace TicTacToeVNovikov.Models;

/// <summary>
/// Class that describes entity of player.
/// </summary>
internal class Player
{
    /// <summary>
    /// Maximal allowed player's name length
    /// </summary>
    static private int _maxNameLength;

    /// <summary>
    /// Minimal allowed player's name length
    /// </summary>
    static private int _minNameLength;

    /// <summary>
    /// Maximal allowed player's age
    /// </summary>
    static private int _maxPlayerAge;

    /// <summary>
    /// Minimal allowed player's age
    /// </summary>
    static private int _minPlayerAge;

    /// <summary>
    /// Player's Id.
    /// </summary>
    public int Id { get; private set; }

    private int _age;

    /// <summary>
    /// Player age property that realize logic of get and set methods in the right way
    /// </summary>
    public int Age
    {
        get
        {
            return _age;
        }
        set
        {
            if (value < _maxPlayerAge && value > _minPlayerAge)
            {
                if (value > Age)
                {
                    _age = value;
                }
                else
                {
                    throw new Exception(Strings.LesserAge);
                }

            }
            else
            {
                throw new Exception(string.Format(Strings.AgeIsOutOfLimits, _minPlayerAge, _maxPlayerAge));
            }
        }
    }
    private string? _name;

    /// <summary>
    /// Player name property that realize logic of get and set methods in the right way
    /// </summary>
    public string? Name
    {
        get
        {
            return _name;
        }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                if (_minNameLength <= value.Length && value.Length <= _maxNameLength)
                {
                    _name = value;
                }
                else
                {
                    throw new ArgumentException(string.Format(Strings.PlayerNameLengthIsOutOfLimits, _minNameLength, _maxNameLength));
                }
            }
            else if (value == null)
            {
                throw new ArgumentNullException(Strings.NullPlayerName);
            }
            else
            {
                throw new ArgumentException(Strings.EmptyPlayerName);
            }
        }
    }

    /// <summary>
    /// Static constructor that initializes static fields
    /// </summary>
    static Player()
    {
        _maxPlayerAge = Constants.GameLimits.MaxPlayerAge;
        _minPlayerAge = Constants.GameLimits.MinPlayerAge;
        _maxNameLength = Constants.GameLimits.MaxNameLeangth;
        _minNameLength = Constants.GameLimits.MinNameLeangth;
    }

    /// <summary>
    /// Main constructor that initializes all fields and used by EntityFramework to create entities
    /// </summary>
    /// <param name="id">Player's id</param>
    /// <param name="playerName">Player's name</param>
    /// <param name="age">Player's age</param>
    /// <exception cref="Exception">If any player parameters are invalid, an exception will be thrown with the corresponding message</exception>
    public Player(int id, string? name, int age)
    {
        try
        {
            Id = id;
            Name = name;
            Age = age;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    /// <summary>
    /// Constructor that initializes all fields but ID and used by EntityFramework to create entities where id will be autoincremented
    /// </summary>
    /// <param name="playerName">Player's name</param>
    /// <param name="age">Player's age</param>
    /// <exception cref="Exception">If any player parameters are invalid, an exception will be thrown with the corresponding message</exception>
    public Player(string playerName, int age)
    {
        try
        {
            Name = playerName;
            Age = age;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    /// <summary>
    /// A method that prompt the user to enter a presonal information.
    /// </summary>
    /// <param name="playerNumber">Player's order number</param>
    /// <returns>Any string expression or <see cref="System"/></returns>
    public static string? AskForPlayerInfo(int playerNumber)
    {
        Console.WriteLine(Strings.AskForPlayerInfo, playerNumber);
        return Console.ReadLine();
    }

    /// <summary>
    /// Method that parse string in certain way to get information about player
    /// </summary>
    /// <param name="playerInfo">String that probably containt information about player</param>
    /// <param name="id">Player's id</param>
    /// <param name="name">Player's name</param>
    /// <param name="age">Player's age</param>
    /// <param name="isRegistered">bool variable that shows whether the user is registered</param>
    /// <exception cref="Exception">Invalid format of player's personal information</exception>
    /// <exception cref="NullReferenceException">Player's information is null</exception>
    public static void ParsePlayerInfo(string? playerInfo, out int id, out string name, out int age, out bool isRegistered)
    {
        if (playerInfo != null)
        {
            string temp = Regex.Replace(playerInfo, Constants.GameRegularExpressions.WightSpaces, " ").Trim();
            Regex regexRegistered = new Regex(Constants.GameRegularExpressions.Registered);
            Regex regexUnregistered = new Regex(Constants.GameRegularExpressions.Unregistered);
            if (regexRegistered.IsMatch(temp))//We parse our string depends onwhat RexEx is would be valid
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
                isRegistered = false;
            }
            else
            {
                throw new Exception(Strings.WrongFormatOfPlayerInfo);
            }
        }
        else
        {
            throw new NullReferenceException(Strings.NullPlayerInfo);
        }
    }

    /// <summary>
    /// override of Equals method
    /// </summary>
    /// <param name="obj">obj to compare</param>
    /// <returns>true if equals , false otherwise</returns>
    public override bool Equals(Object obj)
    {
        //Check for null and compare run-time types.
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            Player player = (Player)obj;
            return (this.Id == player.Id) ||((this.Name == player.Name) && (this.Age == player.Age));
        }
    }
}

