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
    public static class Session
    {
        public static string Role;
        public static string Username;
    }

    public partial class IndZavd2Form : Form
    {
        private DataTable travelTable;
        private string xmlFilePath;

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private string searchPlaceholder = "Введіть текст...";

        private bool hasUnsavedChanges = false;

        public IndZavd2Form(CUser currentUser)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            xmlFilePath = Path.Combine(Application.StartupPath, "TravelData.xml");

            Session.Username = currentUser.Login;
            Session.Role = "Admin";

            chkOnlyRequests.CheckedChanged += (s, e) => ApplyFilters();
        }

        public IndZavd2Form() : this(new CUser("Gogart_Admin", "1111", true)) { }

        private void IndZavd2Form_Load(object sender, EventArgs e)
        {
            pbBack.BackColor = Color.Transparent;
            pbExit.BackColor = Color.Transparent;
            pbBack.SizeMode = PictureBoxSizeMode.Zoom;
            pbExit.SizeMode = PictureBoxSizeMode.Zoom;

            lblTitle.Text = $"Travel Journal (Панель Адміністратора) | {Session.Username}";

            SetupDataTable();
            LoadUsersFromBinary();
            SetupUI();
            SetupChart();
            UpdatePictureBox("Планується");

            int pendingRequests = travelTable.Select("Статус = 'Запит'").Length;
            int pendingReviews = travelTable.Select("Статус = 'Очікує перевірки'").Length;

            if (pendingRequests > 0 || pendingReviews > 0)
            {
                string msg = "У вас є нові сповіщення:\n\n";
                if (pendingRequests > 0) msg += $"📩 Нових запитів на тури: {pendingRequests}\n";
                if (pendingReviews > 0) msg += $"📝 Звітів очікують затвердження: {pendingReviews}\n";
                MessageBox.Show(msg, "Увага, Адміністраторе!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
            dgvData.DataBindingComplete += DgvData_DataBindingComplete;
        }

        private void DgvData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                string status = row.Cells["Статус"].Value?.ToString();
                if (status == "Запит") row.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                else if (status == "Очікує перевірки") row.DefaultCellStyle.BackColor = Color.LightCyan;
                else row.DefaultCellStyle.BackColor = Color.White;
            }
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
                            br.ReadString();
                            bool isAdmin = br.ReadBoolean();
                            if (!isAdmin) cmbAssignedUser.Items.Add(login);
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Помилка читання бази: " + ex.Message); }
            }
            if (cmbAssignedUser.Items.Count > 0) cmbAssignedUser.SelectedIndex = 0;
        }

        private void SetupUI()
        {
            numBudget.DecimalPlaces = 2;
            numBudget.Minimum = 0; numBudget.Maximum = 1000000;

            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new string[] { "Запит", "Планується", "Очікує перевірки", "Завершено" });
            cmbStatus.SelectedIndex = 0;

            cmbFilterMode.Items.Clear();
            cmbFilterMode.Items.AddRange(new string[] { "Усі поля", "Країна", "Місто", "Користувач" });
            cmbFilterMode.SelectedIndex = 0;

            txtSearch.Text = searchPlaceholder;
            txtSearch.ForeColor = Color.Gray;

            txtSearch.Enter += TxtSearch_Enter;
            txtSearch.Leave += TxtSearch_Leave;
            txtSearch.TextChanged += TxtSearch_TextChanged;

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

        private bool PromptUnsavedChanges()
        {
            if (hasUnsavedChanges)
            {
                DialogResult res = MessageBox.Show("У вас є незбережені зміни! Хочете зберегти їх перед виходом?", "Увага, дані можуть бути втрачені", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {
                    SaveToXML();
                    return true;
                }
                else if (res == DialogResult.No)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private void btnOpenFeed_Click(object sender, EventArgs e)
        {
            if (hasUnsavedChanges)
            {
                DialogResult res = MessageBox.Show("Зберегти поточні зміни перед переходом до стрічки?", "Збереження", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Yes) { SaveToXML(); }
                else if (res == DialogResult.Cancel) { return; }
            }

            UserFeedForm feedForm = new UserFeedForm(new CUser(Session.Username, "", true));
            this.Hide();
            feedForm.ShowDialog();
            this.Show();

            travelTable.Clear();
            if (File.Exists(xmlFilePath)) travelTable.ReadXml(xmlFilePath);
            dgvData.DataSource = travelTable;
            chartTravel.DataBind();
            hasUnsavedChanges = false;
        }

        private void ReplaceDotWithComma_KeyPress(object sender, KeyPressEventArgs e) { if (e.KeyChar == '.') e.KeyChar = ','; }

        private void NumericUpDown_Leave(object sender, EventArgs e)
        {
            NumericUpDown num = sender as NumericUpDown;
            if (num != null && string.IsNullOrWhiteSpace(num.Text))
            {
                num.Value = num.Minimum;
                num.Text = num.Minimum.ToString();
            }
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
                foreach (DataRow r in cityTrips) { if (r["Оцінка"] != DBNull.Value && Convert.ToInt32(r["Оцінка"]) > 0) { sum += Convert.ToDouble(r["Оцінка"]); count++; } }
                if (count > 0)
                {
                    lblAverageRating.Text = $"⭐ Рейтинг: {(sum / count):F1} / 10\n(відгуків: {count})";
                    lblAverageRating.ForeColor = Color.DimGray;
                }
                else lblAverageRating.Text = "⭐ Оцінок ще\nнемає";
            }
            else { lblAverageRating.Text = "⭐ Немає завершених\nпоїздок"; lblAverageRating.ForeColor = Color.Gray; }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCountry.Text) || string.IsNullOrWhiteSpace(txtCity.Text)) return;
            travelTable.Rows.Add(null, cmbAssignedUser.SelectedItem.ToString(), txtCountry.Text, txtCity.Text, numBudget.Value, cmbStatus.SelectedItem.ToString(), 1, "");
            RegisterChange();
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
                RegisterChange();
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
                    RegisterChange();
                }
            }
        }

        private void RegisterChange()
        {
            hasUnsavedChanges = true;
            travelTable.AcceptChanges();
            chartTravel.DataBind();
        }

        private void SaveToXML()
        {
            travelTable.AcceptChanges();
            travelTable.WriteXml(xmlFilePath, XmlWriteMode.WriteSchema);
            hasUnsavedChanges = false;
        }

        private void TxtSearch_Enter(object sender, EventArgs e) { if (txtSearch.Text == searchPlaceholder) { txtSearch.Text = ""; txtSearch.ForeColor = Color.Black; } }
        private void TxtSearch_Leave(object sender, EventArgs e) { if (string.IsNullOrWhiteSpace(txtSearch.Text)) { txtSearch.Text = searchPlaceholder; txtSearch.ForeColor = Color.Gray; } }
        private void TxtSearch_TextChanged(object sender, EventArgs e) => ApplyFilters();

        private void ApplyFilters()
        {
            string raw = txtSearch.Text.Trim().Replace("'", "''");
            string searchFilter = "";

            if (raw != searchPlaceholder && !string.IsNullOrWhiteSpace(raw))
            {
                string mode = cmbFilterMode.SelectedItem?.ToString();
                if (mode == "Усі поля") searchFilter = $"(Країна LIKE '%{raw}%' OR Місто LIKE '%{raw}%' OR Користувач LIKE '%{raw}%')";
                else searchFilter = $"{mode} LIKE '%{raw}%'";
            }

            string statusFilter = chkOnlyRequests.Checked ? "(Статус = 'Запит' OR Статус = 'Очікує перевірки')" : "";

            if (!string.IsNullOrEmpty(searchFilter) && !string.IsNullOrEmpty(statusFilter)) travelTable.DefaultView.RowFilter = $"{searchFilter} AND {statusFilter}";
            else if (!string.IsNullOrEmpty(searchFilter)) travelTable.DefaultView.RowFilter = searchFilter;
            else if (!string.IsNullOrEmpty(statusFilter)) travelTable.DefaultView.RowFilter = statusFilter;
            else travelTable.DefaultView.RowFilter = "";
        }

        private void btnClear_Click(object sender, EventArgs e) { txtCountry.Clear(); txtCity.Clear(); numBudget.Value = 0; lblAverageRating.Text = "⭐ Рейтинг міста: ..."; }

        private void btnExportXML_Click(object sender, EventArgs e)
        {
            SaveToXML();
            MessageBox.Show("Всі зміни успішно збережені в XML!", "Збережено", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnImportXML_Click(object sender, EventArgs e)
        {
            if (File.Exists(xmlFilePath)) { travelTable.Clear(); travelTable.ReadXml(xmlFilePath); SaveToXML(); chartTravel.DataBind(); }
        }

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
                    g.DrawString(status == "Завершено" ? "🛬" : (status == "Запит" || status == "Очікує перевірки" ? "📩" : "🛫"), f, Brushes.Black, new PointF(10, 10));
            }
            pbPlane.Image = bmp;
        }

        private void pbBack_Click(object sender, EventArgs e) { if (PromptUnsavedChanges()) this.Close(); }
        private void pbExit_Click(object sender, EventArgs e) { if (PromptUnsavedChanges()) Application.Exit(); }

        private void pnlHeader_MouseDown(object sender, MouseEventArgs e) { dragging = true; dragCursorPoint = System.Windows.Forms.Cursor.Position; dragFormPoint = this.Location; }
        private void pnlHeader_MouseMove(object sender, MouseEventArgs e) { if (dragging) { Point dif = Point.Subtract(System.Windows.Forms.Cursor.Position, new Size(dragCursorPoint)); this.Location = Point.Add(dragFormPoint, new Size(dif)); } }
        private void pnlHeader_MouseUp(object sender, MouseEventArgs e) => dragging = false;
        private void ctxMenuEdit_Click(object sender, EventArgs e) => MessageBox.Show("Використовуйте панель зліва.");
        private void ctxMenuDelete_Click(object sender, EventArgs e) => btnDelete_Click(null, null);
    }
}