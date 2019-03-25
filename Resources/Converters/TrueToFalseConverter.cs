/*----------------------------------------------------------------
* 项目名称 ：Resources.Converters
* 文件名称 ：TrueToFalseConverter
* 文件描述 ：
* 命名空间 ：Resources.Converters
* 开发人员 ：XRQ
* 创建时间 ：2019-01-28 12:36:30
* 更新时间 ：2019-01-28 12:36:30
* 版 本 号 ：v1.0.0.0
----------------------------------------------------------------*/

using System;
using System.Globalization;
using System.Windows.Data;

namespace Resources.Converters
{
    public class TrueToFalseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool ? !(bool)value : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}