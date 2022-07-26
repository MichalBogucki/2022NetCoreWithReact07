using _2022NetCoreWithReact07.DTOs.WebApplicationCrud;

namespace _2022NetCoreWithReact07.Services.WebApplicationCrud
{
    public interface IWebApplicationCrudService
    {
        public Task<List<Client>> GetAllClients();
        public Task<List<Client>> InitializeClients();
        public Task UpdateClientById(int id, Client client);
        public Task CreateClientById(int id, Client client);
    }

    public class WebAppCrudService : IWebApplicationCrudService
    {
        private readonly string _webAppCrudApiBaseUrl;

        public WebAppCrudService(IConfiguration config)
        {
            _webAppCrudApiBaseUrl = config.GetValue<string>("WebAppCrudApiUrl");
        }

        public Task<List<Client>> GetAllClients()
        {
            throw new NotImplementedException();
        }

        public Task<List<Client>> InitializeClients()
        {
            throw new NotImplementedException();
        }

        public Task UpdateClientById(int id, Client client)
        {
            throw new NotImplementedException();
        }

        public Task CreateClientById(int id, Client client)
        {
            throw new NotImplementedException();
        }
    }
}
