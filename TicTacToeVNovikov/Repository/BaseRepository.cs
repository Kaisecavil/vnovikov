using Microsoft.EntityFrameworkCore;
using TicTacToeVNovikov.Interfaces;

namespace TicTacToeVNovikov.Repository
{
    internal class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private ApplicationContext _context;
        private DbSet<TEntity> _entities;
        private bool disposed = false;
        public BaseRepository(ApplicationContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public TEntity GetLast()
        {
            return GetAll().Last();
        }
        public IEnumerable<TEntity> GetAll()
        {
            return _entities.ToList();
        }
        public TEntity GetById(int id)
        {
            return _entities.Find(id);
        }

        public void Insert(TEntity entity)
        {
            _entities.Add(entity);
        }

        public void Delete(int id)
        {
            TEntity entity = _entities.Find(id);
            _entities.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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
