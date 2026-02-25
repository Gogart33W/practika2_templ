using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Navchpract_2
{
    public partial class IndZavd1 : Form
    {
        private List<RealEstate> _masterData = new List<RealEstate>();
        private BindingList<RealEstate> _displayData = new BindingList<RealEstate>();

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private string searchPlaceholder = "Введіть текст або <, >, = для чисел...";

        public IndZavd1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void IndZavd1_Load(object sender, EventArgs e)
        {
            pbBack.BackColor = Color.Transparent; pbExit.BackColor = Color.Transparent;
            pbBack.SizeMode = PictureBoxSizeMode.Zoom; pbExit.SizeMode = PictureBoxSizeMode.Zoom;

            dgvData.DataSource = _displayData;
            dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvData.MultiSelect = false;
            dgvData.ReadOnly = true;
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvData.RowHeadersVisible = false;
            dgvData.AllowUserToAddRows = false;
            dgvData.ContextMenuStrip = ctxGridMenu;

            if (dgvData.Columns["Id"] != null) dgvData.Columns["Id"].Visible = false;
            if (dgvData.Columns["Address"] != null) dgvData.Columns["Address"].HeaderText = "Адреса";
            if (dgvData.Columns["Type"] != null) dgvData.Columns["Type"].HeaderText = "Тип";
            if (dgvData.Columns["Area"] != null) dgvData.Columns["Area"].HeaderText = "Площа (м²)";
            if (dgvData.Columns["Rooms"] != null) dgvData.Columns["Rooms"].HeaderText = "Кімнати";
            if (dgvData.Columns["Price"] != null) dgvData.Columns["Price"].HeaderText = "Ціна ($)";
            if (dgvData.Columns["OwnerName"] != null) dgvData.Columns["OwnerName"].HeaderText = "Власник";

            // --- КРАСИВІ ЧИСЛА ТА ОБМЕЖЕННЯ ---
            numRooms.DecimalPlaces = 0; numRooms.Minimum = 1; numRooms.Maximum = 100;
            numArea.DecimalPlaces = 2; numArea.Minimum = 0.01m; numArea.Maximum = 10000;
            numPrice.DecimalPlaces = 2; numPrice.Minimum = 0.01m; numPrice.Maximum = 100000000;

            numArea.ThousandsSeparator = true;
            numPrice.ThousandsSeparator = true;

            cmbType.Items.Clear();
            cmbType.Items.AddRange(new string[] { "Квартира", "Будинок", "Офіс", "Земля", "Склад" });
            cmbType.SelectedIndex = 0;

            cmbFilterMode.Items.Clear();
            cmbFilterMode.Items.AddRange(new string[] { "Усі поля", "Адреса", "Тип", "Власник", "Кімнати", "Ціна", "Площа" });
            cmbFilterMode.SelectedIndex = 0;

            txtSearch.Text = searchPlaceholder;
            txtSearch.ForeColor = Color.Gray;
            lblSearch.Text = "Фільтр:";

            // --- ПІДКЛЮЧЕННЯ ПОДІЙ ---
            cmbType.SelectedIndexChanged += CmbType_SelectedIndexChanged;
            txtAddress.KeyPress += TxtAddress_KeyPress;
            txtAddress.Leave += txtAddress_Leave;

            txtOwner.KeyPress += txtOwner_KeyPress;
            txtOwner.Leave += TxtOwner_Leave;

            txtSearch.Enter += TxtSearch_Enter;
            txtSearch.Leave += TxtSearch_Leave;
            txtSearch.KeyPress += ReplaceDotWithComma_KeyPress;

            numArea.KeyPress += ReplaceDotWithComma_KeyPress;
            numPrice.KeyPress += ReplaceDotWithComma_KeyPress;

            // ФІКС "СТЕРТИХ" ПОЛІВ
            numRooms.Leave += Numeric_Leave;
            numArea.Leave += Numeric_Leave;
            numPrice.Leave += Numeric_Leave;
        }

        // --- ЛОГІКА ЗЕМЛІ ТА КІМНАТ ---
        private void CmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbType.Text == "Земля")
            {
                numRooms.Minimum = 0;
                numRooms.Value = 0;
                numRooms.Enabled = false;
            }
            else
            {
                numRooms.Enabled = true;
                numRooms.Minimum = 1;
                if (numRooms.Value == 0) numRooms.Value = 1;
            }
        }

        // --- ФІКС ЧИСЛОВИХ ПОЛІВ ---
        private void ReplaceDotWithComma_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Якщо людина тисне крапку - система автоматично міняє її на кому
            if (e.KeyChar == '.') e.KeyChar = ',';
        }

        private void Numeric_Leave(object sender, EventArgs e)
        {
            // Якщо людина просто стерла все бекспейсом і пішла на інше поле, 
            // ми не даємо йому зламатись, а ставимо мінімальне допустиме значення
            NumericUpDown num = sender as NumericUpDown;
            if (num != null && string.IsNullOrWhiteSpace(num.Text))
            {
                num.Value = num.Minimum;
                num.Text = num.Minimum.ToString();
            }
        }

        // --- ЗАХИСТ І ФОРМАТУВАННЯ ---
        private void TxtAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) &&
                e.KeyChar != ' ' && e.KeyChar != '.' && e.KeyChar != ',' && e.KeyChar != '-' && e.KeyChar != '/')
            {
                e.Handled = true;
            }
        }

        private void txtAddress_Leave(object sender, EventArgs e)
        {
            string text = txtAddress.Text.Trim();
            if (!string.IsNullOrWhiteSpace(text))
            {
                string lower = text.ToLower();
                if (!lower.Contains("вул") && !lower.Contains("пров") && !lower.Contains("просп") &&
                    !lower.Contains("бул") && !lower.Contains("площа") && !lower.Contains("ш."))
                {
                    text = "вул. " + text;
                }
                TextInfo textInfo = new CultureInfo("uk-UA", false).TextInfo;
                txtAddress.Text = textInfo.ToTitleCase(text.ToLower());
            }
        }

        private void txtOwner_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != '-' && e.KeyChar != '\'')
            {
                e.Handled = true;
            }
        }

        private void TxtOwner_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOwner.Text))
            {
                txtOwner.Text = "Немає власника";
            }
            else
            {
                TextInfo textInfo = new CultureInfo("uk-UA", false).TextInfo;
                txtOwner.Text = textInfo.ToTitleCase(txtOwner.Text.ToLower().Trim());
            }
        }

        // --- ЛОГІКА ПОШУКУ  ---
        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == searchPlaceholder)
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = searchPlaceholder;
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => ApplyFilter();

        private void cmbFilterMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != searchPlaceholder)
            {
                txtSearch.Clear();
            }
            ApplyFilter();
        }

        private bool EvaluateNumber(double dbValue, string searchStr)
        {
            searchStr = searchStr.Trim();
            if (string.IsNullOrWhiteSpace(searchStr)) return true;

            string op = "=";
            if (searchStr.StartsWith(">=")) { op = ">="; searchStr = searchStr.Substring(2); }
            else if (searchStr.StartsWith("<=")) { op = "<="; searchStr = searchStr.Substring(2); }
            else if (searchStr.StartsWith(">")) { op = ">"; searchStr = searchStr.Substring(1); }
            else if (searchStr.StartsWith("<")) { op = "<"; searchStr = searchStr.Substring(1); }
            else if (searchStr.StartsWith("=")) { op = "="; searchStr = searchStr.Substring(1); }

            searchStr = searchStr.Trim().Replace('.', ',');

            if (double.TryParse(searchStr, out double target))
            {
                switch (op)
                {
                    case ">=": return dbValue >= target;
                    case "<=": return dbValue <= target;
                    case ">": return dbValue > target;
                    case "<": return dbValue < target;
                    case "=": return Math.Abs(dbValue - target) < 0.01;
                }
            }
            return false;
        }

        private void ApplyFilter()
        {
            string rawQuery = txtSearch.Text.Trim();
            if (rawQuery == searchPlaceholder) rawQuery = "";

            string queryLower = rawQuery.ToLower();
            string mode = cmbFilterMode.SelectedItem?.ToString();

            var filtered = _masterData.Where(x =>
            {
                if (string.IsNullOrWhiteSpace(rawQuery)) return true;

                if (mode == "Кімнати") return EvaluateNumber(x.Rooms, rawQuery);
                if (mode == "Ціна") return EvaluateNumber((double)x.Price, rawQuery);
                if (mode == "Площа") return EvaluateNumber(x.Area, rawQuery);

                bool matchAddress = (x.Address?.ToLower() ?? "").Contains(queryLower);
                bool matchType = (x.Type?.ToLower() ?? "").Contains(queryLower);
                bool matchOwner = (x.OwnerName?.ToLower() ?? "").Contains(queryLower);

                if (mode == "Адреса") return matchAddress;
                if (mode == "Тип") return matchType;
                if (mode == "Власник") return matchOwner;

                bool matchRoomsExact = x.Rooms.ToString().Contains(queryLower);
                bool matchPriceExact = x.Price.ToString().Contains(queryLower);
                return matchAddress || matchType || matchOwner || matchRoomsExact || matchPriceExact;
            }).ToList();

            _displayData.Clear();
            foreach (var item in filtered) _displayData.Add(item);
        }

        private void RefreshData() => ApplyFilter();

        // --- ВЗАЄМОДІЯ З ТАБЛИЦЕЮ ---
        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0)
            {
                var selected = dgvData.SelectedRows[0].DataBoundItem as RealEstate;
                if (selected == null) return;

                txtAddress.Text = selected.Address ?? "";
                if (selected.Type != null && cmbType.Items.Contains(selected.Type)) cmbType.SelectedItem = selected.Type;

                numArea.Value = selected.Area >= (double)numArea.Minimum ? (decimal)selected.Area : numArea.Minimum;
                numRooms.Value = selected.Rooms >= numRooms.Minimum ? selected.Rooms : numRooms.Minimum;
                numPrice.Value = selected.Price >= numPrice.Minimum ? selected.Price : numPrice.Minimum;

                txtOwner.Text = selected.OwnerName ?? "";
            }
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("Дані перенесено в панель. Змініть їх і натисніть 'ЗМІНИТИ'.", "Редагування", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ctxMenuEdit_Click(object sender, EventArgs e) => dgvData_CellDoubleClick(null, null);
        private void ctxMenuDelete_Click(object sender, EventArgs e) => btnDelete_Click(null, null);

        // --- ВАЛІДАЦІЯ ---
        private bool ValidateInputs()
        {
            txtAddress.Text = txtAddress.Text.Trim();

            if (string.IsNullOrWhiteSpace(txtOwner.Text))
            {
                txtOwner.Text = "Немає власника";
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Поле 'Адреса' не може бути порожнім!", "Помилка вводу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAddress.Focus();
                return false;
            }
            return true;
        }

        // --- CRUD ЛОГІКА ---
        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnAdd.Focus();

            if (!ValidateInputs()) return;

            if (_masterData.Any(x => x.Address.Equals(txtAddress.Text, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Об'єкт з такою адресою вже існує! Використовуйте кнопку 'ЗМІНИТИ'.", "Дублікат", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newObj = new RealEstate { Address = txtAddress.Text, Type = cmbType.Text, Area = (double)numArea.Value, Rooms = (int)numRooms.Value, Price = numPrice.Value, OwnerName = txtOwner.Text };
            _masterData.Add(newObj);
            RefreshData();
            ClearInputs();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnEdit.Focus();

            if (dgvData.SelectedRows.Count == 0) return;
            var selected = dgvData.SelectedRows[0].DataBoundItem as RealEstate;
            if (selected == null || !ValidateInputs()) return;

            var masterItem = _masterData.FirstOrDefault(x => x.Id == selected.Id);
            if (masterItem != null)
            {
                masterItem.Address = txtAddress.Text;
                masterItem.Type = cmbType.Text;
                masterItem.Area = (double)numArea.Value;
                masterItem.Rooms = (int)numRooms.Value;
                masterItem.Price = numPrice.Value;
                masterItem.OwnerName = txtOwner.Text;
                RefreshData();
                MessageBox.Show("Запис успішно оновлено!", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0)
            {
                var selected = dgvData.SelectedRows[0].DataBoundItem as RealEstate;
                if (selected != null && MessageBox.Show($"Ви дійсно хочете видалити об'єкт за адресою:\n{selected.Address}?", "Увага", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    _masterData.RemoveAll(x => x.Id == selected.Id);
                    RefreshData();
                    ClearInputs();
                }
            }
        }

        // --- ФАЙЛИ ---
        private void btnSaveBin_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog { Filter = "BIN|*.bin" })
                if (sfd.ShowDialog() == DialogResult.OK)
                    using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                        new BinaryFormatter().Serialize(fs, _masterData);
        }

        private void btnLoadBin_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog { Filter = "BIN|*.bin" })
                if (ofd.ShowDialog() == DialogResult.OK)
                    try
                    {
                        using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open))
                        {
                            object loaded = new BinaryFormatter().Deserialize(fs);
                            if (loaded is BindingList<RealEstate> old) _masterData = old.ToList();
                            else if (loaded is List<RealEstate> nData) _masterData = nData;
                            RefreshData();
                        }
                    }
                    catch { MessageBox.Show("Помилка читання файлу!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnExportTxt_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog { Filter = "TXT|*.txt" })
                if (sfd.ShowDialog() == DialogResult.OK)
                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
                        foreach (var i in _masterData) sw.WriteLine(i.ToTxtString());
        }

        private void btnImportTxt_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog { Filter = "TXT|*.txt" })
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _masterData.Clear();
                    foreach (var l in File.ReadAllLines(ofd.FileName))
                    {
                        var o = RealEstate.FromTxtString(l);
                        if (o != null) _masterData.Add(o);
                    }
                    RefreshData();
                }
        }

        // --- ДІАЛОГИ ПРИ ВИХОДІ ---
        private void pbBack_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ви дійсно хочете повернутися до головного меню? Переконайтеся, що ви зберегли дані у файл.", "Повернення", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void pbExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ви дійсно хочете закрити програму повністю?", "Вихід", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void ClearInputs()
        {
            txtAddress.Clear();
            txtOwner.Clear();
            numArea.Value = numArea.Minimum;
            numRooms.Value = numRooms.Minimum;
            numPrice.Value = numPrice.Minimum;
            dgvData.ClearSelection();
        }

        // Перетягування вікна
        private void pnlHeader_MouseDown(object sender, MouseEventArgs e) { dragging = true; dragCursorPoint = Cursor.Position; dragFormPoint = this.Location; }
        private void pnlHeader_MouseMove(object sender, MouseEventArgs e) { if (dragging) { Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint)); this.Location = Point.Add(dragFormPoint, new Size(dif)); } }
        private void pnlHeader_MouseUp(object sender, MouseEventArgs e) { dragging = false; }
    }
}