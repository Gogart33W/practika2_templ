using System;
using System.IO;
using System.Windows.Forms;

namespace Navchpract_2
{
    public partial class zavd1 : Form
    {
        public zavd1()
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
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.FullOpen = true;
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    this.Title_panel.BackColor = colorDialog.Color;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (FontDialog fontDialog = new FontDialog())
            {
                fontDialog.ShowColor = true;
                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    this.label1.Font = fontDialog.Font;
                    this.label1.ForeColor = fontDialog.Color;
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Відкрийте текстовий файл";
                openFileDialog.Filter = "Текстові файли (*.txt)|*.txt|Усі файли (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        textBox1.Clear();
                        string path = openFileDialog.FileName;
                        textBox1.Text = File.ReadAllText(path);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка читання файлу: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Текстобокс порожній. Немає що зберігати.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Title = "Збереження текстового файлу";
                    saveFileDialog.Filter = "Текстові файли (*.txt)|*.txt|Усі файли (*.*)|*.*";
                    saveFileDialog.InitialDirectory = Application.StartupPath;
                    saveFileDialog.OverwritePrompt = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string path = saveFileDialog.FileName;
                        File.WriteAllText(path, textBox1.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка збереження файлу: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.FullOpen = true;
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox1.BackColor = colorDialog.Color;
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            using (FontDialog fontDialog = new FontDialog())
            {
                fontDialog.ShowColor = true;
                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Font = fontDialog.Font;
                    textBox1.ForeColor = fontDialog.Color;
                }
            }
        }
    }
}