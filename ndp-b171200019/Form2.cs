/************************************************************************************************************************************
**                                                        SAKARYA ÜNİVERSİTESİ
**                                              BİLGİSAYAR VE BİLİŞİM BİLİMLERİ FAKÜLTESİ
**                                               BİLİŞİM SİSTEMLERİ MÜHENDİSLİĞİ BÖLÜMÜ
**                                                  NESNEYE DAYALI PROGRAMLAMA DERSİ
**                                                        2019-2020 YAZ DÖNEMİ
**
**                                                   PROJE NUMARASI.........:01
**                                                   ÖĞRENCİ ADI............:SILA ÖZTEKİN
**                                                   ÖĞRENCİ NUMARASI.......:B171200019
**                                                   DERSİN ALINDIĞI GRUP...:A
***********************************************************************************************************************************/


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//System.Data.OleDb kütüphanesi tanımlanması
using System.Data.OleDb;
//System.Text.RegularExpression kütüphanesi tanımlanması
using System.Text.RegularExpressions;
using System.IO;
using ndp_b171200019.Model;
using ndp_b171200019.AppCode;

//PERSONEL TAKİP PROGRAMI

namespace ndp_b171200019
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //Form2 Ayarları
            pictureBox1.Height = 150;
            pictureBox1.Width = 150;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            //PictureBox'a resim getirme 
            //Eğer tc numarası kullaniciresimler dosyasıyla eşleşiyorsa pictureBoz1'e resim getir
            try
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Form1._tcno + ".jpg");


            }
            //Eşleşmiyorsa ekrana bosresim getir 
            catch 
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\bosresim.jpg");
            }
            //Kullanıcı işlemleri sekmesi ayarları
            this.Text = "YÖNETİCİ İŞLEMLERİ";
            label12.ForeColor = Color.DarkRed;
            label12.Text = Form1._adi + " " + Form1._soyadi;
            textBox1.MaxLength = 11; //Karakter sınırlaması
            textBox4.MaxLength = 8;
            toolTip1.SetToolTip(this.textBox1, "TC Kimlik No 11 Karakter Olmalı!");
            radioButton1.Checked = true;

            //Küçük karakterle yazılan harfleri otomatik olarak büyüğe çevirme
            textBox2.CharacterCasing = CharacterCasing.Upper;
            textBox3.CharacterCasing = CharacterCasing.Upper;
            textBox5.MaxLength = 10; //Karakter sınırlaması
            textBox6.MaxLength = 10;
            progressBar1.Maximum = 100; //ProgressBar nesneleri belirleme
            progressBar1.Value = 0;
            kullanicilari_goster();


            //Personel işlemleri sekmesi ayarları
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Width = 100;pictureBox2.Height = 100;
            pictureBox2.BorderStyle = BorderStyle.Fixed3D; //Çerçeve ayarı 3 boyutlu
            maskedTextBox1.Mask = "00000000000";   //Kullanıcıyı istenilen şablona uygun veri girişi yapmaya zorlama
            maskedTextBox2.Mask = "LL????????????????????"; //En az 2 karakter zorunlu
            maskedTextBox3.Mask = "LL????????????????????";
            maskedTextBox4.Mask = "0000"; //En az 4 karakterli maaş
            maskedTextBox4.Text = "0";
            maskedTextBox2.Text.ToUpper();
            maskedTextBox3.Text.ToUpper();

            comboBox1.Items.Add("İlköğretim");comboBox1.Items.Add("Ortaöğretim");
            comboBox1.Items.Add("Lise"); comboBox1.Items.Add("Üniversite"); comboBox1.Items.Add("Yükseklisans");

            comboBox2.Items.Add("Yönetici"); comboBox2.Items.Add("Memur"); comboBox2.Items.Add("Şoför"); comboBox2.Items.Add("İşçi");

            comboBox3.Items.Add("Arge"); comboBox3.Items.Add("Bilgi İşlem"); comboBox3.Items.Add("Muhasebe"); comboBox3.Items.Add("Satış"); comboBox3.Items.Add("Üretim"); comboBox3.Items.Add("Paketleme"); comboBox3.Items.Add("Nakliye");

            DateTime zaman = DateTime.Now;
            int Yil = int.Parse(zaman.ToString("yyyy"));
            int Ay = int.Parse(zaman.ToString("MM"));
            int Gun = int.Parse(zaman.ToString("dd"));

            dateTimePicker1.MinDate = new DateTime(1960, 1, 1); //Personele yaş sınırı koyuldu
            dateTimePicker1.MaxDate = new DateTime(Yil-18,Ay,Gun); //18 yaşından büyükler seçildi
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            radioButton3.Checked = true;
            personelleri_goster(); //Ekranda personel listesini görünür yapma
        }
        //Veri tabanı dosya yolu tanımlama
        OleDbConnection baglantim = new OleDbConnection
          ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=personel.accdb");
        
        private void kullanicilari_goster()
        {
            try
            {   //Tablodaki adları yerine [] içindeki şekliyle ekrana getirme
                //Ekrandaki datagridview içine tablodaki kullanici verilerini getirme
                  baglantim.Open();
                  OleDbDataAdapter kullanicilariListele = new OleDbDataAdapter
                  ("select tcno AS [TC KİMLİK NO],ad AS [ADI],soyad AS[SOYADI],yetki AS [YETKİ],kullaniciadi AS[KULLANICI ADI],parola AS[PAROLA] from kullanicilar Order By ad ASC", baglantim);
                  DataSet dsHafiza = new DataSet();
                  kullanicilariListele.Fill(dsHafiza);
                  dataGridView1.DataSource = dsHafiza.Tables[0];
                  baglantim.Close();

            }
            catch(Exception hataMesaj)
            {
                 MessageBox.Show(hataMesaj.Message, "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 baglantim.Close();
            }
        }



        private void personelleri_goster()
        {
            try
            {
                //Ekrandaki datagridview içine tablodaki personel verilerini getirme
                baglantim.Open();
                OleDbDataAdapter personelleriListele = new OleDbDataAdapter
                  ("select tcno AS [TC KİMLİK NO],ad AS [ADI],soyad AS[SOYADI],cinsiyet AS[CİNSİYETİ],mezuniyet AS[MEZUNİYETİ],dogumtarihi AS [DOĞUM TARİHİ],gorevi AS[GÖREVİ],gorevyeri AS [GÖREV YERİ],maasi AS [MAAŞI] from personeller Order By ad ASC", baglantim);
                DataSet dsHafiza = new DataSet();
                personelleriListele.Fill(dsHafiza);
                dataGridView2.DataSource = dsHafiza.Tables[0];
                baglantim.Close();

            }
            catch (Exception hataMesaj)
            {
                MessageBox.Show(hataMesaj.Message, "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {   //TC texbox'ına 11 haneden az veri girilirse hata mesajı ver
            if (textBox1.Text.Length < 11)
                errorProvider1.SetError(textBox1, "TC Kimlik No 11 karakter olmalı!");
            else
                errorProvider1.Clear();
        }
        //Kullanıcının rakam dışında ve backspace tuşu dışında karakter girmesini engelleme
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || (int)e.KeyChar == 8)
                e.Handled = false;
            else
                e.Handled = true;
        }
        //Kulanıcının harf,boşluk ve backspace tuşu dışında karakter girmesini engelleme
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {   //Kullaniciadi için kural belirleme, gerçekleşmediği halde uyarı verme
            if (textBox4.Text.Length != 8)
                errorProvider1.SetError(textBox4, "Kullanıcı adı 8 karakter olmalı!");
            else
                errorProvider1.Clear();
        }
        //Yalnızca harf,sayı ve backspace
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsDigit(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;
        }

        //Başlangıç değeri 0 olan parola skoru tanımlama
        int _parolaSkoru = 0;
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string parolaSeviyesi = ""; //Parolanın güçlü-zayıf seviyesini tutacak değişken
            int kucukHarfSkoru, buyukHarfSkoru = 0, rakamSkoru = 0, sembolSkoru = 0; //Parolada istenen kriterler
            string Sifre = textBox5.Text;
            //Türkçe karakterde sorun yaşamamak için sifre string ifadesindeki Türkçe karakterleri İngilizce karakterlere dönüştürmek gerekir
            string duzeltilmisSifre = "";
            duzeltilmisSifre = Sifre;
            duzeltilmisSifre = duzeltilmisSifre.Replace('İ','I');
            duzeltilmisSifre = duzeltilmisSifre.Replace('ı', 'i');
            duzeltilmisSifre = duzeltilmisSifre.Replace('Ç', 'C');
            duzeltilmisSifre = duzeltilmisSifre.Replace('ç', 'c');
            duzeltilmisSifre = duzeltilmisSifre.Replace('Ş', 'S');
            duzeltilmisSifre = duzeltilmisSifre.Replace('ş', 's');
            duzeltilmisSifre = duzeltilmisSifre.Replace('Ğ', 'G');
            duzeltilmisSifre = duzeltilmisSifre.Replace('ğ', 'g');
            duzeltilmisSifre = duzeltilmisSifre.Replace('Ü', 'U');
            duzeltilmisSifre = duzeltilmisSifre.Replace('ü', 'u');
            duzeltilmisSifre = duzeltilmisSifre.Replace('Ö', 'O');
            duzeltilmisSifre = duzeltilmisSifre.Replace('ö', 'o');
            if(Sifre != duzeltilmisSifre)
            {
                Sifre = duzeltilmisSifre;
                textBox5.Text = Sifre;
                MessageBox.Show("Paroladaki Türkçe karakterler İngilizce karakterlere dönüştürülmüştür!");
            }
            //1 küçük harf 10 puan,2 ve üzeri 20 puan
            int azKarakterSayisi = Sifre.Length - Regex.Replace(Sifre, "[a-z]", "").Length;
            kucukHarfSkoru = Math.Min(2, azKarakterSayisi) * 10;

            //1 büyük harf 10 puan,2 ve üzeri 20 puan
            int AZKarakterSayisi = Sifre.Length - Regex.Replace(Sifre, "[A-Z]", "").Length;
            buyukHarfSkoru = Math.Min(2, AZKarakterSayisi) * 10;

            //1 rakam 10 puan,2 ve üzeri 20 puan
            int rakamSayisi = Sifre.Length - Regex.Replace(Sifre, "[0-9]", "").Length;
            rakamSkoru = Math.Min(2, rakamSayisi) * 10;

            //1 sembol harf 10 puan,2 ve üzeri 20 puan
            int sembolSayisi = Sifre.Length - azKarakterSayisi - AZKarakterSayisi - rakamSayisi;
            sembolSkoru = Math.Min(2, sembolSayisi) * 10;

            _parolaSkoru = kucukHarfSkoru + buyukHarfSkoru + rakamSkoru + sembolSkoru;

            if (Sifre.Length == 9)
                _parolaSkoru += 10;
            else if (Sifre.Length == 10)
                _parolaSkoru += 20;

            if (kucukHarfSkoru == 0 || buyukHarfSkoru == 0 || rakamSkoru == 0 || sembolSkoru == 0)
                label22.Text = "Büyük harf,küçük harf,rakam ve sembol kullanılmalıdır!";
            if (kucukHarfSkoru != 0 && buyukHarfSkoru != 0 && rakamSkoru != 0 && sembolSkoru != 0)
                label22.Text = "";
            if (_parolaSkoru < 70)
                parolaSeviyesi = "Kabul edilemez!";
            else if (_parolaSkoru == 70 || _parolaSkoru == 80)
                parolaSeviyesi = "Güçlü";
            else if (_parolaSkoru == 90 || _parolaSkoru == 100)
                parolaSeviyesi = "Çok Güçlü";

            label8.Text = "%" + Convert.ToString(_parolaSkoru);
            label9.Text = parolaSeviyesi;
            progressBar1.Value = _parolaSkoru;
        }
        //Parolaların eşleşmesi
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text != textBox5.Text)
                errorProvider1.SetError(textBox6, "Parola tekrarı eşleşmiyor!");
            else
                errorProvider1.Clear();
        }
        //TextBoxlar içindeki yazıların temizlenmesi için method tanımlama
        private void topPage2_temizle()
        {
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); textBox5.Clear(); textBox6.Clear();
        }
        private void topPage1_temizle()
        {
            pictureBox2.Image = null;maskedTextBox1.Clear(); maskedTextBox2.Clear(); maskedTextBox3.Clear(); maskedTextBox4.Clear();
            comboBox1.SelectedIndex = -1; comboBox2.SelectedIndex = -1; comboBox3.SelectedIndex = -1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool kayitKontrol = false; //Tekrar eden kayıt olmadığı varsayıldı
            baglantim.Open();
            OleDbCommand selectSorgu = new OleDbCommand("select *from kullanicilar where tcno='" + textBox1.Text + "'", baglantim);
            OleDbDataReader kayitOkuma = selectSorgu.ExecuteReader();
            //Tekrar eden kayıt engelleme
            while (kayitOkuma.Read())
            {
                kayitKontrol = true;
                break;
            }
            baglantim.Close();


            //Veritabanında tekrar eden kayıt yoksa kayıt işlemi yapma
            if (kayitKontrol == false)
            {
                //TC kimlik no kontrolü
                if (textBox1.Text.Length < 11 || textBox1.Text == "")
                    label1.ForeColor = Color.Red;
                else
                    label1.ForeColor = Color.Black;
                //Adı veri kontrolü
                if (textBox2.Text.Length < 2 || textBox2.Text == "")
                    label2.ForeColor = Color.Red;
                else
                    label2.ForeColor = Color.Black;
                //Soyadı kontrolü
                if (textBox3.Text.Length < 2 || textBox1.Text == "")
                    label3.ForeColor = Color.Red;
                else
                    label3.ForeColor = Color.Black;
                //Kullanıcı Adı veri kontrolü
                if (textBox4.Text.Length != 8 || textBox1.Text == "")
                    label5.ForeColor = Color.Red;
                else
                    label5.ForeColor = Color.Black;
                //Parola veri kontrolü
                if (textBox5.Text == "" || _parolaSkoru < 70)
                    label6.ForeColor = Color.Red;
                else
                    label6.ForeColor = Color.Black;
                //Parola tekrar veri kontrolü
                if (textBox6.Text == "" || textBox5.Text != textBox6.Text)
                    label7.ForeColor = Color.Red;
                else
                    label7.ForeColor = Color.Black;


                if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text.Length > 1 && textBox3.Text != "" && textBox3.Text.Length > 1 && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox5.Text == textBox6.Text && _parolaSkoru >= 70)
                {
                    //Kullanıcı paneline kayıt yapma
                    try
                    {
                        UserModel User = new UserModel();
                        User.TC = textBox1.Text.Trim();
                        User.ad = textBox2.Text.Trim();
                        User.soyad = textBox3.Text.Trim();
                        User.yetki = (bool)(radioButton1.Checked) ? "Yönetici" : "Kullanıcı";
                        User.kullaniciAdi = textBox4.Text.Trim();
                        User.parola = textBox5.Text.Trim();

                        KullaniciIslemleri ki = new KullaniciIslemleri();
                        ki.ekle(User);
                        MessageBox.Show("Yeni kullanıcı kaydı oluşturuldu!", "PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        topPage2_temizle();
                    }
                    //Ekrana hata mesajı verme
                    catch (Exception hataMesaj)
                    {
                        MessageBox.Show(hataMesaj.Message);
                        baglantim.Close();
                    }

                }
                else
                {
                    MessageBox.Show("Kırmızı alanları yeniden doldurunuz.", "PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Girilen TC Kimlik No daha önceden kayıtlıdır.", "PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //Arama butonu işlemleri
        private void button2_Click(object sender, EventArgs e)
        {
            bool kayitAramaDurumu = false; //başlangıçta kayıt olmadığı varsayıldı
            if (textBox1.Text.Length == 11)
            {
                baglantim.Open();
                //Access tablosuyla baglanti kurma
                OleDbCommand selectSorgu = new OleDbCommand("select *from kullanicilar where tcno='" + textBox1.Text + "'", baglantim);
                //selectsorgusunun sonucunu kayitokuma datareader nesnesine tanımlama
                OleDbDataReader kayitOkuma = selectSorgu.ExecuteReader();
                //kayıt olup olmadığını kıyaslama
                while (kayitOkuma.Read())
                {
                    //Sorgu sonucunda kayitokumada kayıt varsa ve read true dönerse while çalışır
                    kayitAramaDurumu = true;
                    textBox2.Text = kayitOkuma.GetValue(1).ToString();
                    textBox3.Text = kayitOkuma.GetValue(2).ToString();
                    if (kayitOkuma.GetValue(3).ToString() == "Yönetici")
                        radioButton1.Checked = true;
                    else
                        radioButton2.Checked = true;
                    textBox4.Text = kayitOkuma.GetValue(4).ToString();
                    textBox5.Text = kayitOkuma.GetValue(5).ToString();
                    textBox6.Text = kayitOkuma.GetValue(5).ToString();
                    break;
                }
                //Kayıt bulunmadığı durumunda bilgilendirme mesajı
                if(kayitAramaDurumu == false)
                {
                    MessageBox.Show("Aranan kayıt bulunamadı!", "PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                baglantim.Close();
            
            }
            //11 karakter girilmediği durumunda
            else
            {
                MessageBox.Show("Lütfen 11 haneli TC Kimlik No giriniz!", "PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                topPage2_temizle();
            }
        }
        //Güncelleme İşlemleri
        private void button3_Click(object sender, EventArgs e)
        {
            string Yetki = "";
            
                //TC kimlik no kontrolü
                if (textBox1.Text.Length < 11 || textBox1.Text == "")
                    label1.ForeColor = Color.Red;
                else
                    label1.ForeColor = Color.Black;
                //Adı veri kontrolü
                if (textBox2.Text.Length < 2 || textBox2.Text == "")
                    label2.ForeColor = Color.Red;
                else
                    label2.ForeColor = Color.Black;
                //Soyadı kontrolü
                if (textBox3.Text.Length < 2 || textBox1.Text == "")
                    label3.ForeColor = Color.Red;
                else
                    label3.ForeColor = Color.Black;
                //Kullanıcı Adı veri kontrolü
                if (textBox4.Text.Length != 8 || textBox1.Text == "")
                    label5.ForeColor = Color.Red;
                else
                    label5.ForeColor = Color.Black;
                //Parola veri kontrolü
                if (textBox5.Text == "" || _parolaSkoru < 70)
                    label6.ForeColor = Color.Red;
                else
                    label6.ForeColor = Color.Black;
                //Parola tekrar veri kontrolü
                if (textBox6.Text == "" || textBox5.Text != textBox6.Text)
                    label7.ForeColor = Color.Red;
                else
                    label7.ForeColor = Color.Black;


                if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text.Length > 1 && textBox3.Text != "" && textBox3.Text.Length > 1 && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox5.Text == textBox6.Text && _parolaSkoru >= 70)
                {
                    if (radioButton1.Checked == true)
                         Yetki = "Yönetici";
                    else if (radioButton2.Checked == true)
                         Yetki = "Kullanıcı";
                    //Kullanıcı paneline kayıt yapma
                    try
                    {
                        baglantim.Open();
                        OleDbCommand guncelleKomutu = new OleDbCommand("update kullanicilar set ad='" + textBox2.Text + "',soyad='" + textBox3.Text + "',yetki='" + Yetki + "',kullaniciadi='" + textBox4.Text + "',parola='" + textBox5.Text + "' where tcno='"+textBox1.Text+"'", baglantim);
                        guncelleKomutu.ExecuteNonQuery(); //Güncelle komutlarının sonucunu veritabanına işleme
                        baglantim.Close();
                        MessageBox.Show("Kullanıcı bilgileri güncellendi!", "PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        kullanicilari_goster();
                    }
                    //Ekrana hata mesajı verme
                    catch (Exception hataMesaj)
                    {
                        MessageBox.Show(hataMesaj.Message,"PERSONEL TAKİP PROGRAMI",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        baglantim.Close();
                    }

                }
                else
                {
                    MessageBox.Show("Kırmızı alanları yeniden doldurunuz.", "PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        //Silme İşlemleri
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 11)
            {
                bool kayitAramaDurumu = false;
                baglantim.Open();
                OleDbCommand selectSorgu = new OleDbCommand("select *from kullanicilar where tcno='" + textBox1.Text + "'", baglantim);
                OleDbDataReader kayitOkuma = selectSorgu.ExecuteReader(); //Sorgu işleminin sonucu kayitokuma datareaderına eşitlendi.
                while (kayitOkuma.Read())
                {
                    kayitAramaDurumu = true; //Kayıt okuma gerçekleştiyse false yerine true döner.
                    OleDbCommand deleteSorgu = new OleDbCommand("delete from kullanicilar where tcno='" + textBox1.Text + "'", baglantim);  //silme işlemi gerçekleştirme
                    deleteSorgu.ExecuteNonQuery();
                    MessageBox.Show("Kullanıcı kaydı silindi!", "PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); //Kullanıcı bilgilendirme
                    baglantim.Close();
                    kullanicilari_goster();
                    topPage2_temizle();
                    break;
                }

                //Kayıt bulunmadığı durumda
                if (kayitAramaDurumu == false)
                {
                    MessageBox.Show("Silinecek Kayıt Bulunamadı!", "PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglantim.Close();
                    topPage2_temizle();
                }

            }
            //11 karakterden az veri girişi yapıldığında ekrana verilecek uyarı
            else
            {
                MessageBox.Show("Lütfen 11 karakterli TC Kimlik No giriniz!", "PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //Formu Temizleme İşlemleri
        private void button5_Click(object sender, EventArgs e)
        {
            topPage2_temizle();
        }

        //Personel işlemleri
        private void button6_Click(object sender, EventArgs e)
        //Resim seçme ve yükleme
        {
            OpenFileDialog resimSec = new OpenFileDialog();
            resimSec.Title = "Personel resmi seçiniz.";
            resimSec.Filter = "JPG Dosyalar (*.jpg) | *.jpg";  //Kullanıcı ekranda sadece .jpg dosyaları görebilecek
            if(resimSec.ShowDialog()==DialogResult.OK)
            {
                this.pictureBox2.Image = new Bitmap(resimSec.OpenFile());
            }
        }
        //Personel işlemleri kaydetme 
        private void button8_Click(object sender, EventArgs e)
        {
            string Cinsiyet = "";
            bool kayitKontrol = false;

            //Veri tabanında kayıtlı veri sorgusu
            baglantim.Open();
            OleDbCommand selectSorgu = new OleDbCommand("select *from personeller where tcno='" + maskedTextBox1.Text + "'", baglantim);
            OleDbDataReader kayitOkuma = selectSorgu.ExecuteReader();
           
            //Eğer kayıt varsa bool değişkeni true olur
            while (kayitOkuma.Read())
            {
                kayitKontrol = true;
                break;
            }
            baglantim.Close();

            if (kayitKontrol == false)
            {
                if (pictureBox2.Image == null) //PictureBox'a resim yüklendiyse siyah,boşsa gözat butonu kırmızı olur 
                    button6.ForeColor = Color.Red;
                else
                    button6.ForeColor = Color.Black;

                //Alanlar doldurulmadığında renk uyarısı
                if (maskedTextBox1.MaskCompleted == false)
                    label13.ForeColor = Color.Red;
                else
                    label13.ForeColor = Color.Black;

                if (maskedTextBox2.MaskCompleted == false)
                    label14.ForeColor = Color.Red;
                else
                    label14.ForeColor = Color.Black;

                if (maskedTextBox3.MaskCompleted == false)
                    label15.ForeColor = Color.Red;
                else
                    label15.ForeColor = Color.Black;
                if (comboBox1.Text == "")
                    label17.ForeColor = Color.Red;
                else
                    label17.ForeColor = Color.Black;
                if (comboBox2.Text == "")
                    label19.ForeColor = Color.Red;
                else
                    label19.ForeColor = Color.Black;
                if (comboBox3.Text == "")
                    label20.ForeColor = Color.Red;
                else
                    label20.ForeColor = Color.Black;

                if (maskedTextBox4.MaskCompleted == false)
                    label21.ForeColor = Color.Red;
                else
                    label21.ForeColor = Color.Black;
                if (int.Parse(maskedTextBox4.Text) < 1000)
                    label21.ForeColor = Color.Red;
                else
                    label21.ForeColor = Color.Black;
                //Kayıt işlemler
                if (pictureBox2.Image != null && maskedTextBox1.MaskCompleted != false && maskedTextBox2.MaskCompleted != false && maskedTextBox3.MaskCompleted != false && comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && maskedTextBox4.MaskCompleted != false)
                {
                    //Cinsiyet belirleme
                    if (radioButton3.Checked == true)
                        Cinsiyet = "Bay";
                    else if (radioButton4.Checked == true)
                        Cinsiyet = "Bayan";
                    try
                    {
                        PersonelModel Pm = new PersonelModel();
                        Pm.TC = maskedTextBox1.Text;
                        Pm.ad = maskedTextBox2.Text;
                        Pm.soyad = maskedTextBox3.Text;
                        Pm.cinsiyet = (bool)(radioButton1.Checked) ? "Bay" : "Erkek";
                        Pm.mezuniyet = comboBox1.Text;
                        Pm.dogumTarih = dateTimePicker1.Value;
                        Pm.gorevi = comboBox2.Text;
                        Pm.gorevYeri = comboBox3.Text;
                        Pm.maasi = Convert.ToInt32(maskedTextBox4.Text);

                        //Veritabanındaki tabloyla bağlantı kurma
                        baglantim.Open();
                        OleDbCommand ekleKomutu = new OleDbCommand("insert into personeller values('" + Pm.TC + "','" + Pm.ad + "','" + Pm.soyad + "','" + Pm.cinsiyet + "','" + Pm.mezuniyet + "','" + Pm.dogumTarih.ToString() + "', '" + Pm.gorevi + "','" + Pm.gorevYeri + "','" + Pm.maasi + "')", baglantim);;
                        ekleKomutu.ExecuteNonQuery();
                        baglantim.Close();
                        if (!Directory.Exists(Application.StartupPath + "\\personelresimler")) //bin'deki debug klasöru içindeki personel resimleri kontrol etme
                            Directory.CreateDirectory(Application.StartupPath + "\\personelresimler");  //Bulunmadığı durumda klasör oluşturma 
                        pictureBox2.Image.Save(Application.StartupPath + "\\personelresimler\\" + maskedTextBox1.Text + ".jpg");
                        MessageBox.Show("Yeni personel kaydı oluşturuldu.", "PERSONEL TAKİP SİSTEMİ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        personelleri_goster();
                        topPage1_temizle();
                        maskedTextBox4.Text = "0";

                    }
                    catch (Exception hataMesaj)
                    {
                        MessageBox.Show(hataMesaj.Message, "PERSONEL TAKİP SİSTEMİ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        baglantim.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Kırmızı alanları yeniden gözden geçiriniz!", "PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Girilen TC Kimlik No kayıtlıdır!", "PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }              

            }

        private void button11_Click(object sender, EventArgs e)
        {
            topPage1_temizle();
        }
        //Ara butonu işlemleri
        private void button7_Click(object sender, EventArgs e)
        {
            bool kayitAramaDurumu = false;
            if(maskedTextBox1.Text.Length==11) //11 karakterli tc kimlik no kontrolu
            {
                baglantim.Open();
                OleDbCommand selectSorgu = new OleDbCommand("select *from personeller where tcno='" + maskedTextBox1.Text + "'", baglantim);
                OleDbDataReader kayitOkuma = selectSorgu.ExecuteReader();
                //Kaydın kayıtokuma alanında oluşup oluşmadığı kontrolü
                while (kayitOkuma.Read())
                {
                    kayitAramaDurumu = true;
                    
                    //Girilen tc numarasına ilişkin resmi personel resimler klasöründen alıp stringe dönüştürme ve pictureBox2 nesnesinde görüntülenmesini sağlama
                    try
                    {
                        pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\" + kayitOkuma.GetValue(0).ToString() + ".jpg");    //resmin klasör yolu
                    }
                    catch
                    {
                        //Tc kimlik no'ya ilişkin resim yok ise ekrana "boş resim" koyma
                        pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\bosresim.jpg"); 
                    }
                    //Diğer alanların ara butonuyla otomatik gelmesi
                    maskedTextBox2.Text = kayitOkuma.GetValue(1).ToString();
                    maskedTextBox3.Text = kayitOkuma.GetValue(2).ToString();
                    if (kayitOkuma.GetValue(3).ToString() == "Bay")
                        radioButton3.Checked = true;
                    else
                        radioButton4.Checked = true;
                    comboBox1.Text = kayitOkuma.GetValue(4).ToString();
                    dateTimePicker1.Text = kayitOkuma.GetValue(5).ToString();
                    comboBox2.Text = kayitOkuma.GetValue(6).ToString();
                    comboBox3.Text = kayitOkuma.GetValue(7).ToString();
                    maskedTextBox4.Text = kayitOkuma.GetValue(8).ToString();
                    break;
                }
                if (kayitAramaDurumu == false)
                    MessageBox.Show("Aranan Kayıt Bulunamadı!", "PEROSNEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
                baglantim.Close();
            }
            //11 haneli TC Kimlik No girilmemişse
            else
            {
                MessageBox.Show("11 haneli TC No giriniz!", "PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string Cinsiyet = "";
           
                if (pictureBox2.Image == null) //PictureBox'a resim yüklendiyse siyah,boşsa gözat butonu kırmızı olur 
                    button6.ForeColor = Color.Red;
                else
                    button6.ForeColor = Color.Black;

                //Alanlar doldurulmadığında renk uyarısı
                if (maskedTextBox1.MaskCompleted == false)
                    label13.ForeColor = Color.Red;
                else
                    label13.ForeColor = Color.Black;

                if (maskedTextBox2.MaskCompleted == false)
                    label14.ForeColor = Color.Red;
                else
                    label14.ForeColor = Color.Black;

                if (maskedTextBox3.MaskCompleted == false)
                    label15.ForeColor = Color.Red;
                else
                    label15.ForeColor = Color.Black;
                if (comboBox1.Text == "")
                    label17.ForeColor = Color.Red;
                else
                    label17.ForeColor = Color.Black;
                if (comboBox2.Text == "")
                    label19.ForeColor = Color.Red;
                else
                    label19.ForeColor = Color.Black;
                if (comboBox3.Text == "")
                    label20.ForeColor = Color.Red;
                else
                    label20.ForeColor = Color.Black;

                if (maskedTextBox4.MaskCompleted == false)
                    label21.ForeColor = Color.Red;
                else
                    label21.ForeColor = Color.Black;
                if (int.Parse(maskedTextBox4.Text) < 1000)
                    label21.ForeColor = Color.Red;
                else
                    label21.ForeColor = Color.Black;
                //Güncelleme işlemleri
                if (pictureBox2.Image != null && maskedTextBox1.MaskCompleted != false && maskedTextBox2.MaskCompleted != false && maskedTextBox3.MaskCompleted != false && comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && maskedTextBox4.MaskCompleted != false)
                {
                    //Cinsiyet belirleme
                    if (radioButton3.Checked == true)
                         Cinsiyet = "Bay";
                    else if (radioButton4.Checked == true)
                        Cinsiyet = "Bayan";
                    try
                    {
                        //Veritabanındaki tabloyla bağlantı kurma
                        baglantim.Open();
                        OleDbCommand guncelleKomutu = new OleDbCommand("update personeller set ad='" + maskedTextBox2.Text + "',soyad='" + maskedTextBox3.Text + "',cinsiyet='" + Cinsiyet + "',mezuniyet='" + comboBox1.Text + "',dogumtarihi='" + dateTimePicker1.Text + "',gorevi='" + comboBox2.Text + "',gorevyeri='" + comboBox3.Text + "',maasi='" + maskedTextBox4.Text + "'where tcno='" + maskedTextBox1.Text + "'", baglantim);
                        guncelleKomutu.ExecuteNonQuery();
                        baglantim.Close();
                        personelleri_goster();
                        MessageBox.Show("Güncelleme işlemi gerçekleşti.", "PERSONEL TAKİP SİSTEMİ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        topPage1_temizle();
                        maskedTextBox4.Text = "0";

                    }
                    catch (Exception hataMesaj)
                    {
                        MessageBox.Show(hataMesaj.Message, "PERSONEL TAKİP SİSTEMİ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        baglantim.Close();
                    }
                }
         
            }
        //Silme İşlemleri
        private void button10_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.MaskCompleted == true)
            {
                bool kayitAramaDurumu = false;
                baglantim.Open();
                OleDbCommand aramaSorgusu = new OleDbCommand("select *from personeller where tcno='" + maskedTextBox1.Text + "'", baglantim);
                OleDbDataReader kayitOkuma = aramaSorgusu.ExecuteReader();
                while (kayitOkuma.Read())
                {
                    kayitAramaDurumu = true;
                    //Silme sorgusu
                    OleDbCommand deleteSorgusu = new OleDbCommand("delete from personeller where tcno='" + maskedTextBox1.Text + "'", baglantim);
                    deleteSorgusu.ExecuteNonQuery();
                    break;
                }
                if (kayitAramaDurumu == false)
                {
                    MessageBox.Show("Silinecek kayıt bulunamadı!", "PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                baglantim.Close();
                personelleri_goster();
                topPage1_temizle();
                maskedTextBox1.Text = "0";
            }
            else
            {
                MessageBox.Show("Lütfen 11 karakterli TC Kimlik No giriniz!", "PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                topPage1_temizle();
                maskedTextBox4.Text = "0";
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
        }
    
    

