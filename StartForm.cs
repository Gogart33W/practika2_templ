using System;
using System.Windows.Forms;

namespace Navchpract_2
{
    public partial class StartForm : Form
    {
        private CUser currentUser;
        private bool isLoggingOut = false;

        public StartForm(CUser user)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            currentUser = user;
            this.Load += StartForm_Load;
            this.FormClosed += StartForm_FormClosed;
        }

        public StartForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            currentUser = new CUser("Адмін (Авто)", "1111", true);
            this.Load += StartForm_Load;
            this.FormClosed += StartForm_FormClosed;
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            string role = currentUser.IsAdmin ? "Адміністратор" : "Юзер";

            if (this.Controls.ContainsKey("lblUserInfo"))
            {
                this.Controls["lblUserInfo"].Text = $"Користувач: {currentUser.Login} ({role})";
                // Центруємо напис
                this.Controls["lblUserInfo"].Left = (this.ClientSize.Width - this.Controls["lblUserInfo"].Width) / 2;
            }
        }

        private void StartForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isLoggingOut)
            {
                Application.Exit();
            }
        }

        private void OpenModule<T>() where T : Form, new()
        {
            this.Hide();

            using (T module = new T())
            {
                module.StartPosition = FormStartPosition.CenterScreen;
                module.ShowDialog();
            }

            this.Show();
        }

        private void практична11ToolStripMenuItem_Click(object sender, EventArgs e) { OpenModule<Form1>(); }
        private void практична12ToolStripMenuItem_Click(object sender, EventArgs e) { OpenModule<Pract1_2>(); }
        private void практична21ToolStripMenuItem_Click(object sender, EventArgs e) { OpenModule<WordDataGrid>(); }
        private void indZavd1ToolStripMenuItem_Click(object sender, EventArgs e) { OpenModule<IndZavd1>(); }
        private void другийТижденьToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void практична31ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!currentUser.IsAdmin)
            {
                MessageBox.Show("Вибачте, але це тільки для адмінів!", "Доступ заборонено", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.Hide();

            using (AdminForm adminForm = new AdminForm(currentUser))
            {
                adminForm.StartPosition = FormStartPosition.CenterScreen;
                adminForm.ShowDialog();
            }

            this.Show();
        }

        private void третійТижденьToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            isLoggingOut = true;

            foreach (Form form in Application.OpenForms)
            {
                if (form is LoginForm)
                {
                    form.Show();
                    this.Close();
                    return;
                }
            }

            LoginForm login = new LoginForm();
            login.Show();
            this.Close();
        }
    }
}