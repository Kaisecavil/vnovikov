﻿using Microsoft.EntityFrameworkCore;

namespace TicTacToeVNovikov
{
    internal class ApplicationContext : DbContext 
    {
        public DbSet<Player> Players => Set<Player>();
        public DbSet<GameResult> GameResults => Set<GameResult>();

        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=vladTicTacToe.db");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Player>().Ignore(b => b.);
        //}
    }
}