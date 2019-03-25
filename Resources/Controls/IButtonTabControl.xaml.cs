using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Resources
{
    public partial class IButtonTabControl : TabControl
    {
        static IButtonTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IButtonTabControl), new FrameworkPropertyMetadata(typeof(IButtonTabControl)));
            IButtonCommandProperty = DependencyProperty.Register("IButtonCommand", typeof(ICommand), typeof(IButtonTabControl), null);
            IButtonSourceProperty = DependencyProperty.Register("IButtonSource", typeof(ImageSource), typeof(IButtonTabControl), null);
            IButtonSizeProperty = DependencyProperty.Register("IButtonSize", typeof(double), typeof(IButtonTabControl), null);
            IButtonTextProperty = DependencyProperty.Register("IButtonText", typeof(string), typeof(IButtonTabControl), null);
        }

        #region 按钮点击事件

        public static readonly DependencyProperty IButtonCommandProperty;

        public ICommand IButtonCommand
        {
            get { return (ICommand)GetValue(IButtonCommandProperty); }
            set { SetValue(IButtonCommandProperty, value); }
        }

        #endregion 按钮点击事件

        #region 图标

        public static readonly DependencyProperty IButtonSourceProperty;

        public ImageSource IButtonSource
        {
            get { return (ImageSource)GetValue(IButtonSourceProperty); }
            set { SetValue(IButtonSourceProperty, value); }
        }

        #endregion 图标

        #region 图标大小

        public static readonly DependencyProperty IButtonSizeProperty;

        public double IButtonSize
        {
            get { return (double)GetValue(IButtonSizeProperty); }
            set { SetValue(IButtonSizeProperty, value); }
        }

        #endregion 图标大小

        #region 图标提示文本

        public static readonly DependencyProperty IButtonTextProperty;

        public string IButtonText
        {
            get { return (string)GetValue(IButtonTextProperty); }
            set { SetValue(IButtonTextProperty, value); }
        }

        #endregion 图标提示文本
    }
}