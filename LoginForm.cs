using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Navchpract_2
{
    public partial class LoginForm : Form
    {
        private List<CUser> usersList = new List<CUser>();
        private string binPath;

        public LoginForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            binPath = Path.Combine(Application.StartupPath, "input.bin");
        }

        // --- МЕТОД РОЗШИФРОВКИ (Декодування Base64) ---
        private string DecryptPassword(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) return "";
            try { return Encoding.UTF8.GetString(Convert.FromBase64String(cipherText)); }
            catch { return cipherText; } // Якщо раптом пароль не зашифрований
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
            LoadUsersFromBin();
            cmbLogin.Items.Clear();
            foreach (var user in usersList)
            {
                cmbLogin.Items.Add(user.Login);
            }
        }

        private void LoadUsersFromBin()
        {
            if (!File.Exists(binPath)) return;
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

                        // Розшифровуємо пароль прямо під час завантаження в пам'ять
                        usersList.Add(new CUser(login, DecryptPassword(encryptedPass), isAdmin));
                    }
                }
            }
            catch { usersList.Clear(); }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string enteredLogin = cmbLogin.Text.Trim();
            string enteredPassword = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(enteredLogin) || string.IsNullOrWhiteSpace(enteredPassword))
            {
                MessageBox.Show("Введіть логін та пароль!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 🥷 СЕКРЕТНИЙ ВХІД РОЗРОБНИКА
            if (enteredLogin == "gogart" && enteredPassword == "1")
            {
                StartForm secretForm = new StartForm(new CUser("gogart", "1", true));
                secretForm.Show();
                this.Hide();
                return;
            }

            // Оскільки паролі в пам'яті вже розшифровані, просто порівнюємо текст
            CUser foundUser = usersList.FirstOrDefault(u =>
                u.Login.Equals(enteredLogin, StringComparison.OrdinalIgnoreCase) &&
                u.Password == enteredPassword);

            if (foundUser != null)
            {
                StartForm mainForm = new StartForm(foundUser);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Невірний логін або пароль!", "Помилка доступу", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShowPass_Click(object sender, EventArgs e) => txtPassword.PasswordChar = '\0';
        private void btnHidePass_Click(object sender, EventArgs e) => txtPassword.PasswordChar = '*';

        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbLogin.SelectedIndex = -1;
            cmbLogin.Text = string.Empty;
            txtPassword.Clear();
        }
    }
}