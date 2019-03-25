/*----------------------------------------------------------------
* 项目名称 ：Common
* 文件名称 ：PublicFunctionClass
* 文件描述 ：
* 命名空间 ：Common
* 开发人员 ：XRQ
* 创建时间 ：2018-08-07 11:03:55
* 更新时间 ：2018-08-07 11:03:55
* 版 本 号 ：v1.0.0.0
----------------------------------------------------------------*/

using System;
using System.Text.RegularExpressions;

namespace Common
{
    public static class PublicFunctionClass
    {
        #region Convert Unix时间戳 To DateTime

        /// <summary>
        /// 将DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>double</returns>
        public static double ConvertDateTimeInt(DateTime time)
        {
            double intResult = 0;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            intResult = (Int64)(time - startTime).TotalSeconds;
            return intResult;
        }

        /// <summary>
        /// 将DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>double</returns>
        public static double ConvertDateTimeInt(DateTime time, UnixType type = UnixType.Seconds)
        {
            double intResult = 0;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            switch (type)
            {
                case UnixType.Seconds:
                    intResult = (Int64)(time - startTime).TotalSeconds;
                    break;

                case UnixType.Milliseconds:
                    intResult = (Int64)(time - startTime).TotalMilliseconds;
                    break;

                default:
                    intResult = (Int64)(time - startTime).TotalSeconds;
                    break;
            }
            return intResult;
        }

        /// <summary>
        /// 将Unix时间戳转换为DateTime时间格式格式
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static DateTime ConvertIntDateTime(double d)
        {
            DateTime time = DateTime.MinValue;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            time = startTime.AddSeconds(d);
            return time;
        }

        #endregion Convert Unix时间戳 To DateTime

        #region IP相关

        /// <summary>
        /// 检测IP
        /// </summary>
        /// <param name="strIP"></param>
        /// <returns></returns>
        public static bool PingIP(string strIP)
        {
            if (!IsValidIP(strIP))
            {
                return false;
            }
            try
            {

                System.Net.NetworkInformation.Ping psender = new System.Net.NetworkInformation.Ping();
                System.Net.NetworkInformation.PingReply prep = psender.Send(strIP, 2000, System.Text.Encoding.Default.GetBytes("afdafdadfsdacareqretrqtqeqrq8899tu"));
                if (prep.Status == System.Net.NetworkInformation.IPStatus.Success)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 验证IP
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsValidIP(string ip)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(ip, "[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}"))
            {
                string[] ips = ip.Split('.');
                if (ips.Length == 4 || ips.Length == 6)
                {
                    if (System.Int32.Parse(ips[0]) < 256 && System.Int32.Parse(ips[1]) < 256 & System.Int32.Parse(ips[2]) < 256 & System.Int32.Parse(ips[3]) < 256)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
        }

        #endregion IP相关

        #region Convert Object To Other

        /// <summary>
        /// Object 转 Int
        /// </summary>
        public static int ToInt(this object obj)
        {
            int result = 0;
            if (obj != null && obj != DBNull.Value)
            {
                string str = obj.ToString();
                if (IsInt(str) && !string.IsNullOrEmpty(str))
                {
                    result = Convert.ToInt32(str);
                }
            }
            return result;
        }

        #endregion Convert Object To Other

        #region 判断格式是否规范（正则表达式）

        /// <summary></summary>
        public static bool IsNumeric(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }

        /// <summary>检查string是否为整数型字符串</summary>
        public static bool IsInt(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*$");
        }

        /// <summary></summary>
        public static bool IsUnsign(string value)
        {
            return Regex.IsMatch(value, @"^\d*[.]?\d*$");
        }

        /// <summary></summary>
        public static bool isTel(string strInput)
        {
            return Regex.IsMatch(strInput, @"\d{3}-\d{8}|\d{4}-\d{7}");
        }

        /// <summary>判断输入的字符串是否只包含数字和英文字母</summary>
        public static bool IsNumAndEnCh(string value)
        {
            //string pattern = @"^[A-Za-z0-9]+$";
            //Regex regex = new Regex(pattern);
            //return regex.IsMatch(input);
            return Regex.IsMatch(value, @"^[A-Za-z0-9]+$");
        }

        #endregion 判断格式是否规范（正则表达式）
    }

    /// <summary>Unix时间戳类型</summary>
    public enum UnixType
    {
        /// <summary>秒：10位数</summary>
        Seconds = 0,

        /// <summary>毫秒：13位数</summary>
        Milliseconds = 1,
    }
}