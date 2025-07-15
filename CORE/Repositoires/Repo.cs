using CORE.Entities;
using Microsoft.EntityFrameworkCore;

namespace CORE.Repositoires
{
    // CRUD Repository Base
    public abstract class Repo<TEntity> : IDisposable where TEntity : Entity, new()
    {
        private readonly DbContext _db; // Dependency Injection

        protected Repo(DbContext db) // Constructor Injection
        {
            _db = db;
        }

        public virtual IQueryable<TEntity> Query()
        {
            return _db.Set<TEntity>().OrderByDescending(entity => entity.UpdateDate)
                .ThenByDescending(entity => entity.CreateDate)
                .Where(entity => entity.IsDeleted ?? false == false);
        }

        public virtual void Create(TEntity entity, bool save = true)
        {
            entity.CreateDate = DateTime.Now;
            _db.Set<TEntity>().Add(entity);
            if (save)
                Save(); 
        }

        public virtual void Update(TEntity entity, bool save = true)
        {
            entity.UpdateDate = DateTime.Now;
            _db.Set<TEntity>().Update(entity);
            if (save)
                Save();
        }

        public virtual void Delete(TEntity entity, bool softDelete = true, bool save = true)
        {
            if (softDelete)
            {
                entity.IsDeleted = true;
                _db.Set<TEntity>().Update(entity);
            }
            else
            {
                _db.Set<TEntity>().Remove(entity);
            }
            if (save)
                Save();
        }

        public void Delete(int id, bool softDelete = true, bool save = true)
        {
            var entity = _db.Set<TEntity>().Find(id);
            if (entity is null)
                return;
            Delete(entity, softDelete, save);
        }

        public virtual int Save()
        {
            return _db.SaveChanges(); // Unit of Work
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
