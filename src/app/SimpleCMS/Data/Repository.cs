using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NHibernate;
using SimpleCMS.Models;

namespace SimpleCMS.Data
{
    public class Repository : IRepository
    {
        private readonly ISession session;

        public Repository(ISession session)
        {
            this.session = session;
        }

        public T Find<T>(int id) where T : DataModel
        {
            return session.Get<T>(id);
        }

        public T Find<T>(Expression<Func<T, bool>> criteria) where T : DataModel
        {
            return session
                .QueryOver<T>()
                .Where(criteria)
                .SingleOrDefault();
        }

        public IList<T> FindAll<T>() where T : DataModel
        {
            return session
                .CreateCriteria<T>()
                .List<T>();
        }

        public IList<T> FindAll<T>(Expression<Func<T, bool>> criteria) where T : DataModel
        {
            return session
                .QueryOver<T>()
                .Where(criteria)
                .List();
        }

        public IList<T> FindAllAscending<T>(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> projection) where T : DataModel
        {
            return session
                .QueryOver<T>()
                .Where(criteria)
                .OrderBy(projection).Asc
                .List();
        }

        public IList<T> FindAllDescending<T>(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> projection) where T : DataModel
        {
            return session
                .QueryOver<T>()
                .Where(criteria)
                .OrderBy(projection).Desc
                .List();
        }

        public void Save<T>(T item) where T : DataModel
        {
            using (var transaction = session.BeginTransaction())
            {
                //item.UpdateForSave();
                session.SaveOrUpdate(item);
                transaction.Commit();
            }
        }

        public void Delete<T>(int id) where T : DataModel
        {
            Delete(Find<T>(id));
        }

        public void Delete<T>(T entity) where T : DataModel
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Delete(entity);
                transaction.Commit();
            }
        }
    }
}