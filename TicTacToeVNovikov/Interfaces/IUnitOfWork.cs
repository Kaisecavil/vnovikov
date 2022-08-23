using TicTacToeVNovikov.Models;

namespace TicTacToeVNovikov.Interfaces
{
    /// <summary>
    /// Interface for realization of UnitOfWork Pattern
    /// </summary>
    internal interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Property that provides access to DbSet of <see cref="Player"/> type
        /// </summary>
        IRepository<Player> Players { get; }

        /// <summary>
        /// Property that provides access to DbSet of <see cref="GameResult"/> type
        /// </summary>
        IRepository<GameResult> GameResults { get; }

        /// <summary>
        /// Method that save all changes made in context
        /// </summary>
        void Commit();
    }
}
