/************************************************************************ 
 * 项目名称 ：DAL
 * 项目描述 ：
 * 文件名称 ：BaseRepository.cs
 * 版 本 号 ：v1.0.0.0
 * 说    明 ：
 * 作    者 ：WUZE
 * IDE 环境 ：Visual Studio 2013
 * 创建时间 ：2018/7/27 11:18:09
 * 更新时间 ：2018/7/27 11:18:09
************************************************************************
 * Copyright @ Njbosa 2018. All rights reserved.
************************************************************************/

using IDAL;
using ORM;

namespace DAL
{
    public class BaseRepository : IBaseRepository
    {
        internal ORMHelper DB;
        internal BaseRepository()
        {
            DB = new ORMHelper();
        }
    }
}
