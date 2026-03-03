using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Navchpract_2
{
    // Клас сесії для зберігання даних про того, хто увійшов
    public static class Session
    {
        public static string Role;
        public static string Username;
    }

    public partial class IndZavd2Form : Form
    {
        private DataTable travelTable;
        private string xmlFilePath;

        // Змінні для перетягування вікна без рамки
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private string searchPlaceholder = "Введіть текст або >=, <= для чисел...";

        // Конструктор, який викликає StartForm
        public IndZavd2Form(CUser currentUser)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            xmlFilePath = Path.Combine(Application.StartupPath, "TravelData.xml");

            Session.Username = currentUser.Login;
            Session.Role = "Admin";
        }

        // Конструктор для дизайнера (заглушка)
        public IndZavd2Form() : this(new CUser("Gogart_Admin", "1111", true)) { }

        private void IndZavd2Form_Load(object sender, EventArgs e)
        {
            pbBack.BackColor = Color.Transparent;
            pbExit.BackColor = Color.Transparent;
            pbBack.SizeMode = PictureBoxSizeMode.Zoom;
            pbExit.SizeMode = PictureBoxSizeMode.Zoom;

            lblTitle.Text = $"Travel Journal (Панель Адміністратора) | {Session.Username}";

            SetupDataTable();
            LoadUsersFromBinary(); // Підвантажуємо юзерів з input.bin
            SetupUI();
            SetupChart();
            UpdatePictureBox("Планується");
        }

        private void SetupDataTable()
        {
            travelTable = new DataTable("Travels");

            if (File.Exists(xmlFilePath))
            {
                travelTable.ReadXml(xmlFilePath);
            }
            else
            {
                // Якщо файлу немає, створюємо структуру з нуля
                travelTable.Columns.Add("ID", typeof(int)).AutoIncrement = true;
                travelTable.Columns.Add("Користувач", typeof(string));
                travelTable.Columns.Add("Країна", typeof(string));
                travelTable.Columns.Add("Місто", typeof(string));
                travelTable.Columns.Add("Бюджет", typeof(decimal));
                travelTable.Columns.Add("Статус", typeof(string));
                travelTable.Columns.Add("Оцінка", typeof(int));
                travelTable.Columns.Add("Коментар", typeof(string));
            }

            dgvData.DataSource = travelTable;
            if (dgvData.Columns.Contains("ID")) dgvData.Columns["ID"].Visible = false;

            dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvData.MultiSelect = false;
            dgvData.ReadOnly = true;
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvData.RowHeadersVisible = false;
            dgvData.AllowUserToAddRows = false;
            dgvData.BackgroundColor = Color.White;
            dgvData.BorderStyle = BorderStyle.None;
            dgvData.ContextMenuStrip = ctxGridMenu;

            dgvData.SelectionChanged += DgvData_SelectionChanged;
        }

        private void LoadUsersFromBinary()
        {
            cmbAssignedUser.Items.Clear();
            string binPath = Path.Combine(Application.StartupPath, "input.bin");

            if (File.Exists(binPath))
            {
                try
                {
                    using (BinaryReader br = new BinaryReader(File.Open(binPath, FileMode.Open)))
                    {
                        int count = br.ReadInt32();
                        for (int i = 0; i < count; i++)
                        {
                            string login = br.ReadString();
                            string hashPass = br.ReadString();
                            bool isAdmin = br.ReadBoolean();

                            // Адмін не може бути призначеним на подорож
                            if (!isAdmin) cmbAssignedUser.Items.Add(login);
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Помилка читання бази користувачів: " + ex.Message); }
            }

            if (cmbAssignedUser.Items.Count > 0) cmbAssignedUser.SelectedIndex = 0;
        }

        private void SetupUI()
        {
            numBudget.DecimalPlaces = 2;
            numBudget.Minimum = 0; numBudget.Maximum = 1000000;

            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new string[] { "Планується", "Завершено" });
            cmbStatus.SelectedIndex = 0;

            cmbFilterMode.Items.Clear();
            cmbFilterMode.Items.AddRange(new string[] { "Усі поля", "Країна", "Місто", "Користувач" });
            cmbFilterMode.SelectedIndex = 0;

            txtSearch.Text = searchPlaceholder;
            txtSearch.ForeColor = Color.Gray;

            // Події пошуку
            txtSearch.Enter += TxtSearch_Enter;
            txtSearch.Leave += TxtSearch_Leave;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            // Валідація вводу
            txtSearch.KeyPress += ReplaceDotWithComma_KeyPress;
            numBudget.KeyPress += ReplaceDotWithComma_KeyPress;
            numBudget.Leave += NumericUpDown_Leave;
            txtCountry.Leave += AutoCapitalize_Leave;
            txtCity.Leave += AutoCapitalize_Leave;

            btnOpenMap.Click += (s, ev) =>
            {
                using (MapForm map = new MapForm())
                {
                    if (map.ShowDialog() == DialogResult.OK)
                    {
                        txtCountry.Text = map.SelectedCountry;
                        txtCity.Focus();
                    }
                }
            };
        }

        // --- ВАЛІДАТОРИ ТА ХЕЛПЕРИ ---
        private void ReplaceDotWithComma_KeyPress(object sender, KeyPressEventArgs e) { if (e.KeyChar == '.') e.KeyChar = ','; }

        private void NumericUpDown_Leave(object sender, EventArgs e)
        {
            NumericUpDown num = sender as NumericUpDown;
            if (num != null && string.IsNullOrWhiteSpace(num.Text)) { num.Value = num.Minimum; num.Text = num.Minimum.ToString(); }
        }

        private void AutoCapitalize_Leave(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (!string.IsNullOrWhiteSpace(txt?.Text))
            {
                TextInfo textInfo = new CultureInfo("uk-UA", false).TextInfo;
                txt.Text = textInfo.ToTitleCase(txt.Text.ToLower().Trim());
            }
        }

        private void DgvData_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0)
            {
                var row = (DataRowView)dgvData.SelectedRows[0].DataBoundItem;
                txtCountry.Text = row["Країна"].ToString();
                txtCity.Text = row["Місто"].ToString();
                numBudget.Value = Convert.ToDecimal(row["Бюджет"]);
                cmbStatus.SelectedItem = row["Статус"].ToString();

                string user = row["Користувач"].ToString();
                if (cmbAssignedUser.Items.Contains(user)) cmbAssignedUser.SelectedItem = user;

                UpdatePictureBox(cmbStatus.SelectedItem.ToString());
                CalculateAverageCityRating(txtCountry.Text, txtCity.Text);
            }
        }

        private void CalculateAverageCityRating(string country, string city)
        {
            if (lblAverageRating == null) return;

            string filter = $"Країна = '{country.Replace("'", "''")}' AND Місто = '{city.Replace("'", "''")}' AND Статус = 'Завершено'";
            DataRow[] cityTrips = travelTable.Select(filter);

            if (cityTrips.Length > 0)
            {
                double sum = 0; int count = 0;
                foreach (DataRow r in cityTrips)
                {
                    if (r["Оцінка"] != DBNull.Value && Convert.ToInt32(r["Оцінка"]) > 0)
                    {
                        sum += Convert.ToDouble(r["Оцінка"]);
                        count++;
                    }
                }

                if (count > 0)
                {
                    lblAverageRating.Text = $"⭐ Рейтинг: {(sum / count):F1} / 10 (відгуків: {count})";
                    lblAverageRating.ForeColor = Color.DimGray;
                }
                else lblAverageRating.Text = "⭐ Оцінок ще немає";
            }
            else
            {
                lblAverageRating.Text = "⭐ Немає завершених поїздок";
                lblAverageRating.ForeColor = Color.Gray;
            }
        }

        // --- CRUD ОПЕРАЦІЇ ---
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCountry.Text) || string.IsNullOrWhiteSpace(txtCity.Text)) return;
            travelTable.Rows.Add(null, cmbAssignedUser.SelectedItem.ToString(), txtCountry.Text, txtCity.Text, numBudget.Value, cmbStatus.SelectedItem.ToString(), 0, "");
            SaveAndRefresh();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0)
            {
                var row = (DataRowView)dgvData.SelectedRows[0].DataBoundItem;
                row["Користувач"] = cmbAssignedUser.SelectedItem.ToString();
                row["Країна"] = txtCountry.Text;
                row["Місто"] = txtCity.Text;
                row["Бюджет"] = numBudget.Value;
                row["Статус"] = cmbStatus.SelectedItem.ToString();
                SaveAndRefresh();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Видалити цей запис?", "Підтвердження", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var row = (DataRowView)dgvData.SelectedRows[0].DataBoundItem;
                    row.Row.Delete();
                    SaveAndRefresh();
                }
            }
        }

        private void SaveAndRefresh()
        {
            travelTable.AcceptChanges();
            travelTable.WriteXml(xmlFilePath, XmlWriteMode.WriteSchema);
            chartTravel.DataBind();
        }

        // --- ПОШУК ---
        private void TxtSearch_Enter(object sender, EventArgs e) { if (txtSearch.Text == searchPlaceholder) { txtSearch.Text = ""; txtSearch.ForeColor = Color.Black; } }
        private void TxtSearch_Leave(object sender, EventArgs e) { if (string.IsNullOrWhiteSpace(txtSearch.Text)) { txtSearch.Text = searchPlaceholder; txtSearch.ForeColor = Color.Gray; } }
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string raw = txtSearch.Text.Trim().Replace("'", "''");
            if (raw == searchPlaceholder || string.IsNullOrWhiteSpace(raw)) { travelTable.DefaultView.RowFilter = ""; return; }

            string mode = cmbFilterMode.SelectedItem?.ToString();
            try
            {
                if (mode == "Усі поля") travelTable.DefaultView.RowFilter = $"(Країна LIKE '%{raw}%' OR Місто LIKE '%{raw}%' OR Користувач LIKE '%{raw}%')";
                else travelTable.DefaultView.RowFilter = $"{mode} LIKE '%{raw}%'";
            }
            catch { travelTable.DefaultView.RowFilter = ""; }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCountry.Clear(); txtCity.Clear(); numBudget.Value = 0;
            lblAverageRating.Text = "⭐ Рейтинг міста: ...";
        }

        private void btnExportXML_Click(object sender, EventArgs e) => SaveAndRefresh();
        private void btnImportXML_Click(object sender, EventArgs e) { if (File.Exists(xmlFilePath)) { travelTable.Clear(); travelTable.ReadXml(xmlFilePath); SaveAndRefresh(); } }

        private void SetupChart()
        {
            chartTravel.Series.Clear();
            Series s = new Series("Бюджет") { ChartType = SeriesChartType.Column, XValueMember = "Місто", YValueMembers = "Бюджет" };
            chartTravel.Series.Add(s);
            chartTravel.DataSource = travelTable;
        }

        private void UpdatePictureBox(string status)
        {
            Bitmap bmp = new Bitmap(pbPlane.Width, pbPlane.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.AliceBlue);
                using (Font f = new Font("Segoe UI Emoji", 48))
                    g.DrawString(status == "Завершено" ? "🛬" : "🛫", f, Brushes.Black, new PointF(10, 10));
            }
            pbPlane.Image = bmp;
        }

        // --- УПРАВЛІННЯ ФОРМОЮ ---
        private void pbBack_Click(object sender, EventArgs e) => this.Close();
        private void pbExit_Click(object sender, EventArgs e) => Application.Exit();
        private void pnlHeader_MouseDown(object sender, MouseEventArgs e) { dragging = true; dragCursorPoint = System.Windows.Forms.Cursor.Position; dragFormPoint = this.Location; }
        private void pnlHeader_MouseMove(object sender, MouseEventArgs e) { if (dragging) { Point dif = Point.Subtract(System.Windows.Forms.Cursor.Position, new Size(dragCursorPoint)); this.Location = Point.Add(dragFormPoint, new Size(dif)); } }
        private void pnlHeader_MouseUp(object sender, MouseEventArgs e) => dragging = false;

        private void ctxMenuEdit_Click(object sender, EventArgs e) => MessageBox.Show("Використовуйте панель зліва.");
        private void ctxMenuDelete_Click(object sender, EventArgs e) => btnDelete_Click(null, null);
    }
}