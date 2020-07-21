namespace Main2Cache.Utilities
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Handles the generation of the cache key based on the entity structure.
    /// </summary>
    internal static class KeyGenerator
    {
        /// <summary>
        /// Generates the key based on the query structure.
        /// </summary>
        /// <param name="key">The generated query structure</param>
        /// <returns>Returns the SHA256 key from the query structure</returns>
        public static string ComputeKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new InvalidOperationException("Invalid key, key can't be empty!");

            using var sha256 = new SHA256Managed();
            return BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(key))).Replace("-", string.Empty);
        }
    }
}
