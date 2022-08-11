using System.Text.RegularExpressions;
using TicTacToeVNovikov.Constants;

namespace TicTacToeVNovikov
{
    internal class Game //class that responsible for game logic
    {
        private int turnCounter;
        private int amountOfSkippedTurns;
        private List<Player> playerList;
        private GameField gameField;
        private Player currentPlayer;
        private int mistakesInRow;

        public Game()
        {
            turnCounter = 0;
            playerList = new List<Player>();
            gameField = new GameField(GameConstants.FieldSize, GameConstants.FieldSize);
            mistakesInRow = 0;
        }
        public bool AskForNewGame()
        {
            Console.WriteLine("Would you like to start new game?..Press Enter to begin or any othe button to exit");
            return Console.ReadKey().Key == ConsoleKey.Enter;
        }
        public void Startgame()
        {
            PlayersRegister();
            currentPlayer = playerList[0];
            gameField.DisplayField();
            int winnerIndex = 0;
            while (winnerIndex == 0)
            {
                MakeTurn(currentPlayer);
                gameField.CheckVictory(currentPlayer,turnCounter,amountOfSkippedTurns,out winnerIndex);
                PassTurn();
            }
            if(winnerIndex != -1)
            {
                Console.WriteLine($"Player#{playerList[winnerIndex-1].PlayerSequenceNumber} ({playerList[winnerIndex-1].PlayerName}) is winner");
            }
            else
            {
                Console.WriteLine("DRAW");
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
                    ParseTurnInfo(AskPlayerForTurn(player), out x, out y);
                    gameField.PutMark(x, y, player.Mark);
                    gameField.DisplayField();
                    break;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    mistakesInRow++;
                    if (mistakesInRow >= GameConstants.MaxMistakesCount)
                    {
                        //skip turn if player done to much mistakes
                        Console.WriteLine($"is's yours {mistakesInRow}-th mistake in a row, your turn is skipped");
                        amountOfSkippedTurns++;
                        mistakesInRow=0;
                        return;
                    }
                    continue;
                }
            }
        }

        private void PassTurn()
        {
            turnCounter++;
            int necesseryPlayerSequenceNum = turnCounter % GameConstants.PlayersCount;
            currentPlayer = playerList[necesseryPlayerSequenceNum];
            mistakesInRow = 0;
        }

        private void PlayersRegister()
        {
            for (int i = 1;i<=GameConstants.PlayersCount;i++)
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine($"Player #{i} input your name:");
                        char mark = GameConstants.GameMarks[i];
                        playerList.Add(new Player(Console.ReadLine(), mark,i));
                        break;
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Invalid player name: " + e.Message);
                        continue;
                    }
                }
            }
        }

        private string AskPlayerForTurn(Player player) //Метод запрашиваюший у нужного игрока строку на ввод
        {
            Console.WriteLine($"Player #{player.PlayerSequenceNumber} ({player.PlayerName}) input two numbers in range of [1-{GameConstants.FieldSize}]:");
            return Console.ReadLine();
            
        }

        private void ParseTurnInfo(string turnStr, out int x, out int y)//Method for parsing Players input
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
                throw new Exception($"Wrong format of Info,must be two numbers from 1 to {GameConstants.FieldSize}");
            }

        }
    }
}
