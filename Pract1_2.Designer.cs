namespace Navchpract_2
{
    partial class Pract1_2
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_ToStartForm = new System.Windows.Forms.Button();
            this.buttonToModal = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxNumeric = new System.Windows.Forms.TextBox();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox_Exit = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Exit)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(16, 116);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(261, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Поле для введення тексту";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(16, 60);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(251, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Поле для введення числа";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_ToStartForm);
            this.groupBox1.Controls.Add(this.buttonToModal);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(0, 170);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(764, 202);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Перехід між формами";
            // 
            // button_ToStartForm
            // 
            this.button_ToStartForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_ToStartForm.Location = new System.Drawing.Point(477, 71);
            this.button_ToStartForm.Margin = new System.Windows.Forms.Padding(4);
            this.button_ToStartForm.Name = "button_ToStartForm";
            this.button_ToStartForm.Size = new System.Drawing.Size(155, 59);
            this.button_ToStartForm.TabIndex = 2;
            this.button_ToStartForm.Text = "На головну форму";
            this.button_ToStartForm.UseVisualStyleBackColor = true;
            this.button_ToStartForm.Click += new System.EventHandler(this.button_ToStartForm_Click);
            // 
            // buttonToModal
            // 
            this.buttonToModal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonToModal.Location = new System.Drawing.Point(275, 71);
            this.buttonToModal.Margin = new System.Windows.Forms.Padding(4);
            this.buttonToModal.Name = "buttonToModal";
            this.buttonToModal.Size = new System.Drawing.Size(155, 59);
            this.buttonToModal.TabIndex = 1;
            this.buttonToModal.Text = "На модальну форму";
            this.buttonToModal.UseVisualStyleBackColor = true;
            this.buttonToModal.Click += new System.EventHandler(this.buttonToModal_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(71, 71);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 59);
            this.button1.TabIndex = 0;
            this.button1.Text = "На немодальну форму";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxNumeric
            // 
            this.textBoxNumeric.Location = new System.Drawing.Point(319, 60);
            this.textBoxNumeric.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxNumeric.Name = "textBoxNumeric";
            this.textBoxNumeric.Size = new System.Drawing.Size(224, 22);
            this.textBoxNumeric.TabIndex = 3;
            this.textBoxNumeric.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxNumeric_KeyPress);
            // 
            // textBoxText
            // 
            this.textBoxText.Location = new System.Drawing.Point(319, 118);
            this.textBoxText.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.Size = new System.Drawing.Size(224, 22);
            this.textBoxText.TabIndex = 4;
            this.textBoxText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxText_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Controls.Add(this.pictureBox_Exit);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(764, 53);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = global::Navchpract_2.Properties.Resources.free_icon_eraser_3894589;
            this.pictureBox1.Location = new System.Drawing.Point(656, 19);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(47, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox1, "Очистити поля");
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox_Exit
            // 
            this.pictureBox_Exit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_Exit.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox_Exit.Image = global::Navchpract_2.Properties.Resources.free_icon_window_14062773;
            this.pictureBox_Exit.Location = new System.Drawing.Point(703, 19);
            this.pictureBox_Exit.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox_Exit.Name = "pictureBox_Exit";
            this.pictureBox_Exit.Size = new System.Drawing.Size(57, 30);
            this.pictureBox_Exit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Exit.TabIndex = 5;
            this.pictureBox_Exit.TabStop = false;
            this.pictureBox_Exit.Click += new System.EventHandler(this.pictureBox_Exit_Click);
            // 
            // Pract1_2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 372);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.textBoxText);
            this.Controls.Add(this.textBoxNumeric);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Pract1_2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Практична робота 1_2";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Exit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox_Exit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_ToStartForm;
        private System.Windows.Forms.Button buttonToModal;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox textBoxNumeric;
        private System.Windows.Forms.TextBox textBoxText;
    }
}