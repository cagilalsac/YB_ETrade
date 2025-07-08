using Microsoft.EntityFrameworkCore;

namespace CORE.Repositoires
{
    // CRUD
    public abstract class RepoBase<TEntity>
    {
        DbContext _db;

        public IQueryable<TEntity> Query()
        {
            return _db.Set<TEntity>();
        }
    }
}
