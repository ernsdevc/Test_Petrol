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

namespace Test_Petrol
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=MACHINEX\MSSQLSERVER01;Initial Catalog=TestBenzin;Integrated Security=True");

        void Kasa()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM TBLKasa", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblKasa.Text = dr[0].ToString();
            }
            baglanti.Close();
        }

        void FiyatListesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBLBenzin", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);

            lblKursunsuz95.Text = dt.Rows[0].Field<decimal>("SatisFiyat").ToString();
            progressBar1.Value = dt.Rows[0].Field<int>("Stok");
            lblKursunsuz95Lt.Text = dt.Rows[0].Field<int>("Stok").ToString();
            lblKursunsuz97.Text = dt.Rows[1].Field<decimal>("SatisFiyat").ToString();
            progressBar2.Value = dt.Rows[1].Field<int>("Stok");
            lblKursunsuz97Lt.Text = dt.Rows[1].Field<int>("Stok").ToString();
            lblEuroDizel10.Text = dt.Rows[2].Field<decimal>("SatisFiyat").ToString();
            progressBar3.Value = dt.Rows[2].Field<int>("Stok");
            lblEuroDizel10Lt.Text = dt.Rows[2].Field<int>("Stok").ToString();
            lblYeniProDizel.Text = dt.Rows[3].Field<decimal>("SatisFiyat").ToString();
            progressBar4.Value = dt.Rows[3].Field<int>("Stok");
            lblYeniProDizelLt.Text = dt.Rows[3].Field<int>("Stok").ToString();
            lblGaz.Text = dt.Rows[4].Field<decimal>("SatisFiyat").ToString();
            progressBar5.Value = dt.Rows[4].Field<int>("Stok");
            lblGazLt.Text = dt.Rows[4].Field<int>("Stok").ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FiyatListesi();
            Kasa();
        }

        string TutarHesapla(double petrolTur,double litre)
        {
            double tutar = petrolTur * litre;
            return tutar.ToString(); ;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            txtKursunsuz95Tutar.Text = TutarHesapla(Convert.ToDouble(lblKursunsuz95.Text), Convert.ToDouble(numericUpDown1.Value));
            if (numericUpDown1.Value != 0)
            {
                numericUpDown2.Enabled = false;
                numericUpDown3.Enabled = false;
                numericUpDown4.Enabled = false;
                numericUpDown5.Enabled = false;
            }
            else
            {
                numericUpDown2.Enabled = true;
                numericUpDown3.Enabled = true;
                numericUpDown4.Enabled = true;
                numericUpDown5.Enabled = true;
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            txtKursunsuz97Tutar.Text = TutarHesapla(Convert.ToDouble(lblKursunsuz97.Text), Convert.ToDouble(numericUpDown2.Value));
            if (numericUpDown2.Value != 0)
            {
                numericUpDown1.Enabled = false;
                numericUpDown3.Enabled = false;
                numericUpDown4.Enabled = false;
                numericUpDown5.Enabled = false;
            }
            else
            {
                numericUpDown1.Enabled = true;
                numericUpDown3.Enabled = true;
                numericUpDown4.Enabled = true;
                numericUpDown5.Enabled = true;
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            txtEuroDizel10Tutar.Text = TutarHesapla(Convert.ToDouble(lblEuroDizel10.Text), Convert.ToDouble(numericUpDown3.Value));
            if (numericUpDown3.Value != 0)
            {
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;
                numericUpDown4.Enabled = false;
                numericUpDown5.Enabled = false;
            }
            else
            {
                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = true;
                numericUpDown4.Enabled = true;
                numericUpDown5.Enabled = true;
            }
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            txtYeniProDizelTutar.Text = TutarHesapla(Convert.ToDouble(lblYeniProDizel.Text), Convert.ToDouble(numericUpDown4.Value));
            if (numericUpDown4.Value != 0)
            {
                numericUpDown2.Enabled = false;
                numericUpDown3.Enabled = false;
                numericUpDown1.Enabled = false;
                numericUpDown5.Enabled = false;
            }
            else
            {
                numericUpDown2.Enabled = true;
                numericUpDown3.Enabled = true;
                numericUpDown1.Enabled = true;
                numericUpDown5.Enabled = true;
            }
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            txtGazTutar.Text = TutarHesapla(Convert.ToDouble(lblGaz.Text), Convert.ToDouble(numericUpDown5.Value));
            if (numericUpDown5.Value != 0)
            {
                numericUpDown2.Enabled = false;
                numericUpDown3.Enabled = false;
                numericUpDown4.Enabled = false;
                numericUpDown1.Enabled = false;
            }
            else
            {
                numericUpDown2.Enabled = true;
                numericUpDown3.Enabled = true;
                numericUpDown4.Enabled = true;
                numericUpDown1.Enabled = true;
            }
        }

        void DepoDoldur(string petrolTur, decimal litre, string fiyat)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO TBLHareket (Plaka,PetrolTur,Litre,Fiyat) VALUES(@p1,@p2,@p3,@p4)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtPlaka.Text);
            komut.Parameters.AddWithValue("@p2", petrolTur);
            komut.Parameters.AddWithValue("@p3", litre);
            komut.Parameters.AddWithValue("@p4", Convert.ToDecimal(fiyat));
            komut.ExecuteNonQuery();

            SqlCommand komut2 = new SqlCommand("UPDATE TBLKasa SET Miktar=Miktar+@p1", baglanti);
            komut2.Parameters.AddWithValue("@p1", Convert.ToDecimal(fiyat));
            komut2.ExecuteNonQuery();

            SqlCommand komut3 = new SqlCommand("UPDATE TBLBenzin SET Stok=Stok-@p1 WHERE PetrolTur=@p2", baglanti);
            komut3.Parameters.AddWithValue("@p1", Convert.ToInt32(litre));
            komut3.Parameters.AddWithValue("@p2", petrolTur);
            komut3.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Satış Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            FiyatListesi();
            Kasa();
        }

        private void btnDepoDoldur_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value != 0)
            {
                DepoDoldur("Kursunsuz95",numericUpDown1.Value,txtKursunsuz95Tutar.Text);
            }
            else if (numericUpDown2.Value != 0)
            {
                DepoDoldur("Kursunsuz97", numericUpDown2.Value, txtKursunsuz97Tutar.Text);
            }
            else if (numericUpDown3.Value != 0)
            {
                DepoDoldur("EuroDizel10", numericUpDown3.Value, txtEuroDizel10Tutar.Text);
            }
            else if (numericUpDown4.Value != 0)
            {
                DepoDoldur("YeniProDizel", numericUpDown4.Value, txtYeniProDizelTutar.Text);
            }
            else if (numericUpDown5.Value != 0)
            {
                DepoDoldur("Gaz", numericUpDown5.Value, txtGazTutar.Text);
            }
        }
    }
}
