using System;
using System.Collections.Generic;
using System.Data.SQLite;
using CafeOtomasyon.Models;

namespace CafeOtomasyon.Database
{
    public class UrunRepository
    {
        public List<Urun> GetAll()
        {
            var urunler = new List<Urun>();
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            
            string query = @"SELECT u.*, k.Ad as KategoriAd 
                           FROM Urunler u 
                           INNER JOIN Kategoriler k ON u.KategoriId = k.Id 
                           ORDER BY k.Ad, u.Ad";
            using var cmd = new SQLiteCommand(query, conn);
            using var reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                urunler.Add(new Urun
                {
                    Id = reader.GetInt32(0),
                    Ad = reader.GetString(1),
                    KategoriId = reader.GetInt32(2),
                    Fiyat = reader.GetDecimal(3),
                    Aciklama = reader.IsDBNull(4) ? "" : reader.GetString(4),
                    Aktif = reader.GetBoolean(5),
                    KategoriAd = reader.GetString(6)
                });
            }
            
            return urunler;
        }

        public List<Urun> GetByKategori(int kategoriId)
        {
            var urunler = new List<Urun>();
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            
            string query = @"SELECT u.*, k.Ad as KategoriAd 
                           FROM Urunler u 
                           INNER JOIN Kategoriler k ON u.KategoriId = k.Id 
                           WHERE u.KategoriId = @KategoriId AND u.Aktif = 1
                           ORDER BY u.Ad";
            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@KategoriId", kategoriId);
            using var reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                urunler.Add(new Urun
                {
                    Id = reader.GetInt32(0),
                    Ad = reader.GetString(1),
                    KategoriId = reader.GetInt32(2),
                    Fiyat = reader.GetDecimal(3),
                    Aciklama = reader.IsDBNull(4) ? "" : reader.GetString(4),
                    Aktif = reader.GetBoolean(5),
                    KategoriAd = reader.GetString(6)
                });
            }
            
            return urunler;
        }

        public void Add(Urun urun)
        {
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            
            string query = "INSERT INTO Urunler (Ad, KategoriId, Fiyat, Aciklama, Aktif) VALUES (@Ad, @KategoriId, @Fiyat, @Aciklama, @Aktif)";
            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@Ad", urun.Ad);
            cmd.Parameters.AddWithValue("@KategoriId", urun.KategoriId);
            cmd.Parameters.AddWithValue("@Fiyat", urun.Fiyat);
            cmd.Parameters.AddWithValue("@Aciklama", urun.Aciklama);
            cmd.Parameters.AddWithValue("@Aktif", urun.Aktif);
            cmd.ExecuteNonQuery();
        }

        public void Update(Urun urun)
        {
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            
            string query = "UPDATE Urunler SET Ad = @Ad, KategoriId = @KategoriId, Fiyat = @Fiyat, Aciklama = @Aciklama, Aktif = @Aktif WHERE Id = @Id";
            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", urun.Id);
            cmd.Parameters.AddWithValue("@Ad", urun.Ad);
            cmd.Parameters.AddWithValue("@KategoriId", urun.KategoriId);
            cmd.Parameters.AddWithValue("@Fiyat", urun.Fiyat);
            cmd.Parameters.AddWithValue("@Aciklama", urun.Aciklama);
            cmd.Parameters.AddWithValue("@Aktif", urun.Aktif);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            
            string query = "DELETE FROM Urunler WHERE Id = @Id";
            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }
}

