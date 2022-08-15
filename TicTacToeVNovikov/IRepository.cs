using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeVNovikov
{
    internal interface IRepository<TEntity> : IDisposable where TEntity : class 
    {
        IEnumerable<TEntity> GetAll(); // получение всех объектов
        TEntity GetById(int id); // получение одного объекта по id
        void Insert(TEntity entity); // создание объекта
        void Update(TEntity entity); // обновление объекта
        void Delete(int id); // удаление объекта по id
        void Save();  // сохранение изменений
    }
}
