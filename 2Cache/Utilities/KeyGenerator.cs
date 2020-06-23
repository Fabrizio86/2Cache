namespace Main2Cache.Utilities
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    internal static class KeyGenerator
    {
        public static string ComputeKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new InvalidOperationException("Invalid key, key can't be empty!");

            using var sha256 = new SHA256Managed();
            return BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(key))).Replace("-", string.Empty);
        }
    }
}
