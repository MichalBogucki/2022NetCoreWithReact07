using System.Collections.Concurrent;
using System.Text;
using _2022NetCoreWithReact07.Caches;
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
        private readonly IConfigProvider _configProvider;
        private readonly LoggerHelper<NasaAppService> _loggerHelper;
        private readonly HttpClient _httpClient;
        public readonly IRequestsCache _requestsCache;
        private readonly string _nasaImageApiBaseUrl;

        private bool _useAmpersand = false;

        public NasaAppService(HttpClient httpClient, IConfigProvider configProvider, ILogger<NasaAppService> logger, IRequestsCache requestsCache)
        {;
            _configProvider = configProvider;
            _nasaImageApiBaseUrl = _configProvider.GetNasaBaseUrl();
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_nasaImageApiBaseUrl);
            _loggerHelper = new LoggerHelper<NasaAppService>(logger, GetType().Name);
            _requestsCache = requestsCache;
        }

        public async Task<IEnumerable<MinimalImageData>> GetNasaImages(NasaQueryParameters nasaQueryParameters)
        {
            try
            {
                _loggerHelper.LogStart();
                var queryString = nasaQueryParameters.ToString();
                if (_requestsCache.Contains(queryString))
                {
                    return _requestsCache.GetValue(queryString);
                }

                var requestUri = new StringBuilder("search?");
                BuildQueryParameters("q", nasaQueryParameters.Query, requestUri);
                BuildQueryParameters("year_start", nasaQueryParameters.StartYear, requestUri);
                BuildQueryParameters("year_end", nasaQueryParameters.EndYear, requestUri);
                BuildQueryParameters("media_type", nasaQueryParameters.MediaType, requestUri);

                var nasaImagesDto = await GetAsync<NasaImagesDto>(requestUri.ToString());

                IEnumerable<MinimalImageData> topTenImages;
                if (nasaImagesDto != null && nasaImagesDto.Collection != null)
                {
                    topTenImages = nasaImagesDto!.Collection.Items.Take(12).Select(img => img.GetMinimalData());
                }
                else
                {
                    topTenImages = new List<MinimalImageData>();
                }

                _requestsCache.AddValue(queryString, topTenImages);

                _loggerHelper.LogFinish();
                return topTenImages!;
            }
            catch (Exception ex)
            {
                _loggerHelper.LogError(ex.Message, ex.StackTrace);
                throw;
            }
        }

        private void BuildQueryParameters(string queryParameterName, string queryParameterValue, StringBuilder requestUri)
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
