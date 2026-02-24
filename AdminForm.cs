using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Navchpract_2
{
    public partial class AdminForm : Form
    {
        private List<CUser> usersList = new List<CUser>();
        private string txtPath;
        private string binPath;
        private bool isUpdating = false;

        // Змінна для зберігання того, хто зараз увійшов
        private CUser sessionUser;

        // Новий конструктор, який приймає юзера
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

        // Заглушка для старих викликів
        public AdminForm() : this(new CUser("Адмін (Авто)", "1111", true)) { }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
            txtEditPassword.PasswordChar = '\0';
            ClearEditFieldsAction();

            // Якщо це звичайний юзер, видаляємо вкладку "Редагування", щоб він туди не міг зайти
            if (!sessionUser.IsAdmin)
            {
                foreach (Control c in this.Controls)
                {
                    if (c is TabControl tc && tc.TabPages.Count > 1)
                    {
                        tc.TabPages.RemoveAt(1); // Видаляємо другу вкладку
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
                    foreach (var u in usersList) sw.WriteLine($"{u.Login};{u.Password};{u.IsAdmin}");
                }
            }
            catch (Exception ex) { MessageBox.Show($"Помилка збереження TXT: {ex.Message}"); }
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
                        bw.Write(u.Password ?? "");
                        bw.Write(u.IsAdmin);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show($"Помилка збереження BIN: {ex.Message}"); }
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
                    for (int i = 0; i < count; i++) usersList.Add(new CUser(br.ReadString(), br.ReadString(), br.ReadBoolean()));
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

            foreach (var u in usersList)
            {
                string info = u.ToString();
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
                    string[] p = line.Split(';');
                    listBoxTxt.Items.Add(p.Length >= 3 ? $"{p[0]};***;{p[2]}" : line);
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
            string login = txtLogin.Text.Trim();
            string pass = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(pass))
            { MessageBox.Show("Заповніть логін і пароль!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (usersList.Any(u => u.Login.Equals(login, StringComparison.OrdinalIgnoreCase)))
            { MessageBox.Show("Користувач з таким логіном вже існує!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            usersList.Add(new CUser(login, pass, chkIsAdmin.Checked));
            SaveToTxtFile(); SaveToBinFile();
            txtLogin.Clear(); txtPassword.Clear(); chkIsAdmin.Checked = false;
            UpdateAllUI();
            MessageBox.Show("Користувача зареєстровано!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSaveBin_Click(object sender, EventArgs e) { SaveToBinFile(); UpdateAllUI(); MessageBox.Show("Дані збережено!"); }
        private void btnLoadBin_Click(object sender, EventArgs e) { LoadFromBinFile(); UpdateAllUI(); MessageBox.Show("Дані завантажено!"); }
        private void btnClearBin_Click(object sender, EventArgs e) { listBoxBin.Items.Clear(); listBoxTxt.Items.Clear(); }
        private void btnShowAllUsers_Click(object sender, EventArgs e) { UpdateAllUI(); }
        private void btnClearAllUsers_Click(object sender, EventArgs e) { listBoxAllUsersGray.Items.Clear(); }
        private void btnLoadEditList_Click(object sender, EventArgs e) { LoadFromBinFile(); UpdateAllUI(); ClearEditFieldsAction(); MessageBox.Show("Дані для редагування завантажено!"); }

        private void btnGoToStart_Click(object sender, EventArgs e) { if (MessageBox.Show("Повернутися на головне вікно?", "Підтвердження", MessageBoxButtons.YesNo) == DialogResult.Yes) this.Close(); }
        private void btnCloseForm_Click(object sender, EventArgs e) { if (MessageBox.Show("Вийти з програми?", "Підтвердження", MessageBoxButtons.YesNo) == DialogResult.Yes) Application.Exit(); }

        #endregion

        #region Редагування та Захист Адміна

        private bool ValidateEditInput(int index, string newLogin, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newLogin) || string.IsNullOrWhiteSpace(newPassword))
            { MessageBox.Show("Логін та пароль не можуть бути порожніми!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }

            if (usersList.Where((u, i) => i != index).Any(u => u.Login.Equals(newLogin, StringComparison.OrdinalIgnoreCase)))
            { MessageBox.Show("Цей логін вже використовується іншим користувачем!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }

            // Броня: Адмін не може зняти галочку "IsAdmin" з самого себе
            if (usersList[index].Login.Equals(sessionUser.Login, StringComparison.OrdinalIgnoreCase) && !chkEditIsAdmin.Checked)
            {
                MessageBox.Show("Ви не можете зняти права адміністратора з власного акаунту!", "Захист", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                chkEditIsAdmin.Checked = true; // Автоматично повертаємо галочку
                return false;
            }

            return true;
        }

        private void SaveSelectedUserChanges(object sender, EventArgs e)
        {
            int index = cmbEditSelectUser.SelectedIndex;
            if (index >= 0)
            {
                string newLogin = txtEditLogin.Text.Trim();
                string newPassword = txtEditPassword.Text.Trim();

                if (!ValidateEditInput(index, newLogin, newPassword)) return;

                // Якщо адмін змінив свій власний логін/пароль, оновлюємо поточну сесію
                if (usersList[index].Login.Equals(sessionUser.Login, StringComparison.OrdinalIgnoreCase))
                {
                    sessionUser.Login = newLogin;
                    sessionUser.Password = newPassword;
                }

                usersList[index].Login = newLogin;
                usersList[index].Password = newPassword;
                usersList[index].IsAdmin = chkEditIsAdmin.Checked;

                SaveToBinFile(); SaveToTxtFile(); UpdateAllUI(); ClearEditFieldsAction();
                MessageBox.Show("Зміни успішно збережено у файли!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tsBtnApply_Click(object sender, EventArgs e)
        {
            int index = cmbEditSelectUser.SelectedIndex;
            if (index >= 0)
            {
                string newLogin = txtEditLogin.Text.Trim();
                string newPassword = txtEditPassword.Text.Trim();

                if (!ValidateEditInput(index, newLogin, newPassword)) return;

                if (usersList[index].Login.Equals(sessionUser.Login, StringComparison.OrdinalIgnoreCase))
                {
                    sessionUser.Login = newLogin;
                    sessionUser.Password = newPassword;
                }

                usersList[index].Login = newLogin;
                usersList[index].Password = newPassword;
                usersList[index].IsAdmin = chkEditIsAdmin.Checked;

                UpdateAllUI();
                MessageBox.Show("Зміни застосовано (але ще не збережено у файл)!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DeleteSelectedUser(object sender, EventArgs e)
        {
            int index = cmbEditSelectUser.SelectedIndex;
            if (index >= 0)
            {
                // Броня: Адмін не може видалити себе
                if (usersList[index].Login.Equals(sessionUser.Login, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Ви не можете видалити свій власний акаунт!", "Захист", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (MessageBox.Show("Ви дійсно хочете видалити цього користувача?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    usersList.RemoveAt(index);
                    SaveToBinFile(); SaveToTxtFile(); UpdateAllUI(); ClearEditFieldsAction();
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
            txtEditLogin.Text = u.Login; txtEditPassword.Text = u.Password; chkEditIsAdmin.Checked = u.IsAdmin;
            isUpdating = false;
        }

        private void cmbEditSelectUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUpdating || cmbEditSelectUser.SelectedIndex < 0) return;
            isUpdating = true;
            listBoxEditUsers.SelectedIndex = cmbEditSelectUser.SelectedIndex;
            var u = usersList[cmbEditSelectUser.SelectedIndex];
            txtEditLogin.Text = u.Login; txtEditPassword.Text = u.Password; chkEditIsAdmin.Checked = u.IsAdmin;
            isUpdating = false;
        }

        #endregion
    }
}