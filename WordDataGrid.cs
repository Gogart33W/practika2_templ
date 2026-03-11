using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Navchpract_2
{
    public partial class WordDataGrid : Form
    {
        private BindingList<Product> StoreList = new BindingList<Product>();
        private DataTable dtStore = new DataTable();
        private BindingSource bsStore = new BindingSource();

        private Product _selectedItem = null;

        public WordDataGrid()
        {
            InitializeComponent();
            this.Load += Data_Load;

            if (this.textBox3 != null) this.textBox3.TextChanged += textBox3_TextChanged;
            if (this.pictureBox9 != null) this.pictureBox9.Click += pictureBox9_Click;
            if (this.button_loaddata != null) this.button_loaddata.Click += button_loaddata_Click;
            if (this.button_cl != null) this.button_cl.Click += button_cl_Click;
            if (this.toolStripButton2 != null) this.toolStripButton2.Click += toolStripButton2_Click;
            if (this.toolStripButton4 != null) this.toolStripButton4.Click += toolStripButton4_Click;
            if (this.pictureBox3 != null) this.pictureBox3.Click += pictureBox3_Click;
            if (this.pictureBox4 != null) this.pictureBox4.Click += pictureBox4_Click;
            if (this.dataGrid_bslist != null) this.dataGrid_bslist.SelectionChanged += dataGrid_bslist_SelectionChanged;

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
            if (listBox1 != null) listBox1.DataSource = StoreList;

            if (dataGrid_bslist != null) dataGrid_bslist.DataSource = null;
            if (dataGrid_DataTable != null) dataGrid_DataTable.DataSource = null;
            if (dataGrid_BindingSource != null) dataGrid_BindingSource.DataSource = null;

            ConfigureNumericInput(numeric_inSt);
            ConfigureNumericInput(numericUpDown1);
            if (numericUpDown1_3 != null) ConfigureNumericInput(numericUpDown1_3);
            if (numericUpDown1_4 != null) ConfigureNumericInput(numericUpDown1_4);

            ConfigureGrid_(dataGrid_bslist);
            if (dataGrid_DataTable != null) ConfigureGrid_(dataGrid_DataTable);
            if (dataGrid_BindingSource != null) ConfigureGrid_(dataGrid_BindingSource);

            if (button_cl != null) button_cl.Enabled = true;
            if (button_loaddata != null) button_loaddata.Enabled = true;

            if (groupBox2 != null) groupBox2.Enabled = true;
            if (groupBox3 != null) groupBox3.Enabled = true;
            if (groupBox4 != null) groupBox4.Enabled = true;

            ResetEditGroup();
        }

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

        private void button_loaddata_Click(object sender, EventArgs e)
        {
            if (StoreList.Count == 0) { MessageBox.Show("Список пустий! Завантажте файл на першій вкладці."); return; }
            if (textBox3 != null) textBox3.Text = "";
            if (dataGrid_bslist != null)
            {
                dataGrid_bslist.DataSource = null;
                dataGrid_bslist.DataSource = StoreList;
            }
        }

        private void button_load3_Click(object sender, EventArgs e)
        {
            if (StoreList.Count == 0) { MessageBox.Show("Список пустий! Завантажте файл на першій вкладці."); return; }
            if (textBox_Search3 != null) textBox_Search3.Text = "";

            dtStore = ToDataTable(StoreList);
            if (dataGrid_DataTable != null)
            {
                dataGrid_DataTable.DataSource = null;
                dataGrid_DataTable.DataSource = dtStore.DefaultView;
            }
        }

        private void button_load4_Click(object sender, EventArgs e)
        {
            if (StoreList.Count == 0) { MessageBox.Show("Список пустий! Завантажте файл на першій вкладці."); return; }
            if (textBox_Search4 != null) textBox_Search4.Text = "";

            dtStore = ToDataTable(StoreList);
            bsStore.DataSource = dtStore;
            if (dataGrid_BindingSource != null)
            {
                dataGrid_BindingSource.DataSource = null;
                dataGrid_BindingSource.DataSource = bsStore;
            }
        }

        private void RefreshAllSources()
        {
            dtStore = ToDataTable(StoreList);
            bsStore.DataSource = dtStore;

            if (dataGrid_bslist != null && dataGrid_bslist.DataSource != null)
            {
                dataGrid_bslist.DataSource = null;
                dataGrid_bslist.DataSource = StoreList;
            }
            if (dataGrid_DataTable != null && dataGrid_DataTable.DataSource != null)
            {
                dataGrid_DataTable.DataSource = null;
                dataGrid_DataTable.DataSource = dtStore.DefaultView;
            }
            if (dataGrid_BindingSource != null && dataGrid_BindingSource.DataSource != null)
            {
                dataGrid_BindingSource.DataSource = null;
                dataGrid_BindingSource.DataSource = bsStore;
            }
        }

        private void SetInputsAccess(bool allowEdit)
        {
            if (textBox1 != null) textBox1.Enabled = allowEdit;
            if (textBox2 != null) textBox2.Enabled = allowEdit;
            if (numericUpDown1 != null) numericUpDown1.Enabled = allowEdit;
            if (pictureBox4 != null) pictureBox4.Visible = allowEdit;

            if (textBox1_3 != null) { textBox1_3.Enabled = allowEdit; textBox2_3.Enabled = allowEdit; numericUpDown1_3.Enabled = allowEdit; pictureBox4_3.Visible = allowEdit; }
            if (textBox1_4 != null) { textBox1_4.Enabled = allowEdit; textBox2_4.Enabled = allowEdit; numericUpDown1_4.Enabled = allowEdit; pictureBox4_4.Visible = allowEdit; }
        }

        private void FillFields(Product item)
        {
            _selectedItem = item;
            if (textBox1 != null) textBox1.Text = item.Name;
            if (numericUpDown1 != null) numericUpDown1.Value = item.Quantity;
            if (textBox2 != null) textBox2.Text = item.Price.ToString("F2");
            if (textBox1_3 != null) { textBox1_3.Text = item.Name; numericUpDown1_3.Value = item.Quantity; textBox2_3.Text = item.Price.ToString("F2"); }
            if (textBox1_4 != null) { textBox1_4.Text = item.Name; numericUpDown1_4.Value = item.Quantity; textBox2_4.Text = item.Price.ToString("F2"); }
        }

        private void ResetEditGroup()
        {
            _selectedItem = null;
            if (textBox1 != null) textBox1.Text = "";
            if (textBox2 != null) textBox2.Text = "";
            if (numericUpDown1 != null) numericUpDown1.Value = 0;
            if (textBox1_3 != null) { textBox1_3.Text = ""; textBox2_3.Text = ""; numericUpDown1_3.Value = 0; }
            if (textBox1_4 != null) { textBox1_4.Text = ""; textBox2_4.Text = ""; numericUpDown1_4.Value = 0; }

            SetInputsAccess(false);

            if (groupBox2 != null) groupBox2.Text = "Інформація";
            if (groupBox3 != null) groupBox3.Text = "Інформація";
            if (groupBox4 != null) groupBox4.Text = "Інформація";

            if (dataGrid_bslist != null) dataGrid_bslist.ClearSelection();
            if (dataGrid_DataTable != null) dataGrid_DataTable.ClearSelection();
            if (dataGrid_BindingSource != null) dataGrid_BindingSource.ClearSelection();

            if (toolStripButton2 != null) toolStripButton2.Enabled = false;
            if (toolStripButton4 != null) toolStripButton4.Enabled = false;
            if (toolStripButton2_3 != null) { toolStripButton2_3.Enabled = false; toolStripButton4_3.Enabled = false; }
            if (toolStripButton2_4 != null) { toolStripButton2_4.Enabled = false; toolStripButton4_4.Enabled = false; }
        }

        private void EnableEditingMode()
        {
            SetInputsAccess(true);
            string title = _selectedItem != null ? "Редагування: " + _selectedItem.Name : "Новий товар";
            if (groupBox2 != null) groupBox2.Text = title;
            if (groupBox3 != null) groupBox3.Text = title;
            if (groupBox4 != null) groupBox4.Text = title;
        }

        private void GridSelectionChanged(DataGridView dgv, ToolStripButton btnEdit, ToolStripButton btnDel, System.Windows.Forms.GroupBox gb)
        {
            if (dgv != null && dgv.CurrentRow != null)
            {
                Product item = null;

                if (dgv.CurrentRow.DataBoundItem is Product p)
                {
                    item = p;
                }
                else if (dgv.CurrentRow.DataBoundItem is DataRowView drv)
                {
                    string searchName = drv["Name"].ToString();
                    item = StoreList.FirstOrDefault(x => x.Name == searchName);
                }

                if (item != null)
                {
                    FillFields(item);
                    if (btnEdit != null) btnEdit.Enabled = true;
                    if (btnDel != null) btnDel.Enabled = true;
                    SetInputsAccess(false);
                    if (gb != null) gb.Text = "Перегляд: " + item.Name;
                }
            }
            else
            {
                if (btnEdit != null) btnEdit.Enabled = false;
                if (btnDel != null) btnDel.Enabled = false;
            }
        }

        private void dataGrid_bslist_SelectionChanged(object sender, EventArgs e) => GridSelectionChanged(dataGrid_bslist, toolStripButton2, toolStripButton4, groupBox2);
        private void dataGrid_DataTable_SelectionChanged(object sender, EventArgs e) => GridSelectionChanged(dataGrid_DataTable, toolStripButton2_3, toolStripButton4_3, groupBox3);
        private void dataGrid_BindingSource_SelectionChanged(object sender, EventArgs e) => GridSelectionChanged(dataGrid_BindingSource, toolStripButton2_4, toolStripButton4_4, groupBox4);

        private void EditClicked(DataGridView dgv, TextBox focusBox)
        {
            if (dgv != null && dgv.CurrentRow != null)
            {
                EnableEditingMode();
                if (focusBox != null) focusBox.Focus();
            }
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
            ResetEditGroup();
            _selectedItem = null;
            EnableEditingMode();
            if (focusBox != null) focusBox.Focus();
        }

        private void pictureBox3_Click(object sender, EventArgs e) => AddClicked(textBox1);
        private void pictureBox3_3_Click(object sender, EventArgs e) => AddClicked(textBox1_3);
        private void pictureBox3_4_Click(object sender, EventArgs e) => AddClicked(textBox1_4);

        private void SaveEditClicked(TextBox tName, NumericUpDown numQty, TextBox tPrice)
        {
            if (tName == null || numQty == null || tPrice == null) return;

            if (string.IsNullOrWhiteSpace(tName.Text)) { MessageBox.Show("Введіть назву!"); return; }

            string priceText = tPrice.Text.Replace(',', '.');
            if (!float.TryParse(priceText, NumberStyles.Any, CultureInfo.InvariantCulture, out float price))
            {
                MessageBox.Show("Некоректна ціна!");
                return;
            }

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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (dataGrid_bslist == null || textBox3 == null) return;
            if (dataGrid_bslist.DataSource == null && StoreList.Count > 0) return;

            string search = textBox3.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(search))
            {
                dataGrid_bslist.DataSource = StoreList;
            }
            else
            {
                dataGrid_bslist.DataSource = new BindingList<Product>(StoreList.Where(p => p.Name.ToLower().Contains(search)).ToList());
            }
            ResetEditGroup();
        }

        private void textBox_Search3_TextChanged(object sender, EventArgs e)
        {
            if (dtStore == null || dtStore.Rows.Count == 0 || textBox_Search3 == null || dataGrid_DataTable == null) return;
            string search = textBox_Search3.Text.Trim().Replace("'", "''");
            dtStore.DefaultView.RowFilter = string.IsNullOrEmpty(search) ? "" : $"Name LIKE '%{search}%'";
            dataGrid_DataTable.DataSource = dtStore.DefaultView;
            ResetEditGroup();
        }

        private void textBox_Search4_TextChanged(object sender, EventArgs e)
        {
            if (bsStore == null || bsStore.DataSource == null || textBox_Search4 == null) return;
            string search = textBox_Search4.Text.Trim().Replace("'", "''");
            bsStore.Filter = string.IsNullOrEmpty(search) ? "" : $"Name LIKE '%{search}%'";
            ResetEditGroup();
        }

        private void pictureBox9_Click(object sender, EventArgs e) { if (textBox3 != null) textBox3.Text = ""; }
        private void pictureBox9_3_Click(object sender, EventArgs e) { if (textBox_Search3 != null) textBox_Search3.Text = ""; }
        private void pictureBox9_4_Click(object sender, EventArgs e) { if (textBox_Search4 != null) textBox_Search4.Text = ""; }

        private void LoadFromFile()
        {
            using (OpenFileDialog ofd = new OpenFileDialog { Filter = "BIN|*.bin", InitialDirectory = Application.StartupPath })
            {
                if (ofd.ShowDialog() != DialogResult.OK) return;
                try
                {
                    using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        if (fs.Length == 0) return;
                        if (br.ReadString() != "StoreList") { MessageBox.Show("Помилка формату"); return; }

                        int c = br.ReadInt32();
                        int f = br.ReadInt32();
                        for (int i = 0; i < f; i++) br.ReadString();

                        StoreList.Clear();
                        for (int i = 0; i < c; i++)
                        {
                            StoreList.Add(new Product(br.ReadString(), br.ReadInt32(), br.ReadSingle()));
                        }
                    }
                    MessageBox.Show($"Завантажено в пам'ять {StoreList.Count} елементів. Перейдіть на інші вкладки та натисніть 'Завантажити'.");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void pictureSavetobin_Click(object sender, EventArgs e)
        {
            if (StoreList.Count == 0) return;
            using (SaveFileDialog sfd = new SaveFileDialog { Filter = "BIN|*.bin" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            bw.Write("StoreList");
                            bw.Write(StoreList.Count);
                            bw.Write(3);
                            bw.Write("N");
                            bw.Write("Q");
                            bw.Write("P");

                            foreach (var p in StoreList)
                            {
                                bw.Write(p.Name ?? "");
                                bw.Write(p.Quantity);
                                bw.Write(p.Price);
                            }
                        }
                        MessageBox.Show("Збережено!");
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
        }

        private void pictureBox_load_Click(object sender, EventArgs e) => LoadFromFile();
        private void button_load_Click(object sender, EventArgs e) => LoadFromFile();

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (numeric_inSt != null) { numeric_inSt.Enabled = true; numeric_inSt.Value = 0; }
            if (textBox_model != null) { textBox_model.Enabled = true; textBox_model.Text = ""; textBox_model.Focus(); }
            if (textBox_price != null) { textBox_price.Enabled = true; textBox_price.Text = ""; }
            if (add_list != null) add_list.Enabled = true;
        }

        private void add_list_Click(object sender, EventArgs e)
        {
            if (textBox_model == null || string.IsNullOrEmpty(textBox_model.Text)) return;
            if (textBox_price == null) return;

            string priceText = textBox_price.Text.Replace(',', '.');
            if (float.TryParse(priceText, NumberStyles.Any, CultureInfo.InvariantCulture, out float p))
            {
                int qty = numeric_inSt != null ? (int)numeric_inSt.Value : 0;
                StoreList.Add(new Product(textBox_model.Text, qty, p));
                MessageBox.Show("Додано!");

                if (numeric_inSt != null) numeric_inSt.Enabled = false;
                if (textBox_model != null) textBox_model.Enabled = false;
                if (textBox_price != null) textBox_price.Enabled = false;
                if (add_list != null) add_list.Enabled = false;

                RefreshAllSources();
            }
            else
            {
                MessageBox.Show("Некоректна ціна!");
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
                this.Close();
            }
        }

        public void dataGrid_bslist_CellClick(object sender, DataGridViewCellEventArgs e) { }
        public void dataGrid_DataTable_CellClick(object sender, DataGridViewCellEventArgs e) { }
        public void dataGrid_BindingSource_CellClick(object sender, DataGridViewCellEventArgs e) { }

        private void ValidatePriceInput(object sender, KeyPressEventArgs e)
        {
            var txt = sender as TextBox;
            if (txt == null) return;

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
            if (txt == null || string.IsNullOrWhiteSpace(txt.Text)) return;

            string rawText = txt.Text.Replace(',', '.');
            if (float.TryParse(rawText, NumberStyles.Any, CultureInfo.InvariantCulture, out float val))
            {
                txt.Text = val.ToString("F2");
            }
        }

        private void textBox_price_KeyPress(object sender, KeyPressEventArgs e) => ValidatePriceInput(sender, e);
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e) => ValidatePriceInput(sender, e);
        public void textBox_price_Leave(object sender, EventArgs e) => FormatPriceOnLeave(sender, e);
        public void textBox2_Leave(object sender, EventArgs e) => FormatPriceOnLeave(sender, e);
    }
}