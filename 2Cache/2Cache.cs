namespace Main2Cache
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::Main2Cache.Interfaces;

    public static class Main2Cache
    {
        /// <summary>
        /// Returns the cached result if exists, or will execute the query and add the result in cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IEnumerable<T> FromCache<T>(this IQueryable<T> query)
        {
            return query.FromCache(TimeSpan.FromMinutes(1));
        }

        /// <summary>
        /// Returns the cached result if exists, or will execute the query and add the result in cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="slidingExpiration"></param>
        /// <returns></returns>
        public static IEnumerable<T> FromCache<T>(this IQueryable<T> query, TimeSpan expiration)
        {
            IEnumerable<T> result = null;

            string key = query.GetKey();
            ICachingProvider provider = ProviderManager.Provider;
            provider.Get(out result, key, expiration, query);

            return result;
        }
    }
}
