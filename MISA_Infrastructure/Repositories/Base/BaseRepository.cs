using Microsoft.EntityFrameworkCore;
using MISA.Core.Exceptions;
using MISA.Infrastructure.Database;
using MISA_Core.Interface.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_Infrastructure.Repositories.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual IEnumerable<T>? GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public virtual T? GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public virtual T Insert(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual T Update(Guid id, T entity)
        {
            var existing = GetById(id);
            if (existing == null)
            {
                throw new ValidateException($"Không tìm thấy bản ghi có ID = {id}");
            }

            _context.Entry(existing).CurrentValues.SetValues(entity);
            _context.SaveChanges();

            return existing;
        }

        public virtual Guid Delete(Guid id)
        {
            var entity = GetById(id);
            if (entity == null)
                throw new ValidateException($"Không tìm thấy bản ghi có ID = {id}");

            _dbSet.Remove(entity);
            _context.SaveChanges();

            return id;
        }
    }
}
