using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Resources
{
    public partial class Waiting : Control
    {
        static Waiting()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Waiting), new FrameworkPropertyMetadata(typeof(Waiting)));
            WaitVisibilityProperty = DependencyProperty.Register("WaitVisibility", typeof(Visibility), typeof(Waiting), new UIPropertyMetadata(Visibility.Visible));
            LoadTextProperty = DependencyProperty.Register("LoadText", typeof(string), typeof(Waiting), new UIPropertyMetadata(string.Empty));
        }

        #region 是否显示载入条

        public static readonly DependencyProperty WaitVisibilityProperty;

        [Description("是否显示载入条"), Category("个性配置"), DefaultValue(Visibility.Visible)]
        public Visibility WaitVisibility
        {
            get { return (Visibility)GetValue(WaitVisibilityProperty); }
            set { SetValue(WaitVisibilityProperty, value); }
        }

        #endregion 是否显示载入条

        #region 文本内容

        public static readonly DependencyProperty LoadTextProperty;

        [Description("文本内容"), Category("个性配置"), DefaultValue("")]
        public string LoadText
        {
            get { return (string)GetValue(LoadTextProperty); }
            set { SetValue(LoadTextProperty, value); }
        }

        #endregion 文本内容
    }
}
