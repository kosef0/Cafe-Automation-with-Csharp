using CafeOtomasyon.Models;

namespace CafeOtomasyon.Helpers
{
    public static class CurrentUser
    {
        public static Kullanici? User { get; set; }
        
        public static bool IsLoggedIn => User != null;
        
        public static bool IsAdmin => User?.Rol == "Admin";
        
        public static void Logout()
        {
            User = null;
        }
    }
}

