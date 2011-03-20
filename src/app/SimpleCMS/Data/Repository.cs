using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NHibernate;
using SimpleCMS.Models;

namespace SimpleCMS.Data
{
    public class Repository : IRepository
    {
        public ISession Session { get; private set; }

        public Repository(ISession session)
        {
            Session = session;
        }

        public T Find<T>(int id) where T : DataModel
        {
            return Session.Get<T>(id);
        }

        public T Find<T>(Expression<Func<T, bool>> criteria) where T : DataModel
        {
            return Session
                .QueryOver<T>()
                .Where(criteria)
                .SingleOrDefault();
        }

        public IList<T> FindAll<T>() where T : DataModel
        {
            return Session
                .CreateCriteria<T>()
                .List<T>();
        }

        public IList<T> FindAll<T>(Expression<Func<T, bool>> criteria) where T : DataModel
        {
            return Session
                .QueryOver<T>()
                .Where(criteria)
                .List();
        }

        public IList<T> FindAll<T>(Expression<Func<T, object>> projection, bool ascending = true) where T : DataModel
        {
            var queryOverOrderBuilder = Session
                .QueryOver<T>()
                .OrderBy(projection);

            return ascending ? queryOverOrderBuilder.Asc.List() : queryOverOrderBuilder.Desc.List();
        }

        public IList<T> FindAll<T>(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> projection, bool ascending = true) where T : DataModel
        {
            var queryOverOrderBuilder = Session
                .QueryOver<T>()
                .Where(criteria)
                .OrderBy(projection);

            return ascending ? queryOverOrderBuilder.Asc.List() : queryOverOrderBuilder.Desc.List();
        }

        public T Save<T>(T item) where T : DataModel
        {
            using (var transaction = Session.BeginTransaction())
            {
                item.UpdateForSave();
                Session.SaveOrUpdate(item);
                transaction.Commit();
                return item;
            }
        }

        public void Delete<T>(int id) where T : DataModel
        {
            Delete(Find<T>(id));
        }

        public void Delete<T>(T entity) where T : DataModel
        {
            using (var transaction = Session.BeginTransaction())
            {
                Session.Delete(entity);
                transaction.Commit();
            }
        }
    }
}