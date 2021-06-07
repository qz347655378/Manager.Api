using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IBaseBll<T> where T : class, new()
    {
        /// <summary>
        /// 查询所有符合的数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        IQueryable<T> GetList(Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 异步查询
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereLambda);


        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderBy"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        IQueryable<T> GetPageList<TS>(int page, int limit, out int totalCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TS>> orderBy, bool isAsc);

        /// <summary>
        /// 异步获取分页数据
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderBy"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        Task<List<T>> GetPageListAsync<TS>(int page, int limit, out int totalCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TS>> orderBy, bool isAsc);

        /// <summary>
        /// 硬删除
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        bool Delete(Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 异步硬删除
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        T Edit(T model);

        /// <summary>
        /// 异步编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<T> EditAsync(T model);


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        T Add(T model);

        /// <summary>
        /// 异步添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<T> AddAsync(T model);

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IQueryable<T> ExecuteCommand(string sql, params object[] parameters);

        /// <summary>
        /// 异步执行sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<List<T>> ExecuteCommandAsync(string sql, params object[] parameters);

        /// <summary>
        /// 执行SQL语句，返回受影响的行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteNonQuery(string sql, params object[] parameters);


        /// <summary>
        /// 异步执行SQL语句，返回受影响的行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<int> ExecuteNonQueryAsync(string sql, params object[] parameters);
    }
}
