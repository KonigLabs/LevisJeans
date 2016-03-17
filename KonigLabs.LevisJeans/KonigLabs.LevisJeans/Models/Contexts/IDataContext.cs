using System.Linq;

namespace KonigLabs.LevisJeans.Models.Contexts
{
    public interface IDataContext
    {
        IQueryable<T> GetQuery<T>() where T : class;
        void Add<T>(T item) where T : class;
        void Remove<T>(T item) where T : class;
        void RemoveAll(string tableName);
        void Reseed(string tableName);
        void Commit();
    }
}