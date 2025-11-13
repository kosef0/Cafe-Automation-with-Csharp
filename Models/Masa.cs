namespace CafeOtomasyon.Models
{
    public class Masa
    {
        public int Id { get; set; }
        public string MasaNo { get; set; } = string.Empty;
        public int Kapasite { get; set; } = 4;
        public string Durum { get; set; } = "Boş"; // Boş, Dolu, Rezerve
        public int? AktifSiparisId { get; set; }
        public bool Aktif { get; set; } = true;
    }
}

