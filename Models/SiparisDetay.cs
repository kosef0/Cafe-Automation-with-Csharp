namespace CafeOtomasyon.Models
{
    public class SiparisDetay
    {
        public int Id { get; set; }
        public int SiparisId { get; set; }
        public int UrunId { get; set; }
        public string UrunAd { get; set; } = string.Empty;
        public int Adet { get; set; }
        public decimal BirimFiyat { get; set; }
        public decimal ToplamFiyat { get; set; }
    }
}

