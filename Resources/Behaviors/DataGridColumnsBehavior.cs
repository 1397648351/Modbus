/*----------------------------------------------------------------
* 项目名称 ：Resources.Behaviors
* 文件名称 ：DataGridColumnsBehavior
* 文件描述 ：
* 命名空间 ：Resources.Behaviors
* 开发人员 ：XRQ
* 创建时间 ：2019-01-28 14:24:34
* 更新时间 ：2019-01-28 14:24:34
* 版 本 号 ：v1.0.0.0
----------------------------------------------------------------*/

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace Resources.Behaviors
{
    public class DataGridColumnsBehavior
    {
        public static readonly DependencyProperty BindableColumnsProperty =
            DependencyProperty.RegisterAttached(
                "BindableColumns",
                typeof(ObservableCollection<DataGridColumn>),
                typeof(DataGridColumnsBehavior),
                new UIPropertyMetadata(null, BindableColumnsPropertyChanged));

        public static void SetBindableColumns(DependencyObject element, ObservableCollection<DataGridColumn> value)
        {
            element.SetValue(BindableColumnsProperty, value);
        }

        public static ObservableCollection<DataGridColumn> GetBindableColumns(DependencyObject element)
        {
            return (ObservableCollection<DataGridColumn>)element.GetValue(BindableColumnsProperty);
        }

        private static void BindableColumnsPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            DataGrid dataGrid = source as DataGrid;
            ObservableCollection<DataGridColumn> columns = e.NewValue as ObservableCollection<DataGridColumn>;
            if (dataGrid != null)
            {
                dataGrid.Columns.Clear();
                if (columns == null)
                {
                    return;
                }

                foreach (DataGridColumn column in columns)
                {
                    dataGrid.Columns.Add(column);
                }

                columns.CollectionChanged += (sender, e2) =>
                {
                    NotifyCollectionChangedEventArgs ne = e2;
                    if (ne.Action == NotifyCollectionChangedAction.Add)
                    {
                        foreach (DataGridColumn column in ne.NewItems)
                        {
                            dataGrid.Columns.Add(column);
                        }
                    }
                };
            }
        }
    }
}