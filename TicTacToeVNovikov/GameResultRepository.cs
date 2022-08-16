using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeVNovikov
{
    internal class GameResultRepository : IRepository<GameResult>
    {
        private ApplicationContext _context;
        private DbSet<GameResult> _gameResults;

        public GameResultRepository(ApplicationContext applicationContext)
        {
            _context = applicationContext;
            _gameResults = _context.GameResults;
        }

        public IEnumerable<GameResult> GetAll()
        {
            return _gameResults.ToList();
        }
        public GameResult GetById(int id)
        {
            GameResult gameResult = _gameResults.Find(id);
            return gameResult;
        }
        public void Insert(GameResult entity)
        {
            _gameResults.Add(entity);
        }
        public void Update(GameResult entity)
        {
            _gameResults.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            GameResult gameResult = _gameResults.Find(id);
            if (gameResult != null)
            {
                _gameResults.Remove(gameResult);
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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

