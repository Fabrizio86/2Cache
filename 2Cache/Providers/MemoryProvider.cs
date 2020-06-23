namespace Main2Cache.Providers
{
    using System;
    using System.Collections.Generic;
    using global::Main2Cache.Interfaces;
    using System.Text.Json;
    using System.Linq;

    internal sealed class MemoryProvider : ICachingProvider
    {
        private Dictionary<string, OperationResult> cache = new Dictionary<string, OperationResult>();

        public MemoryProvider(Configuration configuration)
        {
            bool result = bool.TryParse(configuration.Parameters?["EnableSliding"], out bool val);
            OperationResult.SlidingExpiration = !result || val;
        }

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
