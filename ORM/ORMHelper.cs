/************************************************************************
 * 项目名称 ：ORM
 * 项目描述 ：
 * 文件名称 ：ORMHelper.cs
 * 版 本 号 ：v1.0.0.0
 * 说    明 ：
 * 作    者 ：WUZE
 * IDE 环境 ：Visual Studio 2013
 * 创建时间 ：2018/6/8 15:58:24
 * 更新时间 ：2018/6/8 15:58:24
************************************************************************
 * Copyright @ Njbosa 2018. All rights reserved.
************************************************************************/

using DBUtility;
using ORMAttributes;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;

namespace ORM
{
    public class ORMHelper
    {
        private DbHelperBase DbHelper;
        private object lockobj = new object();
        //private Dictionary<string, object> options;
        //private Queue<WhereParam> conditions;
        //private string TableName { get; set; }

        public ORMHelper()
        {
            DBConfig dbConfig = DBConfig.Instance;
            string connStr = string.Empty;
            switch (dbConfig.Type.ToLower())
            {
                case "oracle":
                    connStr = string.Format("User ID={0};Password={1};Data Source={2};",
                        dbConfig.Username,
                        dbConfig.Password,
                        dbConfig.HostName);
                    foreach (var item in dbConfig.Params)
                    {
                        string value = item.Value;
                        if (string.IsNullOrEmpty(value))
                            value = "none";
                        connStr += string.Format("{0}={1};", item.Key, value);
                    }
                    DbHelper = new OracleHelper(connStr);
                    break;

                case "mysql":
                    connStr = string.Format("server={0};User Id={1};password={2};Database={3};charset={4};",
                        dbConfig.HostName,
                        dbConfig.Username,
                        dbConfig.Password,
                        dbConfig.DataBase,
                        dbConfig.Charset);

                    foreach (var item in dbConfig.Params)
                    {
                        string value = item.Value;
                        if (string.IsNullOrEmpty(value))
                            value = "none";
                        connStr += string.Format("{0}={1};", item.Key, value);
                    }
                    DbHelper = new MysqlHelper(connStr);
                    break;

                case "sqlserver":
                    connStr = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3};",
                        dbConfig.HostName,
                        dbConfig.DataBase,
                        dbConfig.Username,
                        dbConfig.Password);
                    DbHelper = new SQLHelper(connStr);
                    foreach (var item in dbConfig.Params)
                    {
                        string value = item.Value;
                        if (string.IsNullOrEmpty(value))
                            value = "none";
                        connStr += string.Format("{0}={1};", item.Key, value);
                    }
                    break;

                default:
                    throw new Exception("不支持的数据库类型");
            }
            //options = new Dictionary<string, object>();
            //conditions = new Queue<WhereParam>();
            //TableName = AttributeProcess.GetTableName(typeof(T));
        }

        public ORMHelper(string connStr, string type = "mysql")
        {
            switch (type.ToLower())
            {
                case "oracle":
                    DbHelper = new OracleHelper(connStr);
                    break;

                case "sqlserver":
                    DbHelper = new SQLHelper(connStr);
                    break;

                case "mysql":
                    DbHelper = new MysqlHelper(connStr);
                    break;

                default:
                    throw new Exception("不支持的数据库类型");
            }
            //options = new Dictionary<string, object>();
            //conditions = new Queue<WhereParam>();
            //TableName = AttributeProcess.GetTableName(typeof(T));
        }

        #region ORM未完成

        //public ORMHelper<T> Where(object field, string op = null, string condition = null)//查询逻辑 and or xor
        //{
        //    this.ParseWhereExp("AND", field, op, condition);
        //    return this;
        //}

        //public ORMHelper<T> WhereOr(object field, string op = null, string condition = null)
        //{
        //    this.ParseWhereExp("OR", field, op, condition);
        //    return this;
        //}

        //protected void ParseWhereExp(string logic, object field, string op, string condition)
        //{
        //    if (field is string)
        //    {
        //        WhereParam wp = new WhereParam
        //        {
        //            logic = logic,
        //            filed = field.ToString(),
        //            op = op,
        //            condition = condition
        //        };
        //        conditions.Enqueue(wp);
        //    }
        //    else if (field is Dictionary<string, string>)
        //    {
        //        foreach (var item in (Dictionary<string, string>)field)
        //        {
        //            WhereParam wp = new WhereParam
        //            {
        //                logic = logic,
        //                filed = item.Key,
        //                op = "=",
        //                condition = item.Value
        //            };
        //            conditions.Enqueue(wp);
        //        }
        //    }
        //}

        ///// <summary>
        ///// 创建子查询SQL
        ///// </summary>
        ///// <returns></returns>
        //public string BuildSql()
        //{
        //    string sql = string.Empty,
        //        str_fileds = string.Empty,
        //        str_where = string.Empty;
        //    while (conditions.Count > 0)
        //    {
        //        WhereParam wp = conditions.Dequeue();
        //        if (string.IsNullOrEmpty(str_where))
        //        {
        //            str_where = string.Format("where {0}{1}{2}",wp.filed,wp.logic);
        //        }
        //    }
        //    return sql;
        //}

        #endregion ORM未完成

        /// <summary>
        /// 查找单条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql指令</param>
        /// <returns></returns>
        public T Find<T>(string sql)
        {
            lock (lockobj)
            {
                DbDataReader reader = null;
                try
                {
                    reader = DbHelper.GetDataReader(sql);
                    T result = default(T);
                    if (reader.Read())
                    {
                        Type type = typeof(T);
                        if (type.IsPrimitive || type == typeof(string) || type == typeof(DateTime) || type.IsEnum)
                        {
                            if (type.IsEnum)
                            {
                                result = (T)Enum.ToObject(type, reader.GetValue(0));
                            }
                            else
                            {
                                result = (T)Convert.ChangeType(reader.GetValue(0), type);
                            }
                        }
                        else
                        {
                            result = Activator.CreateInstance<T>();
                            PropertyInfo[] properyies = type.GetProperties();
                            string columName;
                            foreach (PropertyInfo property in properyies)
                            {
                                columName = AttributeProcess.GetColumnName(property);
                                if (!ReaderExists(reader, columName)) continue;
                                var value = reader.GetValue(reader.GetOrdinal(columName));
                                if (property.PropertyType.IsPrimitive && value.Equals(DBNull.Value))
                                    value = Activator.CreateInstance(property.PropertyType);
                                if (property.PropertyType.IsEnum)
                                    property.SetValue(result, Enum.ToObject(property.PropertyType, value), null);
                                else
                                    property.SetValue(result, Convert.ChangeType(value, property.PropertyType), null);
                            }
                        }
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception(sql, ex);
                }
                finally
                {
                    if (reader != null)
                        reader.Close();
                }
            }
        }

        /// <summary>
        /// 查找记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql指令</param>
        /// <returns></returns>
        public List<T> Select<T>(string sql)
        {
            lock (lockobj)
            {
                //if (string.IsNullOrEmpty(sql)) sql = this.BuildSql();
                return Query<T>(sql);
            }
        }

        /// <summary>
        /// 执行查询 返回数据集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql指令</param>
        /// <returns></returns>
        public List<T> Query<T>(string sql)
        {
            lock (lockobj)
            {
                //if (string.IsNullOrEmpty(sql)) sql = this.BuildSql();
                DbDataReader reader = DbHelper.GetDataReader(sql);
                string columName = string.Empty;
                try
                {
                    List<T> list = new List<T>();
                    Type type = typeof(T);
                    if (type.IsPrimitive || type == typeof(string) || type == typeof(DateTime) || type.IsEnum)
                    {
                        while (reader.Read())
                        {
                            if (type.IsEnum)
                            {
                                list.Add((T)Enum.ToObject(type, reader.GetValue(0)));
                            }
                            else
                            {
                                list.Add((T)Convert.ChangeType(reader.GetValue(0), type));
                            }
                        }
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            T result = Activator.CreateInstance<T>();
                            PropertyInfo[] properyies = type.GetProperties();
                            foreach (PropertyInfo property in properyies)
                            {
                                columName = AttributeProcess.GetColumnName(property);
                                if (!ReaderExists(reader, columName)) continue;
                                var value = reader.GetValue(reader.GetOrdinal(columName));
                                if (property.PropertyType.IsPrimitive && value.Equals(DBNull.Value))
                                    value = Activator.CreateInstance(property.PropertyType);
                                if (property.PropertyType.IsEnum)
                                    property.SetValue(result, Enum.ToObject(property.PropertyType, value), null);
                                else
                                    property.SetValue(result, Convert.ChangeType(value, property.PropertyType), null);
                            }
                            list.Add(result);
                        }
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    throw new Exception(sql, ex);
                }
                finally
                {
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// 执行语句
        /// </summary>
        /// <param name="sql">sql指令</param>
        public int Execute(string sql)
        {
            lock (lockobj)
            {
                try
                {
                    return DbHelper.ExecNonQuery(sql);
                }
                catch (Exception ex)
                {
                    throw new Exception(sql, ex);
                }
            }
        }

        /// <summary>
        /// 批处理执行SQL语句
        /// </summary>
        /// <param name="sqlList">SQL批处理指令</param>
        /// <returns>bool</returns>
        public bool BatchQuery(List<string> sqlList)
        {
            try
            {
                return DbHelper.BatchQuery(sqlList);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 启动事务
        /// </summary>
        public void StartTrans()
        {
            DbHelper.TransStart();
        }

        /// <summary>
        /// 用于非自动提交状态下面的查询提交
        /// </summary>
        public void Commit()
        {
            DbHelper.TransCommit();
        }

        /// <summary>
        /// 事务回滚
        /// </summary>
        public void Rollback()
        {
            DbHelper.TransRollback();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        private bool ReaderExists(DbDataReader dr, string columnName)
        {
            return dr.GetSchemaTable().Rows.OfType<System.Data.DataRow>().Any(row => row["ColumnName"].ToString().ToLower() == columnName.ToLower());
        }
    }
}