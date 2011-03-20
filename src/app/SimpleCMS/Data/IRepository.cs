using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SimpleCMS.Models;

namespace SimpleCMS.Data
{
    public interface IRepository
    {
        T Find<T>(int id) where T : DataModel;
        T Find<T>(Expression<Func<T, bool>> criteria) where T : DataModel;
        IList<T> FindAll<T>() where T : DataModel;
        IList<T> FindAll<T>(Expression<Func<T, bool>> criteria) where T : DataModel;
        IList<T> FindAllAscending<T>(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> projection) where T : DataModel;
        IList<T> FindAllDescending<T>(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> projection) where T : DataModel;
        void Save<T>(T item) where T : DataModel;
        void Delete<T>(int id) where T : DataModel;
        void Delete<T>(T entity) where T : DataModel;
    }
}