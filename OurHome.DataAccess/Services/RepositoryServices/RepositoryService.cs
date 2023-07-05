using Microsoft.EntityFrameworkCore;
using OurHome.DataAccess.Context;
using OurHome.DataAccess.Services.RepoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public Task<bool> Add(T obj)
        {
            dbSet.Add(obj);
            return
        }

        public Task<bool> Delete(T obj)
        {
            throw new NotImplementedException();
        }

        public Task<T> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
