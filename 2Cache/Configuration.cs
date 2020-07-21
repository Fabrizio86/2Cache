namespace Main2Cache
{
    using System.Collections.Generic;
    using global::Main2Cache.Configurations;

    /// <summary>
    /// Configuration class.
    /// </summary>
    public sealed class Configuration
    {
        /// <summary>
        /// Gets or sets the caching <see cref="ProviderManager"/>.
        /// </summary>
        public ProviderType ProviderType { get; set; }

        /// <summary>
        /// Gets or sets the configuration parameters.
        /// </summary>
        public Dictionary<string, string> Parameters { get; set; }
    }
}
