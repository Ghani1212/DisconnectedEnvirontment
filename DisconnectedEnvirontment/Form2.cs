using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisconnectedEnvirontment
{
    public partial class Form2 : Form
    {
        private string stringConnection = "data source=LAPTOP-3GHBBCII\\GHANIZUMAR;" + "database=DisEnv;User ID=sa;Password=ghani1211";
        private SqlConnection koneksi;

        public Form2()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringConnection);
            refreshform();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        private void refreshform()
        {
            textBox1.Text = "";
            textBox1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void dataGridView()
        {
            koneksi.Open();
            string str = "select nama_prodi from dbo.Prodi";
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            koneksi.Close();
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            dataGridView();
            button1.Enabled = false;
        }
        private void Add_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
        }
        private void Clear_Click(object sender, EventArgs e)
        {
            refreshform();
        }
        private void Save_Click(object sender, EventArgs e)
        {
            string nmProdi = textBox1.Text;

            if (nmProdi == "")
            {
                MessageBox.Show("Masukkan Nama Prodi", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                koneksi.Open();
                string str = "insert into dbo.prodi (nama_prodi)" + "values(@id)";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("id", nmProdi));
                cmd.ExecuteNonQuery();

                koneksi.Close();
                MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView();
                refreshform();
            }

        }
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 hu = new Form1();
            hu.Show();
            this.Hide();
        }
    }

}
