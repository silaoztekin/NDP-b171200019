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


using ndp_b171200019.Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ndp_b171200019.AppCode
{   //Kalıtım örneği
    public class KullaniciIslemleri : IKullaniciIslemleri
    {
        OleDbConnection baglantim = new OleDbConnection
          ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=personel.accdb");
        public void ekle(UserModel user)
        {
            try
            {
                baglantim.Open();
                OleDbCommand ekleKomutu = new OleDbCommand("insert into kullanicilar values ('" + user.TC + "','" + user.ad + "','" + user.soyad + "','" + user.yetki + "','" + user.kullaniciAdi + "','" + user.parola + "')", baglantim);
                ekleKomutu.ExecuteNonQuery();
                baglantim.Close();            
            }
            catch {  }
        }
    }
}
