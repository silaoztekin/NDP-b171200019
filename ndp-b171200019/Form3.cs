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
using System.Data.OleDb;

namespace ndp_b171200019
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        OleDbConnection baglantim=new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = personel.accdb");

        //Personellerin gösterilmesi methodu
        private void personelleri_goster()
        {
            try
            {   //Datagridview için tabloyu getirme
                baglantim.Open();
                OleDbDataAdapter personelleriListele = new OleDbDataAdapter("select tcno AS[TC KİMLİK NO],ad AS[ADI],soyad AS[SOYADI],cinsiyet as[CİNSİYETİ],mezuniyet as[MEZUNİYETİ],dogumtarihi as[DOĞUM TARİHİ],gorevi as[GÖREVİ],gorevyeri as[GÖREV YERİ],maasi as[MAAŞI] from personeller Order By ad ASC", baglantim);
                DataSet dsHafiza = new DataSet(); //Kayıt saklanması için bellekte alan oluşturma 
                personelleriListele.Fill(dsHafiza);
                dataGridView1.DataSource = dsHafiza.Tables[0];
                baglantim.Close();
            
            }
            catch (Exception hataMesaj)
            {
                MessageBox.Show(hataMesaj.Message, "PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            personelleri_goster();
            this.Text = "KULLANICI İŞLEMLERİ";
            label19.Text = Form1._adi + " " + Form1._soyadi; //Form1'den giriş yapan kullanıcı adı ve soyadı getirme
            //pictureBox1 ve pictureBox  2 özellikleri
            pictureBox1.Height = 150;pictureBox1.Width = 150;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox2.Height = 150; pictureBox2.Width = 150;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.BorderStyle = BorderStyle.Fixed3D;
            try
            {   //Kullaniciresimler klasöründeki tc no ile ilişkili resmi pictureBox2'ye getirme
                pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Form1._tcno + ".jpg");

            }
            //Resim bulunamadığında ayarlı olan boş resmi getirme 
            catch 
            {
                pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\bosresim.jpg");
            }
            maskedTextBox1.Mask = "00000000000"; //11 haneli veri girişini zorunlu kılma

        }
        //Ara butonu işlemleri
        private void button1_Click(object sender, EventArgs e)
        {
            bool kayitAramaDurumu = false;
            if (maskedTextBox1.Text.Length == 11) //TC alanına 11 hale şartı
            {
                baglantim.Open();
                //Personeller tablosundan tc no seçimi
                OleDbCommand selectSorgu = new OleDbCommand("select *from personeller where tcno='" + maskedTextBox1.Text + "'", baglantim);
                OleDbDataReader kayitOkuma = selectSorgu.ExecuteReader();
                while (kayitOkuma.Read())
                {
                    kayitAramaDurumu = true;
                    try
                    {
                        pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\" + kayitOkuma.GetValue(0) + ".jpg");

                    }
                    catch
                    {
                        pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\bosresim.jpg");
                    }
                    label10.Text = kayitOkuma.GetValue(1).ToString();
                    label11.Text = kayitOkuma.GetValue(2).ToString();
                    if (kayitOkuma.GetValue(3).ToString() == "Bay")
                        label12.Text = "Bay";
                    else
                        label12.Text = "Bayan";
                    label13.Text = kayitOkuma.GetValue(4).ToString();
                    label14.Text = kayitOkuma.GetValue(5).ToString();
                    label15.Text = kayitOkuma.GetValue(6).ToString();
                    label16.Text = kayitOkuma.GetValue(7).ToString();
                    label17.Text = kayitOkuma.GetValue(8).ToString();
                    break;
                }
                //While döngüsü çalışmadıysa-Kayıt bulunamadıysa
                if (kayitAramaDurumu == false)
                    MessageBox.Show("Aranan Kayıt Bulunamadı!", "PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
            }
            else
                MessageBox.Show("11 karakterli TC Kimlik No giriniz!", "PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
    }

