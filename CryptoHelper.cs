using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Navchpract_2
{
    public static class CryptoHelper
    {
        private const int CURRENT_ITERATIONS = 200000;
        private const int SALT_SIZE = 16;
        private const int HASH_SIZE = 32;

        private const string FORMAT_VERSION = "v1";
        private const string ALGORITHM = "PBKDF2-SHA256";

        private static readonly string PepperFilePath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "Navchpract_2",
                "pepper.dat");

        public static string HashPassword(string password)
        {
            byte[] salt = GenerateRandomBytes(SALT_SIZE);
            byte[] hash = Derive(password, salt, CURRENT_ITERATIONS);

            return FORMAT_VERSION + "$" +
                   ALGORITHM + "$" +
                   CURRENT_ITERATIONS + "$" +
                   Convert.ToBase64String(salt) + "$" +
                   Convert.ToBase64String(hash);
        }

        public static bool VerifyPassword(string password, string storedValue, out string upgradedHash)
        {
            upgradedHash = null;

            try
            {
                string[] parts = storedValue.Split('$');
                if (parts.Length != 5)
                    return false;

                string version = parts[0];
                string algorithm = parts[1];
                int iterations = int.Parse(parts[2]);

                if (version != FORMAT_VERSION || algorithm != ALGORITHM)
                    return false;

                byte[] salt = Convert.FromBase64String(parts[3]);
                byte[] storedHash = Convert.FromBase64String(parts[4]);

                byte[] computedHash = Derive(password, salt, iterations);

                if (!FixedTimeEquals(storedHash, computedHash))
                    return false;

                if (iterations < CURRENT_ITERATIONS)
                {
                    upgradedHash = HashPassword(password);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static byte[] Derive(string password, byte[] salt, int iterations)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(
                password + GetPepper(),
                salt,
                iterations,
                HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(HASH_SIZE);
            }
        }

        private static bool FixedTimeEquals(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;

            int diff = 0;

            for (int i = 0; i < a.Length; i++)
            {
                diff |= a[i] ^ b[i];
            }

            return diff == 0;
        }

        private static string GetPepper()
        {
            if (!File.Exists(PepperFilePath))
                CreatePepper();

            byte[] encrypted = File.ReadAllBytes(PepperFilePath);

            byte[] decrypted = ProtectedData.Unprotect(
                encrypted,
                null,
                DataProtectionScope.LocalMachine);

            return Encoding.UTF8.GetString(decrypted);
        }

        private static void CreatePepper()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(PepperFilePath));

            byte[] pepper = GenerateRandomBytes(32);

            byte[] encrypted = ProtectedData.Protect(
                pepper,
                null,
                DataProtectionScope.LocalMachine);

            File.WriteAllBytes(PepperFilePath, encrypted);
        }

        private static byte[] GenerateRandomBytes(int length)
        {
            byte[] bytes = new byte[length];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            return bytes;
        }

        public static string HashTempPasswordSHA1(string password)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}