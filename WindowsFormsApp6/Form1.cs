using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti= new SqlConnection(@"Data Source=DESKTOP-P1SH4NN\SQL;Initial Catalog=Rehber;Integrated Security=True");

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da= new SqlDataAdapter("select * from Tbl_kisiler",baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        void temizle()
        {
            txtAd.Text = "";
            txtId.Text = "";
            txtMail.Text = "";
            txtSoyad.Text = "";
            mskTel.Text = "";
            txtAd.Focus();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           
            listele();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into tbl_kisiler (kisiAdi,kisiSoyadi,Numara,Mail) values (@p1,@p2,@p3,@p4)",baglanti);
            komut.Parameters.AddWithValue("@p1",txtAd.Text);
            komut.Parameters.AddWithValue("@p2",txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3",mskTel.Text);
            komut.Parameters.AddWithValue("@p4", txtMail.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kaydedildi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            listele();
            temizle();
           
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("update  tbl_kisiler set Kisiadi=@p1,kisiSoyadi=@p2,Numara=@p3,MAil=@p4 where ID=@p5",baglanti);
            komut2.Parameters.AddWithValue("@p1", txtAd.Text);
            komut2.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut2.Parameters.AddWithValue("@p3", mskTel.Text);
            komut2.Parameters.AddWithValue("@p4", txtMail.Text);
            komut2.Parameters.AddWithValue("@p5", txtId.Text);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncellendi...");
            listele();
            temizle();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtId.Text=dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAd.Text=dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            mskTel.Text=dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtMail.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
           DialogResult dialog= new DialogResult();
            dialog = MessageBox.Show("Emin misiniz ?","Bilgi",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komutsil = new SqlCommand("delete from Tbl_kisiler where ID=@p1", baglanti);
                komutsil.Parameters.AddWithValue("@p1", txtId.Text);
                komutsil.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();
                temizle();
            }
            else
            {
                MessageBox.Show("Silinmedi.");
            }
           
            
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtMail.Text = "";
            mskTel.Text = "";

        }
    }
}
