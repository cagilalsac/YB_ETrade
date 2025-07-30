using CORE.Entities;
using Microsoft.EntityFrameworkCore;

namespace CORE.Repositoires
{
    public class Repo<TEntity> : RepoBase<TEntity> where TEntity : Entity, new()
    {
        public Repo(DbContext db) : base(db)
        {
        }
    }
}
