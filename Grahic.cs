using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Navchpract_2
{
    public partial class Grahic : Form
    {
        // ЦЕНТРУВАННЯ ЗА ПОЧАТКОМ КООРДИНАТ (0, 0)
        private float scale = 40f;
        private float offsetX = 0f;
        private float offsetY = 0f;

        private const double BREAK_X = 8.0;
        private const double MAX_Y = 1000.0;

        // Керування мишею для графіка
        private Point lastMousePos;
        private bool isDraggingGraph = false;

        // Керування формою (перетягування)
        private bool isFormDragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public Grahic()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // Подвійна буферизація від лагів
            this.DoubleBuffered = true;
            typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                           ?.SetValue(pictureBox2, true, null);

            // Події графіка
            pictureBox2.Paint += PictureBox2_Paint;
            pictureBox2.MouseWheel += PictureBox2_MouseWheel;
            pictureBox2.MouseDown += PictureBox2_MouseDown;
            pictureBox2.MouseMove += PictureBox2_MouseMove;
            pictureBox2.MouseUp += PictureBox2_MouseUp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox2.Invalidate();
            BuildChart();
        }

        // ==========================================
        // МАТЕМАТИКА (Варіант №11)
        // ==========================================
        private double CalculateFunction(double x)
        {
            if (x < BREAK_X)
            {
                double denom = -x * x + 10 * x - 16;
                // Захист від ділення на нуль поблизу асимптот
                if (Math.Abs(denom) < 1e-5) return double.NaN;
                return Math.Pow(Math.Sin(x - 5), 2) / denom;
            }
            else
            {
                if (x - 7 <= 0) return double.NaN;
                return Math.Log10(x - 7) + 2 * x + 5;
            }
        }

        // Координати
        private float MathToScreenX(float x, int w) => w / 2f + offsetX + x * scale;
        private float MathToScreenY(float y, int h) => h / 2f + offsetY - y * scale;
        private float ScreenToMathX(float sx, int w) => (sx - w / 2f - offsetX) / scale;
        private float ScreenToMathY(float sy, int h) => (h / 2f + offsetY - sy) / scale;

        // ==========================================
        // ВЛАСНИЙ РЕНДЕР (БЕЗ ЛАГІВ)
        // ==========================================
        private void PictureBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.WhiteSmoke);

            int w = pictureBox2.Width;
            int h = pictureBox2.Height;

            // 🔥 ВИПРАВЛЕНО: Читаємо межі графіка з текстових полів!
            float xSt = Parse(textBox1.Text, 6f);
            float xFi = Parse(textBox2.Text, 10f);

            float mathMinX = ScreenToMathX(0, w);
            float mathMaxX = ScreenToMathX(w, w);
            float mathMinY = ScreenToMathY(h, h);
            float mathMaxY = ScreenToMathY(0, h);

            float step = 1f;
            while (scale * step < 40) step *= 2f;
            while (scale * step > 100) step /= 2f;

            Pen gridPen = new Pen(Color.FromArgb(220, 220, 220), 1);
            Pen axisPen = new Pen(Color.FromArgb(50, 50, 50), 2);
            Pen graphPen = new Pen(Color.Crimson, 2.5f);
            Font font = new Font("Segoe UI", 8, FontStyle.Bold);
            Brush brush = new SolidBrush(Color.FromArgb(100, 100, 100));

            // Сітка X
            float startX = (float)Math.Floor(mathMinX / step) * step;
            for (float x = startX; x <= mathMaxX; x += step)
            {
                float sx = MathToScreenX(x, w);
                g.DrawLine(gridPen, sx, 0, sx, h);
                if (Math.Abs(x) > 0.001) g.DrawString(Math.Round(x, 2).ToString(), font, brush, sx + 2, MathToScreenY(0, h) + 2);
            }

            // Сітка Y
            float startY = (float)Math.Floor(mathMinY / step) * step;
            for (float y = startY; y <= mathMaxY; y += step)
            {
                float sy = MathToScreenY(y, h);
                g.DrawLine(gridPen, 0, sy, w, sy);
                if (Math.Abs(y) > 0.001) g.DrawString(Math.Round(y, 2).ToString(), font, brush, MathToScreenX(0, w) + 2, sy - 15);
            }

            // Осі
            float axisX = MathToScreenX(0, w);
            float axisY = MathToScreenY(0, h);
            g.DrawLine(axisPen, axisX, 0, axisX, h);
            g.DrawLine(axisPen, 0, axisY, w, axisY);

            // ГРАФІК ПО ПІКСЕЛЯХ
            PointF? prevPoint = null;
            float? prevMathX = null;

            for (int sx = 0; sx <= w; sx++)
            {
                float mathX = ScreenToMathX(sx, w);

                // 🔥 ВИПРАВЛЕНО: Якщо точка виходить за межі [Початок, Кінець], ми її НЕ малюємо
                if (mathX < xSt || mathX > xFi)
                {
                    prevPoint = null;
                    prevMathX = mathX;
                    continue;
                }

                double mathY = CalculateFunction(mathX);

                if (prevMathX.HasValue)
                {
                    // Асимптота знаменника x = 2.0 та розрив функції x = 8.0
                    if (prevMathX.Value < 2.0f && mathX >= 2.0f) prevPoint = null;
                    if (prevMathX.Value < BREAK_X && mathX >= BREAK_X) prevPoint = null;
                }

                if (double.IsNaN(mathY) || Math.Abs(mathY) > MAX_Y)
                {
                    prevPoint = null;
                    prevMathX = mathX;
                    continue;
                }

                float sy = MathToScreenY((float)mathY, h);
                PointF currentPoint = new PointF(sx, sy);

                if (prevPoint != null)
                {
                    // Захист від малювання вертикальних "спайків"
                    if (Math.Abs(sy - prevPoint.Value.Y) < h * 2)
                    {
                        g.DrawLine(graphPen, prevPoint.Value, currentPoint);
                    }
                }

                prevPoint = currentPoint;
                prevMathX = mathX;
            }
        }

        // ==========================================
        // ЗУМ І ПЕРЕТЯГУВАННЯ ГРАФІКА
        // ==========================================
        private void PictureBox2_MouseWheel(object sender, MouseEventArgs e)
        {
            float oldMathX = ScreenToMathX(e.X, pictureBox2.Width);
            float oldMathY = ScreenToMathY(e.Y, pictureBox2.Height);

            if (e.Delta > 0) scale *= 1.15f;
            else scale /= 1.15f;

            offsetX = e.X - pictureBox2.Width / 2f - oldMathX * scale;
            offsetY = e.Y - pictureBox2.Height / 2f + oldMathY * scale;

            pictureBox2.Invalidate();
        }

        private void PictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDraggingGraph = true;
                lastMousePos = e.Location;
                pictureBox2.Cursor = Cursors.SizeAll;
            }
        }

        private void PictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDraggingGraph)
            {
                offsetX += e.X - lastMousePos.X;
                offsetY += e.Y - lastMousePos.Y;
                lastMousePos = e.Location;
                pictureBox2.Invalidate();
            }
        }

        private void PictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            isDraggingGraph = false;
            pictureBox2.Cursor = Cursors.Cross;
        }

        private void button_build_Click(object sender, EventArgs e)
        {
            pictureBox2.Invalidate();
        }

        // ==========================================
        // ТАБУЛЮВАННЯ
        // ==========================================
        private void button_protab_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            float xSt = Parse(textBox1.Text, 6f);
            float xFi = Parse(textBox2.Text, 10f);
            float step = Parse(textBox3.Text, 0.12f);

            if (step <= 0) step = 0.12f;

            dataGridView1.SuspendLayout();
            for (double x = xSt; x <= xFi + 1e-6; x += step)
            {
                double y = CalculateFunction(x);

                if (double.IsNaN(y))
                    dataGridView1.Rows.Add(Math.Round(x, 4), "Не визначено");
                else
                    dataGridView1.Rows.Add(Math.Round(x, 4), Math.Round(y, 4));
            }
            dataGridView1.ResumeLayout();

            // 🔥 Оновлюємо малюнок при перерахунку
            pictureBox2.Invalidate();
        }

        // ==========================================
        // MS CHART
        // ==========================================
        private void button1_Click(object sender, EventArgs e)
        {
            BuildChart();
        }

        private void BuildChart()
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            ChartArea area = new ChartArea("Main");
            chart1.ChartAreas.Add(area);

            area.CursorX.IsUserEnabled = true;
            area.CursorX.IsUserSelectionEnabled = true;
            area.AxisX.ScaleView.Zoomable = true;
            area.AxisY.ScaleView.Zoomable = true;

            Series s = new Series("f(x)");
            s.ChartType = SeriesChartType.Line;
            s.BorderWidth = 3;
            s.Color = Color.DarkOrchid;

            float xSt = Parse(textBox1.Text, 6f);
            float xFi = Parse(textBox2.Text, 10f);
            float step = Parse(textBox3.Text, 0.12f);

            if (step <= 0) step = 0.12f;

            double prevY = double.NaN;

            for (double x = xSt; x <= xFi + 1e-6; x += step)
            {
                double y = CalculateFunction(x);

                if (double.IsNaN(y) || Math.Abs(y) > MAX_Y)
                {
                    var emptyPoint = new DataPoint(x, 0) { IsEmpty = true };
                    s.Points.Add(emptyPoint);
                }
                else
                {
                    if (!double.IsNaN(prevY) && Math.Abs(y - prevY) > 10)
                    {
                        var emptyPoint = new DataPoint(x, 0) { IsEmpty = true };
                        s.Points.Add(emptyPoint);
                    }
                    s.Points.AddXY(x, Math.Round(y, 4));
                }
                prevY = y;
            }

            chart1.Series.Add(s);

            area.AxisX.LabelStyle.Format = "0.##";
            area.AxisY.LabelStyle.Format = "0.##";
            area.AxisX.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisX.Crossing = 0;
            area.AxisY.Crossing = 0;
            area.AxisX.LineWidth = 2;
            area.AxisY.LineWidth = 2;
        }

        // ==========================================
        // КЕРУВАННЯ ВІКНОМ
        // ==========================================
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int HTBOTTOMRIGHT = 17;
            const int RESIZE_HANDLE_SIZE = 15;

            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST)
            {
                Point screenPoint = new Point(m.LParam.ToInt32());
                Point clientPoint = this.PointToClient(screenPoint);

                if (clientPoint.X >= this.ClientSize.Width - RESIZE_HANDLE_SIZE &&
                    clientPoint.Y >= this.ClientSize.Height - RESIZE_HANDLE_SIZE)
                {
                    m.Result = (IntPtr)HTBOTTOMRIGHT;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor,
                new Rectangle(this.ClientSize.Width - 16, this.ClientSize.Height - 16, 16, 16));
        }

        private void pbBack_Click(object sender, EventArgs e) => this.Close();
        private void pbExit_Click(object sender, EventArgs e) => Application.Exit();

        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            isFormDragging = true;
            dragCursorPoint = System.Windows.Forms.Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void pnlHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (isFormDragging)
            {
                Point dif = Point.Subtract(System.Windows.Forms.Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void pnlHeader_MouseUp(object sender, MouseEventArgs e) => isFormDragging = false;

        // ================================
        // ПАРСИНГ
        // ================================
        private float Parse(string input, float def)
        {
            if (float.TryParse(input.Replace(",", "."),
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out float result))
                return result;

            return def;
        }
    }
}