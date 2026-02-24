using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Navchpract_2
{
    public partial class WordDataGrid : Form
    {
        BindingList<Product> StoreList = new BindingList<Product>();
        DataTable dtStore = new DataTable();
        BindingSource bsStore = new BindingSource();

        private Product _selectedItem = null;

        public WordDataGrid()
        {
            InitializeComponent();
            this.Load += Data_Load;

            this.textBox3.TextChanged += textBox3_TextChanged;
            this.pictureBox9.Click += pictureBox9_Click;
            this.button_loaddata.Click += button_loaddata_Click;
            this.button_cl.Click += button_cl_Click;
            this.toolStripButton2.Click += toolStripButton2_Click;
            this.toolStripButton4.Click += toolStripButton4_Click;
            this.pictureBox3.Click += pictureBox3_Click;
            this.pictureBox4.Click += pictureBox4_Click;
            this.dataGrid_bslist.SelectionChanged += dataGrid_bslist_SelectionChanged;

            if (this.textBox_Search3 != null) this.textBox_Search3.TextChanged += textBox_Search3_TextChanged;
            if (this.pictureBox9_3 != null) this.pictureBox9_3.Click += pictureBox9_3_Click;
            if (this.button_load3 != null) this.button_load3.Click += button_load3_Click; 
            if (this.button_cl3 != null) this.button_cl3.Click += button_cl_Click;
            if (this.toolStripButton2_3 != null) this.toolStripButton2_3.Click += toolStripButton2_3_Click;
            if (this.toolStripButton4_3 != null) this.toolStripButton4_3.Click += toolStripButton4_3_Click;
            if (this.pictureBox3_3 != null) this.pictureBox3_3.Click += pictureBox3_3_Click;
            if (this.pictureBox4_3 != null) this.pictureBox4_3.Click += pictureBox4_3_Click;
            if (this.dataGrid_DataTable != null) this.dataGrid_DataTable.SelectionChanged += dataGrid_DataTable_SelectionChanged;

            if (this.textBox_Search4 != null) this.textBox_Search4.TextChanged += textBox_Search4_TextChanged;
            if (this.pictureBox9_4 != null) this.pictureBox9_4.Click += pictureBox9_4_Click;
            if (this.button_load4 != null) this.button_load4.Click += button_load4_Click; 
            if (this.button_cl4 != null) this.button_cl4.Click += button_cl_Click;
            if (this.toolStripButton2_4 != null) this.toolStripButton2_4.Click += toolStripButton2_4_Click;
            if (this.toolStripButton4_4 != null) this.toolStripButton4_4.Click += toolStripButton4_4_Click;
            if (this.pictureBox3_4 != null) this.pictureBox3_4.Click += pictureBox3_4_Click;
            if (this.pictureBox4_4 != null) this.pictureBox4_4.Click += pictureBox4_4_Click;
            if (this.dataGrid_BindingSource != null) this.dataGrid_BindingSource.SelectionChanged += dataGrid_BindingSource_SelectionChanged;
        }

        private void Data_Load(object sender, EventArgs e)
        {
            listBox1.DataSource = StoreList;

            dataGrid_bslist.DataSource = null;
            if (dataGrid_DataTable != null) dataGrid_DataTable.DataSource = null;
            if (dataGrid_BindingSource != null) dataGrid_BindingSource.DataSource = null;

            ConfigureNumericInput(numeric_inSt);
            ConfigureNumericInput(numericUpDown1);
            if (numericUpDown1_3 != null) ConfigureNumericInput(numericUpDown1_3);
            if (numericUpDown1_4 != null) ConfigureNumericInput(numericUpDown1_4);

            ConfigureGrid_(dataGrid_bslist);
            if (dataGrid_DataTable != null) ConfigureGrid_(dataGrid_DataTable);
            if (dataGrid_BindingSource != null) ConfigureGrid_(dataGrid_BindingSource);

            button_cl.Enabled = true;
            button_loaddata.Enabled = true;

            groupBox2.Enabled = true;
            if (groupBox3 != null) groupBox3.Enabled = true;
            if (groupBox4 != null) groupBox4.Enabled = true;

            ResetEditGroup();
        }

        // МЕТОД З МЕТОДИЧКИ 
        public DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        private void ConfigureNumericInput(NumericUpDown nud)
        {
            if (nud == null) return;
            nud.DecimalPlaces = 0;
            nud.Minimum = 0;
            nud.Maximum = 100000;
        }

        private void ConfigureGrid_(DataGridView dgv)
        {
            if (dgv == null) return;

            dgv.AutoGenerateColumns = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            dgv.ColumnHeadersVisible = true;
            dgv.RowHeadersVisible = true;
            dgv.RowHeadersWidth = 30;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv.Columns.Clear();

            DataGridViewTextBoxColumn nameCol = new DataGridViewTextBoxColumn();
            nameCol.HeaderText = "Назва товару";
            nameCol.DataPropertyName = "Name";
            dgv.Columns.Add(nameCol);

            DataGridViewTextBoxColumn qtyCol = new DataGridViewTextBoxColumn();
            qtyCol.HeaderText = "Кількість";
            qtyCol.DataPropertyName = "Quantity";
            qtyCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(qtyCol);

            DataGridViewTextBoxColumn priceCol = new DataGridViewTextBoxColumn();
            priceCol.HeaderText = "Ціна";
            priceCol.DataPropertyName = "Price";
            priceCol.DefaultCellStyle.Format = "N2";
            priceCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns.Add(priceCol);
        }

        // ЛОГІКА ЗАВАНТАЖЕННЯ ДЛЯ КОЖНОЇ ВКЛАДКИ 

        // Вкладка 2 (BindingList)
        private void button_loaddata_Click(object sender, EventArgs e)
        {
            if (StoreList.Count == 0) { MessageBox.Show("Список пустий! Завантажте файл на першій вкладці."); return; }
            textBox3.Text = "";
            dataGrid_bslist.DataSource = null;
            dataGrid_bslist.DataSource = StoreList;
        }

        // Вкладка 3 (DataTable)
        private void button_load3_Click(object sender, EventArgs e)
        {
            if (StoreList.Count == 0) { MessageBox.Show("Список пустий! Завантажте файл на першій вкладці."); return; }
            if (textBox_Search3 != null) textBox_Search3.Text = "";

            dtStore = ToDataTable(StoreList); // Конвертація в DataTable
            dataGrid_DataTable.DataSource = null;
            dataGrid_DataTable.DataSource = dtStore.DefaultView;
        }

        // Вкладка 4 (BindingSource)
        private void button_load4_Click(object sender, EventArgs e)
        {
            if (StoreList.Count == 0) { MessageBox.Show("Список пустий! Завантажте файл на першій вкладці."); return; }
            if (textBox_Search4 != null) textBox_Search4.Text = "";

            dtStore = ToDataTable(StoreList);
            bsStore.DataSource = dtStore; // Підключення DataTable до BindingSource
            dataGrid_BindingSource.DataSource = null;
            dataGrid_BindingSource.DataSource = bsStore;
        }

        // Оновлення всіх джерел при редагуванні/додаванні (щоб дані були актуальними скрізь)
        private void RefreshAllSources()
        {
            dtStore = ToDataTable(StoreList);
            bsStore.DataSource = dtStore;

            if (dataGrid_bslist.DataSource != null) { dataGrid_bslist.DataSource = null; dataGrid_bslist.DataSource = StoreList; }
            if (dataGrid_DataTable != null && dataGrid_DataTable.DataSource != null)
            {
                dataGrid_DataTable.DataSource = null;
                dataGrid_DataTable.DataSource = dtStore.DefaultView;
            }
            if (dataGrid_BindingSource != null && dataGrid_BindingSource.DataSource != null) { dataGrid_BindingSource.DataSource = null; dataGrid_BindingSource.DataSource = bsStore; }
        }

        // ЛОГІКА БЛОКУВАННЯ ПОЛІВ 
        private void SetInputsAccess(bool allowEdit)
        {
            textBox1.Enabled = allowEdit;
            textBox2.Enabled = allowEdit;
            numericUpDown1.Enabled = allowEdit;
            pictureBox4.Visible = allowEdit;

            if (textBox1_3 != null) { textBox1_3.Enabled = allowEdit; textBox2_3.Enabled = allowEdit; numericUpDown1_3.Enabled = allowEdit; pictureBox4_3.Visible = allowEdit; }
            if (textBox1_4 != null) { textBox1_4.Enabled = allowEdit; textBox2_4.Enabled = allowEdit; numericUpDown1_4.Enabled = allowEdit; pictureBox4_4.Visible = allowEdit; }
        }

        private void FillFields(Product item)
        {
            _selectedItem = item;
            textBox1.Text = item.Name; numericUpDown1.Value = item.Quantity; textBox2.Text = item.Price.ToString("F2");
            if (textBox1_3 != null) { textBox1_3.Text = item.Name; numericUpDown1_3.Value = item.Quantity; textBox2_3.Text = item.Price.ToString("F2"); }
            if (textBox1_4 != null) { textBox1_4.Text = item.Name; numericUpDown1_4.Value = item.Quantity; textBox2_4.Text = item.Price.ToString("F2"); }
        }

        private void ResetEditGroup()
        {
            _selectedItem = null;
            textBox1.Text = textBox2.Text = ""; numericUpDown1.Value = 0;
            if (textBox1_3 != null) { textBox1_3.Text = textBox2_3.Text = ""; numericUpDown1_3.Value = 0; }
            if (textBox1_4 != null) { textBox1_4.Text = textBox2_4.Text = ""; numericUpDown1_4.Value = 0; }

            SetInputsAccess(false);

            groupBox2.Text = "Інформація";
            if (groupBox3 != null) groupBox3.Text = "Інформація";
            if (groupBox4 != null) groupBox4.Text = "Інформація";

            dataGrid_bslist.ClearSelection();
            if (dataGrid_DataTable != null) dataGrid_DataTable.ClearSelection();
            if (dataGrid_BindingSource != null) dataGrid_BindingSource.ClearSelection();

            toolStripButton2.Enabled = toolStripButton4.Enabled = false;
            if (toolStripButton2_3 != null) { toolStripButton2_3.Enabled = false; toolStripButton4_3.Enabled = false; }
            if (toolStripButton2_4 != null) { toolStripButton2_4.Enabled = false; toolStripButton4_4.Enabled = false; }
        }

        private void EnableEditingMode()
        {
            SetInputsAccess(true);
            string title = _selectedItem != null ? "Редагування: " + _selectedItem.Name : "Новий товар";
            groupBox2.Text = title;
            if (groupBox3 != null) groupBox3.Text = title;
            if (groupBox4 != null) groupBox4.Text = title;
        }

        // ВИБІР РЯДКА (АКТИВАЦІЯ КНОПОК)
        private void GridSelectionChanged(DataGridView dgv, ToolStripButton btnEdit, ToolStripButton btnDel, System.Windows.Forms.GroupBox gb)
        {
            if (dgv.CurrentRow != null)
            {
                Product item = null;

                if (dgv.CurrentRow.DataBoundItem is Product p)
                    item = p;
                else if (dgv.CurrentRow.DataBoundItem is DataRowView drv)
                {
                    string searchName = drv["Name"].ToString();
                    item = StoreList.FirstOrDefault(x => x.Name == searchName);
                }

                if (item != null)
                {
                    FillFields(item);
                    btnEdit.Enabled = true;
                    btnDel.Enabled = true;
                    SetInputsAccess(false);
                    gb.Text = "Перегляд: " + item.Name;
                }
            }
            else
            {
                btnEdit.Enabled = false;
                btnDel.Enabled = false;
            }
        }

        private void dataGrid_bslist_SelectionChanged(object sender, EventArgs e) => GridSelectionChanged(dataGrid_bslist, toolStripButton2, toolStripButton4, groupBox2);
        private void dataGrid_DataTable_SelectionChanged(object sender, EventArgs e) => GridSelectionChanged(dataGrid_DataTable, toolStripButton2_3, toolStripButton4_3, groupBox3);
        private void dataGrid_BindingSource_SelectionChanged(object sender, EventArgs e) => GridSelectionChanged(dataGrid_BindingSource, toolStripButton2_4, toolStripButton4_4, groupBox4);

        // КНОПКИ УПРАВЛІННЯ (Олівець, Смітник, Плюс) 

        private void EditClicked(DataGridView dgv, TextBox focusBox)
        {
            if (dgv.CurrentRow != null) { EnableEditingMode(); focusBox.Focus(); }
        }
        private void toolStripButton2_Click(object sender, EventArgs e) => EditClicked(dataGrid_bslist, textBox1);
        private void toolStripButton2_3_Click(object sender, EventArgs e) => EditClicked(dataGrid_DataTable, textBox1_3);
        private void toolStripButton2_4_Click(object sender, EventArgs e) => EditClicked(dataGrid_BindingSource, textBox1_4);

        private void DeleteClicked()
        {
            if (_selectedItem != null)
            {
                if (MessageBox.Show($"Видалити '{_selectedItem.Name}'?", "Видалення", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    StoreList.Remove(_selectedItem);
                    RefreshAllSources();
                    ResetEditGroup();
                }
            }
        }
        private void toolStripButton4_Click(object sender, EventArgs e) => DeleteClicked();
        private void toolStripButton4_3_Click(object sender, EventArgs e) => DeleteClicked();
        private void toolStripButton4_4_Click(object sender, EventArgs e) => DeleteClicked();

        private void AddClicked(TextBox focusBox)
        {
            ResetEditGroup(); _selectedItem = null; EnableEditingMode(); focusBox.Focus();
        }
        private void pictureBox3_Click(object sender, EventArgs e) => AddClicked(textBox1);
        private void pictureBox3_3_Click(object sender, EventArgs e) => AddClicked(textBox1_3);
        private void pictureBox3_4_Click(object sender, EventArgs e) => AddClicked(textBox1_4);

        private void SaveEditClicked(TextBox tName, NumericUpDown numQty, TextBox tPrice)
        {
            if (string.IsNullOrWhiteSpace(tName.Text)) { MessageBox.Show("Введіть назву!"); return; }
            if (!float.TryParse(tPrice.Text, out float price)) { MessageBox.Show("Некоректна ціна!"); return; }

            if (_selectedItem == null)
            {
                StoreList.Add(new Product(tName.Text, (int)numQty.Value, price));
                MessageBox.Show("Додано!");
            }
            else
            {
                _selectedItem.Name = tName.Text;
                _selectedItem.Quantity = (int)numQty.Value;
                _selectedItem.Price = price;
                int index = StoreList.IndexOf(_selectedItem);
                if (index >= 0) StoreList.ResetItem(index);
                MessageBox.Show("Збережено!");
            }

            RefreshAllSources();
            ResetEditGroup();
        }
        private void pictureBox4_Click(object sender, EventArgs e) => SaveEditClicked(textBox1, numericUpDown1, textBox2);
        private void pictureBox4_3_Click(object sender, EventArgs e) => SaveEditClicked(textBox1_3, numericUpDown1_3, textBox2_3);
        private void pictureBox4_4_Click(object sender, EventArgs e) => SaveEditClicked(textBox1_4, numericUpDown1_4, textBox2_4);


        // ПОШУК 
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (dataGrid_bslist.DataSource == null && StoreList.Count > 0) return;
            string search = textBox3.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(search)) dataGrid_bslist.DataSource = StoreList;
            else dataGrid_bslist.DataSource = new BindingList<Product>(StoreList.Where(p => p.Name.ToLower().Contains(search)).ToList());
            ResetEditGroup();
        }

        private void textBox_Search3_TextChanged(object sender, EventArgs e)
        {
            if (dtStore == null || dtStore.Rows.Count == 0) return;
            string search = textBox_Search3.Text.Trim().Replace("'", "''");
            dtStore.DefaultView.RowFilter = string.IsNullOrEmpty(search) ? "" : $"Name LIKE '%{search}%'";
            dataGrid_DataTable.DataSource = dtStore.DefaultView;

            ResetEditGroup();
        }

        private void textBox_Search4_TextChanged(object sender, EventArgs e)
        {
            if (bsStore.DataSource == null) return;
            string search = textBox_Search4.Text.Trim().Replace("'", "''");
            bsStore.Filter = string.IsNullOrEmpty(search) ? "" : $"Name LIKE '%{search}%'";
            ResetEditGroup();
        }

        private void pictureBox9_Click(object sender, EventArgs e) => textBox3.Text = "";
        private void pictureBox9_3_Click(object sender, EventArgs e) { if (textBox_Search3 != null) textBox_Search3.Text = ""; }
        private void pictureBox9_4_Click(object sender, EventArgs e) { if (textBox_Search4 != null) textBox_Search4.Text = ""; }

        // ФАЙЛИ, 1 ВКЛАДКА, ВАЛІДАЦІЯ 
        private void LoadFromFile()
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "BIN|*.bin", InitialDirectory = Application.StartupPath };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            try
            {
                using (BinaryReader br = new BinaryReader(File.Open(ofd.FileName, FileMode.Open)))
                {
                    if (br.ReadString() != "StoreList") { MessageBox.Show("Помилка формату"); return; }
                    int c = br.ReadInt32(); int f = br.ReadInt32(); for (int i = 0; i < f; i++) br.ReadString();

                    StoreList.Clear();
                    for (int i = 0; i < c; i++)
                        StoreList.Add(new Product(br.ReadString(), br.ReadInt32(), br.ReadSingle()));
                }
                MessageBox.Show($"Завантажено в пам'ять {StoreList.Count} елементів. Перейдіть на інші вкладки та натисніть 'Завантажити'.");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void pictureSavetobin_Click(object sender, EventArgs e)
        {
            if (StoreList.Count == 0) return;
            SaveFileDialog sfd = new SaveFileDialog { Filter = "BIN|*.bin" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (BinaryWriter bw = new BinaryWriter(File.Open(sfd.FileName, FileMode.Create)))
                {
                    bw.Write("StoreList"); bw.Write(StoreList.Count); bw.Write(3); bw.Write("N"); bw.Write("Q"); bw.Write("P");
                    foreach (var p in StoreList) { bw.Write(p.Name); bw.Write(p.Quantity); bw.Write(p.Price); }
                }
                MessageBox.Show("Збережено!");
            }
        }

        private void pictureBox_load_Click(object sender, EventArgs e) => LoadFromFile();
        private void button_load_Click(object sender, EventArgs e) => LoadFromFile(); // Кнопка на 1 вкладці

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            numeric_inSt.Enabled = true; textBox_model.Enabled = true; textBox_price.Enabled = true; add_list.Enabled = true;
            numeric_inSt.Value = 0; textBox_model.Text = ""; textBox_price.Text = ""; textBox_model.Focus();
        }

        private void add_list_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_model.Text)) return;
            if (float.TryParse(textBox_price.Text, out float p))
            {
                StoreList.Add(new Product(textBox_model.Text, (int)numeric_inSt.Value, p));
                MessageBox.Show("Додано!");
                numeric_inSt.Enabled = false; textBox_model.Enabled = false; textBox_price.Enabled = false; add_list.Enabled = false;
                RefreshAllSources(); // Оновлюємо інші таблиці, якщо вони вже були завантажені
            }
        }

        private void button_cl_Click(object sender, EventArgs e)
        {
            StoreList.Clear();
            RefreshAllSources();
            ResetEditGroup();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вихід?", "", MessageBoxButtons.YesNo) == DialogResult.Yes) Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("В меню?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                StartForm sf = new StartForm(); this.Hide(); sf.ShowDialog(); this.Close();
            }
        }

        public void dataGrid_bslist_CellClick(object sender, DataGridViewCellEventArgs e) { }
        public void dataGrid_DataTable_CellClick(object sender, DataGridViewCellEventArgs e) { }
        public void dataGrid_BindingSource_CellClick(object sender, DataGridViewCellEventArgs e) { }

        // ВАЛІДАЦІЯ ЦІНИ
        private void ValidatePriceInput(object sender, KeyPressEventArgs e)
        {
            var txt = sender as TextBox;
            if (e.KeyChar == (char)Keys.Back) return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',' && txt.Text.Contains(",")) { e.Handled = true; return; }
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',') { e.Handled = true; return; }
            if (txt.Text.Contains(","))
            {
                int c = txt.Text.IndexOf(',');
                if (txt.SelectionStart > c && txt.Text.Substring(c + 1).Length >= 2 && txt.SelectionLength == 0) e.Handled = true;
            }
        }

        private void FormatPriceOnLeave(object sender, EventArgs e)
        {
            var txt = sender as TextBox;
            if (float.TryParse(txt.Text, out float val)) txt.Text = val.ToString("F2");
        }

        private void textBox_price_KeyPress(object sender, KeyPressEventArgs e) => ValidatePriceInput(sender, e);
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e) => ValidatePriceInput(sender, e);
        public void textBox_price_Leave(object sender, EventArgs e) => FormatPriceOnLeave(sender, e);
        public void textBox2_Leave(object sender, EventArgs e) => FormatPriceOnLeave(sender, e);
    }
}