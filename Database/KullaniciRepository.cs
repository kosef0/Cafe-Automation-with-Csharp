using System;
using System.Collections.Generic;
using System.Data.SQLite;
using CafeOtomasyon.Models;
using CafeOtomasyon.Helpers;

namespace CafeOtomasyon.Database
{
    public class KullaniciRepository
    {
        public Kullanici? Login(string kullaniciAdi, string sifre)
        {
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            
            string query = "SELECT * FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi AND Aktif = 1";
            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
            using var reader = cmd.ExecuteReader();
            
            if (reader.Read())
            {
                string hashedPassword = reader.GetString(2);
                if (PasswordHelper.VerifyPassword(sifre, hashedPassword))
                {
                    return new Kullanici
                    {
                        Id = reader.GetInt32(0),
                        KullaniciAdi = reader.GetString(1),
                        Sifre = reader.GetString(2),
                        AdSoyad = reader.GetString(3),
                        Email = reader.GetString(4),
                        Rol = reader.GetString(5),
                        KayitTarihi = reader.GetDateTime(6),
                        Aktif = reader.GetBoolean(7)
                    };
                }
            }
            
            return null;
        }

        public bool KullaniciAdiVarMi(string kullaniciAdi)
        {
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            
            string query = "SELECT COUNT(*) FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi";
            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
            
            long count = (long)cmd.ExecuteScalar()!;
            return count > 0;
        }

        public void Add(Kullanici kullanici)
        {
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            
            // Şifreyi hash'le
            string hashedPassword = PasswordHelper.HashPassword(kullanici.Sifre);
            
            string query = @"INSERT INTO Kullanicilar 
                           (KullaniciAdi, Sifre, AdSoyad, Email, Rol, KayitTarihi, Aktif) 
                           VALUES (@KullaniciAdi, @Sifre, @AdSoyad, @Email, @Rol, @KayitTarihi, @Aktif)";
            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@KullaniciAdi", kullanici.KullaniciAdi);
            cmd.Parameters.AddWithValue("@Sifre", hashedPassword);
            cmd.Parameters.AddWithValue("@AdSoyad", kullanici.AdSoyad);
            cmd.Parameters.AddWithValue("@Email", kullanici.Email);
            cmd.Parameters.AddWithValue("@Rol", kullanici.Rol);
            cmd.Parameters.AddWithValue("@KayitTarihi", DateTime.Now);
            cmd.Parameters.AddWithValue("@Aktif", kullanici.Aktif);
            cmd.ExecuteNonQuery();
        }

        public List<Kullanici> GetAll()
        {
            var kullanicilar = new List<Kullanici>();
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            
            string query = "SELECT * FROM Kullanicilar ORDER BY KayitTarihi DESC";
            using var cmd = new SQLiteCommand(query, conn);
            using var reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                kullanicilar.Add(new Kullanici
                {
                    Id = reader.GetInt32(0),
                    KullaniciAdi = reader.GetString(1),
                    Sifre = reader.GetString(2),
                    AdSoyad = reader.GetString(3),
                    Email = reader.GetString(4),
                    Rol = reader.GetString(5),
                    KayitTarihi = reader.GetDateTime(6),
                    Aktif = reader.GetBoolean(7)
                });
            }
            
            return kullanicilar;
        }

        public Kullanici? GetById(int id)
        {
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            
            string query = "SELECT * FROM Kullanicilar WHERE Id = @Id";
            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using var reader = cmd.ExecuteReader();
            
            if (reader.Read())
            {
                return new Kullanici
                {
                    Id = reader.GetInt32(0),
                    KullaniciAdi = reader.GetString(1),
                    Sifre = reader.GetString(2),
                    AdSoyad = reader.GetString(3),
                    Email = reader.GetString(4),
                    Rol = reader.GetString(5),
                    KayitTarihi = reader.GetDateTime(6),
                    Aktif = reader.GetBoolean(7)
                };
            }
            
            return null;
        }

        public void Update(Kullanici kullanici)
        {
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            
            string query = @"UPDATE Kullanicilar 
                           SET KullaniciAdi = @KullaniciAdi, 
                               Sifre = @Sifre,
                               AdSoyad = @AdSoyad,
                               Email = @Email,
                               Rol = @Rol,
                               Aktif = @Aktif
                           WHERE Id = @Id";
            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", kullanici.Id);
            cmd.Parameters.AddWithValue("@KullaniciAdi", kullanici.KullaniciAdi);
            cmd.Parameters.AddWithValue("@Sifre", kullanici.Sifre);
            cmd.Parameters.AddWithValue("@AdSoyad", kullanici.AdSoyad);
            cmd.Parameters.AddWithValue("@Email", kullanici.Email ?? "");
            cmd.Parameters.AddWithValue("@Rol", kullanici.Rol);
            cmd.Parameters.AddWithValue("@Aktif", kullanici.Aktif);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            
            string query = "DELETE FROM Kullanicilar WHERE Id = @Id";
            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }
}

