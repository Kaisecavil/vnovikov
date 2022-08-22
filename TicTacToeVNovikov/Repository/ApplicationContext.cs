using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using TicTacToeVNovikov.Models;
using System.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace TicTacToeVNovikov.Repository
{
    internal class ApplicationContext : DbContext
    {
        /// <summary>
        /// DbSet (Table) that contains entities of <seealso cref="Player"/> type.
        /// </summary>
        public DbSet<Player> Players => Set<Player>();
        /// <summary>
        /// DbSet(Table) that contains entities of <seealso cref="GameResult"/> type
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
