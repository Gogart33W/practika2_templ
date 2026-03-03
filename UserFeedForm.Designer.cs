namespace Navchpract_2
{
    partial class UserFeedForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pbBack = new System.Windows.Forms.PictureBox();
            this.pbExit = new System.Windows.Forms.PictureBox();
            this.tabControlFeed = new System.Windows.Forms.TabControl();
            this.tabMyTrips = new System.Windows.Forms.TabPage();
            this.flowMyTrips = new System.Windows.Forms.FlowLayoutPanel();
            this.tabGlobalFeed = new System.Windows.Forms.TabPage();
            this.flowGlobalFeed = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExit)).BeginInit();
            this.tabControlFeed.SuspendLayout();
            this.tabMyTrips.SuspendLayout();
            this.tabGlobalFeed.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.pbBack);
            this.pnlHeader.Controls.Add(this.pbExit);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(850, 50);
            this.pnlHeader.TabIndex = 0;
            this.pnlHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseDown);
            this.pnlHeader.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseMove);
            this.pnlHeader.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseUp);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(55, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(117, 31);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Мій Профіль";
            // 
            // pbBack
            // 
            this.pbBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbBack.Image = global::Navchpract_2.Properties.Resources.Home3_37171;
            this.pbBack.Location = new System.Drawing.Point(12, 10);
            this.pbBack.Name = "pbBack";
            this.pbBack.Size = new System.Drawing.Size(30, 30);
            this.pbBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbBack.TabIndex = 2;
            this.pbBack.TabStop = false;
            this.pbBack.Click += new System.EventHandler(this.pbBack_Click);
            // 
            // pbExit
            // 
            this.pbExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbExit.Image = global::Navchpract_2.Properties.Resources.free_icon_close_1828665;
            this.pbExit.Location = new System.Drawing.Point(808, 10);
            this.pbExit.Name = "pbExit";
            this.pbExit.Size = new System.Drawing.Size(30, 30);
            this.pbExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbExit.TabIndex = 3;
            this.pbExit.TabStop = false;
            this.pbExit.Click += new System.EventHandler(this.pbExit_Click);
            // 
            // tabControlFeed
            // 
            this.tabControlFeed.Controls.Add(this.tabMyTrips);
            this.tabControlFeed.Controls.Add(this.tabGlobalFeed);
            this.tabControlFeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlFeed.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.tabControlFeed.Location = new System.Drawing.Point(0, 50);
            this.tabControlFeed.Name = "tabControlFeed";
            this.tabControlFeed.Padding = new System.Drawing.Point(20, 10);
            this.tabControlFeed.SelectedIndex = 0;
            this.tabControlFeed.Size = new System.Drawing.Size(850, 600);
            this.tabControlFeed.TabIndex = 1;
            // 
            // tabMyTrips
            // 
            this.tabMyTrips.BackColor = System.Drawing.Color.Gainsboro;
            this.tabMyTrips.Controls.Add(this.flowMyTrips);
            this.tabMyTrips.Location = new System.Drawing.Point(4, 51);
            this.tabMyTrips.Name = "tabMyTrips";
            this.tabMyTrips.Padding = new System.Windows.Forms.Padding(3);
            this.tabMyTrips.Size = new System.Drawing.Size(842, 545);
            this.tabMyTrips.TabIndex = 0;
            this.tabMyTrips.Text = "✍️ Мої подорожі";
            // 
            // flowMyTrips
            // 
            this.flowMyTrips.AutoScroll = true;
            this.flowMyTrips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowMyTrips.Location = new System.Drawing.Point(3, 3);
            this.flowMyTrips.Name = "flowMyTrips";
            this.flowMyTrips.Padding = new System.Windows.Forms.Padding(15);
            this.flowMyTrips.Size = new System.Drawing.Size(836, 539);
            this.flowMyTrips.TabIndex = 0;
            // 
            // tabGlobalFeed
            // 
            this.tabGlobalFeed.BackColor = System.Drawing.Color.Gainsboro;
            this.tabGlobalFeed.Controls.Add(this.flowGlobalFeed);
            this.tabGlobalFeed.Location = new System.Drawing.Point(4, 51);
            this.tabGlobalFeed.Name = "tabGlobalFeed";
            this.tabGlobalFeed.Padding = new System.Windows.Forms.Padding(3);
            this.tabGlobalFeed.Size = new System.Drawing.Size(842, 545);
            this.tabGlobalFeed.TabIndex = 1;
            this.tabGlobalFeed.Text = "🌍 Стрічка спільноти";
            // 
            // flowGlobalFeed
            // 
            this.flowGlobalFeed.AutoScroll = true;
            this.flowGlobalFeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowGlobalFeed.Location = new System.Drawing.Point(3, 3);
            this.flowGlobalFeed.Name = "flowGlobalFeed";
            this.flowGlobalFeed.Padding = new System.Windows.Forms.Padding(15);
            this.flowGlobalFeed.Size = new System.Drawing.Size(836, 539);
            this.flowGlobalFeed.TabIndex = 0;
            // 
            // UserFeedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 650);
            this.Controls.Add(this.tabControlFeed);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UserFeedForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Стрічка";
            this.Load += new System.EventHandler(this.UserFeedForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExit)).EndInit();
            this.tabControlFeed.ResumeLayout(false);
            this.tabMyTrips.ResumeLayout(false);
            this.tabGlobalFeed.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pbBack;
        private System.Windows.Forms.PictureBox pbExit;
        private System.Windows.Forms.TabControl tabControlFeed;
        private System.Windows.Forms.TabPage tabMyTrips;
        private System.Windows.Forms.FlowLayoutPanel flowMyTrips;
        private System.Windows.Forms.TabPage tabGlobalFeed;
        private System.Windows.Forms.FlowLayoutPanel flowGlobalFeed;
    }
}