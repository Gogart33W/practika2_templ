using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            if (cmbLogin != null)
            {
                cmbLogin.MaxDropDownItems = 5;
                cmbLogin.IntegralHeight = false;
            }

            this.VisibleChanged += (s, e) =>
            {
                if (this.Visible) RefreshLoginList();
            };
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
            RefreshLoginList();
        }

        private void RefreshLoginList()
        {
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
                        string hashPass = br.ReadString();
                        bool isAdmin = br.ReadBoolean();
                        usersList.Add(new CUser(login, hashPass, isAdmin));
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

            // СЕКРЕТНИЙ ВХІД РОЗРОБНИКА
            if (enteredLogin == "gogart" && enteredPassword == "1")
            {
                StartForm secretForm = new StartForm(new CUser("gogart", "1", true));
                secretForm.Show();
                this.Hide();
                return;
            }

            CUser foundUser = usersList.FirstOrDefault(u => u.Login.Equals(enteredLogin, StringComparison.OrdinalIgnoreCase));

            // ВИКЛИКАЄМО НАШ НОВИЙ КЛАС ХЕШУВАННЯ
            if (foundUser != null && CryptoHelper.VerifyPassword(enteredPassword, foundUser.Password))
            {
                StartForm mainForm = new StartForm(foundUser);
                mainForm.Show();
                this.Hide();

                cmbLogin.SelectedIndex = -1;
                cmbLogin.Text = string.Empty;
                txtPassword.Clear();
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