using System;

namespace CafeOtomasyon.Models
{
    public class Kullanici
    {
        public int Id { get; set; }
        public string KullaniciAdi { get; set; } = string.Empty;
        public string Sifre { get; set; } = string.Empty;
        public string AdSoyad { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Rol { get; set; } = "Personel"; // Admin, Personel
        public DateTime KayitTarihi { get; set; }
        public bool Aktif { get; set; } = true;
    }
}

