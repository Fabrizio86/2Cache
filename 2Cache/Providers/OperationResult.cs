namespace Main2Cache.Providers
{
    using System;

    internal sealed class OperationResult
    {
        public static bool SlidingExpiration { get; set; }

        private string result;

        private DateTime created;

        public string key { get; set; }

        public TimeSpan Expiration { get; set; }

        public string Result
        {
            get
            {
                if (this.IsExpired)
                    return string.Empty;

                if (SlidingExpiration)
                    this.created = DateTime.UtcNow;

                return this.result;
            }
            set
            {
                this.created = DateTime.UtcNow;
                this.result = value;
            }
        }

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
