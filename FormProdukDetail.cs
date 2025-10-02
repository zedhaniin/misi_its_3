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

namespace ProjectKI
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void FormProdukDetail_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = Koneksi.GetConnection())
            {
                try
                { 
                    conn.Open();
                    string query = "SELECT Id, NamaKategori FROM Kategori";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    Dictionary<int, string> kategoriDict = new Dictionary<int,
                    string>();
                    while (reader.Read())
                    {
                        kategoriDict.Add((int)reader["Id"],
                        reader["NamaKategori"].ToString());
                    }
                cmbKategori.DataSource = new BindingSource(kategoriDict, null);
                cmbKategori.DisplayMember = "Value";
                cmbKategori.ValueMember = "Key";
                reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat kategori: " + ex.Message);
                }
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void lblKategori_Click(object sender, EventArgs e)
        {

        }

        private void cmbKategori_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txtHarga.Text) || !decimal.TryParse(txtHarga.Text, out _))
            {
                MessageBox.Show("Harga harus diisi dengan angka!");
                txtHarga.Focus();
                return;
            }

           
            if (string.IsNullOrWhiteSpace(txtStok.Text) || !int.TryParse(txtStok.Text, out _))
            {
                MessageBox.Show("Stok harus diisi dengan angka!");
                txtStok.Focus();
                return;
            }

            using (SqlConnection conn = Koneksi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO Produk (NamaProduk, Harga, Stok, KategoriId, Deskripsi) 
                                     VALUES (@nama, @harga, @stok, @kategori, @deskripsi)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nama", txtNamaProduk.Text);
                    cmd.Parameters.AddWithValue("@harga", Convert.ToDecimal(txtHarga.Text));
                    cmd.Parameters.AddWithValue("@stok", Convert.ToInt32(txtStok.Text));
                    cmd.Parameters.AddWithValue("@deskripsi", txtDeskripsi.Text);
                    if (cmbKategori.SelectedItem != null)
                    {
                        cmd.Parameters.AddWithValue("@kategori", ((KeyValuePair<int, string>)cmbKategori.SelectedItem).Key);
                    }
                    else
                    {
                        MessageBox.Show("Pilih kategori terlebih dahulu.");
                        return;
                    }
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Produk berhasil ditambahkan!");
                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menambahkan produk: " + ex.Message);
                }
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void txtStok_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
