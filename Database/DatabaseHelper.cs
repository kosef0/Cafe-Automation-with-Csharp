using System;
using System.Data.SQLite;
using System.IO;
using CafeOtomasyon.Helpers;

namespace CafeOtomasyon.Database
{
    public static class DatabaseHelper
    {
        private static string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CafeOtomasyon.db");
        public static string ConnectionString => $"Data Source={dbPath};Version=3;";

        public static void InitializeDatabase()
        {
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
                CreateTables();
                InsertSampleData();
            }
        }

        private static void CreateTables()
        {
            using var conn = new SQLiteConnection(ConnectionString);
            conn.Open();

            string createTables = @"
                CREATE TABLE IF NOT EXISTS Kullanicilar (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    KullaniciAdi TEXT NOT NULL UNIQUE,
                    Sifre TEXT NOT NULL,
                    AdSoyad TEXT NOT NULL,
                    Email TEXT,
                    Rol TEXT DEFAULT 'Personel',
                    KayitTarihi DATETIME NOT NULL,
                    Aktif BOOLEAN DEFAULT 1
                );

                CREATE TABLE IF NOT EXISTS Kategoriler (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Ad TEXT NOT NULL,
                    Aciklama TEXT
                );

                CREATE TABLE IF NOT EXISTS Urunler (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Ad TEXT NOT NULL,
                    KategoriId INTEGER NOT NULL,
                    Fiyat DECIMAL(10,2) NOT NULL,
                    Aciklama TEXT,
                    Aktif BOOLEAN DEFAULT 1,
                    FOREIGN KEY (KategoriId) REFERENCES Kategoriler(Id)
                );

                CREATE TABLE IF NOT EXISTS Masalar (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    MasaNo TEXT NOT NULL UNIQUE,
                    Durum TEXT DEFAULT 'Boş',
                    AktifSiparisId INTEGER
                );

                CREATE TABLE IF NOT EXISTS Siparisler (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    MasaId INTEGER NOT NULL,
                    Tarih DATETIME NOT NULL,
                    ToplamTutar DECIMAL(10,2) DEFAULT 0,
                    Durum TEXT DEFAULT 'Açık',
                    KapanisTarihi DATETIME,
                    FOREIGN KEY (MasaId) REFERENCES Masalar(Id)
                );

                CREATE TABLE IF NOT EXISTS SiparisDetaylari (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    SiparisId INTEGER NOT NULL,
                    UrunId INTEGER NOT NULL,
                    Adet INTEGER NOT NULL,
                    BirimFiyat DECIMAL(10,2) NOT NULL,
                    ToplamFiyat DECIMAL(10,2) NOT NULL,
                    FOREIGN KEY (SiparisId) REFERENCES Siparisler(Id),
                    FOREIGN KEY (UrunId) REFERENCES Urunler(Id)
                );
            ";

            using var cmd = new SQLiteCommand(createTables, conn);
            cmd.ExecuteNonQuery();
        }

        private static void InsertSampleData()
        {
            using var conn = new SQLiteConnection(ConnectionString);
            conn.Open();

            // Admin kullanıcısı ekle
            string adminPassword = PasswordHelper.HashPassword("admin");
            string personelPassword = PasswordHelper.HashPassword("123456");
            
            string insertKullanicilar = @"
                INSERT INTO Kullanicilar (KullaniciAdi, Sifre, AdSoyad, Email, Rol, KayitTarihi, Aktif) 
                VALUES (@KullaniciAdi, @Sifre, @AdSoyad, @Email, @Rol, datetime('now'), 1)";
            
            // Admin ekle
            using (var cmdAdmin = new SQLiteCommand(insertKullanicilar, conn))
            {
                cmdAdmin.Parameters.AddWithValue("@KullaniciAdi", "admin");
                cmdAdmin.Parameters.AddWithValue("@Sifre", adminPassword);
                cmdAdmin.Parameters.AddWithValue("@AdSoyad", "Sistem Yöneticisi");
                cmdAdmin.Parameters.AddWithValue("@Email", "admin@cafe.com");
                cmdAdmin.Parameters.AddWithValue("@Rol", "Admin");
                cmdAdmin.ExecuteNonQuery();
            }
            
            // Personel ekle
            using (var cmdPersonel = new SQLiteCommand(insertKullanicilar, conn))
            {
                cmdPersonel.Parameters.AddWithValue("@KullaniciAdi", "personel");
                cmdPersonel.Parameters.AddWithValue("@Sifre", personelPassword);
                cmdPersonel.Parameters.AddWithValue("@AdSoyad", "Personel Kullanıcı");
                cmdPersonel.Parameters.AddWithValue("@Email", "personel@cafe.com");
                cmdPersonel.Parameters.AddWithValue("@Rol", "Personel");
                cmdPersonel.ExecuteNonQuery();
            }

            // Diğer örnek verileri ekle
            string insertData = @"
                INSERT INTO Kategoriler (Ad, Aciklama) VALUES 
                    ('Sıcak İçecekler', 'Kahve, çay ve sıcak içecekler'),
                    ('Soğuk İçecekler', 'Limonata, kola ve soğuk içecekler'),
                    ('Tatlılar', 'Pasta, kurabiye ve tatlılar'),
                    ('Ana Yemekler', 'Burger, pizza ve ana yemekler'),
                    ('Atıştırmalıklar', 'Patates, nugget ve atıştırmalıklar');

                INSERT INTO Urunler (Ad, KategoriId, Fiyat, Aciklama, Aktif) VALUES 
                    ('Türk Kahvesi', 1, 35.00, 'Geleneksel Türk kahvesi', 1),
                    ('Espresso', 1, 40.00, 'İtalyan espresso', 1),
                    ('Cappuccino', 1, 50.00, 'Sütlü cappuccino', 1),
                    ('Filtre Kahve', 1, 45.00, 'Filtre kahve', 1),
                    ('Çay', 1, 20.00, 'Demlik çay', 1),
                    ('Limonata', 2, 40.00, 'Ev yapımı limonata', 1),
                    ('Kola', 2, 35.00, 'Soğuk kola', 1),
                    ('Ayran', 2, 25.00, 'Ev yapımı ayran', 1),
                    ('Portakal Suyu', 2, 45.00, 'Taze sıkılmış portakal suyu', 1),
                    ('Sufle', 3, 60.00, 'Çikolatalı sufle', 1),
                    ('Cheesecake', 3, 70.00, 'Frambuazlı cheesecake', 1),
                    ('Tiramisu', 3, 65.00, 'İtalyan tiramisu', 1),
                    ('Waffle', 3, 55.00, 'Çikolatalı waffle', 1),
                    ('Hamburger', 4, 120.00, 'Dana hamburger', 1),
                    ('Pizza Margherita', 4, 150.00, 'Klasik margherita pizza', 1),
                    ('Tavuk Burger', 4, 110.00, 'Izgara tavuk burger', 1),
                    ('Patates Kızartması', 5, 50.00, 'Çıtır patates', 1),
                    ('Soğan Halkası', 5, 45.00, 'Çıtır soğan halkası', 1),
                    ('Mozzarella Stick', 5, 55.00, 'Peynir çubukları', 1);

                INSERT INTO Masalar (MasaNo, Durum) VALUES 
                    ('Masa 1', 'Boş'),
                    ('Masa 2', 'Boş'),
                    ('Masa 3', 'Boş'),
                    ('Masa 4', 'Boş'),
                    ('Masa 5', 'Boş'),
                    ('Masa 6', 'Boş'),
                    ('Masa 7', 'Boş'),
                    ('Masa 8', 'Boş'),
                    ('Masa 9', 'Boş'),
                    ('Masa 10', 'Boş');
            ";

            using var cmd = new SQLiteCommand(insertData, conn);
            cmd.ExecuteNonQuery();
        }
    }
}

