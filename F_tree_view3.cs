using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace vvod_danyh
{
    public partial class F_tree_view3 : Form
    {
        DataTable Table {  get; set; }
        DataGridView Grid => dataGridView1;//прив'язка до dataGridView1
        public F_tree_view3()
        {
            InitializeComponent();
            Table = new DataTable();
        }
        private void CreateTable()
        {
            Table.Columns.Add("name", typeof(string));
            Table.Columns.Add("price", typeof(decimal));
            Table.Columns.Add("garantee", typeof(ushort));//створили таблицю зі стовпцями

            Grid.DataSource = Table;
            
        }

        private void ConfigureGrid()
        {
            if (Grid.Columns.Count == 0)
                return;
            Grid.Columns[0].HeaderText = "Марка";
            Grid.Columns[1].HeaderText = "Вартість";
            Grid.Columns[2].HeaderText = "Гарантія\n(міс.)";

            //прибрали 0 стовпчик
            Grid.RowHeadersVisible = false;
            //прибрали останній рядок
            Grid.AllowUserToAddRows = false;

            Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            Grid.Columns[1].DefaultCellStyle.Format = "c2";
            Grid.Columns[2].DefaultCellStyle.Format = "n0";//формат
            Grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//вирівняли загололвки по центру
            Grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe", 12, FontStyle.Bold);//заголовок таблиці
            Grid.RowsDefaultCellStyle.Font = new Font("Tahoma", 12);
        }

        private void LoadBinFile(string fileName)
        {
            if (!System.IO.File.Exists(fileName))
            {
                MessageBox.Show("Файл не знайдено");
                return;
            }
            Table.Clear();//очищаємо старі дані
            using (var reader = new System.IO.BinaryReader(new System.IO.FileStream(fileName,
                System.IO.FileMode.Open), System.Text.Encoding.GetEncoding(1251)))
            {
                string tablename = reader.ReadString();//зчитали назву таблиці
                int columncount = reader.ReadInt32();//зчитали кількість рядків
                int rowcount = reader.ReadInt32();//зчитали кількість стовпчиків

                //зчитуємо назву стовпчиків
                string[] columnsname = new string[columncount];
                for (int i = 0; i < columncount; i++) 
                { 
                    columnsname[i] = reader.ReadString();
                }

                //оновлення структури таблиці
                if(Table.Columns.Count == 0)
                {
                    foreach(var colN in columnsname)
                    {
                        Table.Columns.Add(colN, typeof(String));
                    }
                }

                //читаємо рядки
                for(int i = 0;i < rowcount; i++)
                {
                    object[] rowdata = new object[columncount];
                    for(int j = 0; j < columncount; j++)
                    {
                        rowdata[j] = reader.ReadString();
                    }
                    //додаємо до datagrid
                    Table.Rows.Add(rowdata);
                }
                MessageBox.Show("Дані успішно зчитані");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadBinFile("Monitors.bin");
        }

        private void F_tree_view3_Load(object sender, EventArgs e)
        {
            CreateTable();
            ConfigureGrid();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            treeView_monitors.Nodes.Clear();
            foreach(DataRow row in Table.Rows)
            {
                TreeNode node = new TreeNode(row[0].ToString());//вузел дерева, який в 0-му стовпчику
                for(int i = 1; i < Table.Columns.Count;i++)
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
            //DialogResult rez = MessageBox.Show("Ви дійсно бажаєте видалити вузол " + 
            //    treeView_monitors.SelectedNode.Text + '?', "Попередження!", 
            //    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if(rez == DialogResult.Yes)
            //    treeView_monitors.SelectedNode.Remove();
            //textBox_Node.Clear();
            RemoveCheckNodes(treeView_monitors.Nodes);
        }

        void RemoveCheckNodes(TreeNodeCollection Nodes)
        {
            // Йдемо з кінця до початку (зворотний цикл), щоб не збивалися індекси
            for (int i = Nodes.Count - 1; i >= 0; i--)
            {
                TreeNode node = Nodes[i];

                if (node.Checked)
                {
                    // Видаляємо вузол безпосередньо з тієї колекції, де він знаходиться
                    Nodes.RemoveAt(i);
                }
                else
                {
                    // Якщо вузол не відмічений, перевіряємо його дітей (рекурсія)
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
    }
}
