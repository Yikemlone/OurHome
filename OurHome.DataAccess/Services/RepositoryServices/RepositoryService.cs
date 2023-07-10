using Microsoft.EntityFrameworkCore;
using OurHome.DataAccess.Context;
using OurHome.DataAccess.Services.RepositoryServices;

namespace OurHome.DataAccess.Services.RepositoryServices
{
    public class RepositoryService<T> : IRepositoryService<T> where T : class
    {
        private readonly OurHomeDbContext _context;
        internal DbSet<T> dbSet;

        public RepositoryService(OurHomeDbContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
        }

        public void Add(T obj)
        {
            dbSet.Add(obj);
        }

        public void Update(T obj)
        {
            dbSet.Update(obj);
        }

        public void Delete(T obj)
        { 
            dbSet.Remove(obj);
        }

        public T Get(int id)
        {
            if (id == 0) return null;
            else return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> list = dbSet;
            return list.ToList();
        }
    }
}
