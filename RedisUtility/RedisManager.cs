/************************************************************************
 * 项目名称 ：RedisUtility
 * 项目描述 ：
 * 文件名称 ：RedisHelper.cs
 * 版 本 号 ：v1.0.0.0
 * 说    明 ：
 * 作    者 ：WUZE
 * IDE 环境 ：Visual Studio 2013
 * 创建时间 ：2018/10/8 9:51:44
 * 更新时间 ：2018/10/8 9:51:44
************************************************************************
 * Copyright @ Njbosa 2018. All rights reserved.
************************************************************************/

using Common;
using ServiceStack.Redis;
using System.Collections.Generic;
using System.Linq;

namespace RedisUtility
{
    public class RedisManager
    {
        // redis配置文件信息
        //private  string[] ReadWriteHosts = XmlManager.SelectChildrenAttrArray("Configs/Redis.xml", "configuration/readwritehosts", "value");
        //private  string[] ReadHosts = XmlManager.SelectChildrenAttrArray("Configs/Redis.xml", "configuration/readhosts", "value");
        private string RedisPath = string.Format("{0}:{1}",
            XmlManager.SelectAttribute("Configs/Redis.xml", "configuration/host", "value"),
            XmlManager.SelectAttribute("Configs/Redis.xml", "configuration/port", "value"));

        private string Password = XmlManager.SelectAttribute("Configs/Redis.xml", "configuration/password", "value");
        private PooledRedisClientManager _prcm;

        /// <summary>
        /// 静态构造方法，初始化链接池管理对象
        /// </summary>
        public RedisManager()
        {
            CreateManager();
        }

        /// <summary>
        /// 创建链接池管理对象
        /// </summary>
        private void CreateManager()
        {
            _prcm = CreateManager(new string[] { RedisPath }, new string[] { RedisPath });
        }

        private PooledRedisClientManager CreateManager(string[] readWriteHosts, string[] readOnlyHosts)
        {
            //WriteServerList：可写的Redis链接地址。
            //ReadServerList：可读的Redis链接地址。
            //MaxWritePoolSize：最大写链接数。
            //MaxReadPoolSize：最大读链接数。
            //AutoStart：自动重启。
            //LocalCacheTime：本地缓存到期时间，单位:秒。
            //RecordeLog：是否记录日志,该设置仅用于排查redis运行时出现的问题,如redis工作正常,请关闭该项。
            //RedisConfigInfo类是记录redis连接信息，此信息和配置文件中的RedisConfig相呼应

            // 支持读写分离，均衡负载
            return new PooledRedisClientManager(readWriteHosts, readOnlyHosts, new RedisClientManagerConfig
            {
                MaxWritePoolSize = 5, // “写”链接池链接数
                MaxReadPoolSize = 5, // “读”链接池链接数
                AutoStart = true,
            });
        }

        private IEnumerable<string> SplitString(string strSource, string split)
        {
            return strSource.Split(split.ToArray());
        }

        /// <summary>
        /// 客户端缓存操作对象
        /// </summary>
        public IRedisClient GetClient()
        {
            if (_prcm == null)
            {
                CreateManager();
            }
            IRedisClient client = _prcm.GetClient();
            client.Password = DESHelper.Decrypt(Password);
            return client;
        }
    }
}