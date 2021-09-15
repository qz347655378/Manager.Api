using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL
{
    public abstract class BaseDal<T> where T : class, new()
    {
        private readonly ManagerDbContext _dbContext;

        protected BaseDal(ManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IQueryable<T> GetList(Expression<Func<T, bool>> whereLambda)
        {
            return _dbContext.Set<T>().Where(whereLambda);
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> where)
        {
            return await _dbContext.Set<T>().Where(where).ToListAsync();
        }

        public IQueryable<T> GetPageList<TS>(int page, int limit, out int totalCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TS>> orderBy, bool isAsc)
        {
            var result = _dbContext.Set<T>().Where(whereLambda);
            totalCount = result.Count();
            result = isAsc ? result.OrderBy(orderBy).Skip((page - 1) * limit).Take(limit) : result.OrderByDescending(orderBy).Skip((page - 1) * limit).Take(limit);

            return result;
        }

        public Task<List<T>> GetPageListAsync<TS>(int page, int limit, out int totalCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TS>> orderBy, bool isAsc)
        {
            var result = _dbContext.Set<T>().Where(whereLambda);
            totalCount = result.Count();
            result = isAsc ? result.OrderBy(orderBy).Skip((page - 1) * limit).Take(limit) : result.OrderByDescending(orderBy).Skip((page - 1) * limit).Take(limit);
            return result.ToListAsync();
        }

        public bool Add(T model)
        {
            _dbContext.Entry(model).State = EntityState.Added;

            return _dbContext != null && _dbContext.SaveChanges() > 0;
        }

        public async Task<bool> AddAsync(T model)
        {
            var entity = _dbContext.Set<T>().AddAsync(model).AsTask().Result.Entity;
            return await _dbContext.SaveChangesAsync() > 0;

        }

        /// <summary>
        /// 硬删除
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public bool Delete(Expression<Func<T, bool>> whereLambda)
        {
            var list = _dbContext.Set<T>().Where(whereLambda).ToList();
            _dbContext.Set<T>().RemoveRange(list);
            return _dbContext.SaveChanges() > 0;

        }

        /// <summary>
        /// 异步硬删除,可以实现批量删除
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> whereLambda)
        {
            var list = await _dbContext.Set<T>().Where(whereLambda).ToListAsync();
            _dbContext.Set<T>().RemoveRange(list);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(T model)
        {
            var entity = _dbContext.Set<T>().Update(model).Entity;
            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 异步编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> EditAsync(T model)
        {
            var entity = _dbContext.Set<T>().Update(model).Entity;
            var m = await _dbContext.SaveChangesAsync();
            return m > 0;
        }

        /// <summary>
        /// 异步编辑,只更新部分
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertyExpression"></param>
        /// <returns></returns>
        public async Task<bool> EditAsync<TProperty>(T model, Expression<Func<T, TProperty>> propertyExpression)
        {
            _dbContext.Entry(model).Property(propertyExpression).IsModified = true;
            var count = await _dbContext.SaveChangesAsync();
            //并发了
            if (count != -1) return count > 0;
            //再执行10次
            for (var i = 0; i < 10; i++)
            {
                if (await EditAsync(model, propertyExpression))
                {
                    return true;
                }
            }
            return false;

        }



        /// <summary>
        /// 执行sql语句，返回指定类型
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IQueryable<T> ExecuteCommand(string sql, params object[] parameters)
        {
            return _dbContext.Set<T>().FromSqlRaw(sql, parameters);
        }

        /// <summary>
        /// 异步执行sql语句，返回指定类型
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public async Task<List<T>> ExecuteCommandAsync(string sql, params object[] parameters)
        {
            return await _dbContext.Set<T>().FromSqlRaw(sql, parameters).ToListAsync();
        }


        /// <summary>
        /// 执行SQL语句，返回受影响的行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, params object[] parameters)
        {
            return _dbContext.Database.ExecuteSqlRaw(sql, parameters);
        }


        /// <summary>
        /// 异步执行SQL语句，返回受影响的行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<int> ExecuteNonQueryAsync(string sql, params object[] parameters)
        {
            return await _dbContext.Database.ExecuteSqlRawAsync(sql, parameters);
        }


    }
}
