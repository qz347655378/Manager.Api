using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    ///业务逻辑基础类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// where T:
    ///  泛型约束，约束类型T必须具有无参的构造函数
    ///  表示T必须是class类型或它的派生类。
    ///  new() 构造函数约束允许开发人员实例化一个泛型类型的对象。 
    ///  一般情况下，无法创建一个泛型类型参数的实例。然而，new() 约束改变了这种情况，要求类型参数必须提供一个无参数的构造函数。 
    ///  在使用new()约束时，可以通过调用该无参构造函数来创建对象。 
    ///  基本形式： where T : new()
    public class BaseBll<T> where T : class, new()
    {
        private readonly IBaseDal<T> _currentDal;

        public BaseBll(IBaseDal<T> currentDal)
        {
            _currentDal = currentDal;
        }


        public IQueryable<T> GetList(Expression<Func<T, bool>> whereLambda)
        {
            return _currentDal.GetList(whereLambda);
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> where)
        {
            return await _currentDal.GetListAsync(where);
        }

        public IQueryable<T> GetPageList<TS>(int page, int limit, out int totalCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TS>> orderBy, bool isAsc)
        {
            return _currentDal.GetPageList(page, limit, out totalCount, whereLambda, orderBy, isAsc);
        }

        public Task<List<T>> GetPageListAsync<TS>(int page, int limit, out int totalCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TS>> orderBy, bool isAsc)
        {
            return _currentDal.GetPageListAsync(page, limit, out totalCount, whereLambda, orderBy, isAsc);
        }

        public bool Add(T model)
        {
            return _currentDal.Add(model);

        }

        public async Task<bool> AddAsync(T model)
        {
            return await _currentDal.AddAsync(model);

        }


        public bool Add(List<T> list)
        {
            return _currentDal.Add(list);

        }

        public async Task<bool> AddAsync(List<T> list)
        {
            return await _currentDal.AddAsync(list);

        }





        /// <summary>
        /// 硬删除
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public bool Delete(Expression<Func<T, bool>> whereLambda)
        {
            return _currentDal.Delete(whereLambda);

        }






        /// <summary>
        /// 异步硬删除
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> whereLambda)
        {
            return await _currentDal.DeleteAsync(whereLambda);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(T model)
        {
            return _currentDal.Edit(model);
        }

        /// <summary>
        /// 异步编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> EditAsync(T model)
        {
            return await _currentDal.EditAsync(model);
        }


        /// <summary>
        /// 异步编辑,只更新部分
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertyExpression"></param>
        /// <returns></returns>
        public async Task<bool> EditAsync<TProperty>(T model, Expression<Func<T, TProperty>> propertyExpression)
        {
            return await _currentDal.EditAsync(model, propertyExpression);
        }


        /// <summary>
        /// 执行sql语句，返回指定类型
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IQueryable<T> ExecuteCommand(string sql, params object[] parameters)
        {
            return _currentDal.ExecuteCommand(sql, parameters);
        }

        /// <summary>
        /// 异步执行sql语句，返回指定类型
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public async Task<List<T>> ExecuteCommandAsync(string sql, params object[] parameters)
        {
            return await _currentDal.ExecuteCommandAsync(sql, parameters);
        }


        /// <summary>
        /// 执行SQL语句，返回受影响的行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, params object[] parameters)
        {
            return _currentDal.ExecuteNonQuery(sql, parameters);
        }


        /// <summary>
        /// 异步执行SQL语句，返回受影响的行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<int> ExecuteNonQueryAsync(string sql, params object[] parameters)
        {
            return await _currentDal.ExecuteNonQueryAsync(sql, parameters);
        }




    }
}
