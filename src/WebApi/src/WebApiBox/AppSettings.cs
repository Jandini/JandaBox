namespace WebApiBox
{
    public class AppSettings
    {
        [ConfigurationKeyName("APPLICATION_NAME")]
        public string? ApplicationName { get; set; }

        [ConfigurationKeyName("APPLICATION_VERSION")]
        public string? ApplicationVersion { get; set; }
#if (elasticLog)

        [ConfigurationKeyName("ELASTICSEARCH_URI")]
        public Uri? ElasticsearchUri { get; set; }

        [ConfigurationKeyName("ELASTICSEARCH_DEBUG")]
        public bool ElasticsearchDebug { get; set; }
#endif
    }
}
