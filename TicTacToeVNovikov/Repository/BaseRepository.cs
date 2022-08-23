using Microsoft.EntityFrameworkCore;
using TicTacToeVNovikov.Interfaces;
using TicTacToeVNovikov.Models;

namespace TicTacToeVNovikov.Repository
{
    /// <summary>
    /// Class that realize Repository pattern and helps to interact with necessary <seealso cref="DbSet{TEntity}"/> in database.
    /// </summary>
    /// <typeparam name="TEntity">Any class that presents in <seealso cref="ApplicationContext"/> like DbSet parameter,in our case it can be <see cref="Player"/> or <see cref="GameResult"/></typeparam>
    internal class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Data base context
        /// </summary>
        private ApplicationContext _context;
        /// <summary>
        /// DbSet of entities with necessary type
        /// </summary>
        private DbSet<TEntity> _entities;

        private bool disposed = false;
        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="context">Our data base context</param>
        public BaseRepository(ApplicationContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        
        /// <inheritdoc/>
        public TEntity GetLast()
        {
            return GetAll().Last();
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> GetAll()
        {
            return _entities.ToList();
        }

        /// <inheritdoc/>
        public TEntity GetById(int id)
        {
            return _entities.Find(id);
        }

        /// <inheritdoc/>
        public void Insert(TEntity entity)
        {
            _entities.Add(entity);
        }




        /// <inheritdoc/>
        public void Update(TEntity entity)
        {
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        /// <inheritdoc/>
        public void Delete(int id)
        {
            TEntity entity = _entities.Find(id);
            _entities.Remove(entity);
        }

        /// <inheritdoc/>
        public void Save()
        {
            _context.SaveChanges();
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
                    _context.Dispose();
                }
            }
            disposed = true;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
