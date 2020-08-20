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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ndp_b171200019.Model
{
    public class PersonelModel
    {
        public string TC { get; set; }
        public string ad { get; set; }
        public string soyad { get; set; }
        public string cinsiyet { get; set; }
        public string mezuniyet { get; set; }
        public DateTime dogumTarih { get; set; }
        public string gorevi { get; set; }
        public string gorevYeri { get; set; }
        private int _maas;
        //Sarmalama işlemi
        //Örneğin maaş kısmına 5000'den fazla değer girilse bile ekrana 5000 getirme
        public int maasi { get { return _maas; } set {
                if (value >= 5000)
                    _maas = value;
                else
                    _maas = 5000;
            } }
    }
}
