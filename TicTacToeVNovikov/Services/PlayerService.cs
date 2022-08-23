using TicTacToeVNovikov.GameConstants;
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
        public static List<Player> PlayersRegistration(int playersCount)
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
                        try
                        {
                            ShowAllPlayers();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        Player.ParsePlayerInfo(Player.AskForPlayerInfo(i), out id, out name, out age, out isRegistered);
                        if (isRegistered)
                        {
                            Player foundPlayer = DbService.Unit.Players.GetById(id);
                            if (foundPlayer != null)
                            {
                                
                                Player player = new Player(id, name, age);
                                if (NoAnySimilarPlayer(playerList, player))
                                {
                                    if (foundPlayer.Name != name)
                                    {
                                        try
                                        {
                                            foundPlayer.Name = name;  
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                            continue;
                                        }
                                        //throw new Exception(string.Format(Strings.IdIsOccupied, id));
                                    }

                                    if (foundPlayer.Age != age)
                                    {
                                        try
                                        {
                                            foundPlayer.Age = age;  
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                            continue;
                                        }

                                    }
                                    playerList.Add(player);
                                    DbService.Unit.Commit();
                                }
                                else
                                {
                                    throw new Exception(string.Format(Strings.SimilarPlayerException));
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
                            Console.WriteLine(Strings.SuccessfullRegistation, playerWithId.Name, playerWithId.Id);
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

        private static void ShowAllPlayers()
        {

            List<Player> allPlayers = DbService.Unit.Players.GetAll().ToList();
            if (allPlayers.Count != 0)
            {
                Console.WriteLine($"{Constants.TableParameters.PlayersTableColumnDelimeter}{Strings.ID,-Constants.TableParameters.PlayersTableIdColumnWidth}{Constants.TableParameters.PlayersTableColumnDelimeter}{Strings.Name,-Constants.GameLimits.MaxNameLeangth}{Constants.TableParameters.PlayersTableColumnDelimeter}{Strings.Age,-Constants.TableParameters.PlayersTableAgeColumnWidth}{Constants.TableParameters.PlayersTableColumnDelimeter}");
                foreach (Player player in allPlayers)
                {
                    
                    string outStr = string.Format($"{Constants.TableParameters.PlayersTableColumnDelimeter}{player.Id,-Constants.TableParameters.PlayersTableIdColumnWidth}{Constants.TableParameters.PlayersTableColumnDelimeter}{player.Name,-Constants.GameLimits.MaxNameLeangth}{Constants.TableParameters.PlayersTableColumnDelimeter}{player.Age,-Constants.TableParameters.PlayersTableAgeColumnWidth}{Constants.TableParameters.PlayersTableColumnDelimeter}");
                    Console.WriteLine(new String(Constants.TableParameters.PlayersTableRowDelimeter, outStr.Length));
                    Console.WriteLine(outStr);
                    
                }
            }
            else
            {
                throw new Exception(Strings.EmptyPlayerDbSet);
            }
        }

        private static bool NoAnySimilarPlayer(List<Player> players,Player playerToCheck)
        {
            foreach (Player player in players)
            {
                if (player.Equals(playerToCheck))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
