using Microsoft.EntityFrameworkCore;
using TicTacToeVNovikov.Models;
using System.Configuration;

namespace TicTacToeVNovikov.Repository
{
    /// <summary>
    /// Class that servse to get Data base context
    /// </summary>
    internal class ApplicationContext : DbContext
    {
        /// <summary>
        /// DbSet (Table) that contains entities of <seealso cref="Player"/> type.
        /// </summary>
        public DbSet<Player> Players => Set<Player>();
        /// <summary>
        /// DbSet(Table) that contains entities of <seealso cref="GameResult"/> type.
        /// </summary>
        public DbSet<GameResult> GameResults => Set<GameResult>();

        /// <summary>
        /// Constructor that ensures that data base is exist
        /// </summary>
        public ApplicationContext()
        { 
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.AppSettings["connectionString"]);
        }

    }
}
