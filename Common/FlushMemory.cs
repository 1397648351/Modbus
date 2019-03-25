/*----------------------------------------------------------------
* 项目名称 ：Common
* 文件名称 ：FlushMemory
* 文件描述 ：
* 命名空间 ：Common
* 开发人员 ：XRQ
* 创建时间 ：2018-11-16 11:07:47
* 更新时间 ：2018-11-16 11:07:47
* 版 本 号 ：v1.0.0.0
----------------------------------------------------------------*/

using System;
using System.Runtime.InteropServices;

namespace Common
{
    public class FlushMemory
    {
        [DllImport("kernel32.dll")]
        public static extern bool SetProcessWorkingSetSize(IntPtr proc, int min, int max);

        /// <summary>
        /// 释放内存
        /// </summary>
        public static void Clear()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
        }
    }
}