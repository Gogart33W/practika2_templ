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

            this.Load += LoginForm_Load;
            this.btnLogin.Click += btnLogin_Click;
            this.btnShowPass.Click += btnShowPass_Click;
            this.btnHidePass.Click += btnHidePass_Click;
            this.btnClear.Click += btnClear_Click;
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
                        string password = br.ReadString();
                        bool isAdmin = br.ReadBoolean();
                        usersList.Add(new CUser(login, password, isAdmin));
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

        private void btnShowPass_Click(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '\0';
        }

        private void btnHidePass_Click(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbLogin.SelectedIndex = -1;
            cmbLogin.Text = string.Empty;
            txtPassword.Clear();
        }
    }
}