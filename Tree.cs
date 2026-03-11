using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Navchpract_2
{
    public partial class Tree : Form
    {
        private DataTable Table { get; set; }
        private DataGridView Grid => dataGridView1;

        private bool isFormDragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public Tree()
        {
            InitializeComponent();
            Table = new DataTable();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void CreateTable()
        {
            Table.Columns.Add("name", typeof(string));
            Table.Columns.Add("price", typeof(decimal));
            Table.Columns.Add("garantee", typeof(ushort));

            Grid.DataSource = Table;
        }

        private void ConfigureGrid()
        {
            if (Grid.Columns.Count == 0)
                return;
            Grid.Columns[0].HeaderText = "Марка";
            Grid.Columns[1].HeaderText = "Вартість";
            Grid.Columns[2].HeaderText = "Гарантія\n(міс.)";

            Grid.RowHeadersVisible = false;
            Grid.AllowUserToAddRows = false;
            Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            Grid.Columns[1].DefaultCellStyle.Format = "c2";
            Grid.Columns[2].DefaultCellStyle.Format = "n0";
            Grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe", 12, FontStyle.Bold);
            Grid.RowsDefaultCellStyle.Font = new Font("Tahoma", 12);
        }

        private void LoadBinFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                MessageBox.Show("Файл не знайдено");
                return;
            }
            Table.Clear();
            using (var reader = new BinaryReader(new FileStream(fileName, FileMode.Open), System.Text.Encoding.GetEncoding(1251)))
            {
                string tablename = reader.ReadString();
                int columncount = reader.ReadInt32();
                int rowcount = reader.ReadInt32();

                string[] columnsname = new string[columncount];
                for (int i = 0; i < columncount; i++)
                {
                    columnsname[i] = reader.ReadString();
                }

                if (Table.Columns.Count == 0)
                {
                    foreach (var colN in columnsname)
                    {
                        Table.Columns.Add(colN, typeof(String));
                    }
                }

                for (int i = 0; i < rowcount; i++)
                {
                    object[] rowdata = new object[columncount];
                    for (int j = 0; j < columncount; j++)
                    {
                        rowdata[j] = reader.ReadString();
                    }
                    Table.Rows.Add(rowdata);
                }
                MessageBox.Show("Дані успішно зчитані");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadBinFile("Monitors.bin");
        }

        private void Tree_Load(object sender, EventArgs e)
        {
            pbBack.BackColor = Color.Transparent;
            pbExit.BackColor = Color.Transparent;

            CreateTable();
            ConfigureGrid();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            treeView_monitors.Nodes.Clear();
            foreach (DataRow row in Table.Rows)
            {
                TreeNode node = new TreeNode(row[0].ToString());
                for (int i = 1; i < Table.Columns.Count; i++)
                {
                    node.Nodes.Add(row[i].ToString());
                }
                treeView_monitors.Nodes.Add(node);
            }
            Grid.Enabled = false;
        }

        private void treeView_monitors_AfterSelect(object sender, TreeViewEventArgs e)
        {
            textBox_Node.Clear();
            textBox_Node.Text = treeView_monitors.SelectedNode.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TreeNode tr = new TreeNode(textBox_Node.Text);
            try
            {
                if (textBox_Node.Text == "")
                    throw new Exception("Заповніть поле даних");
                if (!textBox_Node.Modified)
                    throw new Exception("Змініть дані");
                treeView_monitors.Nodes.Add(tr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            textBox_Node.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TreeNode tr = new TreeNode(textBox_Node.Text);
            try
            {
                if (textBox_Node.Text == "")
                    throw new Exception("Заповніть поле даних");
                if (treeView_monitors.SelectedNode == null)
                    throw new Exception("Виберіть вузол вставки");
                if (!textBox_Node.Modified)
                    throw new Exception("Змініть поле даних");
                treeView_monitors.SelectedNode.Nodes.Add(tr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            textBox_Node.Clear();
            treeView_monitors.SelectedNode = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_Node.Text == "")
                    throw new Exception("Заповніть поле даних");
                if (!textBox_Node.Modified)
                    throw new Exception("Змініть поле даних");
                treeView_monitors.SelectedNode.Text = textBox_Node.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            textBox_Node.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RemoveCheckNodes(treeView_monitors.Nodes);
        }

        void RemoveCheckNodes(TreeNodeCollection Nodes)
        {
            for (int i = Nodes.Count - 1; i >= 0; i--)
            {
                TreeNode node = Nodes[i];

                if (node.Checked)
                {
                    Nodes.RemoveAt(i);
                }
                else
                {
                    if (node.Nodes.Count > 0)
                    {
                        RemoveCheckNodes(node.Nodes);
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            treeView_monitors.Nodes.Clear();
        }

        private void pbBack_Click(object sender, EventArgs e)
        {
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