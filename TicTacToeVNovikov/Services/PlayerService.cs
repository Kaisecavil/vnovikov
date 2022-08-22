using TicTacToeVNovikov.Models;
using TicTacToeVNovikov.Resources;

namespace TicTacToeVNovikov.Services
{
    internal static class PlayerService
    {
        /// <summary>
        /// Method that is responsible for registering players
        /// </summary>
        /// <param name="playersCount">players count in the game</param>
        /// <returns>List of players</returns>
        public static List<Player> PlayersGeristration(int playersCount)
        {
            List<Player> playerList = new List<Player>();
            for (int i = 1; i <= playersCount; i++)
            {
                while (true)
                {
                    try
                    {
                        int id;
                        string? name = null;
                        int age;
                        bool isRegistered = false;
                        Player.ParsePlayerInfo(Player.AskForPlayerInfo(i), out id, out name, out age, out isRegistered);
                        if (isRegistered)
                        {
                            Player findedPlayer = DbService.Unit.Players.GetById(id);
                            if (findedPlayer != null)
                            {
                                if (findedPlayer.PlayerName == name)
                                {
                                    Player player = new Player(id, name, age);
                                    if (findedPlayer.Age != age)
                                    {
                                        try
                                        {
                                            findedPlayer.Age = age;
                                            playerList.Add(player);
                                            DbService.Unit.Commit();
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                            continue;
                                        }

                                    }
                                    else
                                    {
                                        playerList.Add(player);
                                    }

                                }
                                else
                                {
                                    throw new Exception(string.Format(Strings.IdIsOccupied, id));
                                }
                            }
                            else
                            {
                                Console.WriteLine(Strings.UnknownPlayer);
                                continue;
                            }
                        }
                        else
                        {
                            Player player = new Player(name, age);
                            DbService.Unit.Players.Insert(player);
                            DbService.Unit.Commit();
                            Player playerWithId = new Player(DbService.Unit.Players.GetLast().Id, name, age);
                            playerList.Add(playerWithId);
                            Console.WriteLine(Strings.SuccessfullRegistation, playerWithId.PlayerName, playerWithId.Id);
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
            return playerList;
        }
    }
}
