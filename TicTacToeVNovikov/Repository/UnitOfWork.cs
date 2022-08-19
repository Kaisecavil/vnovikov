using TicTacToeVNovikov.Interfaces;

namespace TicTacToeVNovikov.Repository
{
    internal class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext _dbContext;
        private BaseRepository<Player> _players;
        private BaseRepository<GameResult> _gameResults;
        private bool disposed = false;

        public UnitOfWork(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<Player> Players
        {
            get
            {
                return _players ??
                    (_players = new BaseRepository<Player>(_dbContext));
            }
        }

        public IRepository<GameResult> GameResults
        {
            get
            {
                return _gameResults ??
                    (_gameResults = new BaseRepository<GameResult>(_dbContext));
            }
        }

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
