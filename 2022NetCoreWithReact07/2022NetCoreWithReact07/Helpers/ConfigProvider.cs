namespace _2022NetCoreWithReact07.Helpers
{
    public interface IConfigProvider
    {
        public string GetNasaBaseUrl();
    }
    public class ConfigProvider : IConfigProvider
    {
        public IConfiguration _config { get; }

        public ConfigProvider(IConfiguration config)
        {
            _config = config;
        }

        public string GetNasaBaseUrl()
        {
            return _config.GetValue<string>("NasaImageApiBaseUrl");
        }
    }
}
