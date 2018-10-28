﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel;
using Microsoft.EntityFrameworkCore;

namespace MSSQLRepository
{
    public class MSSQLRepo<TEntity> : IRepository<TEntity> where TEntity : class
    {
        TaxiSOSContext _context;
        DbSet<TEntity> _dbSet;

        public MSSQLRepo(TaxiSOSContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }
        public TEntity FindById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public void Create(TEntity item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }
        public void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void Remove(TEntity item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }
    }
}
