using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Navchpract_2
{
    public partial class UserFeedForm : Form
    {
        private DataTable travelTable;
        private string xmlFilePath;

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public UserFeedForm(CUser currentUser)
        {
            InitializeComponent();
            Session.Username = currentUser.Login;
            Session.Role = currentUser.IsAdmin ? "Admin" : "User";
            xmlFilePath = Path.Combine(Application.StartupPath, "TravelData.xml");
        }

        public UserFeedForm() : this(new CUser("Gogart_Test", "0000", false)) { }

        private void UserFeedForm_Load(object sender, EventArgs e)
        {
            string roleLabel = Session.Role == "Admin" ? "[МОДЕРАТОР]" : "";
            lblTitle.Text = $"Travel Hub | {Session.Username} {roleLabel}";

            // 🔥 ВИРІЗАЄМО ВКЛАДКУ "МОЇ ПОДОРОЖІ" ДЛЯ АДМІНА З КІНЦЯМИ
            if (Session.Role == "Admin")
            {
                tabControlFeed.TabPages.Remove(tabMyTrips);
            }

            LoadData();
            RenderFeeds();
        }

        private void LoadData()
        {
            travelTable = new DataTable("Travels");
            if (File.Exists(xmlFilePath)) travelTable.ReadXml(xmlFilePath);
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
        }

        private void RenderFeeds()
        {
            if (Session.Role != "Admin")
            {
                flowMyTrips.Controls.Clear();
                DataRow[] myTrips = travelTable.Select($"Користувач = '{Session.Username}'");
                if (myTrips.Length == 0) flowMyTrips.Controls.Add(CreateEmptyLabel("У тебе поки немає призначених подорожей 😢"));
                else foreach (DataRow row in myTrips) flowMyTrips.Controls.Add(CreateMyTripCard(row));
            }

            flowGlobalFeed.Controls.Clear();
            DataRow[] globalTrips = travelTable.Select("Статус = 'Завершено' OR Статус = 'Очікує перевірки'");

            if (globalTrips.Length == 0) flowGlobalFeed.Controls.Add(CreateEmptyLabel("Стрічка поки порожня. Будь першим, хто завершить подорож!"));
            else foreach (DataRow row in globalTrips) flowGlobalFeed.Controls.Add(CreateGlobalFeedCard(row));
        }

        private Label CreateEmptyLabel(string text)
        {
            return new Label { Text = text, Font = new Font("Segoe UI", 14, FontStyle.Italic), ForeColor = Color.Gray, AutoSize = true, Margin = new Padding(50) };
        }

        private Panel CreateMyTripCard(DataRow row)
        {
            int cardWidth = flowMyTrips.ClientSize.Width - 40;
            Panel pnlCard = new Panel { BackColor = Color.White, Width = cardWidth, Height = 280, Margin = new Padding(10, 10, 10, 20), BorderStyle = BorderStyle.FixedSingle };

            Label lblDestination = new Label { Text = $"📍 {row["Країна"]} — {row["Місто"]}", Font = new Font("Segoe UI", 14, FontStyle.Bold), Location = new Point(15, 15), AutoSize = true };
            Label lblBudget = new Label { Text = $"💰 Бюджет: {row["Бюджет"]} $", Font = new Font("Segoe UI", 10), ForeColor = Color.DimGray, Location = new Point(20, 50), AutoSize = true };

            Label lblStatusText = new Label { Text = "Статус:", Location = new Point(20, 90), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            ComboBox cmbStatus = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10), Location = new Point(85, 87), Width = 140 };

            cmbStatus.Items.AddRange(new string[] { "Планується", "Очікує перевірки", "Завершено" });

            string currentStatus = row["Статус"].ToString();
            if (cmbStatus.Items.Contains(currentStatus)) cmbStatus.SelectedItem = currentStatus;

            Label lblRatingText = new Label { Text = "Оцінка (1-10):", Location = new Point(250, 90), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            NumericUpDown numRating = new NumericUpDown { Minimum = 1, Maximum = 10, Font = new Font("Segoe UI", 10), Location = new Point(365, 87), Width = 60 };

            int rVal = row["Оцінка"] != DBNull.Value ? Convert.ToInt32(row["Оцінка"]) : 1;
            if (rVal < 1) rVal = 1; if (rVal > 10) rVal = 10;
            numRating.Value = rVal;

            TextBox txtComment = new TextBox { Multiline = true, Font = new Font("Segoe UI", 10), Location = new Point(20, 130), Width = cardWidth - 40, Height = 60, Text = row["Коментар"].ToString() };
            if (string.IsNullOrWhiteSpace(txtComment.Text)) { txtComment.Text = "Напишіть відгук..."; txtComment.ForeColor = Color.Gray; }
            txtComment.Enter += (s, e) => { if (txtComment.Text == "Напишіть відгук...") { txtComment.Text = ""; txtComment.ForeColor = Color.Black; } };
            txtComment.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txtComment.Text)) { txtComment.Text = "Напишіть відгук..."; txtComment.ForeColor = Color.Gray; } };

            Label lblInfo = new Label { Text = "ℹ️ Звіт буде показано всім, але потребує затвердження адміном.", Font = new Font("Segoe UI", 8, FontStyle.Italic), ForeColor = Color.Gray, Location = new Point(17, 195), AutoSize = true };
            Button btnSave = new Button { Text = "Опублікувати звіт", BackColor = Color.DodgerBlue, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(20, 220), Size = new Size(cardWidth - 40, 40), Cursor = Cursors.Hand };
            btnSave.FlatAppearance.BorderSize = 0;

            if (currentStatus == "Запит")
            {
                cmbStatus.Items.Add("Запит"); cmbStatus.SelectedItem = "Запит";
                btnSave.Text = "Очікується бюджет від адміністратора";
                cmbStatus.Enabled = false; numRating.Enabled = false; txtComment.Enabled = false; txtComment.ReadOnly = true; btnSave.Enabled = false; btnSave.BackColor = Color.Gray;
            }
            else if (currentStatus == "Очікує перевірки")
            {
                btnSave.Text = "Очікує підтвердження від адміністратора";
                cmbStatus.Enabled = false; numRating.Enabled = false; txtComment.Enabled = false; txtComment.ReadOnly = true; btnSave.Enabled = false; btnSave.BackColor = Color.Gray;
            }
            else if (currentStatus == "Завершено")
            {
                cmbStatus.Enabled = false;
                btnSave.Text = "💾 Оновити відгук";
                btnSave.BackColor = Color.ForestGreen;
            }

            btnSave.Click += (s, e) =>
            {
                try
                {
                    string inputText = txtComment.Text == "Напишіть відгук..." ? "" : txtComment.Text.Trim();
                    string selectedStatus = currentStatus;

                    if (currentStatus == "Планується" && !string.IsNullOrWhiteSpace(inputText))
                    {
                        DialogResult res = MessageBox.Show("Відправити звіт в стрічку?\n\nВідгук з'явиться відразу, але статус подорожі очікуватиме підтвердження адміна.", "Публікація", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (res == DialogResult.Yes) { selectedStatus = "Очікує перевірки"; }
                        else return;
                    }
                    else if (currentStatus == "Завершено")
                    {
                        selectedStatus = "Завершено";
                    }

                    row["Статус"] = selectedStatus;
                    row["Оцінка"] = numRating.Value;
                    row["Коментар"] = inputText;

                    travelTable.AcceptChanges();
                    travelTable.WriteXml(xmlFilePath, XmlWriteMode.WriteSchema);

                    MessageBox.Show("Дані успішно збережено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RenderFeeds();
                }
                catch (Exception ex) { MessageBox.Show($"Помилка: {ex.Message}"); }
            };

            pnlCard.Controls.Add(lblDestination); pnlCard.Controls.Add(lblBudget); pnlCard.Controls.Add(lblStatusText);
            pnlCard.Controls.Add(cmbStatus); pnlCard.Controls.Add(lblRatingText); pnlCard.Controls.Add(numRating);
            pnlCard.Controls.Add(txtComment); pnlCard.Controls.Add(lblInfo); pnlCard.Controls.Add(btnSave);

            return pnlCard;
        }

        private Panel CreateGlobalFeedCard(DataRow row)
        {
            int cardWidth = flowGlobalFeed.ClientSize.Width - 40;
            Panel pnlCard = new Panel { BackColor = Color.White, Width = cardWidth, Height = 210, Margin = new Padding(10, 10, 10, 20), BorderStyle = BorderStyle.FixedSingle };

            string author = row["Користувач"].ToString();
            Label lblUser = new Label { Text = $"👤 @{author}", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.DarkSlateBlue, Location = new Point(15, 15), AutoSize = true };
            Label lblDestination = new Label { Text = $"🌍 {row["Країна"]}, {row["Місто"]}", Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.Black, Location = new Point(15, 45), AutoSize = true };

            int rating = row["Оцінка"] != DBNull.Value ? Convert.ToInt32(row["Оцінка"]) : 1;
            Label lblRating = new Label { Text = $"Оцінка: {rating}/10 {new string('⭐', rating)}", Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.DarkOrange, Location = new Point(15, 70), AutoSize = true };

            string commentText = row["Коментар"].ToString();
            if (string.IsNullOrWhiteSpace(commentText)) commentText = "Користувач залишив поїздку без коментаря.";

            Label lblComment = new Label { Text = $"\"{commentText}\"", Font = new Font("Segoe UI", 11, FontStyle.Italic), Location = new Point(15, 95), MaximumSize = new Size(cardWidth - 30, 50), AutoSize = true };

            pnlCard.Controls.Add(lblUser); pnlCard.Controls.Add(lblDestination); pnlCard.Controls.Add(lblRating); pnlCard.Controls.Add(lblComment);

            if (Session.Role != "Admin")
            {
                Button btnOrder = new Button { Text = "🔥 Хочу сюди!", BackColor = Color.Coral, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9, FontStyle.Bold), Location = new Point(15, 160), Size = new Size(120, 35), Cursor = Cursors.Hand };
                btnOrder.FlatAppearance.BorderSize = 0;
                btnOrder.Click += (s, e) =>
                {
                    DialogResult res = MessageBox.Show($"Відправити заявку на тур у {row["Країна"]} ({row["Місто"]})?", "Замовлення", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        travelTable.Rows.Add(null, Session.Username, row["Країна"], row["Місто"], 0, "Запит", 1, "");
                        travelTable.AcceptChanges();
                        travelTable.WriteXml(xmlFilePath, XmlWriteMode.WriteSchema);
                        MessageBox.Show("Заявку відправлено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RenderFeeds();
                    }
                };
                pnlCard.Controls.Add(btnOrder);
            }

            bool isAdmin = Session.Role == "Admin";
            bool isMyComment = author == Session.Username;

            if (isAdmin || isMyComment)
            {
                Button btnDelete = new Button { Text = "🗑️ Видалити", BackColor = Color.Crimson, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Location = new Point(cardWidth - 110, 160), Size = new Size(95, 35), Cursor = Cursors.Hand };
                btnDelete.FlatAppearance.BorderSize = 0;
                btnDelete.Click += (s, e) =>
                {
                    if (MessageBox.Show("Видалити цей відгук?", "Модерація", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        row["Коментар"] = "";
                        travelTable.AcceptChanges();
                        travelTable.WriteXml(xmlFilePath, XmlWriteMode.WriteSchema);
                        RenderFeeds();
                    }
                };
                pnlCard.Controls.Add(btnDelete);
            }

            if (isMyComment && !isAdmin)
            {
                Button btnEdit = new Button { Text = "✏️ Змінити", BackColor = Color.LightSeaGreen, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Location = new Point(cardWidth - 215, 160), Size = new Size(95, 35), Cursor = Cursors.Hand };
                btnEdit.FlatAppearance.BorderSize = 0;
                btnEdit.Click += (s, e) =>
                {
                    TextBox txtEdit = new TextBox { Text = row["Коментар"].ToString(), Multiline = true, Location = lblComment.Location, Size = new Size(cardWidth - 30, 50), Font = new Font("Segoe UI", 10) };
                    pnlCard.Controls.Add(txtEdit);
                    txtEdit.BringToFront();

                    btnEdit.Text = "💾 Зберегти";
                    btnEdit.BackColor = Color.ForestGreen;

                    btnEdit.Click -= null;
                    btnEdit.Click += (sender2, e2) =>
                    {
                        row["Коментар"] = txtEdit.Text;
                        travelTable.AcceptChanges();
                        travelTable.WriteXml(xmlFilePath, XmlWriteMode.WriteSchema);
                        RenderFeeds();
                    };
                };
                pnlCard.Controls.Add(btnEdit);
            }

            return pnlCard;
        }

        private void pbBack_Click(object sender, EventArgs e) => this.Close();
        private void pbExit_Click(object sender, EventArgs e) => Application.Exit();
        private void pnlHeader_MouseDown(object sender, MouseEventArgs e) { dragging = true; dragCursorPoint = System.Windows.Forms.Cursor.Position; dragFormPoint = this.Location; }
        private void pnlHeader_MouseMove(object sender, MouseEventArgs e) { if (dragging) { Point dif = Point.Subtract(System.Windows.Forms.Cursor.Position, new Size(dragCursorPoint)); this.Location = Point.Add(dragFormPoint, new Size(dif)); } }
        private void pnlHeader_MouseUp(object sender, MouseEventArgs e) => dragging = false;
    }
}