using TicTacToeVNovikov.Interfaces;
using TicTacToeVNovikov.Models;
using TicTacToeVNovikov.Resources;

namespace TicTacToeVNovikov.Repository
{
    /// <summary>
    /// Class that encapsulates all necessary <seealso cref="Repository.GenericRepository{TEntity}"></seealso> and provides access to them.
    /// </summary>
    /// 
    internal class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext _dbContext;
        private GenericRepository<Player>? _players;
        private GenericRepository<GameResult>? _gameResults;
        private bool disposed = false;

        /// <summary>
        /// Main constructor that took context of data base as parameter.
        /// </summary>
        /// <param name="dbContext">Data base context</param>
        public UnitOfWork(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        
        /// <inheritdoc/>
        public IRepository<Player> Players
        {
            get
            {
                return _players ??
                    (_players = new GenericRepository<Player>(_dbContext));
            }
        }
        
        /// <inheritdoc/>
        public IRepository<GameResult> GameResults
        {
            get
            {
                return _gameResults ??
                    (_gameResults = new GenericRepository<GameResult>(_dbContext));
            }
        }

        
        /// <inheritdoc/>
        public void Commit()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(Strings.DbSavingException, ex.Message);
            }
        }

        /// <summary>
        /// realization of <see cref="IDisposable"/> , so that allows to use "using(...)" constraction
        /// </summary>
        /// <param name="disposing">Is disposing</param>
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
