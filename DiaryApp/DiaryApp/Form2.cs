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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=VeriTabanı;Integrated Security=True;Pooling=False");
        //Sql bağlantı cümlemizi oluşturduk.



        private void button1_Click(object sender, EventArgs e)
        {
            //kayıt ol butonumuza basınca olacak olanlar:
            try
            {
                //veri işlemleri sırasında bir hata olabilmesine yönelik try catch bloğu içinde yapıyorum.
                baglanti.Open();
                //bağlanıyı açtık
                string kayit = "insert into Giris(KullaniciAdi,Sifre) values (@kullaniciadi,@sifre)";
                //gerçekleştireceğimiz komutu yazdık.
                SqlCommand komut = new SqlCommand(kayit, baglanti);
                //oluşturduğumuz kayiti baglantiya gönderiyoruz.
                komut.Parameters.AddWithValue("@KullaniciAdi", textBox1.Text);
                komut.Parameters.AddWithValue("@Sifre", textBox2.Text);
                //verileri aktarıyoruz.
                komut.ExecuteNonQuery();
                //veri tabanında değişiklik yapacak olan komut işlemini gerçekleştiriyoruz.
                baglanti.Close();
                //baglantimizi kapatıyoruz.
                MessageBox.Show("Kayıt işlemi gerçekleşti.", "Başarılı Kayıt");
                Form1 frm1 = new Form1();
                frm1.Show();
                this.Hide();

            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem sırasında hata oluştu! " + hata.Message, "Hata!" );
            }
        }

        
    }
}
