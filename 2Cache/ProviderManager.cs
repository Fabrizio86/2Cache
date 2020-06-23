namespace Main2Cache
{
    using System;
    using global::Main2Cache.Interfaces;
    using global::Main2Cache.Providers;

    public sealed class ProviderManager
    {
        private static readonly Lazy<ProviderManager> instance = new Lazy<ProviderManager>(() => new ProviderManager());

        private static Configuration Config { get; set; }

        private bool initialized = false;

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
                default:
                    initialized = false;
                    throw new InvalidOperationException("Invalid configuration parameters");
            }
        }

        public static ICachingProvider Provider { get; private set; }

        public static void Configuration(Configuration configuration)
        {
            Config = configuration ?? throw new InvalidOperationException("Invalid configuration");

            if (instance.Value.initialized)
                throw new InvalidOperationException("Configuration already initialized");

            instance.Value.initialized = true;
        }
    }
}
