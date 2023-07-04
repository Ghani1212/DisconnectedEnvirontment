using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace DisconnectedEnvirontment
{
    public partial class Form3 : Form
    {
        private string stringConnection = "data source=LAPTOP-3GHBBCII\\GHANIZUMAR;" + "database=DisEnv;User ID=sa;Password=ghani1211";
        private SqlConnection koneksi;
        private string nim, nama, alamat, jk, prodi;
        private DateTime tgl;
        BindingSource customersBindingSource = new BindingSource();

        public Form3()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringConnection);
            this.bindingNavigator1.BindingSource = this.customersBindingSource;
            refreshform();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
        private void FormDataMahasiswa_Load()
        {
            koneksi.Open();
            SqlDataAdapter dataAdapter1 = new SqlDataAdapter(
                new SqlCommand(
                    "Select m.nim, m.nama_mahasiswa, " + "m.alamat, m.jenis_kelamin, m.tgl_lahir, p.nama_prodi From dbo.Mahasiswa m " +
                    "join dbo.Prodi p on m.id_prodi = p.id_prodi",
                    koneksi
                )
            );
            DataSet ds = new DataSet();
            dataAdapter1.Fill(ds);

            this.customersBindingSource.DataSource = ds.Tables[0];
            this.textBox1.DataBindings.Add(
                new Binding("Text", this.customersBindingSource, "NIM", true));
            this.textBox2.DataBindings.Add(
                new Binding("Text", this.customersBindingSource, "nama_mahasiswa", true));
            this.textBox3.DataBindings.Add(
                new Binding("Text", this.customersBindingSource, "alamat", true));
            this.comboBox1.DataBindings.Add(new Binding("Text", this.customersBindingSource, "jenis_kelamin", true));
            this.dateTimePicker1.DataBindings.Add(new Binding("Text", this.customersBindingSource, "tgl_lahir", true));
            this.comboBox2.DataBindings.Add(new Binding("Text", this.customersBindingSource, "nama_prodi", true));
            koneksi.Close();
        }
        private void clearBinding()
        {
            this.textBox1.DataBindings.Clear();
            this.textBox2.DataBindings.Clear();
            this.textBox3.DataBindings.Clear();
            this.comboBox1.DataBindings.Clear();
            this.dateTimePicker1.DataBindings.Clear();
            this.comboBox2.DataBindings.Clear();
        }
        private void refreshform()
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            comboBox1.Enabled = false;
            textBox3.Enabled = false;
            dateTimePicker1.Enabled = false;
            comboBox2.Enabled = false;
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
            clearBinding();
            FormDataMahasiswa_Load();
        }
        private void Prodicbx()
        {
            koneksi.Open();
            string str = "select nama_prodi from dbo.Prodi";
            SqlCommand cmd = new SqlCommand(str, koneksi);
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmd.ExecuteReader();
            koneksi.Close();
            comboBox2.DisplayMember = "nama_prodi";
            comboBox2.ValueMember = "id_prodi";
            comboBox2.DataSource = ds.Tables[0];
        }
        private void Jkcbx()
        {
            koneksi.Open();
            string str = "select jenis_kelamin from dbo.mahasiswa";
            SqlCommand cmd = new SqlCommand(str, koneksi);
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmd.ExecuteReader();
            koneksi.Close();
            comboBox1.DisplayMember = "jenis_kelamin";
            comboBox1.ValueMember = "jenis_kelamin";
            comboBox1.DataSource = ds.Tables[0];
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            dateTimePicker1.Value = DateTime.Today;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            comboBox1.Enabled = true;
            Jkcbx();
            textBox3.Enabled = true;
            dateTimePicker1.Enabled = true;
            comboBox2.Enabled = true;
            Prodicbx();
            button2.Enabled = true;
            button3.Enabled = true;
            button1.Enabled = false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            nim = label1.Text;
            nama = label2.Text;
            jk = comboBox1.Text;    
            alamat = label4.Text;
            tgl = dateTimePicker1.Value;
            prodi = comboBox2.Text;
            int hs = 0;
            koneksi.Open();
            string strs = "select id_prodi from dbo.Prodi where nama_prodi = @dd";
            SqlCommand cm = new SqlCommand(strs, koneksi);
            cm.CommandType = CommandType.Text;
            cm.Parameters.Add(new SqlParameter("@dd", prodi));
            SqlDataReader dr = cm.ExecuteReader();

            while (dr.Read())
            {
                hs = int.Parse(dr["id_prodi"].ToString());
            }
            dr.Close();

            string str = "insert into dbo.Mahasiswa (nim, nama_mahasiswa, jenis_kelamin, alamat, tgl_lahir, id_prodi) " +
                         "values (@NIM, @Nm, @Jk, @Al, @Tgll, @Idp)";
            SqlCommand cmd = new SqlCommand(str, koneksi);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@NIM", nim));
            cmd.Parameters.Add(new SqlParameter("@Nm", nama));
            cmd.Parameters.Add(new SqlParameter("@Jk", jk));
            cmd.Parameters.Add(new SqlParameter("@Al", alamat));
            cmd.Parameters.Add(new SqlParameter("@Tgll", tgl));
            cmd.Parameters.Add(new SqlParameter("@Idp", hs));
            cmd.ExecuteNonQuery();
            koneksi.Close();

            MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            refreshform();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            refreshform();
        }

        private void FormDataMahasiswa_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Hide();
        }
    }

}
