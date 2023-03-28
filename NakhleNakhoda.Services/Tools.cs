using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace NakhleNakhoda.Services
{
    public class Tools
    {
        private static readonly Random random = new();
        public static string RandomPassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int GenerateOtp()
        {
            int _min = 1000;
            int _max = 9999;
            return random.Next(_min, _max);
        }
        public static bool IsPhone(string phone)
        {
            return phone != null && phone.Length == 11 && IsNumeric(phone);
        }
        public static bool IsValidOtp(string otp)
        {
            return otp != null && otp.Length == 4 && IsNumeric(otp);
        }

        public static bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }
        //public static string CalculateHash(string input)
        //{
        //    using (var algorithm = SHA512.Create()) //or MD5 SHA256 etc.
        //    {
        //        var hashedBytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

        //        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        //    }
        //}
        public static bool CheckMatch(string hash, string input)
        {
            try
            {
                var parts = hash.Split(':');

                var salt = Convert.FromBase64String(parts[0]);

                var bytes = KeyDerivation.Pbkdf2(input, salt, KeyDerivationPrf.HMACSHA512, 10000, 16);

                return parts[1].Equals(Convert.ToBase64String(bytes));
            }
            catch
            {
                return false;
            }
        }

        public static string CalculateHash(string input)
        {
            var salt = GenerateSalt(16);

            var bytes = KeyDerivation.Pbkdf2(input, salt, KeyDerivationPrf.HMACSHA512, 10000, 16);

            return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(bytes)}";
        }
        private static byte[] GenerateSalt(int length)
        {
            var salt = new byte[length];

            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(salt);
            }

            return salt;
        }
    }
}
