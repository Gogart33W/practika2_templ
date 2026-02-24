using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        public AdminForm(CUser user)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            txtPath = Path.Combine(Application.StartupPath, "input.txt");
            binPath = Path.Combine(Application.StartupPath, "input.bin");
            sessionUser = user;

            if (tsBtnClear != null) tsBtnClear.Click += tsBtnClear_Click;
            if (tsBtnRefresh != null) tsBtnRefresh.Click += tsBtnRefresh_Click;
            if (tsBtnApply != null) tsBtnApply.Click += tsBtnApply_Click;
            if (tsBtnSave != null) tsBtnSave.Click += SaveSelectedUserChanges;
            if (tsBtnDelete != null) tsBtnDelete.Click += DeleteSelectedUser;
        }

        public AdminForm() : this(new CUser("Адмін (Авто)", "1111", true)) { }

        // --- МЕТОДИ ШИФРУВАННЯ ТА РОЗШИФРОВКИ ---
        private string EncryptPassword(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return "";
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
        }

        private string DecryptPassword(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) return "";
            try { return Encoding.UTF8.GetString(Convert.FromBase64String(cipherText)); }
            catch { return cipherText; }
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '\0';
            txtEditPassword.PasswordChar = '\0';
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
        }

        #region Робота з файлами
        private void SaveToTxtFile()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(txtPath, false))
                {
                    // Перед записом у файл ШИФРУЄМО пароль
                    foreach (var u in usersList) sw.WriteLine($"{u.Login};{EncryptPassword(u.Password)};{u.IsAdmin}");
                }
            }
            catch (Exception ex) { MessageBox.Show($"Помилка TXT: {ex.Message}"); }
        }

        private void SaveToBinFile()
        {
            try
            {
                using (BinaryWriter bw = new BinaryWriter(File.Open(binPath, FileMode.Create)))
                {
                    bw.Write(usersList.Count);
                    foreach (var u in usersList)
                    {
                        bw.Write(u.Login ?? "");
                        bw.Write(EncryptPassword(u.Password ?? "")); // Записуємо зашифрований
                        bw.Write(u.IsAdmin);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show($"Помилка BIN: {ex.Message}"); }
        }

        private void LoadFromBinFile()
        {
            if (!File.Exists(binPath)) { usersList.Clear(); return; }
            try
            {
                usersList.Clear();
                using (BinaryReader br = new BinaryReader(File.Open(binPath, FileMode.Open)))
                {
                    int count = br.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        string login = br.ReadString();
                        string encryptedPass = br.ReadString();
                        bool isAdmin = br.ReadBoolean();

                        // РОЗШИФРОВУЄМО пароль у пам'ять
                        usersList.Add(new CUser(login, DecryptPassword(encryptedPass), isAdmin));
                    }
                }
            }
            catch { usersList.Clear(); }
        }
        #endregion

        #region Оновлення інтерфейсу
        private void UpdateAllUI()
        {
            isUpdating = true;
            listBoxBin.Items.Clear();
            listBoxEditUsers.Items.Clear();
            cmbEditSelectUser.Items.Clear();
            listBoxAllUsersGray.Items.Clear();

            // Внутрішні списки показують РОЗШИФРОВАНІ паролі
            foreach (var u in usersList)
            {
                string info = $"{u.Login};{u.Password};{u.IsAdmin}";
                listBoxBin.Items.Add(info);
                listBoxEditUsers.Items.Add(info);
                listBoxAllUsersGray.Items.Add(info);
                cmbEditSelectUser.Items.Add(u.Login);
            }

            listBoxTxt.Items.Clear();
            if (File.Exists(txtPath))
            {
                foreach (string line in File.ReadAllLines(txtPath))
                {
                    // Тут ми читаємо напряму з файлу, тому ти бачитимеш, як виглядає зашифрований текст
                    listBoxTxt.Items.Add(line);
                }
            }
            isUpdating = false;
        }

        private void ClearEditFieldsAction()
        {
            isUpdating = true;
            txtEditLogin.Clear(); txtEditPassword.Clear(); chkEditIsAdmin.Checked = false;
            listBoxEditUsers.SelectedIndex = -1; cmbEditSelectUser.SelectedIndex = -1; cmbEditSelectUser.Text = string.Empty;
            isUpdating = false;
        }
        #endregion

        #region Реєстрація та Кнопки
        private void btnRegister_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim(), pass = txtPassword.Text.Trim();
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(pass)) { MessageBox.Show("Заповніть поля!"); return; }
            if (usersList.Any(u => u.Login.Equals(login, StringComparison.OrdinalIgnoreCase))) { MessageBox.Show("Логін існує!"); return; }

            // Зберігаємо в пам'ять нормальний пароль
            usersList.Add(new CUser(login, pass, chkIsAdmin.Checked));
            SaveToTxtFile(); SaveToBinFile();
            txtLogin.Clear(); txtPassword.Clear(); chkIsAdmin.Checked = false;
            UpdateAllUI();
        }

        private void btnSaveBin_Click(object sender, EventArgs e) { SaveToBinFile(); UpdateAllUI(); MessageBox.Show("Збережено!"); }
        private void btnLoadBin_Click(object sender, EventArgs e) { LoadFromBinFile(); UpdateAllUI(); MessageBox.Show("Завантажено!"); }
        private void btnClearBin_Click(object sender, EventArgs e) { listBoxBin.Items.Clear(); listBoxTxt.Items.Clear(); }
        private void btnShowAllUsers_Click(object sender, EventArgs e) { UpdateAllUI(); }
        private void btnClearAllUsers_Click(object sender, EventArgs e) { listBoxAllUsersGray.Items.Clear(); }
        private void btnLoadEditList_Click(object sender, EventArgs e) { LoadFromBinFile(); UpdateAllUI(); ClearEditFieldsAction(); }

        private void btnGoToStart_Click(object sender, EventArgs e) { if (MessageBox.Show("Повернутися?", "Вихід", MessageBoxButtons.YesNo) == DialogResult.Yes) this.Close(); }
        private void btnCloseForm_Click(object sender, EventArgs e) { if (MessageBox.Show("Вийти?", "Вихід", MessageBoxButtons.YesNo) == DialogResult.Yes) Application.Exit(); }
        #endregion

        #region Редагування
        private void SaveSelectedUserChanges(object sender, EventArgs e)
        {
            int index = cmbEditSelectUser.SelectedIndex;
            if (index >= 0)
            {
                string newLogin = txtEditLogin.Text.Trim();
                string newPass = txtEditPassword.Text.Trim();

                if (string.IsNullOrWhiteSpace(newLogin) || string.IsNullOrWhiteSpace(newPass)) { MessageBox.Show("Порожні поля!"); return; }
                if (usersList.Where((u, i) => i != index).Any(u => u.Login.Equals(newLogin, StringComparison.OrdinalIgnoreCase))) { MessageBox.Show("Логін зайнятий!"); return; }

                if (usersList[index].Login.Equals(sessionUser.Login, StringComparison.OrdinalIgnoreCase) && !chkEditIsAdmin.Checked)
                {
                    MessageBox.Show("Ви не можете зняти права адміністратора з себе!", "Захист", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    chkEditIsAdmin.Checked = true;
                    return;
                }

                if (usersList[index].Login.Equals(sessionUser.Login, StringComparison.OrdinalIgnoreCase))
                {
                    sessionUser.Login = newLogin;
                    sessionUser.Password = newPass;
                }

                usersList[index].Login = newLogin;
                usersList[index].Password = newPass;
                usersList[index].IsAdmin = chkEditIsAdmin.Checked;
                SaveToBinFile(); SaveToTxtFile(); UpdateAllUI(); ClearEditFieldsAction();
                MessageBox.Show("Зміни збережено!");
            }
        }

        private void tsBtnApply_Click(object sender, EventArgs e)
        {
            int index = cmbEditSelectUser.SelectedIndex;
            if (index >= 0)
            {
                string newLogin = txtEditLogin.Text.Trim();
                string newPass = txtEditPassword.Text.Trim();

                if (string.IsNullOrWhiteSpace(newLogin) || string.IsNullOrWhiteSpace(newPass)) { MessageBox.Show("Порожні поля!"); return; }
                if (usersList.Where((u, i) => i != index).Any(u => u.Login.Equals(newLogin, StringComparison.OrdinalIgnoreCase))) { MessageBox.Show("Логін зайнятий!"); return; }

                if (usersList[index].Login.Equals(sessionUser.Login, StringComparison.OrdinalIgnoreCase) && !chkEditIsAdmin.Checked)
                {
                    MessageBox.Show("Ви не можете зняти права адміністратора з себе!", "Захист", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    chkEditIsAdmin.Checked = true;
                    return;
                }

                if (usersList[index].Login.Equals(sessionUser.Login, StringComparison.OrdinalIgnoreCase))
                {
                    sessionUser.Login = newLogin;
                    sessionUser.Password = newPass;
                }

                usersList[index].Login = newLogin;
                usersList[index].Password = newPass;
                usersList[index].IsAdmin = chkEditIsAdmin.Checked;
                UpdateAllUI();
                MessageBox.Show("Зміни застосовано!");
            }
        }

        private void DeleteSelectedUser(object sender, EventArgs e)
        {
            int index = cmbEditSelectUser.SelectedIndex;
            if (index >= 0)
            {
                if (usersList[index].Login.Equals(sessionUser.Login, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Спроба самоліквідації відхилена! Ви не можете видалити себе.", "Захист", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (MessageBox.Show("Видалити?", "Увага", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    usersList.RemoveAt(index); SaveToBinFile(); SaveToTxtFile(); UpdateAllUI(); ClearEditFieldsAction();
                }
            }
        }

        private void tsBtnRefresh_Click(object sender, EventArgs e) { LoadFromBinFile(); UpdateAllUI(); ClearEditFieldsAction(); }
        private void tsBtnClear_Click(object sender, EventArgs e) { ClearEditFieldsAction(); }

        private void listBoxEditUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUpdating || listBoxEditUsers.SelectedIndex < 0) return;
            isUpdating = true;
            cmbEditSelectUser.SelectedIndex = listBoxEditUsers.SelectedIndex;
            var u = usersList[listBoxEditUsers.SelectedIndex];
            txtEditLogin.Text = u.Login;
            txtEditPassword.Text = u.Password; // Тепер тут нормальний пароль!
            chkEditIsAdmin.Checked = u.IsAdmin;
            isUpdating = false;
        }

        private void cmbEditSelectUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUpdating || cmbEditSelectUser.SelectedIndex < 0) return;
            isUpdating = true;
            listBoxEditUsers.SelectedIndex = cmbEditSelectUser.SelectedIndex;
            var u = usersList[cmbEditSelectUser.SelectedIndex];
            txtEditLogin.Text = u.Login;
            txtEditPassword.Text = u.Password; // І тут теж
            chkEditIsAdmin.Checked = u.IsAdmin;
            isUpdating = false;
        }
        #endregion
    }
}