﻿/************************************************************************ 
 * 项目名称 ：ORMAttributes
 * 项目描述 ：
 * 文件名称 ：AttributeProcess.cs
 * 版 本 号 ：v1.0.0.0
 * 说    明 ：
 * 作    者 ：MyPC
 * IDE 环境 ：Visual Studio 2013
 * 创建时间 ：2018/6/8 12:34:47
 * 更新时间 ：2018/6/8 12:34:47
************************************************************************
 * Copyright @ Njbosa 2018. All rights reserved.
************************************************************************/

using System;
using System.Reflection;

namespace ORMAttributes
{
    public class AttributeProcess
    {
        /// <summary>  
        /// 获取表名  
        /// </summary>  
        /// <param name="type"></param>  
        /// <returns></returns>  
        public static string GetTableName(Type type)
        {
            string tableName = string.Empty;
            object[] attributes = type.GetCustomAttributes(typeof(TableAttribute), false);
            if (attributes.Length > 0)
            {
                TableAttribute tableAttribute = attributes[0] as TableAttribute;
                tableName = tableAttribute.Value;
            }
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = type.Name;
            }
            return tableName;
        }

        /// <summary>  
        /// 获取字段名  
        /// </summary>  
        /// <param name="property"></param>  
        /// <returns></returns>  
        public static string GetColumnName(PropertyInfo property)
        {
            string columnName = string.Empty;
            object[] attributes = property.GetCustomAttributes(typeof(ColumnAttribute), false);
            if (attributes.Length > 0)
            {
                ColumnAttribute columnAttr = attributes[0] as ColumnAttribute;
                columnName = columnAttr.Value;
            }
            if (string.IsNullOrEmpty(columnName))
            {
                columnName = property.Name;
            }
            return columnName;
        }
    }
}
