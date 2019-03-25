namespace ModbusMain
{
    partial class MainWindow
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_send = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.menu_tool = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_heart = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_tool.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_send
            // 
            this.txt_send.Location = new System.Drawing.Point(12, 27);
            this.txt_send.Multiline = true;
            this.txt_send.Name = "txt_send";
            this.txt_send.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_send.Size = new System.Drawing.Size(695, 43);
            this.txt_send.TabIndex = 1;
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(713, 25);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(75, 23);
            this.btn_send.TabIndex = 0;
            this.btn_send.Text = "发送";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.Btn_Send_Click);
            // 
            // menu_tool
            // 
            this.menu_tool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem});
            this.menu_tool.Location = new System.Drawing.Point(0, 0);
            this.menu_tool.Name = "menu_tool";
            this.menu_tool.Size = new System.Drawing.Size(800, 25);
            this.menu_tool.TabIndex = 2;
            this.menu_tool.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitem_heart});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // menuitem_heart
            // 
            this.menuitem_heart.Name = "menuitem_heart";
            this.menuitem_heart.Size = new System.Drawing.Size(180, 22);
            this.menuitem_heart.Text = "心跳启用";
            this.menuitem_heart.Click += new System.EventHandler(this.Menuitem_Heart_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.txt_send);
            this.Controls.Add(this.menu_tool);
            this.MainMenuStrip = this.menu_tool;
            this.Name = "MainWindow";
            this.Text = "主界面";
            this.menu_tool.ResumeLayout(false);
            this.menu_tool.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_send;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.MenuStrip menu_tool;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuitem_heart;
    }
}

