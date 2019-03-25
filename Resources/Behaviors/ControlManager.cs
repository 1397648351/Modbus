/*----------------------------------------------------------------
* 项目名称 ：Resources.Behaviors
* 文件名称 ：ControlManager
* 文件描述 ：
* 命名空间 ：Resources.Behaviors
* 开发人员 ：XRQ
* 创建时间 ：2019-01-28 11:34:20
* 更新时间 ：2019-01-28 11:34:20
* 版 本 号 ：v1.0.0.0
----------------------------------------------------------------*/

using System;
using System.Windows;
using System.Windows.Input;

namespace Resources
{
    public static class ControlManager
    {
        /// <summary>
        /// 绑定命令和命令事件到宿主UI
        /// </summary>
        public static void BindCommand(this UIElement @ui, ICommand com, Action<object, ExecutedRoutedEventArgs> call)
        {
            var bind = new CommandBinding(com);
            bind.Executed += new ExecutedRoutedEventHandler(call);
            @ui.CommandBindings.Add(bind);
        }

        /// <summary>
        /// 绑定RelayCommand命令到宿主UI
        /// </summary>
        public static void BindCommand(this UIElement @ui, ICommand com)
        {
            var bind = new CommandBinding(com);
            @ui.CommandBindings.Add(bind);
        }
    }
}