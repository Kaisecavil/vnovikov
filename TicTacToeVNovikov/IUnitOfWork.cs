using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeVNovikov
{
    internal interface IUnitOfWork : IDisposable
    {
        IRepository<Player> Players { get; }
        IRepository<GameResult> GameResults { get; }
        void Commit();
    }
}
