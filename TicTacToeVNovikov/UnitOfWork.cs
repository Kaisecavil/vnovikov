using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeVNovikov
{
    internal class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext _dbContext;
        private BaseRepository<Player> _players;
        private BaseRepository<GameResult> _gameResults;

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

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
