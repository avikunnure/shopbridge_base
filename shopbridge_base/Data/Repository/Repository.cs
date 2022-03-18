using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Shopbridge_base.Data.Repository
{
    public class Repository : IRepository
    {
        private readonly Shopbridge_Context dbcontext;

        public Repository(Shopbridge_Context _dbcontext)
        {
            this.dbcontext = _dbcontext;
        }

        public IQueryable<T> AsQueryable<T>() where T : class
        {
            return this.dbcontext.Set<T>().AsQueryable();
        }

        public IQueryable<T> Get<T>(params Expression<Func<T, object>>[] navigationProperties) where T : class
        {
            var QueyQ = this.dbcontext.Set<T>().AsQueryable();
            foreach(var navigationProperty in navigationProperties)
            {
                QueyQ= IncludeOne(QueyQ, navigationProperty);
            }
            return QueyQ;
        }

        public IQueryable<T> Get<T>(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties) where T : class
        {
            var QueyQ = this.dbcontext.Set<T>().AsQueryable();
            foreach (var navigationProperty in navigationProperties)
            {
                QueyQ = IncludeOne(QueyQ, navigationProperty);
            }
            return QueyQ.Where(where);
        }

        public IEnumerable<T> Get<T>() where T : class
        {
            return this.dbcontext.Set<T>().ToList();
        }

        private IQueryable<T> IncludeOne<T>(IQueryable<T> query, Expression<Func<T, object>> navigationProperties) where T : class
        {
            return query.Include<T, object>(navigationProperties);
        }
    }
}
