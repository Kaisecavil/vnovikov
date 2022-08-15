using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeVNovikov
{
    internal class PlayerRepositiry : IRepository<Player>
    {
        private ApplicationContext _context;
        private DbSet<Player> _players;

        public PlayerRepositiry(ApplicationContext applicationContext)
        {
            _context = applicationContext;
            _players = _context.Players;
        }

        public IEnumerable<Player> GetAll()
        {
            return _players.ToList();
        }
        public Player GetById(int id)
        {
            Player player = _players.Find(id);
            return player;
        }
        public void Insert(Player entity)
        {
            _players.Add(entity);
        }
        public void Update(Player entity)
        {
            _players.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(int id) 
        {
            Player player = _players.Find(id);
            if(player != null)
            {
                _players.Remove(player);
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
