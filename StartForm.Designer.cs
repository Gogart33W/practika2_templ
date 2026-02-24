namespace Navchpract_2
{
    partial class StartForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.першийТижденьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.практична11ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.практична12ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.другийТижденьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.практична21ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indZavd1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.третійТижденьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.практична31ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblUserInfo = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.першийТижденьToolStripMenuItem,
            this.другийТижденьToolStripMenuItem,
            this.третійТижденьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            // 
            // першийТижденьToolStripMenuItem
            // 
            this.першийТижденьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.практична11ToolStripMenuItem,
            this.практична12ToolStripMenuItem});
            this.першийТижденьToolStripMenuItem.Name = "першийТижденьToolStripMenuItem";
            this.першийТижденьToolStripMenuItem.Size = new System.Drawing.Size(144, 24);
            this.першийТижденьToolStripMenuItem.Text = "Перший тиждень";
            // 
            // практична11ToolStripMenuItem
            // 
            this.практична11ToolStripMenuItem.Name = "практична11ToolStripMenuItem";
            this.практична11ToolStripMenuItem.Size = new System.Drawing.Size(193, 26);
            this.практична11ToolStripMenuItem.Text = "Практична 1_1";
            this.практична11ToolStripMenuItem.Click += new System.EventHandler(this.практична11ToolStripMenuItem_Click);
            // 
            // практична12ToolStripMenuItem
            // 
            this.практична12ToolStripMenuItem.Name = "практична12ToolStripMenuItem";
            this.практична12ToolStripMenuItem.Size = new System.Drawing.Size(193, 26);
            this.практична12ToolStripMenuItem.Text = "Практична 1_2";
            this.практична12ToolStripMenuItem.Click += new System.EventHandler(this.практична12ToolStripMenuItem_Click);
            // 
            // другийТижденьToolStripMenuItem
            // 
            this.другийТижденьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.практична21ToolStripMenuItem,
            this.indZavd1ToolStripMenuItem});
            this.другийТижденьToolStripMenuItem.Name = "другийТижденьToolStripMenuItem";
            this.другийТижденьToolStripMenuItem.Size = new System.Drawing.Size(136, 24);
            this.другийТижденьToolStripMenuItem.Text = "Другий тиждень";
            this.другийТижденьToolStripMenuItem.Click += new System.EventHandler(this.другийТижденьToolStripMenuItem_Click);
            // 
            // практична21ToolStripMenuItem
            // 
            this.практична21ToolStripMenuItem.Name = "практична21ToolStripMenuItem";
            this.практична21ToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
            this.практична21ToolStripMenuItem.Text = "Практична 2";
            this.практична21ToolStripMenuItem.Click += new System.EventHandler(this.практична21ToolStripMenuItem_Click);
            // 
            // indZavd1ToolStripMenuItem
            // 
            this.indZavd1ToolStripMenuItem.Name = "indZavd1ToolStripMenuItem";
            this.indZavd1ToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
            this.indZavd1ToolStripMenuItem.Text = "Індивідуальне 1";
            this.indZavd1ToolStripMenuItem.Click += new System.EventHandler(this.indZavd1ToolStripMenuItem_Click);
            // 
            // третійТижденьToolStripMenuItem
            // 
            this.третійТижденьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.практична31ToolStripMenuItem});
            this.третійТижденьToolStripMenuItem.Name = "третійТижденьToolStripMenuItem";
            this.третійТижденьToolStripMenuItem.Size = new System.Drawing.Size(130, 24);
            this.третійТижденьToolStripMenuItem.Text = "Третій тиждень";
            this.третійТижденьToolStripMenuItem.Click += new System.EventHandler(this.третійТижденьToolStripMenuItem_Click);
            // 
            // практична31ToolStripMenuItem
            // 
            this.практична31ToolStripMenuItem.Name = "практична31ToolStripMenuItem";
            this.практична31ToolStripMenuItem.Size = new System.Drawing.Size(183, 26);
            this.практична31ToolStripMenuItem.Text = "Практична 3";
            this.практична31ToolStripMenuItem.Click += new System.EventHandler(this.практична31ToolStripMenuItem_Click);
            // 
            // lblUserInfo
            // 
            this.lblUserInfo.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblUserInfo.AutoSize = true;
            this.lblUserInfo.BackColor = System.Drawing.Color.White;
            this.lblUserInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUserInfo.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblUserInfo.ForeColor = System.Drawing.Color.DimGray;
            this.lblUserInfo.Location = new System.Drawing.Point(260, 390);
            this.lblUserInfo.Name = "lblUserInfo";
            this.lblUserInfo.Padding = new System.Windows.Forms.Padding(2);
            this.lblUserInfo.Size = new System.Drawing.Size(282, 29);
            this.lblUserInfo.TabIndex = 1;
            this.lblUserInfo.Text = "Користувач: адмін:Адміністратор";
            this.lblUserInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(32)))));
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnLogout.FlatAppearance.BorderSize = 2;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(640, 360);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(130, 60);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "Змінити\r\nкористувача";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblUserInfo);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "StartForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Головне меню";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem першийТижденьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem практична11ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem практична12ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem другийТижденьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem практична21ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indZavd1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem третійТижденьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem практична31ToolStripMenuItem;
        private System.Windows.Forms.Label lblUserInfo;
        private System.Windows.Forms.Button btnLogout;
    }
}