namespace Main2Cache.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface ICachingProvider
    {
        void Get<T>(out IEnumerable<T> result, string key, TimeSpan expiration, IQueryable<T> query);
    }
}
