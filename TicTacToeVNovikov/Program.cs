
namespace TicTacToeVNovikov;
internal class Program
{
    static void Main(string[] args)
    {
        using(ApplicationContext db = new ApplicationContext())
        {
            Player player = new Player(1, "vlad", 19, 'X', 1);
            db.Add(player);
            db.SaveChanges();
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