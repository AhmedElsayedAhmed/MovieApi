using MovieApp.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieApp.Data
{
    public interface IRepository<TEntity> where TEntity : BaseEntities
    {


        TEntity GetById(int Id);
        IList<TEntity> GetAll();
        void Update(TEntity Entity);
        void Update(IList<TEntity> Entity); // UpdateRange

        void Insert(TEntity Entity);
        void Insert(IList<TEntity> Entity); // AddRange

        void Delete(TEntity Entity);
        void Delete(IList<TEntity> Entity); // DeleteRange

        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }
        void SaveDb(); // SaveChanges 
    }
}
