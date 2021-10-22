using Microsoft.EntityFrameworkCore;
using ProductManagement.Business.Interfaces;
using ProductManagement.Business.Models;
using ProductManagement.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductManagement.Data.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : Entity, new()
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbEntity;

        protected Repository(AppDbContext context)
        {
            _context = context;
            _dbEntity = _context.Set<T>();
        }

        public virtual async Task Adicionar(T entity)
        {
            _dbEntity.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(T entity)
        {
            _dbEntity.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remover(Guid id)
        {
            _dbEntity.Remove(new T { Id = id });
            await SaveChanges();
        }

        public virtual async Task<T> ObterPorId(Guid id)
        {
            return await _dbEntity.FindAsync(id);
        }

        public virtual async Task<IList<T>> ObterTodos()
        {
            return await _dbEntity.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> Buscar(Expression<Func<T, bool>> predicate)
        {
            return await _dbEntity.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}