namespace Main2Cache.Providers
{
    using System;
    using System.Collections;

    /// <summary>
    /// Class representing the status and details of the caching operation.
    /// </summary>
    internal sealed class OperationResult
    {
        /// <summary>
        /// Gets or sets a flag inidcating if the expiration should be extended.
        /// </summary>
        public static bool SlidingExpiration { get; set; }

        /// <summary>
        /// Stores the result of the operation as json in memory.
        /// </summary>
        private IEnumerable result;

        /// <summary>
        /// Stores the date of when the cache was created.
        /// </summary>
        private DateTime created;

        /// <summary>
        /// Gets or sets the key of the caching.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the time-alive for the cache result.
        /// </summary>
        public TimeSpan Expiration { get; set; }

        /// <summary>
        /// Gets or sets the cache result, if the cache is expired it will be cleared from memory and empty string will be returned.
        /// </summary>
        public IEnumerable Result
        {
            get
            {
                // check for expiration
                if (!this.IsExpired && SlidingExpiration)
                {
                    this.created = DateTime.UtcNow;
                }
                else
                {
                    // clear the cache
                    this.result = null;
                }

                // return the cached result;
                return this.result;
            }
            set
            {
                this.created = DateTime.UtcNow;
                this.result = value;
            }
        }

        /// <summary>
        /// Gets a flag idictating if the cache has expired.
        /// </summary>
        public bool IsExpired
        {
            get
            {
                var deltaTime = DateTime.UtcNow - this.created;

                return deltaTime > Expiration;
            }
        }
    }
}
