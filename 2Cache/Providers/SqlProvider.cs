namespace Main2Cache.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::Main2Cache.Interfaces;

    internal sealed class SqlProvider : ICachingProvider
    {
        public SqlProvider(Configuration configuration)
        {

        }

        public void Get<T>(out IEnumerable<T> result, string key, TimeSpan expiration, IQueryable<T> query)
        {
            throw new NotImplementedException();
        }
    }
}
