using System;
using System.Windows.Forms;
using CafeOtomasyon.Database;
using CafeOtomasyon.Helpers;

namespace CafeOtomasyon
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            
            // Veritabanını başlat
            DatabaseHelper.InitializeDatabase();
            
            // Önce login ekranını göster
            var loginForm = new FormLogin();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // Giriş başarılı, kullanıcıyı kaydet
                CurrentUser.User = loginForm.GirisYapanKullanici;
                
                // Ana menüyü aç
                Application.Run(new FormAnaMenu());
            }
            else
            {
                // Giriş yapılmadı, uygulamayı kapat
                Application.Exit();
            }
        }
    }
}

