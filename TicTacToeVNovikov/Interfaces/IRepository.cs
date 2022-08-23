namespace TicTacToeVNovikov.Interfaces

{
    /// <summary>
    /// Interface for realization of Repository Pattern
    /// </summary>
    internal interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        /// <summary>
        /// Method that returns last record in <seealso cref="DbSet{TEntity}"/>
        /// </summary>
        /// <returns>Last Entity in <seealso cref="entities"/></returns>
        TEntity GetLast();
        /// <summary>
        /// Method that allows to get all entities from <seealso cref="DbSet{TEntity}"/>
        /// </summary>
        /// <returns>returns list of entities</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Method that allows to get necessary record in <seealso cref="DbSet{TEntity}"/> by id
        /// </summary>
        /// <param name="id">id to find necessary record in DbSet</param>
        /// <returns>Entity with necessery id or null</returns>
        TEntity GetById(int id);

        /// <summary>
        /// Method that adds a record in DbSet
        /// </summary>
        /// <param name="entity">Entity that you want to add</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Method that tells context to track entity
        /// </summary>
        /// <param name="entity">Necessary entity to track</param>
        void Update(TEntity entity);

        /// <summary>
        /// Method that delete necessry record in DbSet by id
        /// </summary>
        /// <param name="id">id to find necessary record in DbSet</param>
        void Delete(int id);

        /// <summary>
        /// Method that save all made changes in context
        /// </summary>
        void Save();
    }
}
