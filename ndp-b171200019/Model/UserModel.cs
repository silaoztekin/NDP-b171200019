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
    public class UserModel
    {
        public string TC { get; set; }
        public string ad { get; set; }
        public string soyad { get; set; }
        public string yetki { get; set; }
        public string kullaniciAdi { get; set; }
        private string _parola;
        public string parola
        {
            get { return _parola; }
            set
                //Kullanıcı parolaya ! ve * değerleri girmeli 
            {
                if (!value.Contains("!") || !value.Contains("*"))
                {
                    _parola = value;
                }
                else
                    //Girmediği durumda
                    _parola += _parola + "!*";
            }
        }
    }
}
