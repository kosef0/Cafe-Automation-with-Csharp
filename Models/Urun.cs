namespace CafeOtomasyon.Models
{
    public class Urun
    {
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty;
        public int KategoriId { get; set; }
        public string KategoriAd { get; set; } = string.Empty;
        public decimal Fiyat { get; set; }
        public string Aciklama { get; set; } = string.Empty;
        public bool Aktif { get; set; } = true;
    }
}

