using System;

namespace CafeOtomasyon.Models
{
    public class Siparis
    {
        public int Id { get; set; }
        public int MasaId { get; set; }
        public string MasaNo { get; set; } = string.Empty;
        public DateTime Tarih { get; set; }
        public decimal ToplamTutar { get; set; }
        public string Durum { get; set; } = "Açık"; // Açık, Kapalı
        public DateTime? KapanisTarihi { get; set; }
    }
}

