using System;
using System.Linq;

namespace KonigLabs.LevisJeans.Models.Contexts
{
    public class DataContext : IDataContext, IDisposable
    {
        private readonly MainDataContext _entities;

        public DataContext(MainDataContext entities)
        {
            _entities = entities;
        }

        public IQueryable<T> GetQuery<T>() where T : class
        {
            return _entities.Set<T>();
        }

        public void Add<T>(T item) where T : class
        {
            _entities.Set<T>().Add(item);
        }

        public void Remove<T>(T item) where T : class
        {
            _entities.Set<T>().Remove(item);
        }

        public void RemoveAll(string tableName)
        {
            _entities.Database.ExecuteSqlCommand($"delete from [dbo].[{tableName}];");
        }

        public void Commit()
        {
            /*var modifiedOrAddedEntities = _entities.ChangeTracker.Entries()
                .Where(p=>p.State == EntityState.Modified 
                || p.State == EntityState.Added)
                    .Select(x => x.Entity).ToList();
            foreach (var entity in modifiedOrAddedEntities)
            {
                var o = entity;
            }*/
            _entities.SaveChanges();
        }

        public void Dispose()
        {

        }
    }
}