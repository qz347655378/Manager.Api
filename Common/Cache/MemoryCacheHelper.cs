using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Cache
{
    /// <summary>
    /// 缓存帮助类
    /// </summary>
    public static class MemoryCacheHelper
    {
        /// <summary>
        /// 缓存
        /// </summary>
        public static IMemoryCache Cache { get; } = new MemoryCache(new MemoryCacheOptions());

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(object key)
        {
            return Cache.Get<T>(key);
        }

        /// <summary>
        ///  尝试获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryGet<T>(object key,out T value)
        {
            return Cache.TryGetValue(key, out value);
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(object key)
        {
            Cache.Remove(key);
        }
        
    }
}
