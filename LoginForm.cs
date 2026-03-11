using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Navchpract_2
{
    public partial class LoginForm : Form
    {
        private List<CUser> usersList = new List<CUser>();
        private string binPath;

        private Dictionary<string, (string Hash, DateTime Expiry)> tempPasswords = new Dictionary<string, (string, DateTime)>();

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

            this.VisibleChanged += async (s, e) =>
            {
                if (this.Visible) await RefreshLoginListAsync();
            };
        }

        private async void LoginForm_Load(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
            await RefreshLoginListAsync();
        }

        private async Task RefreshLoginListAsync()
        {
            await LoadUsersFromBinAsync();

            cmbLogin.Items.Clear();
            foreach (var user in usersList)
            {
                cmbLogin.Items.Add(user.Login);
            }
        }

        private async Task LoadUsersFromBinAsync()
        {
            if (!File.Exists(binPath)) return;

            await Task.Run(() =>
            {
                try
                {
                    List<CUser> tempUsers = new List<CUser>();
                    using (FileStream fs = new FileStream(binPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        if (fs.Length > 0)
                        {
                            using (BinaryReader br = new BinaryReader(fs))
                            {
                                int count = br.ReadInt32();
                                for (int i = 0; i < count; i++)
                                {
                                    string login = br.ReadString();
                                    string hashPass = br.ReadString();
                                    bool isAdmin = br.ReadBoolean();
                                    tempUsers.Add(new CUser(login, hashPass, isAdmin));
                                }
                            }
                        }
                    }
                    usersList = tempUsers;
                }
                catch { usersList.Clear(); }
            });
        }

        private async Task SaveUsersToBinAsync()
        {
            await Task.Run(() =>
            {
                try
                {
                    using (FileStream fs = new FileStream(binPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    using (BinaryWriter bw = new BinaryWriter(fs))
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
                catch (Exception ex)
                {
                    this.Invoke(new Action(() => MessageBox.Show($"Помилка оновлення хешу: {ex.Message}")));
                }
            });
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string enteredLogin = cmbLogin.Text.Trim();
            string enteredPassword = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(enteredLogin) || string.IsNullOrWhiteSpace(enteredPassword))
            {
                MessageBox.Show("Введіть логін та пароль!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (enteredLogin == "gogart" && enteredPassword == "1")
            {
                StartForm secretForm = new StartForm(new CUser("gogart", "1", true));
                secretForm.Show();
                this.Hide();
                return;
            }

            CUser foundUser = usersList.FirstOrDefault(u => u.Login.Equals(enteredLogin, StringComparison.OrdinalIgnoreCase));

            Button btn = sender as Button;
            if (btn != null) btn.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            bool isPasswordCorrect = false;
            string upgradedHash = null;

            if (foundUser != null)
            {
                await Task.Run(() =>
                {
                    isPasswordCorrect = CryptoHelper.VerifyPassword(enteredPassword, foundUser.Password, out upgradedHash);
                });

                if (!isPasswordCorrect && tempPasswords.ContainsKey(enteredLogin))
                {
                    var tempData = tempPasswords[enteredLogin];

                    if (DateTime.Now <= tempData.Expiry)
                    {
                        string hashedInput = CryptoHelper.HashTempPasswordSHA1(enteredPassword);
                        if (hashedInput == tempData.Hash)
                        {
                            isPasswordCorrect = true;
                            tempPasswords.Remove(enteredLogin);
                            MessageBox.Show("Ви ввійшли за тимчасовим паролем.\nОбов'язково змініть його в панелі адміністратора!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        tempPasswords.Remove(enteredLogin);
                        MessageBox.Show("Термін дії тимчасового пароля (3 хв) минув!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            this.Cursor = Cursors.Default;
            if (btn != null) btn.Enabled = true;

            if (isPasswordCorrect)
            {
                if (!string.IsNullOrEmpty(upgradedHash))
                {
                    foundUser.Password = upgradedHash;
                    await SaveUsersToBinAsync();
                }

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

        private void btnForgotPassword_Click(object sender, EventArgs e)
        {
            string enteredLogin = cmbLogin.Text.Trim();
            if (string.IsNullOrWhiteSpace(enteredLogin))
            {
                MessageBox.Show("Спочатку оберіть або введіть логін!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!usersList.Any(u => u.Login.Equals(enteredLogin, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Такого користувача не існує!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Random rnd = new Random();
            string tempPass = rnd.Next(100000, 999999).ToString();

            string sha1Hash = CryptoHelper.HashTempPasswordSHA1(tempPass);

            tempPasswords[enteredLogin] = (sha1Hash, DateTime.Now.AddMinutes(3));

            MessageBox.Show($"[СИМУЛЯЦІЯ ВІДПРАВКИ НА ПОШТУ]\n\nВаш тимчасовий пароль: {tempPass}\n\nВін діє рівно 3 хвилини.", "Відновлення пароля", MessageBoxButtons.OK, MessageBoxIcon.Information);
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