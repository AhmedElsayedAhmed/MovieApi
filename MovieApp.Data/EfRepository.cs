using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieApp.Data
{
    public class EfRepository<Entity> : IRepository<Entity> where Entity : BaseEntities
    {
        private readonly AppDbContext _dbContext;
        private DbSet<Entity> _entities;
        public virtual IQueryable<Entity> Table => Entities;

        public EfRepository(AppDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public void Delete(Entity Entity)
        {
            if (Entity == null)
                throw new ArgumentNullException();
            Entities.Remove(Entity);
        }

        public void Delete(IList<Entity> Entity) // Remove Range 
        {
            if (Entity == null)
                throw new ArgumentNullException();
            Entities.RemoveRange(Entity);
        }

        public Entity GetById(int Id)
        {
            return Entities.Find(Id);
        }

        public void Insert(Entity Entity)
        {
            if (Entity == null)
                throw new ArgumentNullException();
            Entities.Add(Entity);
        }

        public void Insert(IList<Entity> Entity) // Add Range
        {
            if (Entity == null)
                throw new ArgumentNullException();
            Entities.AddRange(Entity);
        }

        public void Update(Entity Entity)
        {
            if (Entity == null)
                throw new ArgumentNullException();
            //var Image = _dbContext.Set<Entity>().Update(Entity);
            //Entities.Update(Entity);
            Entities.Attach(Entity).State = EntityState.Modified;
            //_dbContext.Update(Entity);
        }

        public void Update(IList<Entity> Entity) // Update Range
        {
            if (Entity == null)
                throw new ArgumentNullException();
            _dbContext.Attach(Entity).State = EntityState.Modified; 
            Entities.UpdateRange(Entity);
        }

        protected virtual DbSet<Entity> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _dbContext.Set<Entity>();

                return _entities;
            }
        }
        public void SaveDb()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }
        public virtual IQueryable<Entity> TableNoTracking => Entities.AsNoTracking();


        public IList<Entity> GetAll()
        {
            return Entities.ToList();
        }
    }
}
