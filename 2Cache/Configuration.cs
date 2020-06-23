namespace Main2Cache
{
    using System.Collections.Generic;
    using global::Main2Cache.Configurations;

    public sealed class Configuration
    {
        public ProviderType ProviderType { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
    }
}
