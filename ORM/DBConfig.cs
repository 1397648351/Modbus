/************************************************************************ 
 * 项目名称 ：ORM
 * 项目描述 ：
 * 文件名称 ：DBConfig.cs
 * 版 本 号 ：v1.0.0.0
 * 说    明 ：
 * 作    者 ：WUZE
 * IDE 环境 ：Visual Studio 2013
 * 创建时间 ：2018/7/19 17:28:34
 * 更新时间 ：2018/7/19 17:28:34
************************************************************************
 * Copyright @ Njbosa 2018. All rights reserved.
************************************************************************/

using Common;
using System.Collections.Generic;

namespace ORM
{
    public class DBConfig
    {
        //private readonly DBConfig _Instance = new DBConfig
        //{
        //    Type = XmlManager.SelectAttribute("Configs/DB.xml", "configuration/type", "value"),
        //    HostName = XmlManager.SelectAttribute("Configs/DB.xml", "configuration/hostname", "value"),
        //    DataBase = XmlManager.SelectAttribute("Configs/DB.xml", "configuration/database", "value"),
        //    Username = XmlManager.SelectAttribute("Configs/DB.xml", "configuration/username", "value"),
        //    Password = DESHelper.Decrypt(XmlManager.SelectAttribute("Configs/DB.xml", "configuration/password", "value")),
        //    Charset = XmlManager.SelectAttribute("Configs/DB.xml", "configuration/charset", "value"),
        //    Params = XmlManager.SelectChildrenAttr("Configs/DB.xml", "configuration/params"),
        //};

        private DBConfig() { }

        public static DBConfig Instance
        {
            get
            {
                return new DBConfig
                {
                    Type = XmlManager.SelectAttribute("Configs/DB.xml", "configuration/type", "value"),
                    HostName = XmlManager.SelectAttribute("Configs/DB.xml", "configuration/hostname", "value"),
                    DataBase = XmlManager.SelectAttribute("Configs/DB.xml", "configuration/database", "value"),
                    Username = XmlManager.SelectAttribute("Configs/DB.xml", "configuration/username", "value"),
                    Password = DESHelper.Decrypt(XmlManager.SelectAttribute("Configs/DB.xml", "configuration/password", "value")),
                    Charset = XmlManager.SelectAttribute("Configs/DB.xml", "configuration/charset", "value"),
                    Params = XmlManager.SelectChildrenAttrDict("Configs/DB.xml", "configuration/params", "value"),
                };
            }
        }

        public string Type { get; set; }
        public string HostName { get; set; }
        public string DataBase { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Charset { get; set; }
        public Dictionary<string, string> Params { get; set; }
    }
}
