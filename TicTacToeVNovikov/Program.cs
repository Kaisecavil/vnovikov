
namespace TicTacToeVNovikov;
internal class Program
{
    static void Main(string[] args)
    {
        using (PlayerRepositiry pr = new(new ApplicationContext()))
        {
            Player player = new Player(3, "gleb", 21);
            //db.Add(player);
            //db.SaveChanges();
            pr.Insert(player);
            pr.Save();
            using(GameResultRepository gmr = new(new ApplicationContext()))
            {
                GameResult gameResult = new(1, "12:23:45", "12:25:01", 1, 2, 'X', 'O', 1);
                gmr.Insert(gameResult);
                gmr.Save();
            }
            return;
        }
        while (true)
        {

            if (Game.AskForNewGame())
            {
                Game game = new Game();
                game.Startgame();
            }
            else { break; }
        }
    }
}