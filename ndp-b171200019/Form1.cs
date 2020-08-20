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
    public partial class Form1 : Form
    {   
            public Form1()
         {
             InitializeComponent();
         }
        //veri tabanı dosya yolu belirleme
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=personel.accdb");

        //Formlar arası veri aktarımında kullanılacak değişkenler
        public static string _tcno, _adi, _soyadi, _yetki;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
             if (_hak != 0)
            {        //Veritabanındaki kullanicilar tablosuyla baglantı ve kayıt okuma işlemi
                     baglantim.Open();
                     OleDbCommand selectSorgu = new OleDbCommand("select*from kullanicilar", baglantim);
                     OleDbDataReader kayitOkuma = selectSorgu.ExecuteReader();
                 while (kayitOkuma.Read())
                 {
                     if (radioButton1.Checked == true)
                    {    //Yönetici radiobuttonunu seçildiğinde kullaniciadi ve parolayla karşılaştırma yapılır,durum true dönerse onaylanır ve form2'e açılır
                         if (kayitOkuma["kullaniciadi"].ToString() == textBox1.Text && kayitOkuma["parola"].ToString() == textBox2.Text && kayitOkuma["yetki"].ToString() == "Yönetici")
                        {
                             _durum = true;
                             _tcno = kayitOkuma.GetValue(0).ToString();
                             _adi = kayitOkuma.GetValue(1).ToString();
                             _soyadi = kayitOkuma.GetValue(2).ToString();
                             _yetki = kayitOkuma.GetValue(3).ToString();
                             this.Hide();
                             Form2 frm2 = new Form2();
                             frm2.Show();
                             break;
                        }
                    }
                    if (radioButton2.Checked == true)
                    {   //Radiobutton2 için de kullanıcı girişi doğrulanırsa true döner ve form3'e açılır
                         if (kayitOkuma["kullaniciadi"].ToString() == textBox1.Text && kayitOkuma["parola"].ToString() == textBox2.Text && kayitOkuma["yetki"].ToString() == "Kullanıcı")
                        {
                           _durum = true;
                           _tcno = kayitOkuma.GetValue(0).ToString();
                           _adi = kayitOkuma.GetValue(1).ToString();
                           _soyadi = kayitOkuma.GetValue(2).ToString();
                           _yetki = kayitOkuma.GetValue(3).ToString();
                            this.Hide();
                            Form3 frm3 = new Form3();
                            frm3.Show();
                            break;
                        }
                    }
                }     //Hatalı giriş yapılırsa giriş için hak azalır
                     if (_durum == false)
                           _hak--;
                           baglantim.Close();
                }
                 label5.Text = Convert.ToString(_hak);
                 if (_hak == 0)
                {     //Hatalı giriş sonunda ekrana hata mesajı verilir
                          button1.Enabled = false;
                          MessageBox.Show("Giriş hakkı kalmadı!", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                          this.Close();
                }

            
        }
        //Toplamda giriş denemesi için 3 hak
        int _hak = 3;bool _durum = false;
        private void Form1_Load(object sender, EventArgs e)
        {
              this.Text = "Kullanıcı Giriş..";
              this.AcceptButton = button1;this.CancelButton = button2;
              label5.Text = Convert.ToString(_hak);
              radioButton1.Checked = true;
              this.StartPosition = FormStartPosition.CenterScreen;
              this.FormBorderStyle = FormBorderStyle.FixedToolWindow;

        }
    }
}
