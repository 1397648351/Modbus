/*----------------------------------------------------------------
* 项目名称 ：Resources.Converters
* 文件名称 ：IntToBoolConverter
* 文件描述 ：
* 命名空间 ：Resources.Converters
* 开发人员 ：XRQ
* 创建时间 ：2019-01-28 12:36:12
* 更新时间 ：2019-01-28 12:36:12
* 版 本 号 ：v1.0.0.0
----------------------------------------------------------------*/

using System;
using System.Globalization;
using System.Windows.Data;

namespace Resources.Converters
{
    public class IntToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
                return ((int)value == 1);
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}