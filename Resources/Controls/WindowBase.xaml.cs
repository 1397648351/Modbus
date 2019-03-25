using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Resources
{
    public partial class WindowBase : Window
    {
        #region 是否显示最小化按钮

        public static readonly DependencyProperty MinboxEnableProperty = DependencyProperty.Register("MinboxEnable", typeof(Visibility), typeof(WindowBase), new PropertyMetadata(Visibility.Visible));

        [Description("是否显示最小化按钮"), Category("个性配置")]
        public Visibility MinboxEnable
        {
            get { return (Visibility)GetValue(MinboxEnableProperty); }
            set { SetValue(MinboxEnableProperty, value); }
        }

        #endregion 是否显示最小化按钮

        public ICommand MinimizeWindowCommand { get; protected set; }
        public ICommand CloseWindowCommand { get; protected set; }

        public WindowBase()
        {
            this.WindowStyle = WindowStyle.None;
            this.AllowsTransparency = true;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            this.Style = this.FindResource("DialogWindowStyle") as Style;
            this.MinimizeWindowCommand = new RoutedUICommand();
            this.CloseWindowCommand = new RoutedUICommand();
            this.BindCommand(MinimizeWindowCommand, this.MinCommand_Execute);
            this.BindCommand(CloseWindowCommand, this.CloseCommand_Execute);
        }

        private void MinCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            e.Handled = true;
        }

        private void CloseCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}