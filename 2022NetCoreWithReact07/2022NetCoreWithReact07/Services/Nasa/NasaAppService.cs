using System.Text;
using _2022NetCoreWithReact07.DTOs.NasaImages.Input;
using _2022NetCoreWithReact07.DTOs.NasaImages.Output;
using _2022NetCoreWithReact07.Helpers;
using Newtonsoft.Json;

namespace _2022NetCoreWithReact07.Services.Nasa
{
    public interface INasaAppService
    {
        public Task<IEnumerable<MinimalImageData>> GetNasaImages(NasaQueryParameters nasaQueryParameters);
    }

    public class NasaAppService : INasaAppService
    {
        private readonly LoggerHelper<NasaAppService> _loggerHelper;
        private readonly string _nasaImageApiBaseUrl;
        private readonly HttpClient _httpClient;
        private bool _useAmpersand = false;

        public NasaAppService(HttpClient httpClient, IConfiguration config, ILogger<NasaAppService> logger)
        {
            _nasaImageApiBaseUrl = config.GetValue<string>("NasaImageApiBaseUrl");
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_nasaImageApiBaseUrl);
            _loggerHelper = new LoggerHelper<NasaAppService>(logger, GetType().Name);
        }

        public async Task<IEnumerable<MinimalImageData>> GetNasaImages(NasaQueryParameters nasaQueryParameters)
        {
            try
            {
                _loggerHelper.LogStart();

                var requestUri = new StringBuilder("search?");
                BuildQueryParameters("q", nasaQueryParameters.Query, requestUri);
                BuildQueryParameters("year_start", nasaQueryParameters.StartYear, requestUri);
                BuildQueryParameters("year_end", nasaQueryParameters.EndYear, requestUri);
                BuildQueryParameters("media_type", nasaQueryParameters.MediaType, requestUri);

                var nasaImagesDto = await GetAsync<NasaImagesDto>(requestUri.ToString());
                var topTenImages = nasaImagesDto!.Collection.Items.Take(10).Select(img => img.GetMinimalData());

                _loggerHelper.LogFinish();
                return topTenImages!;
            }
            catch (Exception ex)
            {
                _loggerHelper.LogError(ex.Message, ex.StackTrace);
                throw;
            }
        }

        private void BuildQueryParameters(string queryParameterName, string queryParameterValue,
            StringBuilder requestUri)
        {
            if (!string.IsNullOrEmpty(queryParameterValue))
            {
                requestUri.Append($"{InsertAmpersand()}{queryParameterName}={queryParameterValue}");
            }
        }

        private string InsertAmpersand()
        {
            var ampersandOrEmpty = _useAmpersand ? "&" : string.Empty;
            if (!_useAmpersand)
                _useAmpersand = true;

            return ampersandOrEmpty;
        }

        private async Task<T?> GetAsync<T>(string requestUri)
        {
            var responseMessage = await _httpClient.GetAsync(requestUri);
            var resultAsJson = await responseMessage.Content.ReadAsStringAsync();
            var createdTwin = JsonConvert.DeserializeObject<T>(resultAsJson);
            return createdTwin;
        }



        
    }
}
