using TicTacToeVNovikov.Constants;

namespace TicTacToeVNovikov
{
    internal class Player
    {        
        public char Mark { get; private set; }
        public int PlayerSequenceNumber { get; private set; }
        private string _PlayerName;
        public string PlayerName
        {
            get
            {
                return _PlayerName;
            }
            set
            {
                if (value != _PlayerName)
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        if (GameConstants.MinNameLeangth <= value.Length && value.Length <= GameConstants.MaxNameLeangth)
                        {
                            _PlayerName = value;
                        }
                        else
                        {
                            throw new ArgumentException($"Player name length must be beetwen {GameConstants.MinNameLeangth} and {GameConstants.MaxNameLeangth} characters");
                        }
                    }
                    else if (value == null)
                    {
                        throw new ArgumentNullException("Plaer name can't be null");
                    }
                    else
                    {
                        throw new ArgumentException("Player name can't be empty or contain only whitespaces");
                    }
                }
            }
        }

        public Player(string name,char mark,int sequenceNum)
        {
            try
            {
                PlayerName = name;
                Mark = mark;
                PlayerSequenceNumber = sequenceNum;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }      
        }
    }
}
