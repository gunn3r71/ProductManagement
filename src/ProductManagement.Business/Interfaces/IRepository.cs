﻿using ProductManagement.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductManagement.Business.Interfaces
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
        Task Adicionar(T entity);
        Task Atualizar(T entity);
        Task Remover(Guid id);
        Task<T> ObterPorId(Guid id);
        Task<IList<T>> ObterTodos();
        Task<IEnumerable<T>> Buscar(Expression<Func<T, bool>> predicate);
        Task<int> SaveChanges();
    }
}