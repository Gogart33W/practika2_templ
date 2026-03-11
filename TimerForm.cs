using System;
using System.Drawing;
using System.Windows.Forms;

namespace Navchpract_2
{
    public partial class TimerForm : Form
    {
        private int timeleft = 30;
        private bool isFormDragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public TimerForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            UpdateTimerDisplay();
        }

        private void TimerForm_Load(object sender, EventArgs e)
        {
            pbBack.BackColor = Color.Transparent;
            pbExit.BackColor = Color.Transparent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (timeleft > 0 && !timer1.Enabled)
            {
                timer1.Start();
                button1.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            button1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timeleft = 30;
            UpdateTimerDisplay();
            button1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timeleft > 0)
            {
                timeleft--;
                UpdateTimerDisplay();
            }
            else
            {
                timer1.Stop();
                button1.Enabled = true;
                MessageBox.Show("Час вийшов!", "Таймер", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UpdateTimerDisplay()
        {
            TimeSpan time = TimeSpan.FromSeconds(timeleft);
            label1.Text = time.ToString(@"mm\:ss");

            if (timeleft <= 10 && timeleft > 0)
            {
                label1.ForeColor = Color.Crimson;
            }
            else if (timeleft == 0)
            {
                label1.ForeColor = Color.DarkGray;
            }
            else
            {
                label1.ForeColor = Color.Black;
            }
        }

        private void pbBack_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Close();
        }

        private void pbExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            isFormDragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void pnlHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (isFormDragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void pnlHeader_MouseUp(object sender, MouseEventArgs e)
        {
            isFormDragging = false;
        }
    }
}