using System;
using System.Windows.Forms;

namespace Navchpract_2
{
    public partial class NoModalForm : Form
    {
        public NoModalForm()
        {
            InitializeComponent();
        }

        public NoModalForm(string num, string txt)
        {
            InitializeComponent();
            textBoxNumericM4.Text = num;
            textBoxTextM4.Text = txt;
            FillGlobal();
        }

        private void FillGlobal()
        {
            textBoxNumericM3.Text = DataValue.Numeric;
            textBoxTextM3.Text = DataValue.Text;
        }

        public string MyStr_Numeric
        {
            get { return textBoxNumericM2.Text; }
            set { textBoxNumericM2.Text = value; }
        }

        public string MyStr_Text
        {
            get { return textBoxTextM2.Text; }
            set { textBoxTextM2.Text = value; }
        }

        private void NoModalForm_Load(object sender, EventArgs e)
        {
            FillGlobal();
        }

        private void pictureBox_Exit_Click(object sender, EventArgs e)
        {
            DialogResult rez = MessageBox.Show("Закрити немодальне вікно?", "Запитання",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rez == DialogResult.Yes) this.Close();
        }
    }
}