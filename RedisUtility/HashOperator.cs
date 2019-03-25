/************************************************************************
 * 项目名称 ：RedisUtility
 * 项目描述 ：
 * 文件名称 ：HashOperator.cs
 * 版 本 号 ：v1.0.0.0
 * 说    明 ：
 * 作    者 ：WUZE
 * IDE 环境 ：Visual Studio 2013
 * 创建时间 ：2018/10/8 10:00:38
 * 更新时间 ：2018/10/8 10:00:38
************************************************************************
 * Copyright @ Njbosa 2018. All rights reserved.
************************************************************************/

using ServiceStack.Text;
using System;
using System.Collections.Generic;

namespace RedisUtility
{
    /// <summary>
    /// HashOperator类，是操作哈希表类。继承自RedisOperatorBase类
    /// </summary>
    public class HashOperator : RedisOperatorBase
    {
        private object lockObj = new object();

        public HashOperator()
            : base()
        {
        }

        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        public bool Exist<T>(string hashId, string key)
        {
            return Redis.HashContainsEntry(hashId, key);
        }

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        public bool Set<T>(string hashId, string key, T t)
        {
            string value = JsonSerializer.SerializeToString<T>(t);
            return Redis.SetEntryInHash(hashId, key, value);
        }

        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        public bool Remove(string hashId, string key)
        {
            return Redis.RemoveEntryFromHash(hashId, key);
        }

        /// <summary>
        /// 移除整个hash
        /// </summary>
        public bool Remove(string key)
        {
            return Redis.Remove(key);
        }

        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        public T Get<T>(string hashId, string key)
        {
            lock (lockObj)
            {
                string value = Redis.GetValueFromHash(hashId, key);
                return JsonSerializer.DeserializeFromString<T>(value);
            }
        }

        /// <summary>
        /// 获取整个hash的数据
        /// </summary>
        public List<T> GetAll<T>(string hashId)
        {
            lock (lockObj)
            {
                List<T> result = new List<T>();
                List<string> list = Redis.GetHashValues(hashId);
                if (list != null && list.Count > 0)
                {
                    list.ForEach(x =>
                    {
                        T value = JsonSerializer.DeserializeFromString<T>(x);
                        result.Add(value);
                    });
                }
                return result;
            }
        }

        /// <summary>
        /// 设置缓存过期
        /// </summary>
        public void SetExpire(string key, DateTime datetime)
        {
            Redis.ExpireEntryAt(key, datetime);
        }
    }
}