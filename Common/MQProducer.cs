/************************************************************************
 * 项目名称 ：Common
 * 项目描述 ：
 * 文件名称 ：MQProducer.cs
 * 版 本 号 ：v1.0.0.0
 * 说    明 ：
 * 作    者 ：WUZE
 * IDE 环境 ：Visual Studio 2013
 * 创建时间 ：2018/8/14 11:12:36
 * 更新时间 ：2018/8/14 11:12:36
************************************************************************
 * Copyright @ Njbosa 2018. All rights reserved.
************************************************************************/

using Apache.NMS;
using Apache.NMS.ActiveMQ;
using System;
using System.Threading;

namespace Common
{
    public static class MQProducer
    {
        public static void SendText(string text, string filter, int timeOut)
        {
            Thread threadToKill = null;
            Action action = () =>
            {
                threadToKill = Thread.CurrentThread;
                SendText(text, filter);
            };
            IAsyncResult result = action.BeginInvoke(null, null);
            if (result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(timeOut)))
            {
                action.EndInvoke(result);
            }
            else
            {
                if (threadToKill != null)
                {
                    threadToKill.Abort();
                    throw new TimeoutException("MQ请求超时！");
                }
            }
        }

        public static void SendText(string text, string filter)
        {
            try
            {
                IConnectionFactory factory = new ConnectionFactory("failover:(tcp://192.168.100.199:61616,tcp://192.168.100.199:61617,tcp://192.168.100.199:61618)?randomize=false");
                using (IConnection connection = factory.CreateConnection())
                {
                    using (ISession session = connection.CreateSession())
                    {
                        IMessageProducer prod = session.CreateProducer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue("njbosaQueue"));
                        ITextMessage message = prod.CreateTextMessage();
                        message.Text = text;
                        message.Properties.SetString("filter", filter);
                        prod.Send(message, MsgDeliveryMode.NonPersistent, MsgPriority.Normal, TimeSpan.MinValue);
                    }
                }
            }
            catch (ThreadAbortException)
            {
                return;
            }
        }

        public static void SendObj(object body, string filter)
        {
            try
            {
                IConnectionFactory factory = new ConnectionFactory("failover:(tcp://192.168.100.199:61616,tcp://192.168.100.199:61617,tcp://192.168.100.199:61618)?randomize=false");
                using (IConnection connection = factory.CreateConnection())
                {
                    using (ISession session = connection.CreateSession())
                    {
                        IMessageProducer prod = session.CreateProducer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue("njbosaQueue"));
                        IObjectMessage message = prod.CreateObjectMessage(body);
                        //message.Body = body;
                        message.Properties.SetString("filter", filter);
                        prod.Send(message, MsgDeliveryMode.NonPersistent, MsgPriority.Normal, TimeSpan.MinValue);
                    }
                }
            }
            catch (ThreadAbortException)
            {
                return;
            }
        }
    }
}