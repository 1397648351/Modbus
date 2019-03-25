/************************************************************************ 
 * 项目名称 ：BLL
 * 项目描述 ：
 * 文件名称 ：BaseService.cs
 * 版 本 号 ：v1.0.0.0
 * 说    明 ：
 * 作    者 ：WUZE
 * IDE 环境 ：Visual Studio 2013
 * 创建时间 ：2018/7/27 11:32:34
 * 更新时间 ：2018/7/27 11:32:34
************************************************************************
 * Copyright @ Njbosa 2018. All rights reserved.
************************************************************************/

using IDAL;

namespace BLL
{
    public abstract class BaseService
    {
        internal IBaseRepository CurrentRepository { get; set; }

        /// <summary>
        /// 基类的构造函数
        /// </summary>
        public BaseService()
        {
            SetCurrentRepository();  //构造函数里面去调用了，此设置当前仓储的抽象方法
        }
        protected abstract void SetCurrentRepository();
    }
}
