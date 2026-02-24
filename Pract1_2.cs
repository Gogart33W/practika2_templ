using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Navchpract_2
{
    public partial class Pract1_2 : Form
    {
        public Pract1_2()
        {
            InitializeComponent();
            textBoxNumeric.TextChanged += (s, e) => textBoxNumeric.BackColor = Color.White;
            textBoxText.TextChanged += (s, e) => textBoxText.BackColor = Color.White;
        }

        private void pictureBox_Exit_Click(object sender, EventArgs e)
        {
            DialogResult rez = MessageBox.Show("Чи дійсно Ви бажаєте вийти з додатку?", "Запитання",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rez == DialogResult.Yes) Application.Exit();
        }

        private void button_ToStartForm_Click(object sender, EventArgs e)
        {
            DialogResult rez = MessageBox.Show("Чи дійсно Ви бажаєте перейти на стартову форму?", "Запитання",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rez == DialogResult.Yes)
            {
                this.TopMost = true;
                StartForm sf = new StartForm();
                this.Hide();
                sf.ShowDialog();
            }
        }

        private bool ValidateFields()
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(textBoxNumeric.Text))
            {
                textBoxNumeric.BackColor = Color.MistyRose;
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(textBoxText.Text))
            {
                textBoxText.BackColor = Color.MistyRose;
                isValid = false;
            }

            if (!isValid)
                MessageBox.Show("Заповніть підсвічені поля!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return isValid;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;

            DataValue.Numeric = textBoxNumeric.Text;
            DataValue.Text = textBoxText.Text;

            NoModalForm noModal = new NoModalForm(textBoxNumeric.Text, textBoxText.Text);
            noModal.textBoxNumericM1.Text = textBoxNumeric.Text;
            noModal.textBoxTextM1.Text = textBoxText.Text;
            noModal.MyStr_Numeric = textBoxNumeric.Text;
            noModal.MyStr_Text = textBoxText.Text;

            noModal.Show();
        }

        private void buttonToModal_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;

            DataValue.Numeric = textBoxNumeric.Text;
            DataValue.Text = textBoxText.Text;

            this.Hide();
            ModalForm modal = new ModalForm(textBoxNumeric.Text, textBoxText.Text);
            modal.textBoxNumericM1.Text = textBoxNumeric.Text;
            modal.textBoxTextM1.Text = textBoxText.Text;
            modal.MyStr_Numeric = textBoxNumeric.Text;
            modal.MyStr_Text = textBoxText.Text;

            modal.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            textBoxNumeric.Clear();
            textBoxText.Clear();
            textBoxNumeric.BackColor = Color.White;
            textBoxText.BackColor = Color.White;
        }

        private void textBoxNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == '\b' || char.IsDigit(e.KeyChar)) return;
            if (e.KeyChar == ',' && !((TextBox)sender).Text.Contains(',')) return;
            e.Handled = true;
        }

        private void textBoxText_KeyPress(object sender, KeyPressEventArgs e) { }
    }

    public static class DataValue
    {
        public static string Numeric;
        public static string Text;
    }
}