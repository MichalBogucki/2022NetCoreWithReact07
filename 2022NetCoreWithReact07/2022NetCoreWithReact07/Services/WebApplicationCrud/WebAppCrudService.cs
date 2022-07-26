using System.Diagnostics;
using System.Text;
using _2022NetCoreWithReact07.DTOs.WebApplicationCrud;
using Newtonsoft.Json;

namespace _2022NetCoreWithReact07.Services.WebApplicationCrud
{
    public interface IWebApplicationCrudService
    {
        public Task<List<Client>> GetAllClients();
        public Task<List<ClientExecution>> InitializeClients();
        public Task<ClientExecution> UpdateClientName(Client client);
        public Task<List<ClientExecution>> CreateClientTwins(Client client);
    }

    public class WebAppCrudService : IWebApplicationCrudService
    {
        private readonly string _webAppCrudApiBaseUrl;
        private readonly HttpClient _httpClient;
        public WebAppCrudService(HttpClient httpClient, IConfiguration config)
        {
            _webAppCrudApiBaseUrl = config.GetValue<string>("WebAppCrudApiUrl");
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_webAppCrudApiBaseUrl);
        }

        public async Task<List<Client>> GetAllClients()
        {
            var requestUri = $"api/Client";
            var clients = await GetAsync<Client>(requestUri); 

            return clients!;
        }

        public async Task<List<ClientExecution>> InitializeClients()
        {
            throw new NotImplementedException();
        }

        public async Task<ClientExecution> UpdateClientName(Client client)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ClientExecution>> CreateClientTwins(Client client)
        {
            var stopwatch = Stopwatch.StartNew();

            var requestUri = $"api/Client/{client.Id}";

            var twins = new List<Client>()
            {
                new Client() { Id = "100_"+ client.Id, Name = client.Name + "_TwinA" },
                new Client() { Id = "101_"+ client.Id, Name = client.Name + "_TwinB" }
            };

            var createdTwins = new List<ClientExecution>();
            foreach (var twin in twins)
            {
                var createdTwin = await PutAsync(twin, requestUri);
                createdTwins.Add(new ClientExecution(createdTwin, stopwatch.ElapsedMilliseconds));
            }

            return createdTwins;
        }

        private async Task<List<T>?> GetAsync<T>(string requestUri)
        {
            var responseMessage = await _httpClient.GetAsync(requestUri);
            var resultAsJson = await responseMessage.Content.ReadAsStringAsync();
            var createdTwin = JsonConvert.DeserializeObject<List<T>>(resultAsJson);
            return createdTwin;
        }

        private async Task<T?> PutAsync<T>(T twin, string requestUri)
        {
            var content = JsonContent.Create(twin);
            var responseMessage = await _httpClient.PutAsync(requestUri, content);
            var resultAsJson = await responseMessage.Content.ReadAsStringAsync();
            var createdTwin = JsonConvert.DeserializeObject<T>(resultAsJson);
            return createdTwin;
        }
    }
}
