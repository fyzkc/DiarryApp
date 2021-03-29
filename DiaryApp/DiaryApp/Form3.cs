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
using System.Drawing.Imaging;
using System.IO;

namespace DiaryApp
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=VeriTabanı;Integrated Security=True;Pooling=False");


        private void Form3_Load(object sender, EventArgs e)
        {

            progressBar2.Hide(); //başlagıçta kb foto kısmındaki gözükmeyecek
            progressBar3.Hide(); //başlagıçta ajanda foto kısmındaki gözükmeyecek
            //Form açıldığında verilerin gözükmesi için

            SqlDataAdapter da = new SqlDataAdapter("Select isim,soyIsim,dogumtarihi,telefonnum,sehirulke,meslek,hobiler,muziktur,fotograf from KisiselBilgiler", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;


            SqlDataAdapter da4 = new SqlDataAdapter("Select Yazilar,Tarihler,Modlar from GunlukYazilari", baglanti);
            DataTable dt4 = new DataTable();
            da4.Fill(dt4);
            dataGridView3.DataSource = dt4;

            SqlDataAdapter da6 = new SqlDataAdapter("Select dogumgunleri,kisiIsimleri,fotograf from Ajanda", baglanti);
            DataTable dt6 = new DataTable();
            da6.Fill(dt6);
            dataGridView2.DataSource = dt6;

            dateTimePicker1.MaxDate = new DateTime(9998, 12, 31);
            dateTimePicker1.MinDate = new DateTime(1753, 01, 1);
            dateTimePicker2.MaxDate = new DateTime(9998, 12, 31);
            dateTimePicker2.MinDate = new DateTime(1753, 01, 1);
            dateTimePicker3.MaxDate = new DateTime(9998, 12, 31);
            dateTimePicker3.MinDate = new DateTime(1753, 01, 1);

            label14.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e) //Kişisel bilgiler fotoğraf ekleme butonu
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            timer2.Start();
            progressBar2.Show();
        }

        private void button6_Click(object sender, EventArgs e) //Kişisel Bilgiler Kaydet Butonu
        {
            try
            {
                baglanti.Open();
                string kayit = "insert into KisiselBilgiler(isim,soyIsim,dogumtarihi,telefonnum,sehirulke,meslek,hobiler,muziktur,fotograf) values (@0,@1,@2,@3,@4,@5,@6,@7,@8)";
                SqlCommand komut = new SqlCommand(kayit, baglanti);
                komut.Parameters.AddWithValue("@0", textBox1.Text); //isim
                komut.Parameters.AddWithValue("@1", textBox2.Text); //soyisim
                komut.Parameters.AddWithValue("@2", dateTimePicker2.Text); //dogum
                komut.Parameters.AddWithValue("@3", maskedTextBox1.Text); //telefon
                komut.Parameters.AddWithValue("@4", textBox6.Text); //sehirulke
                komut.Parameters.AddWithValue("@5", textBox3.Text); //meslek
                komut.Parameters.AddWithValue("@6", textBox4.Text); //hobiler
                komut.Parameters.AddWithValue("@7", textBox5.Text); //muziktur
                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                komut.Parameters.AddWithValue("@8", ms.ToArray());
                komut.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter("Select * from KisiselBilgiler", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                baglanti.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem sırasında hata oluştu! " + hata.Message, "Hata!");
            }
        }



        private void timer2_Tick(object sender, EventArgs e) //Kişisel bilgilerde progress bar
        {
            for (int i = 0; i < 101; i++)
            {
                progressBar2.Value = i;
            }
            timer2.Stop();
        }

        private void button2_Click(object sender, EventArgs e) //Kişisel bilgileri güncelleme
        {
            baglanti.Open();
            string guncelle = "Update KisiselBilgiler Set isim=@0,soyIsim=@1,dogumtarihi=@2,telefonnum=@3,sehirulke=@4,meslek=@5,hobiler=@6,muziktur=@7,fotograf=@8";
            SqlCommand komut = new SqlCommand(guncelle, baglanti);
            komut.Parameters.AddWithValue("@0", textBox1.Text);
            komut.Parameters.AddWithValue("@1", textBox2.Text);
            komut.Parameters.AddWithValue("@2", dateTimePicker2.Text);
            komut.Parameters.AddWithValue("@3", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@4", textBox6.Text);
            komut.Parameters.AddWithValue("@5", textBox3.Text);
            komut.Parameters.AddWithValue("@6", textBox4.Text);
            komut.Parameters.AddWithValue("@7", textBox5.Text);
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            komut.Parameters.AddWithValue("@8", ms.ToArray());
            komut.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter("Select * from KisiselBilgiler", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
            MessageBox.Show("Kayıt işlemi gerçekleşti.", "Başarılı Kayıt");

        }

        string secili = "";
        public void checks()
        {
            if (checkBox1.Checked)
                secili += checkBox1.Text + " ";
            if (checkBox2.Checked)
                secili += checkBox2.Text + " ";
            if (checkBox3.Checked)
                secili += checkBox3.Text + " ";
            if (checkBox4.Checked)
                secili += checkBox4.Text + " ";
            if (checkBox5.Checked)
                secili += checkBox5.Text + " ";
            if (checkBox6.Checked)
                secili += checkBox6.Text + " ";
            if (checkBox7.Checked)
                secili += checkBox7.Text + " ";
            if (checkBox8.Checked)
                secili += checkBox8.Text + " ";
        }

        private void button8_Click(object sender, EventArgs e) //Günlük kaydetme butonu
        {
            baglanti.Open();
            string kayit = "insert into GunlukYazilari(Yazilar,Tarihler,Modlar) values (@0,@1,@2)";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            komut.Parameters.AddWithValue("@0", textBox7.Text); //yazilar
            komut.Parameters.AddWithValue("@1", dateTimePicker1.Text); //tarihler
            //MODLAR
            checks();
            komut.Parameters.AddWithValue("@2", secili);
            komut.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter("Select * from GunlukYazilari", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView3.DataSource = dt;
            baglanti.Close();
            MessageBox.Show("Kayıt işlemi gerçekleşti.", "Başarılı Kayıt");
        }

        private void textBox7_Validated(object sender, EventArgs e) //Günlük kısmı boş geçilmesin
        {
            try
            {
                if (textBox7.Text.Trim() == "")
                    errorProvider1.SetError(textBox7, "Bu alan boş geçilemez!");
                else
                    errorProvider1.Clear();
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem sırasında hata oluştu! " + hata.Message, "Hata!");
            }
        }

        private void groupBox4_Validating(object sender, CancelEventArgs e) //Modların hiç biri seçili değilse
        {
            if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false && checkBox5.Checked == false && checkBox6.Checked == false && checkBox7.Checked == false && checkBox8.Checked == false)
                errorProvider1.SetError(label13, "En az birini seçmelisiniz!");
            else
                errorProvider1.Clear();
        }



        private void dataGridView3_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e) //Seçilen günlüğü okuyabilmek için
        {
            textBox9.Text = dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString();
            label14.Visible = true;
            label14.Text = dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox10.Text = dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString();

        }

        private void button3_Click(object sender, EventArgs e) //Datagridview da seçilenleri silme
        {
            for (int i = 0; i < dataGridView3.SelectedRows.Count; i++)
            {
                baglanti.Open();
                string sil = "delete from GunlukYazilari where Yazilar= '" + dataGridView3.SelectedRows[i].Cells["Yazilar"].Value.ToString() + "' and Tarihler= '" + dataGridView3.SelectedRows[i].Cells["Tarihler"].Value.ToString() + "' and Modlar= '" + dataGridView3.SelectedRows[i].Cells["Modlar"].Value.ToString() + "' ";
                SqlCommand komut = new SqlCommand(sil, baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
            }
            MessageBox.Show("Silme işlemi gerçekleşti.", "Başarılı silme");
            SqlDataAdapter da = new SqlDataAdapter("Select * from GunlukYazilari", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView3.DataSource = dt;

            label14.Visible = false;
            textBox10.Clear();
            textBox9.Clear();
        }

        private void button4_Click(object sender, EventArgs e) //Ajandada fotoğraf ekleme butonu
        {
            openFileDialog3.ShowDialog();
            pictureBox2.ImageLocation = openFileDialog3.FileName;
            timer3.Start();
            progressBar3.Show();
        }
        private void timer3_Tick(object sender, EventArgs e) //Ajandadaki progress bar
        {
            for (int i = 0; i < 101; i++)
            {
                progressBar3.Value = i;
            }
            timer3.Stop();
        }

        private void button5_Click(object sender, EventArgs e) //Ajanda foto ekleme
        {
            try
            {
                baglanti.Open();
                string kayit = "insert into Ajanda(fotograf) values (@0)";
                SqlCommand komut = new SqlCommand(kayit, baglanti);
                MemoryStream ms = new MemoryStream();
                pictureBox2.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                komut.Parameters.AddWithValue("@0", ms.ToArray()); //foto
                komut.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter("Select * from Ajanda", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView3.DataSource = dt;
                baglanti.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem sırasında hata oluştu! " + hata.Message, "Hata!");
            }


        }
        private void button9_Click(object sender, EventArgs e) //Ajandayı kaydetme
        {
            try
            {
                baglanti.Open();
                string kayit = "insert into Ajanda(dogumgunleri,kisiIsimleri) values (@0,@1)";
                SqlCommand komut = new SqlCommand(kayit, baglanti);
                komut.Parameters.AddWithValue("@0", dateTimePicker3.Text); //dg
                komut.Parameters.AddWithValue("@1", textBox8.Text); //kişiisimleri
                komut.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter("Select * from Ajanda", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView3.DataSource = dt;
                baglanti.Close();
                MessageBox.Show("Kayıt işlemi gerçekleşti.", "Başarılı Kayıt");

            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem sırasında hata oluştu! " + hata.Message, "Hata!");
            }
        }

        
    }
}
