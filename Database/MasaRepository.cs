using System;
using System.Collections.Generic;
using System.Data.SQLite;
using CafeOtomasyon.Models;

namespace CafeOtomasyon.Database
{
    public class MasaRepository
    {
        public List<Masa> GetAll()
        {
            var masalar = new List<Masa>();
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            
            string query = "SELECT * FROM Masalar ORDER BY MasaNo";
            using var cmd = new SQLiteCommand(query, conn);
            using var reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                masalar.Add(new Masa
                {
                    Id = reader.GetInt32(0),
                    MasaNo = reader.GetString(1),
                    Durum = reader.GetString(2),
                    AktifSiparisId = reader.IsDBNull(3) ? null : reader.GetInt32(3)
                });
            }
            
            return masalar;
        }

        public void UpdateDurum(int masaId, string durum, int? aktifSiparisId = null)
        {
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            
            string query = "UPDATE Masalar SET Durum = @Durum, AktifSiparisId = @AktifSiparisId WHERE Id = @Id";
            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", masaId);
            cmd.Parameters.AddWithValue("@Durum", durum);
            cmd.Parameters.AddWithValue("@AktifSiparisId", aktifSiparisId.HasValue ? (object)aktifSiparisId.Value : DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void Add(Masa masa)
        {
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            
            string query = "INSERT INTO Masalar (MasaNo, Durum) VALUES (@MasaNo, @Durum)";
            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@MasaNo", masa.MasaNo);
            cmd.Parameters.AddWithValue("@Durum", masa.Durum);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = new SQLiteConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            
            string query = "DELETE FROM Masalar WHERE Id = @Id";
            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }
}

