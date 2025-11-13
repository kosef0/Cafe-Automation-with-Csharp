using System;
using System.Collections.Generic;
using System.Data.SQLite;
using CafeOtomasyon.Models;

namespace CafeOtomasyon.Database
{
    public class KategoriRepository
    {
        public List<Kategori> GetAll()
        {
            var kategoriler = new List<Kategori>();
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            
            string query = "SELECT * FROM Kategoriler ORDER BY Ad";
            using var cmd = new SQLiteCommand(query, conn);
            using var reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                kategoriler.Add(new Kategori
                {
                    Id = reader.GetInt32(0),
                    Ad = reader.GetString(1),
                    Aciklama = reader.IsDBNull(2) ? "" : reader.GetString(2)
                });
            }
            
            return kategoriler;
        }

        public void Add(Kategori kategori)
        {
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            
            string query = "INSERT INTO Kategoriler (Ad, Aciklama) VALUES (@Ad, @Aciklama)";
            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@Ad", kategori.Ad);
            cmd.Parameters.AddWithValue("@Aciklama", kategori.Aciklama);
            cmd.ExecuteNonQuery();
        }

        public void Update(Kategori kategori)
        {
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            
            string query = "UPDATE Kategoriler SET Ad = @Ad, Aciklama = @Aciklama WHERE Id = @Id";
            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", kategori.Id);
            cmd.Parameters.AddWithValue("@Ad", kategori.Ad);
            cmd.Parameters.AddWithValue("@Aciklama", kategori.Aciklama);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            
            string query = "DELETE FROM Kategoriler WHERE Id = @Id";
            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }
}

