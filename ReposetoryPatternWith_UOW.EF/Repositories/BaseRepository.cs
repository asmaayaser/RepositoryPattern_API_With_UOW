using Microsoft.EntityFrameworkCore;
using ReposetoryPatternWith_UOW.Core.Consts;
using ReposetoryPatternWith_UOW.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ReposetoryPatternWith_UOW.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected AppDBContext Context;
        public BaseRepository(AppDBContext context)
        {
            Context = context;
        }

        public IEnumerable<T> GetAll()
        {

            return Context.Set<T>().ToList();
        }


        public IEnumerable<T> GetAll(string[] includes = null)
        {
            IQueryable<T> query = Context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.ToList();
        }
        public T GetById(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public T Find(Expression<Func<T, bool>> match)
        {
            return Context.Set<T>().FirstOrDefault(match);
        }


        public T Find(Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T> query = Context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.FirstOrDefault(match);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T> query = Context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.Where(match).ToList();
        }
        public IEnumerable<T> FindAll(Expression<Func<T, bool>> match, int take, int skip)
        {
            return Context.Set<T>().Where(match).Skip(skip).Take(take);
        }
 

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> match, int? take, int? skip, Expression<Func<T, object>> orderBy = null, string orderByDir = OrderBy.Ascending)
        {
            IQueryable<T> query = Context.Set<T>().Where(match);
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }
            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }
            if (orderBy != null)
            {
                if (orderByDir == OrderBy.Ascending)
                {
                    query = query.OrderBy(orderBy);
                }
                else
                {
                    query = query.OrderByDescending(orderBy);
                }

            }
            return query.ToList();
        }

 


        public T Add(T entity)
        {
            Context.Set<T>().Add(entity);
            return entity;

        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            Context.Set<T>().AddRange(entities);
            return entities;
        }
        public T Update(T entity)
        {
            Context.Set<T>().Update(entity);
            return entity;
        }
        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }
        public void DeleteRange(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);

        }
        public void Attach(T entity)
        {
            Context.Set<T>().Attach(entity);
        }
        public int Count(T entity)
        {
           return Context.Set<T>().Count(); 

        }
        public int Count(Expression<Func<T, bool>> match)
        {
            return Context.Set<T>().Count(match);
        }





    }


}
