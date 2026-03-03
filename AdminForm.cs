using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Navchpract_2
{
    public partial class AdminForm : Form
    {
        private List<CUser> usersList = new List<CUser>();
        private string txtPath;
        private string binPath;
        private bool isUpdating = false;
        private CUser sessionUser;

        private Dictionary<TextBox, Stack<string>> undoStacks = new Dictionary<TextBox, Stack<string>>();
        private Dictionary<TextBox, Stack<string>> redoStacks = new Dictionary<TextBox, Stack<string>>();
        private bool isUndoRedoAction = false;
        private bool hasUnsavedChanges = false;

        public AdminForm(CUser user)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            txtPath = Path.Combine(Application.StartupPath, "input.txt");
            binPath = Path.Combine(Application.StartupPath, "input.bin");
            sessionUser = user;

            if (cmbEditSelectUser != null)
            {
                cmbEditSelectUser.MaxDropDownItems = 5;
                cmbEditSelectUser.IntegralHeight = false;
            }

            if (tsBtnClear != null) tsBtnClear.Click += tsBtnClear_Click;
            if (tsBtnRefresh != null) tsBtnRefresh.Click += async (s, e) => { await LoadFromBinFileAsync(); UpdateAllUI(); ClearEditFieldsAction(); };
            if (tsBtnApply != null) tsBtnApply.Click += async (s, e) => await SaveSelectedUserChangesAsync(true);
            if (tsBtnSave != null) tsBtnSave.Click += async (s, e) => await SaveSelectedUserChangesAsync(false);
            if (tsBtnDelete != null) tsBtnDelete.Click += async (s, e) => await DeleteSelectedUserAsync();
        }

        public AdminForm() : this(new CUser("Адмін (Авто)", "1111", true)) { }

        private async void AdminForm_Load(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '\0';
            ClearEditFieldsAction();

            if (!sessionUser.IsAdmin)
            {
                foreach (Control c in this.Controls)
                {
                    if (c is TabControl tc && tc.TabPages.Count > 1)
                    {
                        tc.TabPages.RemoveAt(1);
                        break;
                    }
                }
            }

            if (txtEditPassword != null)
            {
                txtEditPassword.Enter += TxtEditPassword_Enter;
                txtEditPassword.Leave += TxtEditPassword_Leave;
            }

            if (txtEditLogin != null) RegisterUndoRedo(txtEditLogin);
            if (txtEditPassword != null) RegisterUndoRedo(txtEditPassword);

            if (toolStrip1 != null)
            {
                ToolStripButton tsUndo = new ToolStripButton("↩");
                tsUndo.ToolTipText = "Відмінити ввід (Ctrl+Z)";
                tsUndo.Click += (s, ev) => PerformUndo(GetActiveUndoTextBox());

                ToolStripButton tsRedo = new ToolStripButton("↪");
                tsRedo.ToolTipText = "Повернути ввід (Ctrl+Y)";
                tsRedo.Click += (s, ev) => PerformRedo(GetActiveUndoTextBox());

                toolStrip1.Items.Add(new ToolStripSeparator());
                toolStrip1.Items.Add(tsUndo);
                toolStrip1.Items.Add(tsRedo);
            }

        }

        #region Логіка Placeholder
        private void SetPasswordPlaceholder()
        {
            if (txtEditPassword == null) return;
            txtEditPassword.PasswordChar = '\0';
            txtEditPassword.Text = "Введіть новий пароль";
            txtEditPassword.ForeColor = System.Drawing.Color.Gray;
        }

        private void TxtEditPassword_Enter(object sender, EventArgs e)
        {
            if (txtEditPassword.Text == "Введіть новий пароль")
            {
                txtEditPassword.Text = "";
                txtEditPassword.ForeColor = System.Drawing.Color.Black;
                txtEditPassword.PasswordChar = '\0';
            }
        }

        private void TxtEditPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEditPassword.Text))
            {
                SetPasswordPlaceholder();
            }
        }
        #endregion

        #region Логіка UNDO / REDO
        private void RegisterUndoRedo(TextBox txt)
        {
            undoStacks[txt] = new Stack<string>();
            redoStacks[txt] = new Stack<string>();
            undoStacks[txt].Push(txt.Text ?? "");

            txt.TextChanged += (s, e) =>
            {
                if (!isUndoRedoAction)
                {
                    undoStacks[txt].Push(txt.Text);
                    redoStacks[txt].Clear();
                }
            };

            txt.KeyDown += (s, e) =>
            {
                if (e.Control && e.KeyCode == Keys.Z) { e.Handled = true; e.SuppressKeyPress = true; PerformUndo(txt); }
                if (e.Control && e.KeyCode == Keys.Y) { e.Handled = true; e.SuppressKeyPress = true; PerformRedo(txt); }
            };
        }

        private TextBox GetActiveUndoTextBox()
        {
            if (this.ActiveControl == txtEditLogin) return txtEditLogin;
            return txtEditPassword;
        }

        private void PerformUndo(TextBox txt)
        {
            if (txt != null && undoStacks.ContainsKey(txt) && undoStacks[txt].Count > 1)
            {
                isUndoRedoAction = true;
                redoStacks[txt].Push(undoStacks[txt].Pop());
                txt.Text = undoStacks[txt].Peek();
                txt.SelectionStart = txt.Text.Length;
                isUndoRedoAction = false;
            }
        }

        private void PerformRedo(TextBox txt)
        {
            if (txt != null && redoStacks.ContainsKey(txt) && redoStacks[txt].Count > 0)
            {
                isUndoRedoAction = true;
                string redoText = redoStacks[txt].Pop();
                undoStacks[txt].Push(redoText);
                txt.Text = redoText;
                txt.SelectionStart = txt.Text.Length;
                isUndoRedoAction = false;
            }
        }

        private void InitUndoRedoState()
        {
            if (txtEditLogin != null && undoStacks.ContainsKey(txtEditLogin))
            {
                undoStacks[txtEditLogin].Clear();
                undoStacks[txtEditLogin].Push(txtEditLogin.Text);
                redoStacks[txtEditLogin].Clear();
            }
            if (txtEditPassword != null && undoStacks.ContainsKey(txtEditPassword))
            {
                undoStacks[txtEditPassword].Clear();
                undoStacks[txtEditPassword].Push(txtEditPassword.Text);
                redoStacks[txtEditPassword].Clear();
            }
        }
        #endregion

        #region АСИНХРОННА Робота з файлами
        private async Task SaveToTxtFileAsync()
        {
            await Task.Run(() =>
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(txtPath, false))
                    {
                        foreach (var u in usersList) sw.WriteLine($"{u.Login};{u.Password};{u.IsAdmin}");
                    }
                }
                catch (Exception ex) { this.Invoke(new Action(() => MessageBox.Show($"Помилка TXT: {ex.Message}"))); }
            });
        }

        private async Task SaveToBinFileAsync()
        {
            await Task.Run(() =>
            {
                try
                {
                    using (BinaryWriter bw = new BinaryWriter(File.Open(binPath, FileMode.Create)))
                    {
                        bw.Write(usersList.Count);
                        foreach (var u in usersList)
                        {
                            bw.Write(u.Login ?? "");
                            bw.Write(u.Password ?? "");
                            bw.Write(u.IsAdmin);
                        }
                    }
                    hasUnsavedChanges = false;
                }
                catch (Exception ex) { this.Invoke(new Action(() => MessageBox.Show($"Помилка BIN: {ex.Message}"))); }
            });
        }

        private async Task LoadFromBinFileAsync()
        {
            if (!File.Exists(binPath)) { usersList.Clear(); return; }
            await Task.Run(() =>
            {
                try
                {
                    List<CUser> temp = new List<CUser>();
                    using (BinaryReader br = new BinaryReader(File.Open(binPath, FileMode.Open)))
                    {
                        int count = br.ReadInt32();
                        for (int i = 0; i < count; i++)
                        {
                            string login = br.ReadString();
                            string hashPass = br.ReadString();
                            bool isAdmin = br.ReadBoolean();
                            temp.Add(new CUser(login, hashPass, isAdmin));
                        }
                    }
                    usersList = temp;
                    hasUnsavedChanges = false;
                }
                catch { usersList.Clear(); }
            });
        }
        #endregion

        #region Оновлення інтерфейсу
        private void UpdateAllUI()
        {
            isUpdating = true;
            listBoxBin.Items.Clear();
            listBoxEditUsers.Items.Clear();
            cmbEditSelectUser.Items.Clear();

            foreach (var u in usersList)
            {
                string role = u.IsAdmin ? "Адмін" : "Юзер";
                string cleanInfo = $"{u.Login} [{role}]";

                listBoxBin.Items.Add(cleanInfo);
                listBoxEditUsers.Items.Add(cleanInfo);
                cmbEditSelectUser.Items.Add(u.Login);
            }

            listBoxTxt.Items.Clear();
            if (File.Exists(txtPath))
            {
                foreach (string line in File.ReadAllLines(txtPath))
                {
                    string[] parts = line.Split(';');
                    if (parts.Length >= 3)
                    {
                        string role = bool.Parse(parts[2]) ? "Адмін" : "Юзер";
                        listBoxTxt.Items.Add($"{parts[0]} [{role}]");
                    }
                }
            }
            isUpdating = false;
        }

        private void ClearEditFieldsAction()
        {
            isUpdating = true;
            txtEditLogin.Clear(); chkEditIsAdmin.Checked = false;
            listBoxEditUsers.SelectedIndex = -1; cmbEditSelectUser.SelectedIndex = -1; cmbEditSelectUser.Text = string.Empty;

            if (txtEditLogin != null) txtEditLogin.Enabled = false;
            if (txtEditPassword != null) txtEditPassword.Enabled = false;
            if (chkEditIsAdmin != null) chkEditIsAdmin.Enabled = false;

            SetPasswordPlaceholder();

            this.ActiveControl = null;
            InitUndoRedoState();
            isUpdating = false;
        }
        #endregion

        #region АСИНХРОННА Перевірка незбережених даних
        private bool HasUnsavedData()
        {
            if (hasUnsavedChanges) return true;

            if (!string.IsNullOrWhiteSpace(txtLogin.Text) || !string.IsNullOrWhiteSpace(txtPassword.Text))
                return true;

            if (cmbEditSelectUser.SelectedIndex >= 0 && cmbEditSelectUser.SelectedIndex < usersList.Count)
            {
                var u = usersList[cmbEditSelectUser.SelectedIndex];
                bool isPasswordChanged = txtEditPassword.Text != "Введіть новий пароль" && !string.IsNullOrWhiteSpace(txtEditPassword.Text);

                if (txtEditLogin.Text.Trim() != u.Login || chkEditIsAdmin.Checked != u.IsAdmin || isPasswordChanged)
                {
                    return true;
                }
            }
            return false;
        }

        private async Task ForceSaveAllAsync()
        {
            string newHashForEdit = null;
            string newHashForAdd = null;

            bool saveNeeded = false;

            if (cmbEditSelectUser.SelectedIndex >= 0 && cmbEditSelectUser.SelectedIndex < usersList.Count)
            {
                int index = cmbEditSelectUser.SelectedIndex;
                var u = usersList[index];
                string newLogin = txtEditLogin.Text.Trim();
                string newPass = txtEditPassword.Text.Trim();
                bool isPasswordChanged = newPass != "Введіть новий пароль" && !string.IsNullOrWhiteSpace(newPass);

                if (!string.IsNullOrWhiteSpace(newLogin) && !usersList.Where((user, i) => i != index).Any(user => user.Login.Equals(newLogin, StringComparison.OrdinalIgnoreCase)))
                {
                    if (!(u.Login.Equals(sessionUser.Login, StringComparison.OrdinalIgnoreCase) && !chkEditIsAdmin.Checked))
                    {
                        u.Login = newLogin;
                        u.IsAdmin = chkEditIsAdmin.Checked;
                        if (u.Login.Equals(sessionUser.Login, StringComparison.OrdinalIgnoreCase)) sessionUser.Login = newLogin;
                        saveNeeded = true;

                        if (isPasswordChanged)
                        {
                            await Task.Run(() => { newHashForEdit = CryptoHelper.HashPassword(newPass); });
                            u.Password = newHashForEdit;
                        }
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(txtLogin.Text) && !string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                string login = txtLogin.Text.Trim();
                string pass = txtPassword.Text.Trim();
                if (!usersList.Any(u => u.Login.Equals(login, StringComparison.OrdinalIgnoreCase)))
                {
                    bool isAdmin = chkIsAdmin.Checked;
                    await Task.Run(() => { newHashForAdd = CryptoHelper.HashPassword(pass); });
                    usersList.Add(new CUser(login, newHashForAdd, isAdmin));
                    saveNeeded = true;
                }
            }

            if (saveNeeded)
            {
                await SaveToTxtFileAsync();
                await SaveToBinFileAsync();
            }
        }

        private async void CheckAndClose(Action closeAction)
        {
            if (HasUnsavedData())
            {
                DialogResult res = MessageBox.Show("У вас є незбережені дані.\nЗберегти їх перед виходом?", "Незбережені зміни", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    await ForceSaveAllAsync();
                    this.Cursor = Cursors.Default;
                    closeAction();
                }
                else if (res == DialogResult.No) closeAction();
            }
            else closeAction();
        }
        #endregion

        #region АСИНХРОННА Реєстрація та Кнопки
        private async void btnRegister_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim(), pass = txtPassword.Text.Trim();
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(pass)) { MessageBox.Show("Заповніть поля!"); return; }
            if (usersList.Any(u => u.Login.Equals(login, StringComparison.OrdinalIgnoreCase))) { MessageBox.Show("Логін існує!"); return; }

            btnRegister.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            string hashedPass = "";
            bool isAdmin = chkIsAdmin.Checked;
            await Task.Run(() => { hashedPass = CryptoHelper.HashPassword(pass); });

            usersList.Add(new CUser(login, hashedPass, isAdmin));
            await SaveToTxtFileAsync();
            await SaveToBinFileAsync();

            txtLogin.Clear(); txtPassword.Clear(); chkIsAdmin.Checked = false;
            UpdateAllUI();

            this.Cursor = Cursors.Default;
            btnRegister.Enabled = true;
            this.ActiveControl = null;
        }

        private async void btnSaveBin_Click(object sender, EventArgs e)
        {
            btnSaveBin.Enabled = false;
            await SaveToBinFileAsync();
            UpdateAllUI();
            btnSaveBin.Enabled = true;
            MessageBox.Show("Збережено!");
            this.ActiveControl = null;
        }

        private async void btnLoadBin_Click(object sender, EventArgs e)
        {
            btnLoadBin.Enabled = false;
            await LoadFromBinFileAsync();
            UpdateAllUI();
            btnLoadBin.Enabled = true;
            MessageBox.Show("Завантажено!");
            this.ActiveControl = null;
        }

        private void btnClearBin_Click(object sender, EventArgs e) { listBoxBin.Items.Clear(); listBoxTxt.Items.Clear(); this.ActiveControl = null; }

        private async void btnLoadEditList_Click(object sender, EventArgs e)
        {
            btnLoadEditList.Enabled = false;
            await LoadFromBinFileAsync();
            UpdateAllUI();
            ClearEditFieldsAction();
            btnLoadEditList.Enabled = true;
        }

        private void btnGoToStart_Click(object sender, EventArgs e) { CheckAndClose(() => this.Close()); }
        private void btnCloseForm_Click(object sender, EventArgs e) { CheckAndClose(() => Application.Exit()); }
        #endregion

        #region АСИНХРОННЕ Редагування
        private async Task SaveSelectedUserChangesAsync(bool isApplyOnly)
        {
            int index = cmbEditSelectUser.SelectedIndex;
            if (index >= 0)
            {
                string newLogin = txtEditLogin.Text.Trim();
                string newPassText = txtEditPassword.Text.Trim();
                bool isPasswordChanged = newPassText != "Введіть новий пароль" && !string.IsNullOrWhiteSpace(newPassText);

                if (string.IsNullOrWhiteSpace(newLogin)) { MessageBox.Show("Порожній логін!"); return; }
                if (usersList.Where((u, i) => i != index).Any(u => u.Login.Equals(newLogin, StringComparison.OrdinalIgnoreCase))) { MessageBox.Show("Логін зайнятий!"); return; }

                if (usersList[index].Login.Equals(sessionUser.Login, StringComparison.OrdinalIgnoreCase) && !chkEditIsAdmin.Checked)
                {
                    MessageBox.Show("Ви не можете зняти права з себе!", "Захист", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    chkEditIsAdmin.Checked = true;
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                if (usersList[index].Login.Equals(sessionUser.Login, StringComparison.OrdinalIgnoreCase))
                {
                    sessionUser.Login = newLogin;
                }

                usersList[index].Login = newLogin;

                if (isPasswordChanged)
                {
                    string newHash = null;
                    await Task.Run(() => { newHash = CryptoHelper.HashPassword(newPassText); });
                    usersList[index].Password = newHash;
                }

                usersList[index].IsAdmin = chkEditIsAdmin.Checked;

                if (!isApplyOnly)
                {
                    await SaveToBinFileAsync();
                    await SaveToTxtFileAsync();
                }
                else
                {
                    hasUnsavedChanges = true;
                }

                UpdateAllUI();
                ClearEditFieldsAction();
                this.Cursor = Cursors.Default;

                MessageBox.Show(isApplyOnly ? "Зміни застосовано!" : "Зміни збережено!");
            }
        }

        private async Task DeleteSelectedUserAsync()
        {
            int index = cmbEditSelectUser.SelectedIndex;
            if (index >= 0)
            {
                if (usersList[index].Login.Equals(sessionUser.Login, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Ви не можете видалити себе.", "Захист", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (MessageBox.Show("Видалити?", "Увага", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    usersList.RemoveAt(index);
                    this.Cursor = Cursors.WaitCursor;
                    await SaveToBinFileAsync();
                    await SaveToTxtFileAsync();
                    this.Cursor = Cursors.Default;
                    UpdateAllUI();
                    ClearEditFieldsAction();
                }
            }
        }

        private void tsBtnClear_Click(object sender, EventArgs e) { ClearEditFieldsAction(); }

        private void listBoxEditUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUpdating || listBoxEditUsers.SelectedIndex < 0) return;
            isUpdating = true;
            cmbEditSelectUser.SelectedIndex = listBoxEditUsers.SelectedIndex;
            var u = usersList[listBoxEditUsers.SelectedIndex];

            txtEditLogin.Enabled = true;
            txtEditPassword.Enabled = true;
            chkEditIsAdmin.Enabled = true;

            txtEditLogin.Text = u.Login;
            SetPasswordPlaceholder();
            chkEditIsAdmin.Checked = u.IsAdmin;

            InitUndoRedoState();
            isUpdating = false;
        }

        private void cmbEditSelectUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUpdating || cmbEditSelectUser.SelectedIndex < 0) return;
            isUpdating = true;
            listBoxEditUsers.SelectedIndex = cmbEditSelectUser.SelectedIndex;
            var u = usersList[cmbEditSelectUser.SelectedIndex];

            txtEditLogin.Enabled = true;
            txtEditPassword.Enabled = true;
            chkEditIsAdmin.Enabled = true;

            txtEditLogin.Text = u.Login;
            SetPasswordPlaceholder();
            chkEditIsAdmin.Checked = u.IsAdmin;

            InitUndoRedoState();
            isUpdating = false;
        }
        #endregion
    }
}