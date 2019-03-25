/*----------------------------------------------------------------
* 项目名称 ：Resources.Converters
* 文件名称 ：BoolToVisibilityConverter
* 文件描述 ：
* 命名空间 ：Resources.Converters
* 开发人员 ：XRQ
* 创建时间 ：2019-01-28 12:35:46
* 更新时间 ：2019-01-28 12:35:46
* 版 本 号 ：v1.0.0.0
----------------------------------------------------------------*/

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Resources.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityConverter : IValueConverter
    {
        public BoolToVisibilityConverter()
            : this(true)
        {
        }

        public BoolToVisibilityConverter(bool collapsewhenInvisible)
            : base()
        {
            CollapseWhenInvisible = collapsewhenInvisible;
        }

        public bool CollapseWhenInvisible { get; set; }

        public Visibility FalseVisible
        {
            get
            {
                if (CollapseWhenInvisible)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Hidden;
                }
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Visible;
            return (bool)value ? Visibility.Visible : FalseVisible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return true;
            return ((Visibility)value == Visibility.Visible);
        }
    }
}