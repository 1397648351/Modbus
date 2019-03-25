/************************************************************************
 * 项目名称 ：RedisUtility
 * 项目描述 ：
 * 文件名称 ：RedisOperatorBase.cs
 * 版 本 号 ：v1.0.0.0
 * 说    明 ：
 * 作    者 ：WUZE
 * IDE 环境 ：Visual Studio 2013
 * 创建时间 ：2018/10/8 9:59:48
 * 更新时间 ：2018/10/8 9:59:48
************************************************************************
 * Copyright @ Njbosa 2018. All rights reserved.
************************************************************************/

using ServiceStack.Redis;
using System;

namespace RedisUtility
{
    public abstract class RedisOperatorBase : IDisposable
    {
        protected IRedisClient Redis { get; private set; }
        private bool _disposed = false;

        protected RedisOperatorBase()
        {
            RedisManager manager = new RedisManager();
            Redis = manager.GetClient();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    Redis.Dispose();
                    Redis = null;
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 保存数据DB文件到硬盘
        /// </summary>
        public void Save()
        {
            Redis.Save();
        }

        /// <summary>
        /// 异步保存数据DB文件到硬盘
        /// </summary>
        public void SaveAsync()
        {
            Redis.SaveAsync();
        }
    }
}