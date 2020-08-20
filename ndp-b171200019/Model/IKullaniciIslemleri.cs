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
    //Arayüz oluşturma
    public interface IKullaniciIslemleri
    {
        void ekle(UserModel user);
    }
}
