using System;
using System.Collections.Generic;
using System.Data.SQLite;
using CafeOtomasyon.Models;

namespace CafeOtomasyon.Database
{
    public class SiparisRepository
    {
        public int CreateSiparis(int masaId)
        {
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            
            string query = "INSERT INTO Siparisler (MasaId, Tarih, ToplamTutar, Durum) VALUES (@MasaId, @Tarih, 0, 'Açık')";
            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@MasaId", masaId);
            cmd.Parameters.AddWithValue("@Tarih", DateTime.Now);
            cmd.ExecuteNonQuery();
            
            return (int)conn.LastInsertRowId;
        }

        public Siparis? GetById(int id)
        {
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            
            string query = @"SELECT s.*, m.MasaNo 
                           FROM Siparisler s 
                           INNER JOIN Masalar m ON s.MasaId = m.Id 
                           WHERE s.Id = @Id";
            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using var reader = cmd.ExecuteReader();
            
            if (reader.Read())
            {
                return new Siparis
                {
                    Id = reader.GetInt32(0),
                    MasaId = reader.GetInt32(1),
                    Tarih = reader.GetDateTime(2),
                    ToplamTutar = reader.GetDecimal(3),
                    Durum = reader.GetString(4),
                    KapanisTarihi = reader.IsDBNull(5) ? null : reader.GetDateTime(5),
                    MasaNo = reader.GetString(6)
                };
            }
            
            return null;
        }

        public List<SiparisDetay> GetSiparisDetaylar(int siparisId)
        {
            var detaylar = new List<SiparisDetay>();
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            
            string query = @"SELECT sd.*, u.Ad as UrunAd 
                           FROM SiparisDetaylari sd 
                           INNER JOIN Urunler u ON sd.UrunId = u.Id 
                           WHERE sd.SiparisId = @SiparisId";
            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@SiparisId", siparisId);
            using var reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                detaylar.Add(new SiparisDetay
                {
                    Id = reader.GetInt32(0),
                    SiparisId = reader.GetInt32(1),
                    UrunId = reader.GetInt32(2),
                    Adet = reader.GetInt32(3),
                    BirimFiyat = reader.GetDecimal(4),
                    ToplamFiyat = reader.GetDecimal(5),
                    UrunAd = reader.GetString(6)
                });
            }
            
            return detaylar;
        }

        public void AddSiparisDetay(SiparisDetay detay)
        {
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            using var transaction = conn.BeginTransaction();

            try
            {
                // Sipariş detayını ekle
                string query = "INSERT INTO SiparisDetaylari (SiparisId, UrunId, Adet, BirimFiyat, ToplamFiyat) VALUES (@SiparisId, @UrunId, @Adet, @BirimFiyat, @ToplamFiyat)";
                using var cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@SiparisId", detay.SiparisId);
                cmd.Parameters.AddWithValue("@UrunId", detay.UrunId);
                cmd.Parameters.AddWithValue("@Adet", detay.Adet);
                cmd.Parameters.AddWithValue("@BirimFiyat", detay.BirimFiyat);
                cmd.Parameters.AddWithValue("@ToplamFiyat", detay.ToplamFiyat);
                cmd.ExecuteNonQuery();

                // Sipariş toplam tutarını güncelle
                UpdateSiparisTotal(detay.SiparisId, conn);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void DeleteSiparisDetay(int detayId, int siparisId)
        {
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            using var transaction = conn.BeginTransaction();

            try
            {
                string query = "DELETE FROM SiparisDetaylari WHERE Id = @Id";
                using var cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", detayId);
                cmd.ExecuteNonQuery();

                UpdateSiparisTotal(siparisId, conn);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        private void UpdateSiparisTotal(int siparisId, SQLiteConnection conn)
        {
            string query = "UPDATE Siparisler SET ToplamTutar = (SELECT COALESCE(SUM(ToplamFiyat), 0) FROM SiparisDetaylari WHERE SiparisId = @SiparisId) WHERE Id = @SiparisId";
            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@SiparisId", siparisId);
            cmd.ExecuteNonQuery();
        }

        public void CloseSiparis(int siparisId)
        {
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            
            string query = "UPDATE Siparisler SET Durum = 'Kapalı', KapanisTarihi = @KapanisTarihi WHERE Id = @Id";
            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", siparisId);
            cmd.Parameters.AddWithValue("@KapanisTarihi", DateTime.Now);
            cmd.ExecuteNonQuery();
        }

        public List<Siparis> GetSiparislerByDateRange(DateTime baslangic, DateTime bitis)
        {
            var siparisler = new List<Siparis>();
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            
            string query = @"SELECT s.*, m.MasaNo 
                           FROM Siparisler s 
                           INNER JOIN Masalar m ON s.MasaId = m.Id 
                           WHERE s.Durum = 'Kapalı' AND s.KapanisTarihi >= @Baslangic AND s.KapanisTarihi <= @Bitis
                           ORDER BY s.KapanisTarihi DESC";
            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@Baslangic", baslangic);
            cmd.Parameters.AddWithValue("@Bitis", bitis);
            using var reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                siparisler.Add(new Siparis
                {
                    Id = reader.GetInt32(0),
                    MasaId = reader.GetInt32(1),
                    Tarih = reader.GetDateTime(2),
                    ToplamTutar = reader.GetDecimal(3),
                    Durum = reader.GetString(4),
                    KapanisTarihi = reader.IsDBNull(5) ? null : reader.GetDateTime(5),
                    MasaNo = reader.GetString(6)
                });
            }
            
            return siparisler;
        }
    }
}

