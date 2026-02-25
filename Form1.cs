using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Navchpract_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox_Exit_Click(object sender, EventArgs e)
        {
            DialogResult rez = MessageBox.Show("Чи дійсно Ви бажаєте вийти з додатку?", "Запитання",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rez == DialogResult.Yes)
                Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult rez = MessageBox.Show("Чи дійсно Ви бажаєте повернутися на головне меню?", "Запитання",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rez == DialogResult.Yes)
            {
                this.Close(); 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult rez = MessageBox.Show("Виникла помилка!", "Помилка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.FullOpen = true;
            DialogResult dialogResult = colorDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                this.Title_panel.BackColor = colorDialog.Color;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.ShowColor = true;
            DialogResult rez = fontDialog.ShowDialog();
            if (rez == DialogResult.OK)
            {
                this.label1.Font = fontDialog.Font;
                this.label1.ForeColor = fontDialog.Color;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Відкрийте текстовий файл";
            openFileDialog.Filter = "Текстові файли (*.txt)|*.txt|Усі файли (*.*)|*.*";
            DialogResult openResult = openFileDialog.ShowDialog();
            if (openResult == DialogResult.OK)
            {
                textBox1.Clear();
                string path = openFileDialog.FileName;
                textBox1.Text = File.ReadAllText(path);
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                    throw new Exception("Текстобокс порожній");
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Збереження текстового файлу";
                saveFileDialog.Filter = "Текстові файли (*.txt)|*.txt|Усі файли (*.*)|*.*";
                saveFileDialog.InitialDirectory = @"D:\Навчальна практика №2\Navchpract#2";
                saveFileDialog.OverwritePrompt = true;
                DialogResult saveResult = saveFileDialog.ShowDialog();
                if (saveResult == DialogResult.OK)
                {
                    string path = saveFileDialog.FileName;
                    File.WriteAllText(path, textBox1.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.FullOpen = true;
            DialogResult dr = colorDialog.ShowDialog();
            if (dr == DialogResult.OK)
                textBox1.BackColor = colorDialog.Color;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.ShowColor = true;
            DialogResult dialogResult = fontDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                textBox1.Font = fontDialog.Font;
                textBox1.ForeColor = fontDialog.Color;
            }
        }
    }
}