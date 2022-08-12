namespace TicTacToeVNovikov;

internal class Player
{

    public char Mark { get; private set; }
    public int PlayerSequenceNumber { get; private set; }
    private int _MaxNameLeangth;
    private int _MinNameLeangth;
    private string? _PlayerName;
    public string? PlayerName
    {
        get
        {
            return _PlayerName;
        }
        private set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                if (_MinNameLeangth <= value.Length && value.Length <= _MaxNameLeangth)
                {
                    _PlayerName = value;
                }
                else
                {
                    throw new ArgumentException($"Player name length must be beetwen {_MinNameLeangth} and {_MaxNameLeangth} characters");
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

    public Player(string? name, char mark, int sequenceNumber, int maxNameLeangth = GameConstants.MaxNameLeangth, int minNameLeangth = GameConstants.MinNameLeangth)
    {
        try
        {
            _MaxNameLeangth = maxNameLeangth;
            _MinNameLeangth = minNameLeangth;
            PlayerName = name;
            Mark = mark;
            PlayerSequenceNumber = sequenceNumber;

        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}

