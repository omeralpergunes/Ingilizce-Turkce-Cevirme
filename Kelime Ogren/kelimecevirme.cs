using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace Kelime_Ogren
{
    public partial class kelimecevirme : Form
    {
        public kelimecevirme()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Ömer Alper Güneş\Desktop\dbSozluk.accdb");

        Random rast = new Random();
        int sure = 90;
        int kelime = 0;

        void getir()
        {
            int sayi;
            sayi = rast.Next(1, 2490);
            LblCevap.Text = sayi.ToString();

            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Select * from sozluk where id=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", sayi);
            OleDbDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Txtİngilizce.Text = dr[1].ToString();
                LblCevap.Text = dr[2].ToString();
            }
            baglanti.Close();
        }
        private void kelimecevirme_Load(object sender, EventArgs e)
        {
            getir();
            timer1.Start();
        }

        private void TxtTürkçe_TextChanged(object sender, EventArgs e)
        {
            if (TxtTürkçe.Text == LblCevap.Text)
            {
                kelime++;
                LblKelime.Text = kelime.ToString();
                getir();
                TxtTürkçe.Clear();

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sure--;
            LblSüre.Text = sure.ToString();
            if (sure == 0)
            {
                TxtTürkçe.Enabled = false;
                Txtİngilizce.Enabled = false;
                timer1.Stop();
            }
        }
    }
}
