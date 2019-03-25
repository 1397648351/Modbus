/*----------------------------------------------------------------
* 项目名称 ：Resources.Controls
* 文件名称 ：IPTextBox
* 文件描述 ：
* 命名空间 ：Resources.Controls
* 开发人员 ：XRQ
* 创建时间 ：2019-01-28 12:42:54
* 更新时间 ：2019-01-28 12:42:54
* 版 本 号 ：v1.0.0.0
----------------------------------------------------------------*/

using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace Resources.Controls
{
    public class IPTextBox : TextBox
    {
        private IPTextBox leftBox = null;
        private IPTextBox rightBox = null;

        //设置邻居
        public void setNeighbour(IPTextBox left, IPTextBox right)
        {
            leftBox = left;
            rightBox = right;
        }

        // 按下键
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            // 删除键
            if (e.Key == Key.Back)
            {
                if ((CaretIndex == 0) && (leftBox != null) && SelectionLength == 0)
                {
                    leftBox.Focus();
                    leftBox.CaretIndex = leftBox.Text.Length;
                    e.Handled = true;
                }
            }

            // 光标左移
            if (e.Key == Key.Left)
            {
                if ((CaretIndex == 0) && (leftBox != null))
                {
                    leftBox.Focus();
                    leftBox.CaretIndex = leftBox.Text.Length;
                    e.Handled = true;
                }
            }

            // 光标右移
            if (e.Key == Key.Right)
            {
                if ((CaretIndex == Text.Length) && (rightBox != null))
                {
                    rightBox.Focus();
                    rightBox.CaretIndex = 0;
                    e.Handled = true;
                }
            }
        }

        // 文本输入时
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);

            char ch = char.Parse(e.Text);

            if (ch == '.')
            {
                if ((CaretIndex == Text.Length) && (rightBox != null) && Text.Length > 0)
                {
                    rightBox.Focus();
                    rightBox.SelectAll();
                    e.Handled = true;
                    return;
                }
            }
            if (ch >= 19968 && ch <= 40869)
            {
                Text = Text.Replace(e.Text, string.Empty);
                base.SelectionStart = Text.Length;
                e.Handled = true;
                return;
            }
            if (ch < '0' || ch > '9')
            {
                e.Handled = true;
                return;
            }
            if ((Text.Length >= 3) && SelectionLength == 0)
            {
                e.Handled = true;
                return;
            }
        }

        // 文本输入
        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);

            int ip = Int16.Parse(Text);
            if (ip > 255)
                Text = "255";

            //01 -> 去掉首个0
            string pattern = @"^0\d+$";
            if (Regex.IsMatch(this.Text, pattern))
            {
                base.Text = base.Text.Substring(1, base.Text.Length - 1);
            }
            base.SelectionStart = base.Text.Length;

            if (Text.Length == 3)
            {
                if ((CaretIndex == Text.Length) && (rightBox != null))
                {
                    rightBox.Focus();
                    rightBox.SelectAll();
                }
            }
        }
    }
}