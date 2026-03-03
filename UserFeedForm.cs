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
            Session.Role = "User";
            xmlFilePath = Path.Combine(Application.StartupPath, "TravelData.xml");
        }

        public UserFeedForm() : this(new CUser("Gogart_Test", "0000", false)) { }

        private void UserFeedForm_Load(object sender, EventArgs e)
        {
            lblTitle.Text = $"Travel Hub | {Session.Username}";

            // 🔥 ЖОРСТКА ФІКСАЦІЯ КАРТОК В ОДИН СТОВПЧИК ЗГОРУ ДОНИЗУ
            flowMyTrips.FlowDirection = FlowDirection.TopDown;
            flowMyTrips.WrapContents = false;
            flowMyTrips.AutoScroll = true;

            flowGlobalFeed.FlowDirection = FlowDirection.TopDown;
            flowGlobalFeed.WrapContents = false;
            flowGlobalFeed.AutoScroll = true;

            LoadData();
            RenderFeeds();
        }

        private void LoadData()
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
        }

        private void RenderFeeds()
        {
            flowMyTrips.Controls.Clear();
            flowGlobalFeed.Controls.Clear();

            DataRow[] myTrips = travelTable.Select($"Користувач = '{Session.Username}'");
            if (myTrips.Length == 0) flowMyTrips.Controls.Add(CreateEmptyLabel("У тебе поки немає призначених подорожей 😢"));
            else foreach (DataRow row in myTrips) flowMyTrips.Controls.Add(CreateMyTripCard(row));

            DataRow[] globalTrips = travelTable.Select($"Статус = 'Завершено'");
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
            // Трохи збільшив висоту картки (до 280), щоб влізла підказка
            Panel pnlCard = new Panel { BackColor = Color.White, Width = cardWidth, Height = 280, Margin = new Padding(10, 10, 10, 20), BorderStyle = BorderStyle.FixedSingle };

            Label lblDestination = new Label { Text = $"📍 {row["Країна"]} — {row["Місто"]}", Font = new Font("Segoe UI", 14, FontStyle.Bold), Location = new Point(15, 15), AutoSize = true };
            Label lblBudget = new Label { Text = $"💰 Бюджет: {row["Бюджет"]} $", Font = new Font("Segoe UI", 10), ForeColor = Color.DimGray, Location = new Point(20, 50), AutoSize = true };

            Label lblStatusText = new Label { Text = "Статус:", Location = new Point(20, 90), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            ComboBox cmbStatus = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10), Location = new Point(85, 87), Width = 140 };
            cmbStatus.Items.AddRange(new string[] { "Планується", "Завершено" });

            string currentStatus = row["Статус"].ToString();
            if (cmbStatus.Items.Contains(currentStatus)) cmbStatus.SelectedItem = currentStatus;
            else cmbStatus.SelectedIndex = 0;

            Label lblRatingText = new Label { Text = "Оцінка (1-10):", Location = new Point(250, 90), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            NumericUpDown numRating = new NumericUpDown { Minimum = 1, Maximum = 10, Font = new Font("Segoe UI", 10), Location = new Point(365, 87), Width = 60 };
            numRating.Value = row["Оцінка"] != DBNull.Value ? Convert.ToInt32(row["Оцінка"]) : 1;

            TextBox txtComment = new TextBox { Multiline = true, Font = new Font("Segoe UI", 10), Location = new Point(20, 130), Width = cardWidth - 40, Height = 60, Text = row["Коментар"].ToString() };
            if (string.IsNullOrWhiteSpace(txtComment.Text)) { txtComment.Text = "Напишіть відгук..."; txtComment.ForeColor = Color.Gray; }
            txtComment.Enter += (s, e) => { if (txtComment.Text == "Напишіть відгук...") { txtComment.Text = ""; txtComment.ForeColor = Color.Black; } };
            txtComment.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txtComment.Text)) { txtComment.Text = "Напишіть відгук..."; txtComment.ForeColor = Color.Gray; } };

            // 🔥 1. ВІЗУАЛЬНА ПІДКАЗКА
            Label lblInfo = new Label
            {
                Text = "ℹ️ Відгук з'явиться у загальній стрічці лише для 'Завершених' подорожей.",
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = Color.Gray,
                Location = new Point(17, 195),
                AutoSize = true
            };

            Button btnSave = new Button { Text = "Опублікувати звіт", BackColor = Color.DodgerBlue, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(20, 220), Size = new Size(cardWidth - 40, 40), Cursor = Cursors.Hand };
            btnSave.FlatAppearance.BorderSize = 0;

            btnSave.Click += (s, e) =>
            {
                try
                {
                    string inputText = txtComment.Text == "Напишіть відгук..." ? "" : txtComment.Text.Trim();
                    string selectedStatus = cmbStatus.SelectedItem.ToString();

                    // 🔥 2. РОЗУМНЕ ПОПЕРЕДЖЕННЯ
                    if (selectedStatus == "Планується" && !string.IsNullOrWhiteSpace(inputText))
                    {
                        DialogResult res = MessageBox.Show(
                            "Ти написав відгук, але статус подорожі — 'Планується'. Такі відгуки не потрапляють у Стрічку спільноти.\n\nБажаєш автоматично змінити статус на 'Завершено' та опублікувати?",
                            "Публікація відгуку",
                            MessageBoxButtons.YesNoCancel,
                            MessageBoxIcon.Question);

                        if (res == DialogResult.Yes)
                        {
                            cmbStatus.SelectedItem = "Завершено";
                            selectedStatus = "Завершено";
                        }
                        else if (res == DialogResult.Cancel)
                        {
                            return; // Скасовуємо збереження, якщо юзер передумав
                        }
                    }

                    row["Статус"] = selectedStatus;
                    row["Оцінка"] = numRating.Value;
                    row["Коментар"] = inputText;

                    travelTable.AcceptChanges();
                    travelTable.WriteXml(xmlFilePath, XmlWriteMode.WriteSchema);

                    MessageBox.Show("Звіт успішно збережено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RenderFeeds();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Виникла помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            pnlCard.Controls.Add(lblDestination); pnlCard.Controls.Add(lblBudget); pnlCard.Controls.Add(lblStatusText);
            pnlCard.Controls.Add(cmbStatus); pnlCard.Controls.Add(lblRatingText); pnlCard.Controls.Add(numRating);
            pnlCard.Controls.Add(txtComment); pnlCard.Controls.Add(lblInfo); pnlCard.Controls.Add(btnSave);

            return pnlCard;
        }

        private Panel CreateGlobalFeedCard(DataRow row)
        {
            int cardWidth = flowGlobalFeed.ClientSize.Width - 40;
            Panel pnlCard = new Panel { BackColor = Color.White, Width = cardWidth, Height = 170, Margin = new Padding(10, 10, 10, 20), BorderStyle = BorderStyle.FixedSingle };

            Label lblUser = new Label { Text = $"👤 @{row["Користувач"]}", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.DarkSlateBlue, Location = new Point(15, 15), AutoSize = true };
            Label lblDestination = new Label { Text = $"подорожував(-ла) до 🌍 {row["Країна"]}, {row["Місто"]}", Font = new Font("Segoe UI", 11, FontStyle.Italic), ForeColor = Color.DimGray, Location = new Point(15, 45), AutoSize = true };

            int rating = row["Оцінка"] != DBNull.Value ? Convert.ToInt32(row["Оцінка"]) : 1;
            string stars = new string('⭐', rating);
            Label lblRating = new Label { Text = $"Оцінка: {rating}/10 {stars}", Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.DarkOrange, Location = new Point(15, 75), AutoSize = true };

            string commentText = row["Коментар"].ToString();
            if (string.IsNullOrWhiteSpace(commentText)) commentText = "Користувач залишив поїздку без коментаря.";

            Label lblComment = new Label { Text = $"\"{commentText}\"", Font = new Font("Segoe UI", 11), Location = new Point(15, 105), MaximumSize = new Size(cardWidth - 30, 50), AutoSize = true };

            pnlCard.Controls.Add(lblUser); pnlCard.Controls.Add(lblDestination); pnlCard.Controls.Add(lblRating); pnlCard.Controls.Add(lblComment);

            return pnlCard;
        }

        private void pbBack_Click(object sender, EventArgs e) => this.Close();
        private void pbExit_Click(object sender, EventArgs e) => Application.Exit();
        private void pnlHeader_MouseDown(object sender, MouseEventArgs e) { dragging = true; dragCursorPoint = System.Windows.Forms.Cursor.Position; dragFormPoint = this.Location; }
        private void pnlHeader_MouseMove(object sender, MouseEventArgs e) { if (dragging) { Point dif = Point.Subtract(System.Windows.Forms.Cursor.Position, new Size(dragCursorPoint)); this.Location = Point.Add(dragFormPoint, new Size(dif)); } }
        private void pnlHeader_MouseUp(object sender, MouseEventArgs e) => dragging = false;
    }
}