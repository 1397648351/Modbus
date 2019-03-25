/*----------------------------------------------------------------
* 项目名称 ：Common
* 文件名称 ：TimerCustom
* 文件描述 ：
* 命名空间 ：Common
* 开发人员 ：XRQ
* 创建时间 ：2018-12-19 16:34:18
* 更新时间 ：2018-12-19 16:34:18
* 版 本 号 ：v1.0.0.0
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;

namespace Common
{
    public class CustomTimer : System.Timers.Timer
    {
        public Queue<int> queue = new Queue<int>();
        public object lockMe = new object();
        public Action<object> Method = null;

        /// <summary>
        /// 为保持连贯性，默认锁住两个
        /// </summary>
        public long lockNum = 0;

        public CustomTimer()
        {
            for (int i = 0; i < short.MaxValue; i++)
            {
                queue.Enqueue(i);
            }
        }

        public CustomTimer(double interval, Action<object> action)
        {
            Method = action;
            for (int i = 0; i < short.MaxValue; i++)
            {
                queue.Enqueue(i);
            }

            this.Interval = interval;
            this.Elapsed += CustomTimer_Elapsed;
        }

        private void CustomTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Method == null)
            {
                this.Stop();
                return;
            }

            CustomTimer singleTimer = sender as CustomTimer;
            singleTimer.Stop();
            if (singleTimer != null)
            {
                if (singleTimer.queue.Count != 0)
                {
                    int item = singleTimer.queue.Dequeue();
                    Method(item);
                    singleTimer.Start();
                }
            }
        }

        /// <summary>
        /// 启动定时器
        /// </summary>
        public void Begin()
        {
            if (Method != null)
            {
                Method(null);
                this.Start();
            }
        }

        /// <summary>
        /// 设置时间间隔（毫秒）
        /// </summary>
        /// <param name="interval">时间间隔</param>
        public void SetInterval(double interval)
        {
            this.Interval = interval;
        }
    }
}