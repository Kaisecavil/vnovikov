using TicTacToeVNovikov.Interfaces;
using TicTacToeVNovikov.Models;

namespace TicTacToeVNovikov.Repository
{
    /// <summary>
    /// Class that encapsulates all necessary <seealso cref="Repository.BaseRepository{TEntity}"></seealso> and provides access to them.
    /// </summary>
    /// 
    internal class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext _dbContext;
        private BaseRepository<Player>? _players;
        private BaseRepository<GameResult>? _gameResults;
        private bool disposed = false;

        /// <summary>
        /// Main constructor that took context of data base as parameter.
        /// </summary>
        /// <param name="dbContext">Data base context</param>
        public UnitOfWork(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Property that provides access to necessary DbSet
        /// </summary>
        public IRepository<Player> Players
        {
            get
            {
                return _players ??
                    (_players = new BaseRepository<Player>(_dbContext));
            }
        }
        /// <summary>
        /// Property that provides access to necessary DbSet
        /// </summary>
        public IRepository<GameResult> GameResults
        {
            get
            {
                return _gameResults ??
                    (_gameResults = new BaseRepository<GameResult>(_dbContext));
            }
        }
        /// <summary>
        /// Method that save all changes made in context
        /// </summary>
        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
