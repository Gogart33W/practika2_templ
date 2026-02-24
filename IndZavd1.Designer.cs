namespace Navchpract_2
{
    partial class IndZavd1
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
            this.components = new System.ComponentModel.Container();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.grpInput = new System.Windows.Forms.GroupBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblArea = new System.Windows.Forms.Label();
            this.numArea = new System.Windows.Forms.NumericUpDown();
            this.lblRooms = new System.Windows.Forms.Label();
            this.numRooms = new System.Windows.Forms.NumericUpDown();
            this.lblPrice = new System.Windows.Forms.Label();
            this.numPrice = new System.Windows.Forms.NumericUpDown();
            this.lblOwner = new System.Windows.Forms.Label();
            this.txtOwner = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSaveBin = new System.Windows.Forms.Button();
            this.btnLoadBin = new System.Windows.Forms.Button();
            this.btnExportTxt = new System.Windows.Forms.Button();
            this.btnImportTxt = new System.Windows.Forms.Button();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pbBack = new System.Windows.Forms.PictureBox();
            this.pbExit = new System.Windows.Forms.PictureBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cmbFilterMode = new System.Windows.Forms.ComboBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.ctxGridMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.grpInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRooms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).BeginInit();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExit)).BeginInit();
            this.ctxGridMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvData
            // 
            this.dgvData.BackgroundColor = System.Drawing.Color.White;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(12, 85);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersWidth = 51;
            this.dgvData.Size = new System.Drawing.Size(560, 445);
            this.dgvData.TabIndex = 0;
            this.dgvData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellDoubleClick);
            this.dgvData.SelectionChanged += new System.EventHandler(this.dgvData_SelectionChanged);
            // 
            // grpInput
            // 
            this.grpInput.Controls.Add(this.lblAddress);
            this.grpInput.Controls.Add(this.txtAddress);
            this.grpInput.Controls.Add(this.lblType);
            this.grpInput.Controls.Add(this.cmbType);
            this.grpInput.Controls.Add(this.lblArea);
            this.grpInput.Controls.Add(this.numArea);
            this.grpInput.Controls.Add(this.lblRooms);
            this.grpInput.Controls.Add(this.numRooms);
            this.grpInput.Controls.Add(this.lblPrice);
            this.grpInput.Controls.Add(this.numPrice);
            this.grpInput.Controls.Add(this.lblOwner);
            this.grpInput.Controls.Add(this.txtOwner);
            this.grpInput.Controls.Add(this.btnAdd);
            this.grpInput.Controls.Add(this.btnEdit);
            this.grpInput.Controls.Add(this.btnDelete);
            this.grpInput.Controls.Add(this.btnSaveBin);
            this.grpInput.Controls.Add(this.btnLoadBin);
            this.grpInput.Controls.Add(this.btnExportTxt);
            this.grpInput.Controls.Add(this.btnImportTxt);
            this.grpInput.Location = new System.Drawing.Point(590, 50);
            this.grpInput.Name = "grpInput";
            this.grpInput.Size = new System.Drawing.Size(250, 480);
            this.grpInput.TabIndex = 1;
            this.grpInput.TabStop = false;
            this.grpInput.Text = "Панель управління";
            // 
            // lblAddress
            // 
            this.lblAddress.Location = new System.Drawing.Point(10, 25);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(100, 23);
            this.lblAddress.TabIndex = 0;
            this.lblAddress.Text = "Адреса:";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(10, 45);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(230, 22);
            this.txtAddress.TabIndex = 1;
            this.txtAddress.Leave += new System.EventHandler(this.txtAddress_Leave);
            // 
            // lblType
            // 
            this.lblType.Location = new System.Drawing.Point(10, 70);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(100, 23);
            this.lblType.TabIndex = 2;
            this.lblType.Text = "Тип:";
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Location = new System.Drawing.Point(10, 90);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(230, 24);
            this.cmbType.TabIndex = 3;
            // 
            // lblArea
            // 
            this.lblArea.Location = new System.Drawing.Point(10, 115);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(100, 23);
            this.lblArea.TabIndex = 4;
            this.lblArea.Text = "Площа (м²):";
            // 
            // numArea
            // 
            this.numArea.DecimalPlaces = 2;
            this.numArea.Location = new System.Drawing.Point(10, 135);
            this.numArea.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numArea.Name = "numArea";
            this.numArea.Size = new System.Drawing.Size(230, 22);
            this.numArea.TabIndex = 5;
            // 
            // lblRooms
            // 
            this.lblRooms.Location = new System.Drawing.Point(10, 160);
            this.lblRooms.Name = "lblRooms";
            this.lblRooms.Size = new System.Drawing.Size(100, 23);
            this.lblRooms.TabIndex = 6;
            this.lblRooms.Text = "Кімнат:";
            // 
            // numRooms
            // 
            this.numRooms.Location = new System.Drawing.Point(10, 180);
            this.numRooms.Name = "numRooms";
            this.numRooms.Size = new System.Drawing.Size(230, 22);
            this.numRooms.TabIndex = 7;
            // 
            // lblPrice
            // 
            this.lblPrice.Location = new System.Drawing.Point(10, 205);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(100, 23);
            this.lblPrice.TabIndex = 8;
            this.lblPrice.Text = "Ціна ($):";
            // 
            // numPrice
            // 
            this.numPrice.DecimalPlaces = 2;
            this.numPrice.Location = new System.Drawing.Point(10, 225);
            this.numPrice.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numPrice.Name = "numPrice";
            this.numPrice.Size = new System.Drawing.Size(230, 22);
            this.numPrice.TabIndex = 9;
            // 
            // lblOwner
            // 
            this.lblOwner.Location = new System.Drawing.Point(10, 250);
            this.lblOwner.Name = "lblOwner";
            this.lblOwner.Size = new System.Drawing.Size(100, 23);
            this.lblOwner.TabIndex = 10;
            this.lblOwner.Text = "Власник:";
            // 
            // txtOwner
            // 
            this.txtOwner.Location = new System.Drawing.Point(10, 270);
            this.txtOwner.Name = "txtOwner";
            this.txtOwner.Size = new System.Drawing.Size(230, 22);
            this.txtOwner.TabIndex = 11;
            this.txtOwner.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOwner_KeyPress);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.LightGreen;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(10, 300);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(110, 30);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "ДОДАТИ";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.LightYellow;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Location = new System.Drawing.Point(130, 300);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(110, 30);
            this.btnEdit.TabIndex = 13;
            this.btnEdit.Text = "ЗМІНИТИ";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.LightCoral;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(10, 335);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(230, 30);
            this.btnDelete.TabIndex = 14;
            this.btnDelete.Text = "ВИДАЛИТИ";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSaveBin
            // 
            this.btnSaveBin.Location = new System.Drawing.Point(10, 380);
            this.btnSaveBin.Name = "btnSaveBin";
            this.btnSaveBin.Size = new System.Drawing.Size(110, 30);
            this.btnSaveBin.TabIndex = 15;
            this.btnSaveBin.Text = "Save BIN";
            this.btnSaveBin.Click += new System.EventHandler(this.btnSaveBin_Click);
            // 
            // btnLoadBin
            // 
            this.btnLoadBin.Location = new System.Drawing.Point(130, 380);
            this.btnLoadBin.Name = "btnLoadBin";
            this.btnLoadBin.Size = new System.Drawing.Size(110, 30);
            this.btnLoadBin.TabIndex = 16;
            this.btnLoadBin.Text = "Load BIN";
            this.btnLoadBin.Click += new System.EventHandler(this.btnLoadBin_Click);
            // 
            // btnExportTxt
            // 
            this.btnExportTxt.Location = new System.Drawing.Point(10, 415);
            this.btnExportTxt.Name = "btnExportTxt";
            this.btnExportTxt.Size = new System.Drawing.Size(110, 30);
            this.btnExportTxt.TabIndex = 17;
            this.btnExportTxt.Text = "Export TXT";
            this.btnExportTxt.Click += new System.EventHandler(this.btnExportTxt_Click);
            // 
            // btnImportTxt
            // 
            this.btnImportTxt.Location = new System.Drawing.Point(130, 415);
            this.btnImportTxt.Name = "btnImportTxt";
            this.btnImportTxt.Size = new System.Drawing.Size(110, 30);
            this.btnImportTxt.TabIndex = 18;
            this.btnImportTxt.Text = "Import TXT";
            this.btnImportTxt.Click += new System.EventHandler(this.btnImportTxt_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.pbBack);
            this.pnlHeader.Controls.Add(this.pbExit);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(850, 40);
            this.pnlHeader.TabIndex = 6;
            this.pnlHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseDown);
            this.pnlHeader.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseMove);
            this.pnlHeader.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseUp);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(50, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(212, 28);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Реєстр Нерухомості ";
            //this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // pbBack
            // 
            this.pbBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbBack.Image = global::Navchpract_2.Properties.Resources.Home3_37171;
            this.pbBack.Location = new System.Drawing.Point(10, 5);
            this.pbBack.Name = "pbBack";
            this.pbBack.Size = new System.Drawing.Size(30, 30);
            this.pbBack.TabIndex = 1;
            this.pbBack.TabStop = false;
            this.pbBack.Click += new System.EventHandler(this.pbBack_Click);
            // 
            // pbExit
            // 
            this.pbExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbExit.Image = global::Navchpract_2.Properties.Resources.free_icon_close_1828665;
            this.pbExit.Location = new System.Drawing.Point(810, 5);
            this.pbExit.Name = "pbExit";
            this.pbExit.Size = new System.Drawing.Size(30, 30);
            this.pbExit.TabIndex = 2;
            this.pbExit.TabStop = false;
            this.pbExit.Click += new System.EventHandler(this.pbExit_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(205, 52);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(367, 22);
            this.txtSearch.TabIndex = 5;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // cmbFilterMode
            // 
            this.cmbFilterMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterMode.Location = new System.Drawing.Point(75, 52);
            this.cmbFilterMode.Name = "cmbFilterMode";
            this.cmbFilterMode.Size = new System.Drawing.Size(120, 24);
            this.cmbFilterMode.TabIndex = 4;
            this.cmbFilterMode.SelectedIndexChanged += new System.EventHandler(this.cmbFilterMode_SelectedIndexChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(12, 55);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(52, 16);
            this.lblSearch.TabIndex = 3;
            this.lblSearch.Text = "Пошук:";
            // 
            // ctxGridMenu
            // 
            this.ctxGridMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctxGridMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuEdit,
            this.ctxMenuDelete});
            this.ctxGridMenu.Name = "ctxGridMenu";
            this.ctxGridMenu.Size = new System.Drawing.Size(155, 52);
            // 
            // ctxMenuEdit
            // 
            this.ctxMenuEdit.Name = "ctxMenuEdit";
            this.ctxMenuEdit.Size = new System.Drawing.Size(154, 24);
            this.ctxMenuEdit.Text = "Редагувати";
            this.ctxMenuEdit.Click += new System.EventHandler(this.ctxMenuEdit_Click);
            // 
            // ctxMenuDelete
            // 
            this.ctxMenuDelete.Name = "ctxMenuDelete";
            this.ctxMenuDelete.Size = new System.Drawing.Size(154, 24);
            this.ctxMenuDelete.Text = "Видалити";
            this.ctxMenuDelete.Click += new System.EventHandler(this.ctxMenuDelete_Click);
            // 
            // IndZavd1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 550);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.cmbFilterMode);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.grpInput);
            this.Controls.Add(this.dgvData);
            this.Name = "IndZavd1";
            this.Text = "IndZavd1";
            this.Load += new System.EventHandler(this.IndZavd1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.grpInput.ResumeLayout(false);
            this.grpInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRooms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExit)).EndInit();
            this.ctxGridMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.GroupBox grpInput;
        private System.Windows.Forms.Label lblAddress, lblType, lblArea, lblRooms, lblPrice, lblOwner;
        private System.Windows.Forms.TextBox txtAddress, txtOwner;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.NumericUpDown numArea, numRooms, numPrice;
        private System.Windows.Forms.Button btnAdd, btnEdit, btnDelete, btnSaveBin, btnLoadBin, btnExportTxt, btnImportTxt;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.PictureBox pbBack;
        private System.Windows.Forms.PictureBox pbExit;
        private System.Windows.Forms.Label lblTitle;

        // Нові змінні для фіч
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cmbFilterMode;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.ContextMenuStrip ctxGridMenu;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuEdit;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuDelete;
    }
}