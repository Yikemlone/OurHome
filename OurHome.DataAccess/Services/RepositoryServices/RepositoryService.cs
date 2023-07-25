
using Microsoft.EntityFrameworkCore;
using OurHome.DataAccess.Context;
using OurHome.DataAccess.Services.RepositoryServices;

namespace FlashCardBlazorApp.DataAccess.Services.RepositoryService
{
    public class RepositoryService<T> : IRepositoryService<T> where T : class 
    {
        private readonly OurHomeDbContext _context;
        internal DbSet<T> dbSet;

        public RepositoryService(OurHomeDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T obj)
        {
            await dbSet.AddAsync(obj);
        }
        public void Update(T obj)
        {
            dbSet.Update(obj);
        }

        public void Delete(T obj)
        {
            dbSet.Remove(obj);
        }

        public async Task<List<T>> GetAllAsync()
        {
            IQueryable<T> list = dbSet;
            return await list.ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            if (id == 0) return null;
            else return await dbSet.FindAsync(id);
        }
    }
}   
