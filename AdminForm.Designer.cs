namespace Navchpract_2
{
    partial class AdminForm
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnCloseForm = new System.Windows.Forms.Button();
            this.btnGoToStart = new System.Windows.Forms.Button();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageAdd = new System.Windows.Forms.TabPage();
            this.gbBinList = new System.Windows.Forms.GroupBox();
            this.btnClearBin = new System.Windows.Forms.Button();
            this.btnLoadBin = new System.Windows.Forms.Button();
            this.listBoxBin = new System.Windows.Forms.ListBox();
            this.gbTxtList = new System.Windows.Forms.GroupBox();
            this.listBoxTxt = new System.Windows.Forms.ListBox();
            this.gbAddUser = new System.Windows.Forms.GroupBox();
            this.btnSaveBin = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.chkIsAdmin = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblLogin = new System.Windows.Forms.Label();
            this.tabPageEdit = new System.Windows.Forms.TabPage();
            this.gbEditList = new System.Windows.Forms.GroupBox();
            this.btnLoadEditList = new System.Windows.Forms.Button();
            this.listBoxEditUsers = new System.Windows.Forms.ListBox();
            this.gbEditUser = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsBtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsBtnClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnApply = new System.Windows.Forms.ToolStripButton();
            this.tsBtnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnSavePassword = new System.Windows.Forms.Button();
            this.btnSaveLogin = new System.Windows.Forms.Button();
            this.btnDeleteUser = new System.Windows.Forms.Button();
            this.cmbEditSelectUser = new System.Windows.Forms.ComboBox();
            this.chkEditIsAdmin = new System.Windows.Forms.CheckBox();
            this.txtEditPassword = new System.Windows.Forms.TextBox();
            this.txtEditLogin = new System.Windows.Forms.TextBox();
            this.lblEditPassword = new System.Windows.Forms.Label();
            this.lblEditLogin = new System.Windows.Forms.Label();
            this.lblSelectUser = new System.Windows.Forms.Label();
            this.gbAllUsers = new System.Windows.Forms.GroupBox();
            this.btnClearAllUsers = new System.Windows.Forms.Button();
            this.btnShowAllUsers = new System.Windows.Forms.Button();
            this.listBoxAllUsersGray = new System.Windows.Forms.ListBox();
            this.panelTop.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageAdd.SuspendLayout();
            this.gbBinList.SuspendLayout();
            this.gbTxtList.SuspendLayout();
            this.gbAddUser.SuspendLayout();
            this.tabPageEdit.SuspendLayout();
            this.gbEditList.SuspendLayout();
            this.gbEditUser.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.gbAllUsers.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.LimeGreen;
            this.panelTop.Controls.Add(this.btnCloseForm);
            this.panelTop.Controls.Add(this.btnGoToStart);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1008, 40);
            this.panelTop.TabIndex = 0;
            // 
            // btnCloseForm
            // 
            this.btnCloseForm.BackColor = System.Drawing.Color.IndianRed;
            this.btnCloseForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseForm.ForeColor = System.Drawing.Color.White;
            this.btnCloseForm.Location = new System.Drawing.Point(965, 5);
            this.btnCloseForm.Name = "btnCloseForm";
            this.btnCloseForm.Size = new System.Drawing.Size(30, 30);
            this.btnCloseForm.TabIndex = 1;
            this.btnCloseForm.Text = "X";
            this.btnCloseForm.UseVisualStyleBackColor = false;
            this.btnCloseForm.Click += new System.EventHandler(this.btnCloseForm_Click);
            // 
            // btnGoToStart
            // 
            this.btnGoToStart.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnGoToStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoToStart.Location = new System.Drawing.Point(820, 5);
            this.btnGoToStart.Name = "btnGoToStart";
            this.btnGoToStart.Size = new System.Drawing.Size(140, 30);
            this.btnGoToStart.TabIndex = 0;
            this.btnGoToStart.Text = "На стартову форму";
            this.btnGoToStart.UseVisualStyleBackColor = false;
            this.btnGoToStart.Click += new System.EventHandler(this.btnGoToStart_Click);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageAdd);
            this.tabControlMain.Controls.Add(this.tabPageEdit);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 40);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1008, 491);
            this.tabControlMain.TabIndex = 1;
            // 
            // tabPageAdd
            // 
            this.tabPageAdd.BackColor = System.Drawing.Color.LimeGreen;
            this.tabPageAdd.Controls.Add(this.gbBinList);
            this.tabPageAdd.Controls.Add(this.gbTxtList);
            this.tabPageAdd.Controls.Add(this.gbAddUser);
            this.tabPageAdd.Location = new System.Drawing.Point(4, 25);
            this.tabPageAdd.Name = "tabPageAdd";
            this.tabPageAdd.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAdd.Size = new System.Drawing.Size(1000, 462);
            this.tabPageAdd.TabIndex = 0;
            this.tabPageAdd.Text = "Додати";
            // 
            // gbBinList
            // 
            this.gbBinList.Controls.Add(this.btnClearBin);
            this.gbBinList.Controls.Add(this.btnLoadBin);
            this.gbBinList.Controls.Add(this.listBoxBin);
            this.gbBinList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.gbBinList.Location = new System.Drawing.Point(660, 20);
            this.gbBinList.Name = "gbBinList";
            this.gbBinList.Size = new System.Drawing.Size(320, 420);
            this.gbBinList.TabIndex = 2;
            this.gbBinList.TabStop = false;
            this.gbBinList.Text = "Список користувачів (input.bin)";
            // 
            // btnClearBin
            // 
            this.btnClearBin.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.btnClearBin.Location = new System.Drawing.Point(200, 90);
            this.btnClearBin.Name = "btnClearBin";
            this.btnClearBin.Size = new System.Drawing.Size(110, 40);
            this.btnClearBin.TabIndex = 2;
            this.btnClearBin.Text = "Очистити";
            this.btnClearBin.UseVisualStyleBackColor = true;
            this.btnClearBin.Click += new System.EventHandler(this.btnClearBin_Click);
            // 
            // btnLoadBin
            // 
            this.btnLoadBin.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.btnLoadBin.Location = new System.Drawing.Point(200, 40);
            this.btnLoadBin.Name = "btnLoadBin";
            this.btnLoadBin.Size = new System.Drawing.Size(110, 40);
            this.btnLoadBin.TabIndex = 1;
            this.btnLoadBin.Text = "Завантажити";
            this.btnLoadBin.UseVisualStyleBackColor = true;
            this.btnLoadBin.Click += new System.EventHandler(this.btnLoadBin_Click);
            // 
            // listBoxBin
            // 
            this.listBoxBin.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.listBoxBin.FormattingEnabled = true;
            this.listBoxBin.ItemHeight = 16;
            this.listBoxBin.Location = new System.Drawing.Point(15, 35);
            this.listBoxBin.Name = "listBoxBin";
            this.listBoxBin.Size = new System.Drawing.Size(175, 372);
            this.listBoxBin.TabIndex = 0;
            // 
            // gbTxtList
            // 
            this.gbTxtList.Controls.Add(this.listBoxTxt);
            this.gbTxtList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.gbTxtList.Location = new System.Drawing.Point(340, 20);
            this.gbTxtList.Name = "gbTxtList";
            this.gbTxtList.Size = new System.Drawing.Size(300, 420);
            this.gbTxtList.TabIndex = 1;
            this.gbTxtList.TabStop = false;
            this.gbTxtList.Text = "Список користувачів (input.txt)";
            // 
            // listBoxTxt
            // 
            this.listBoxTxt.BackColor = System.Drawing.Color.PaleGreen;
            this.listBoxTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.listBoxTxt.FormattingEnabled = true;
            this.listBoxTxt.ItemHeight = 16;
            this.listBoxTxt.Location = new System.Drawing.Point(15, 35);
            this.listBoxTxt.Name = "listBoxTxt";
            this.listBoxTxt.Size = new System.Drawing.Size(270, 372);
            this.listBoxTxt.TabIndex = 0;
            // 
            // gbAddUser
            // 
            this.gbAddUser.Controls.Add(this.btnSaveBin);
            this.gbAddUser.Controls.Add(this.btnRegister);
            this.gbAddUser.Controls.Add(this.chkIsAdmin);
            this.gbAddUser.Controls.Add(this.txtPassword);
            this.gbAddUser.Controls.Add(this.txtLogin);
            this.gbAddUser.Controls.Add(this.lblPassword);
            this.gbAddUser.Controls.Add(this.lblLogin);
            this.gbAddUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.gbAddUser.Location = new System.Drawing.Point(20, 20);
            this.gbAddUser.Name = "gbAddUser";
            this.gbAddUser.Size = new System.Drawing.Size(300, 420);
            this.gbAddUser.TabIndex = 0;
            this.gbAddUser.TabStop = false;
            this.gbAddUser.Text = "Дані користувача";
            // 
            // btnSaveBin
            // 
            this.btnSaveBin.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSaveBin.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.btnSaveBin.Location = new System.Drawing.Point(150, 350);
            this.btnSaveBin.Name = "btnSaveBin";
            this.btnSaveBin.Size = new System.Drawing.Size(120, 50);
            this.btnSaveBin.TabIndex = 6;
            this.btnSaveBin.Text = "Запис в бінарний файл";
            this.btnSaveBin.UseVisualStyleBackColor = false;
            this.btnSaveBin.Click += new System.EventHandler(this.btnSaveBin_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnRegister.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.btnRegister.Location = new System.Drawing.Point(20, 350);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(120, 50);
            this.btnRegister.TabIndex = 5;
            this.btnRegister.Text = "Зареєструвати";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // chkIsAdmin
            // 
            this.chkIsAdmin.AutoSize = true;
            this.chkIsAdmin.Location = new System.Drawing.Point(20, 150);
            this.chkIsAdmin.Name = "chkIsAdmin";
            this.chkIsAdmin.Size = new System.Drawing.Size(209, 22);
            this.chkIsAdmin.TabIndex = 4;
            this.chkIsAdmin.Text = "Права адміністратора";
            this.chkIsAdmin.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtPassword.Location = new System.Drawing.Point(120, 95);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(150, 24);
            this.txtPassword.TabIndex = 3;
            // 
            // txtLogin
            // 
            this.txtLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtLogin.Location = new System.Drawing.Point(120, 45);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(150, 24);
            this.txtLogin.TabIndex = 2;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(20, 100);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(67, 18);
            this.lblPassword.TabIndex = 1;
            this.lblPassword.Text = "Пароль";
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Location = new System.Drawing.Point(20, 50);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(49, 18);
            this.lblLogin.TabIndex = 0;
            this.lblLogin.Text = "Логін";
            // 
            // tabPageEdit
            // 
            this.tabPageEdit.BackColor = System.Drawing.Color.Aquamarine;
            this.tabPageEdit.Controls.Add(this.gbEditList);
            this.tabPageEdit.Controls.Add(this.gbEditUser);
            this.tabPageEdit.Controls.Add(this.gbAllUsers);
            this.tabPageEdit.Location = new System.Drawing.Point(4, 25);
            this.tabPageEdit.Name = "tabPageEdit";
            this.tabPageEdit.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEdit.Size = new System.Drawing.Size(1000, 462);
            this.tabPageEdit.TabIndex = 1;
            this.tabPageEdit.Text = "Редагувати";
            // 
            // gbEditList
            // 
            this.gbEditList.Controls.Add(this.btnLoadEditList);
            this.gbEditList.Controls.Add(this.listBoxEditUsers);
            this.gbEditList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.gbEditList.Location = new System.Drawing.Point(730, 20);
            this.gbEditList.Name = "gbEditList";
            this.gbEditList.Size = new System.Drawing.Size(250, 420);
            this.gbEditList.TabIndex = 2;
            this.gbEditList.TabStop = false;
            this.gbEditList.Text = "Список користувачів";
            // 
            // btnLoadEditList
            // 
            this.btnLoadEditList.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLoadEditList.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.btnLoadEditList.Location = new System.Drawing.Point(15, 370);
            this.btnLoadEditList.Name = "btnLoadEditList";
            this.btnLoadEditList.Size = new System.Drawing.Size(220, 40);
            this.btnLoadEditList.TabIndex = 1;
            this.btnLoadEditList.Text = "Завантажити";
            this.btnLoadEditList.UseVisualStyleBackColor = false;
            this.btnLoadEditList.Click += new System.EventHandler(this.btnLoadEditList_Click);
            // 
            // listBoxEditUsers
            // 
            this.listBoxEditUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.listBoxEditUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.listBoxEditUsers.FormattingEnabled = true;
            this.listBoxEditUsers.ItemHeight = 16;
            this.listBoxEditUsers.Location = new System.Drawing.Point(15, 35);
            this.listBoxEditUsers.Name = "listBoxEditUsers";
            this.listBoxEditUsers.Size = new System.Drawing.Size(220, 324);
            this.listBoxEditUsers.TabIndex = 0;
            this.listBoxEditUsers.SelectedIndexChanged += new System.EventHandler(this.listBoxEditUsers_SelectedIndexChanged);
            // 
            // gbEditUser
            // 
            this.gbEditUser.Controls.Add(this.toolStrip1);
            this.gbEditUser.Controls.Add(this.btnSavePassword);
            this.gbEditUser.Controls.Add(this.btnSaveLogin);
            this.gbEditUser.Controls.Add(this.btnDeleteUser);
            this.gbEditUser.Controls.Add(this.cmbEditSelectUser);
            this.gbEditUser.Controls.Add(this.chkEditIsAdmin);
            this.gbEditUser.Controls.Add(this.txtEditPassword);
            this.gbEditUser.Controls.Add(this.txtEditLogin);
            this.gbEditUser.Controls.Add(this.lblEditPassword);
            this.gbEditUser.Controls.Add(this.lblEditLogin);
            this.gbEditUser.Controls.Add(this.lblSelectUser);
            this.gbEditUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.gbEditUser.Location = new System.Drawing.Point(360, 20);
            this.gbEditUser.Name = "gbEditUser";
            this.gbEditUser.Size = new System.Drawing.Size(350, 420);
            this.gbEditUser.TabIndex = 1;
            this.gbEditUser.TabStop = false;
            this.gbEditUser.Text = "Редагування користувача";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnRefresh,
            this.tsBtnClear,
            this.toolStripSeparator1,
            this.tsBtnApply,
            this.tsBtnSave,
            this.toolStripSeparator2,
            this.tsBtnDelete});
            this.toolStrip1.Location = new System.Drawing.Point(3, 20);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(344, 27);
            this.toolStrip1.TabIndex = 11;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsBtnRefresh
            // 
            this.tsBtnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnRefresh.Name = "tsBtnRefresh";
            this.tsBtnRefresh.Size = new System.Drawing.Size(29, 24);
            this.tsBtnRefresh.Text = "🔄";
            this.tsBtnRefresh.ToolTipText = "Оновити з файлу";
            this.tsBtnRefresh.Click += new System.EventHandler(this.tsBtnRefresh_Click);
            // 
            // tsBtnClear
            // 
            this.tsBtnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnClear.Name = "tsBtnClear";
            this.tsBtnClear.Size = new System.Drawing.Size(29, 24);
            this.tsBtnClear.Text = "📝";
            this.tsBtnClear.ToolTipText = "Очистити поля";
            //this.tsBtnClear.Click += new System.EventHandler(this.ClearEditFields);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // tsBtnApply
            // 
            this.tsBtnApply.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnApply.Name = "tsBtnApply";
            this.tsBtnApply.Size = new System.Drawing.Size(29, 24);
            this.tsBtnApply.Text = "✔️";
            this.tsBtnApply.ToolTipText = "Застосувати зміни";
            this.tsBtnApply.Click += new System.EventHandler(this.tsBtnApply_Click);
            // 
            // tsBtnSave
            // 
            this.tsBtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnSave.Name = "tsBtnSave";
            this.tsBtnSave.Size = new System.Drawing.Size(29, 24);
            this.tsBtnSave.Text = "💾";
            this.tsBtnSave.ToolTipText = "Зберегти у файл";
            this.tsBtnSave.Click += new System.EventHandler(this.SaveSelectedUserChanges);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // tsBtnDelete
            // 
            this.tsBtnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnDelete.Name = "tsBtnDelete";
            this.tsBtnDelete.Size = new System.Drawing.Size(29, 24);
            this.tsBtnDelete.Text = "🗑️";
            this.tsBtnDelete.ToolTipText = "Видалити";
            this.tsBtnDelete.Click += new System.EventHandler(this.DeleteSelectedUser);
            // 
            // btnSavePassword
            // 
            this.btnSavePassword.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSavePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavePassword.Location = new System.Drawing.Point(280, 160);
            this.btnSavePassword.Name = "btnSavePassword";
            this.btnSavePassword.Size = new System.Drawing.Size(30, 30);
            this.btnSavePassword.TabIndex = 10;
            this.btnSavePassword.Text = "💾";
            this.btnSavePassword.UseVisualStyleBackColor = false;
            this.btnSavePassword.Click += new System.EventHandler(this.SaveSelectedUserChanges);
            // 
            // btnSaveLogin
            // 
            this.btnSaveLogin.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSaveLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveLogin.Location = new System.Drawing.Point(280, 110);
            this.btnSaveLogin.Name = "btnSaveLogin";
            this.btnSaveLogin.Size = new System.Drawing.Size(30, 30);
            this.btnSaveLogin.TabIndex = 9;
            this.btnSaveLogin.Text = "💾";
            this.btnSaveLogin.UseVisualStyleBackColor = false;
            this.btnSaveLogin.Click += new System.EventHandler(this.SaveSelectedUserChanges);
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.BackColor = System.Drawing.Color.Tomato;
            this.btnDeleteUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteUser.ForeColor = System.Drawing.Color.White;
            this.btnDeleteUser.Location = new System.Drawing.Point(280, 60);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(30, 30);
            this.btnDeleteUser.TabIndex = 7;
            this.btnDeleteUser.Text = "X";
            this.btnDeleteUser.UseVisualStyleBackColor = false;
            this.btnDeleteUser.Click += new System.EventHandler(this.DeleteSelectedUser);
            // 
            // cmbEditSelectUser
            // 
            this.cmbEditSelectUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEditSelectUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cmbEditSelectUser.FormattingEnabled = true;
            this.cmbEditSelectUser.Location = new System.Drawing.Point(130, 62);
            this.cmbEditSelectUser.Name = "cmbEditSelectUser";
            this.cmbEditSelectUser.Size = new System.Drawing.Size(140, 26);
            this.cmbEditSelectUser.TabIndex = 6;
            this.cmbEditSelectUser.SelectedIndexChanged += new System.EventHandler(this.cmbEditSelectUser_SelectedIndexChanged);
            // 
            // chkEditIsAdmin
            // 
            this.chkEditIsAdmin.AutoSize = true;
            this.chkEditIsAdmin.Location = new System.Drawing.Point(20, 220);
            this.chkEditIsAdmin.Name = "chkEditIsAdmin";
            this.chkEditIsAdmin.Size = new System.Drawing.Size(209, 22);
            this.chkEditIsAdmin.TabIndex = 5;
            this.chkEditIsAdmin.Text = "Права адміністратора";
            this.chkEditIsAdmin.UseVisualStyleBackColor = true;
            // 
            // txtEditPassword
            // 
            this.txtEditPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtEditPassword.Location = new System.Drawing.Point(130, 163);
            this.txtEditPassword.Name = "txtEditPassword";
            this.txtEditPassword.Size = new System.Drawing.Size(140, 24);
            this.txtEditPassword.TabIndex = 4;
            // 
            // txtEditLogin
            // 
            this.txtEditLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtEditLogin.Location = new System.Drawing.Point(130, 113);
            this.txtEditLogin.Name = "txtEditLogin";
            this.txtEditLogin.Size = new System.Drawing.Size(140, 24);
            this.txtEditLogin.TabIndex = 3;
            // 
            // lblEditPassword
            // 
            this.lblEditPassword.AutoSize = true;
            this.lblEditPassword.Location = new System.Drawing.Point(20, 166);
            this.lblEditPassword.Name = "lblEditPassword";
            this.lblEditPassword.Size = new System.Drawing.Size(67, 18);
            this.lblEditPassword.TabIndex = 2;
            this.lblEditPassword.Text = "Пароль";
            // 
            // lblEditLogin
            // 
            this.lblEditLogin.AutoSize = true;
            this.lblEditLogin.Location = new System.Drawing.Point(20, 116);
            this.lblEditLogin.Name = "lblEditLogin";
            this.lblEditLogin.Size = new System.Drawing.Size(49, 18);
            this.lblEditLogin.TabIndex = 1;
            this.lblEditLogin.Text = "Логін";
            // 
            // lblSelectUser
            // 
            this.lblSelectUser.AutoSize = true;
            this.lblSelectUser.Location = new System.Drawing.Point(20, 65);
            this.lblSelectUser.Name = "lblSelectUser";
            this.lblSelectUser.Size = new System.Drawing.Size(100, 18);
            this.lblSelectUser.TabIndex = 0;
            this.lblSelectUser.Text = "Користувач";
            // 
            // gbAllUsers
            // 
            this.gbAllUsers.Controls.Add(this.btnClearAllUsers);
            this.gbAllUsers.Controls.Add(this.btnShowAllUsers);
            this.gbAllUsers.Controls.Add(this.listBoxAllUsersGray);
            this.gbAllUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.gbAllUsers.Location = new System.Drawing.Point(20, 20);
            this.gbAllUsers.Name = "gbAllUsers";
            this.gbAllUsers.Size = new System.Drawing.Size(330, 420);
            this.gbAllUsers.TabIndex = 0;
            this.gbAllUsers.TabStop = false;
            this.gbAllUsers.Text = "Користувачі";
            // 
            // btnClearAllUsers
            // 
            this.btnClearAllUsers.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnClearAllUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.btnClearAllUsers.Location = new System.Drawing.Point(120, 370);
            this.btnClearAllUsers.Name = "btnClearAllUsers";
            this.btnClearAllUsers.Size = new System.Drawing.Size(100, 40);
            this.btnClearAllUsers.TabIndex = 2;
            this.btnClearAllUsers.Text = "Очистити";
            this.btnClearAllUsers.UseVisualStyleBackColor = false;
            this.btnClearAllUsers.Click += new System.EventHandler(this.btnClearAllUsers_Click);
            // 
            // btnShowAllUsers
            // 
            this.btnShowAllUsers.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnShowAllUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.btnShowAllUsers.Location = new System.Drawing.Point(15, 370);
            this.btnShowAllUsers.Name = "btnShowAllUsers";
            this.btnShowAllUsers.Size = new System.Drawing.Size(100, 40);
            this.btnShowAllUsers.TabIndex = 1;
            this.btnShowAllUsers.Text = "Показати";
            this.btnShowAllUsers.UseVisualStyleBackColor = false;
            this.btnShowAllUsers.Click += new System.EventHandler(this.btnShowAllUsers_Click);
            // 
            // listBoxAllUsersGray
            // 
            this.listBoxAllUsersGray.BackColor = System.Drawing.Color.DarkGray;
            this.listBoxAllUsersGray.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.listBoxAllUsersGray.FormattingEnabled = true;
            this.listBoxAllUsersGray.ItemHeight = 18;
            this.listBoxAllUsersGray.Location = new System.Drawing.Point(15, 35);
            this.listBoxAllUsersGray.Name = "listBoxAllUsersGray";
            this.listBoxAllUsersGray.Size = new System.Drawing.Size(300, 310);
            this.listBoxAllUsersGray.TabIndex = 0;
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 531);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Адміністрування";
            this.Load += new System.EventHandler(this.AdminForm_Load);
            this.panelTop.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageAdd.ResumeLayout(false);
            this.gbBinList.ResumeLayout(false);
            this.gbTxtList.ResumeLayout(false);
            this.gbAddUser.ResumeLayout(false);
            this.gbAddUser.PerformLayout();
            this.tabPageEdit.ResumeLayout(false);
            this.gbEditList.ResumeLayout(false);
            this.gbEditUser.ResumeLayout(false);
            this.gbEditUser.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gbAllUsers.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        // ... (Тут іде весь попередній список змінних, я додав туди ToolStrip та нові кнопки)
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnCloseForm;
        private System.Windows.Forms.Button btnGoToStart;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageAdd;
        private System.Windows.Forms.GroupBox gbBinList;
        private System.Windows.Forms.Button btnClearBin;
        private System.Windows.Forms.Button btnLoadBin;
        private System.Windows.Forms.ListBox listBoxBin;
        private System.Windows.Forms.GroupBox gbTxtList;
        private System.Windows.Forms.ListBox listBoxTxt;
        private System.Windows.Forms.GroupBox gbAddUser;
        private System.Windows.Forms.Button btnSaveBin;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.CheckBox chkIsAdmin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.TabPage tabPageEdit;
        private System.Windows.Forms.GroupBox gbEditList;
        private System.Windows.Forms.Button btnLoadEditList;
        private System.Windows.Forms.ListBox listBoxEditUsers;
        private System.Windows.Forms.GroupBox gbEditUser;
        private System.Windows.Forms.Button btnSavePassword;
        private System.Windows.Forms.Button btnSaveLogin;
        private System.Windows.Forms.Button btnDeleteUser;
        private System.Windows.Forms.ComboBox cmbEditSelectUser;
        private System.Windows.Forms.CheckBox chkEditIsAdmin;
        private System.Windows.Forms.TextBox txtEditPassword;
        private System.Windows.Forms.TextBox txtEditLogin;
        private System.Windows.Forms.Label lblEditPassword;
        private System.Windows.Forms.Label lblEditLogin;
        private System.Windows.Forms.Label lblSelectUser;
        private System.Windows.Forms.GroupBox gbAllUsers;
        private System.Windows.Forms.Button btnClearAllUsers;
        private System.Windows.Forms.Button btnShowAllUsers;
        private System.Windows.Forms.ListBox listBoxAllUsersGray;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsBtnRefresh;
        private System.Windows.Forms.ToolStripButton tsBtnClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsBtnApply;
        private System.Windows.Forms.ToolStripButton tsBtnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsBtnDelete;
    }
}