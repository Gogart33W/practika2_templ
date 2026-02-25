using System;
using System.Configuration; // для читання App.config
using System.Security.Cryptography;

namespace Navchpract_2
{
    public static class CryptoHelper
    {
        // 100 000 ітерацій для захисту від райдужних таблиць
        private const int ITERATIONS = 100000;

        // Отримуємо перець з App.config
        private static string GetPepper()
        {
            string pepper = ConfigurationManager.AppSettings["SecurityPepper"];

            if (string.IsNullOrEmpty(pepper))
            {
                throw new Exception("Критична помилка безпеки: 'SecurityPepper' не знайдено у файлі App.config! Зверніться до адміністратора.");
            }

            return pepper;
        }

        // --- МЕТОД ХЕШУВАННЯ ---
        public static string HashPassword(string plainPassword)
        {
            string passwordWithPepper = plainPassword + GetPepper();

            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider()) { rng.GetBytes(salt); }

            using (var pbkdf2 = new Rfc2898DeriveBytes(passwordWithPepper, salt, ITERATIONS))
            {
                byte[] hash = pbkdf2.GetBytes(32);
                byte[] hashBytes = new byte[48];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 32);
                return Convert.ToBase64String(hashBytes);
            }
        }

        // --- МЕТОД ПЕРЕВІРКИ ---
        public static bool VerifyPassword(string plainPassword, string storedHash)
        {
            try
            {
                byte[] hashBytes = Convert.FromBase64String(storedHash);
                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);

                string passwordWithPepper = plainPassword + GetPepper();

                using (var pbkdf2 = new Rfc2898DeriveBytes(passwordWithPepper, salt, ITERATIONS))
                {
                    byte[] hash = pbkdf2.GetBytes(32);
                    for (int i = 0; i < 32; i++)
                    {
                        if (hashBytes[i + 16] != hash[i]) return false;
                    }
                    return true;
                }
            }
            catch { return false; }
        }
    }
}