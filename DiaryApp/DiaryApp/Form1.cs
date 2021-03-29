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

namespace DiaryApp
{
    public partial class Form1 : Form
    {

        SqlConnection baglanti = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=VeriTabanı;Integrated Security=True;Pooling=False");

        public Form1()
        {
            InitializeComponent();
            
        }

        private void DiaryApp_Load(object sender, EventArgs e)
        {
            notifyIcon1.ShowBalloonTip(2000,"DiaryApp","DiaryApp çalışıyor",ToolTipIcon.None);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Hide();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string kayit = "select * from Giris where KullaniciAdi= '"+ textBox1.Text+"' and Sifre= '"+ textBox2.Text+"' ";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            SqlDataReader oku = komut.ExecuteReader();

            if (oku.Read())
            {
                MessageBox.Show("Giriş başarılı!");
                Form3 anasayfa = new Form3();
                anasayfa.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Hata!");
            }

            baglanti.Close();
        }

    
        
    }

}