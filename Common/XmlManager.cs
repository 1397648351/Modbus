/************************************************
 * 项目名称 ：Common   
 * 项目描述 ：
 * 文件名称 ：XmlManager.cs
 * 版 本 号 ：v1.0.0.0  
 * 说    明 ：
 * 作    者 ：WUZE 
 * 创建时间 ：2018/6/11 22:15:13 
 * 更新时间 ：2018/6/11 22:15:13 
*************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Common
{
    public static class XmlManager
    {
        /// <summary>
        /// 取指定的结点
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="nodePath"></param>
        /// <returns>取指定的结点</returns>
        public static XmlNode Find(string xmlPath, string nodePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            //取指定的单个结点
            XmlNode node = xmlDoc.SelectSingleNode(nodePath);
            return node;
        }

        /// <summary>
        /// 取指定的结点
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="nodePath"></param>
        /// <returns></returns>
        public static XmlNode Find(XmlDocument xmlDoc, string nodePath)
        {
            //取指定的单个结点
            XmlNode node = xmlDoc.SelectSingleNode(nodePath);
            return node;
        }

        /// <summary>
        /// 取指定的结点的集合
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="nodePath"></param>
        /// <returns>取指定的结点的集合</returns>
        public static XmlNodeList SelectByPath(string xmlPath, string nodePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            //取指定的结点的集合
            XmlNodeList nodes = xmlDoc.SelectNodes(nodePath);
            return nodes;
        }

        /// <summary>
        /// 取指定的结点的集合
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="nodePath"></param>
        /// <returns></returns>
        public static XmlNodeList SelectByPath(XmlDocument xmlDoc, string nodePath)
        {
            //取指定的结点的集合
            XmlNodeList nodes = xmlDoc.SelectNodes(nodePath);
            return nodes;
        }

        /// <summary>
        /// 取指定的结点的集合
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="nodePath"></param>
        /// <returns>取指定的结点的集合</returns>
        public static XmlNodeList SelectByTagName(string xmlPath, string nodePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            //取到所有的xml结点
            XmlNodeList nodes = xmlDoc.GetElementsByTagName(nodePath);
            return nodes;
        }

        /// <summary>
        /// 取指定的结点的集合
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="nodePath"></param>
        /// <returns></returns>
        public static XmlNodeList SelectByTagName(XmlDocument xmlDoc, string nodePath)
        {
            //取到所有的xml结点
            XmlNodeList nodes = xmlDoc.GetElementsByTagName(nodePath);
            return nodes;
        }

        /// <summary>
        /// 取指定的结点的子节点集合
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="nodePath"></param>
        /// <returns>取指定的结点的集合</returns>
        public static XmlNodeList SelectChildren(string xmlPath, string nodePath)
        {
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(xmlPath);
            //取到所有的xml结点
            XmlNodeList nodes = null;
            XmlNode node = Find(xmlPath, nodePath);
            if (node != null)
                nodes = node.ChildNodes;
            return nodes;
        }

        /// <summary>
        /// 取指定的结点的子节点集合
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="nodePath"></param>
        /// <returns></returns>
        public static XmlNodeList SelectChildren(XmlDocument xmlDoc, string nodePath)
        {
            //取到所有的xml结点
            XmlNodeList nodes = null;
            XmlNode node = Find(xmlDoc, nodePath);
            if (node != null)
                nodes = node.ChildNodes;
            return nodes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="nodePath"></param>
        /// <param name="attrName"></param>
        /// <returns></returns>
        public static Dictionary<string, string> SelectChildrenAttrDict(string xmlPath, string nodePath, string attrName = "")
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            XmlNodeList nodes = SelectChildren(xmlPath, nodePath);
            if (nodes == null || nodes.Count == 0)
                return res;
            string val = string.Empty;
            foreach (XmlElement item in nodes)
            {
                if (!string.IsNullOrEmpty(attrName))
                    val = ((XmlElement)item).GetAttribute(attrName);
                else
                    val = item.InnerText;
                res.Add(item.Name, val);
            }
            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="nodePath"></param>
        /// <param name="attrName"></param>
        /// <returns></returns>
        public static Dictionary<string, string> SelectChildrenAttrDict(XmlDocument xmlDoc, string nodePath, string attrName = "")
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            XmlNodeList nodes = SelectChildren(xmlDoc, nodePath);
            if (nodes == null || nodes.Count == 0)
                return res;
            string val = string.Empty;
            foreach (XmlElement item in nodes)
            {
                if (!string.IsNullOrEmpty(attrName))
                    val = ((XmlElement)item).GetAttribute(attrName);
                else
                    val = item.InnerText;
                res.Add(item.Name, val);
            }
            return res;
        }

        /// <summary>
        /// 将XML字符串反序列化为List对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <param name="rootName"></param>
        /// <returns></returns>
        public static List<T> DeserializeToList<T>(string xml, string rootName) where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>), new XmlRootAttribute(rootName));
            using (StringReader sr = new StringReader(xml))
            {
                List<T> list = serializer.Deserialize(sr) as List<T>;
                return list;
            }
        }

        /// <summary>
        /// 将XML字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="xml">XML字符</param>
        /// <returns></returns>
        public static T DeserializeToObject<T>(string xml, string rootName) where T : class
        {
            T myObject;
            XmlSerializer serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(rootName));
            using (StringReader sr = new StringReader(xml))
            {
                myObject = (T)serializer.Deserialize(sr);
                return myObject;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="nodePath"></param>
        /// <param name="attrName"></param>
        /// <returns></returns>
        public static string[] SelectChildrenAttrArray(string xmlPath, string nodePath, string attrName = "")
        {
            string[] res = null;
            string val = string.Empty;
            XmlNodeList nodes = SelectChildren(xmlPath, nodePath);
            if (nodes == null || nodes.Count == 0)
                return res;
            res = new string[nodes.Count];
            for (int i = 0; i < nodes.Count; i++)
            {
                if (!string.IsNullOrEmpty(attrName))
                    val = ((XmlElement)nodes[i]).GetAttribute("value");
                else
                    val = nodes[i].InnerText;
                res[i] = val;
            }
            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="nodePath"></param>
        /// <param name="attrName"></param>
        /// <returns></returns>
        public static string[] SelectChildrenAttrArray(XmlDocument xmlDoc, string nodePath, string attrName = "")
        {
            string[] res = null;
            string val = string.Empty;
            XmlNodeList nodes = SelectChildren(xmlDoc, nodePath);
            if (nodes == null || nodes.Count == 0)
                return res;
            res = new string[nodes.Count];
            for (int i = 0; i < nodes.Count; i++)
            {
                if (!string.IsNullOrEmpty(attrName))
                    val = ((XmlElement)nodes[i]).GetAttribute("value");
                else
                    val = nodes[i].InnerText;
                res[i] = val;
            }
            return res;
        }

        /// <summary>
        /// 取指定节点属性值
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="nodePath"></param>
        /// <param name="attrName"></param>
        public static string SelectAttribute(string xmlPath, string nodePath, string attrName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlElement element = (XmlElement)xmlDoc.SelectSingleNode(nodePath);
            string attrValue = element != null ? element.GetAttribute(attrName) : string.Empty;
            return attrValue;
        }

        /// <summary>
        /// 取指定节点属性值
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="nodePath"></param>
        /// <param name="attrName"></param>
        /// <returns></returns>
        public static string SelectAttribute(XmlDocument xmlDoc, string nodePath, string attrName)
        {
            XmlElement element = (XmlElement)xmlDoc.SelectSingleNode(nodePath);
            string attrValue = element != null ? element.GetAttribute(attrName) : string.Empty;
            return attrValue;
        }

        /// <summary>
        /// 取指定节点内容
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="nodePath"></param>
        /// <param name="attrName"></param>
        public static string SelectContent(string xmlPath, string nodePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlElement element = (XmlElement)xmlDoc.SelectSingleNode(nodePath);
            string attrValue = element != null ? element.InnerText : string.Empty;
            return attrValue;
        }

        /// <summary>
        /// 取指定节点内容
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="nodePath"></param>
        /// <returns></returns>
        public static string SelectContent(XmlDocument xmlDoc, string nodePath)
        {
            XmlElement element = (XmlElement)xmlDoc.SelectSingleNode(nodePath);
            string attrValue = element != null ? element.InnerText : string.Empty;
            return attrValue;
        }

        /// <summary>
        /// 设置节点属性
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="nodePath"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void CreateAttribute(string xmlPath, string nodePath, string name, string value)
        {
            //属性
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlElement node = (XmlElement)xmlDoc.SelectSingleNode(nodePath);
            if (node == null) return;
            node.SetAttribute(name, value);
            xmlDoc.Save(xmlPath);
        }
    }
}
