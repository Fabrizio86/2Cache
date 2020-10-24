namespace Main2Cache
{
    using System;
    using global::Main2Cache.Interfaces;
    using global::Main2Cache.Providers;

    /// <summary>
    /// Class to manage the caching backend.
    /// </summary>
    public sealed class ProviderManager
    {
        /// <summary>
        /// Singleton instance of <see cref="ProviderManager"/>
        /// </summary>
        private static readonly Lazy<ProviderManager> instance = new Lazy<ProviderManager>(() => new ProviderManager());

        /// <summary>
        /// Instance of <see cref="Configuration(Main2Cache.Configuration)"/>.
        /// </summary>
        private static Configuration Config { get; set; }

        private ProviderManager()
        {
            // initialize based on config file
            switch (Config.ProviderType)
            {
                case Configurations.ProviderType.InMemory:
                    Provider = new MemoryProvider(Config);
                    break;
                case Configurations.ProviderType.SqlBackend:
                    Provider = new SqlProvider(Config);
                    break;
                case Configurations.ProviderType.Custom:
                    throw new NotImplementedException();
                default:
                    throw new InvalidOperationException("Invalid configuration parameters");
            }
        }

        /// <summary>
        /// Gets if the provider is initailized.
        /// </summary>
        public bool Initialized { get { return Provider == null; } }

        /// <summary>
        /// Gets or private set the <see cref="ICachingProvider"/>.
        /// </summary>
        public static ICachingProvider Provider { get; private set; }

        /// <summary>
        /// Sets the configuration for the caching system.
        /// </summary>
        /// <param name="configuration"></param>
        public static void Configuration(Configuration configuration)
        {
            Config = configuration ?? throw new InvalidOperationException("Invalid configuration");

            if (instance.Value.Initialized)
                throw new InvalidOperationException("Configuration already initialized");
        }
    }
}
