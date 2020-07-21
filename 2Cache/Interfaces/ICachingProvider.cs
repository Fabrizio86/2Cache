namespace Main2Cache.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// ICachingProvider interface class to implement retrieving items from the cache.
    /// </summary>
    public interface ICachingProvider
    {
        /// <summary>
        /// Gets the result from cache or retrieves the result from database.
        /// </summary>
        /// <typeparam name="T">The entity to retrieve from cache</typeparam>
        /// <param name="result">The result instance of <see cref="T"/></param>
        /// <param name="key">The key for the cache query</param>
        /// <param name="expiration">The life span for the cache</param>
        /// <param name="query">The entity to cache</param>
        void Get<T>(out IEnumerable<T> result, string key, TimeSpan expiration, IQueryable<T> query);
    }
}
