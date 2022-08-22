using TicTacToeVNovikov.Models;

namespace TicTacToeVNovikov.Interfaces
{
    internal interface IUnitOfWork : IDisposable
    {
        IRepository<Player> Players { get; }
        IRepository<GameResult> GameResults { get; }
        void Commit();
    }
}
