using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NHibernate;
using SimpleCMS.Models;

namespace SimpleCMS.Data
{
    public interface IRepository
    {
        ISession Session { get; }
        T Find<T>(int id) where T : DataModel;
        T Find<T>(Expression<Func<T, bool>> criteria) where T : DataModel;
        IList<T> FindAll<T>() where T : DataModel;
        IList<T> FindAll<T>(Expression<Func<T, bool>> criteria) where T : DataModel;
        IList<T> FindAll<T>(Expression<Func<T, object>> projection, bool ascending = true) where T : DataModel;
        IList<T> FindAll<T>(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> projection, bool ascending = true) where T : DataModel;
        T Save<T>(T item) where T : DataModel;
        void Delete<T>(int id) where T : DataModel;
        void Delete<T>(T entity) where T : DataModel;
    }
}