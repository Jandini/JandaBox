namespace WebApiBox
{
    public class AppSettings
    {
        [ConfigurationKeyName("APPLICATION_NAME")]
        public string? ApplicationName { get; set; }

        [ConfigurationKeyName("APPLICATION_VERSION")]
        public string? ApplicationVersion { get; set; }
    }
}
