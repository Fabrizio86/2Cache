namespace Main2Cache.Providers
{
    using System;
    using System.Collections.Generic;
    using global::Main2Cache.Interfaces;
    using System.Text.Json;
    using System.Linq;

    /// <summary>
    /// Class implementing <see cref="ICachingProvider"/>.
    /// </summary>
    internal sealed class MemoryProvider : ICachingProvider
    {
        /// <summary>
        /// In memory caching storage.
        /// </summary>
        private readonly Dictionary<string, OperationResult> cache = new Dictionary<string, OperationResult>();

        /// <summary>
        /// Constructor accepting instance of <see cref="Configuration"/> as parameter.
        /// </summary>
        /// <param name="configuration">Instance of <see cref="Configuration"/></param>
        public MemoryProvider(Configuration configuration)
        {
            bool result = bool.TryParse(configuration.Parameters?["EnableSliding"], out bool val);
            OperationResult.SlidingExpiration = !result || val;
        }

        /// <inheritdoc cref="ICachingProvider.Get{T}(out IEnumerable{T}, string, TimeSpan, IQueryable{T})"/>
        public void Get<T>(out IEnumerable<T> result, string key, TimeSpan expiration, IQueryable<T> query)
        {
            if (cache.ContainsKey(key) && !cache[key].IsExpired)
            {
                result = JsonSerializer.Deserialize<List<T>>(cache[key].Result);
            }
            else
            {
                result = query.ToList();

                this.cache[key] = new OperationResult
                {
                    key = key,
                    Expiration = expiration,
                    Result = JsonSerializer.Serialize(result)
                };
            }
        }
    }
}
