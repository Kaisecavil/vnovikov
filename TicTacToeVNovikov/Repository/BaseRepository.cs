using Microsoft.EntityFrameworkCore;
using TicTacToeVNovikov.Interfaces;

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
        /// <summary>
        /// Method that returns last record in <seealso cref="DbSet{TEntity}"/>
        /// </summary>
        /// <returns>Last Entity in <seealso cref="entities"/></returns>
        public TEntity GetLast()
        {
            return GetAll().Last();
        }
        /// <summary>
        /// Method that allows to get all entities from <seealso cref="DbSet{TEntity}"/>
        /// </summary>
        /// <returns>returns list of entities</returns>
        public IEnumerable<TEntity> GetAll()
        {
            return _entities.ToList();
        }
        /// <summary>
        /// Method that allows to get necessary record in <seealso cref="DbSet{TEntity}"/> by id
        /// </summary>
        /// <param name="id">id to find necessary record in DbSet</param>
        /// <returns>Entity with necessery id or null</returns>
        public TEntity GetById(int id)
        {
            return _entities.Find(id);
        }
        /// <summary>
        /// Method that adds a record in DbSet
        /// </summary>
        /// <param name="entity">Entity that you want to add</param>
        public void Insert(TEntity entity)
        {
            _entities.Add(entity);
        }
        /// <summary>
        /// Method that delete necessry record in DbSet by id
        /// </summary>
        /// <param name="id">id to find necessary record in DbSet</param>
        public void Delete(int id)
        {
            TEntity entity = _entities.Find(id);
            _entities.Remove(entity);
        }
        /// <summary>
        /// Method that tells context to track entity
        /// </summary>
        /// <param name="entity">Necessary entity to track</param>
        public void Update(TEntity entity)
        {
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        /// <summary>
        /// Method that save all made changes in context
        /// </summary>
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
