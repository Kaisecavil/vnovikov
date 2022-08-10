using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp2.Constants;

namespace ConsoleApp2
{
    public class Player
    {
        private string playerName;

        public string PlayerName
        {
            get { return playerName; }
            private set
            { 
                if(value is not null)
                {
                    if (value.Length != 0)
                    {
                        if (value.Length <= GameConstants.MAX_NAME_LENGTH)
                        {
                            playerName = value;
                        }
                        else
                        {
                            throw new Exception($"Length of Player name if greater than Max valid leangth = {GameConstants.MAX_NAME_LENGTH}");
                        }
                    }
                    else
                    {
                        throw new Exception("name leangth is 0");
                    }
                }
                else
                {
                    throw new Exception("name is null");
                }
            }
        }

        public int PlayerNumber { get; private set; }
        public char Mark { get; private set; }

        public Player(string playerName, int playerNumber)
        {
            PlayerName = playerName;
            PlayerNumber = playerNumber;
            Mark = GameConstants.GAME_MARKS[playerNumber-1];
        }
    }
}
