/*----------------------------------------------------------------
 * 项目名称 ：Modbus
 * 项目描述 ：
 * 文件名称 ：ModbusHelper.cs
 * 说    明 ：
 * 作    者 ：WUZE
 * IDE 环境 ：Visual Studio 2017
 * 创建时间 ：2019/3/25 16:28:36
 * 更新时间 ：2019/3/25 16:28:36
 * 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modbus
{
    public static class ModbusHelper
    {
        public const byte MB_READ_COILS = 0x01;             // 读线圈寄存器
        public const byte MB_READ_DISCRETE = 0x02;          // 读离散输入寄存器
        public const byte MB_READ_HOLD_REG = 0x03;          // 读保持寄存器
        public const byte MB_READ_INPUT_REG = 0x04;         // 读输入寄存器
        public const byte MB_WRITE_SINGLE_COIL = 0x05;      // 写单个线圈
        public const byte MB_WRITE_SINGLE_REG = 0x06;       // 写单寄存器
        public const byte MB_WRITE_MULTIPLE_COILS = 0x0f;   // 写多线圈
        public const byte MB_WRITE_MULTIPLE_REGS = 0x10;    // 写多寄存器

        private const int MB_MAX_LENGTH = 120;              // 最大数据长度

        /// <summary>
        /// 报文数据
        /// </summary>
        /// <param name="code">功能码</param>
        /// <param name="addr">地址</param>
        /// <param name="count">数量</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        private static byte[] SendTrainBytes(byte code, int addr, int count, byte[] data)
        {
            byte[] res = null;
            switch (code)
            {
                case MB_READ_COILS:
                case MB_READ_DISCRETE:
                case MB_READ_HOLD_REG:
                case MB_READ_INPUT_REG:
                    res = new byte[5];
                    res[0] = code;
                    res[1] = (byte)(addr >> 8);
                    res[2] = (byte)(addr & 0xFF);
                    res[3] = (byte)(count >> 8);
                    res[4] = (byte)(count & 0xFF);
                    break;
                case MB_WRITE_SINGLE_COIL:
                case MB_WRITE_SINGLE_REG:
                    if (data == null || data.Length != 2)
                        throw new Exception("写入数据为空或个数不对！");
                    res = new byte[5];
                    res[0] = code;
                    res[1] = (byte)(addr >> 8);
                    res[2] = (byte)(addr & 0xFF);
                    data.CopyTo(res, 3);
                    break;
                case MB_WRITE_MULTIPLE_COILS:
                case MB_WRITE_MULTIPLE_REGS:
                    if (data == null)
                        throw new Exception("写入数据为空或个数不对！");
                    if (count <= 0)
                        throw new Exception("数量不正确！");
                    res = new byte[6 + data.Length];
                    res[0] = code;
                    res[1] = (byte)(addr >> 8);
                    res[2] = (byte)(addr & 0xFF);
                    res[3] = (byte)(count >> 8);
                    res[4] = (byte)(count & 0xFF);
                    res[5] = (byte)(count * 2);
                    data.CopyTo(res, 6);
                    break;
                default: break;
            }
            return res;
        }

        /// <summary>
        /// 发送报文拼接
        /// </summary>
        /// <param name="num">序号</param>
        /// <param name="code">功能码</param>
        /// <param name="addr">偏移地址</param>
        /// <param name="count">数量</param>
        /// <param name="data">写入数据</param>
        /// <returns></returns>
        public static byte[] SendTrainCyclostyle(int num, byte code, int addr, int count, byte[] data = null)
        {
            byte[] res = null;
            // 报头
            byte[] header = new byte[] { (byte)(num >> 8), (byte)(num & 0xFF), 0, 0, 0, 0, 0x01 };
            // 数据
            byte[] body = SendTrainBytes(code, addr, count, data);
            if (body == null) return res;
            // 数据长度
            int length = body.Length + 1;
            header[4] = (byte)(length >> 8);
            header[5] = (byte)(length & 0xFF);
            res = header.Concat(body).ToArray();
            return res;
        }
    }
}
