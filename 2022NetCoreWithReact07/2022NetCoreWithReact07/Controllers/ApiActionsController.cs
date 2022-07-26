using System.Net;
using _2022NetCoreWithReact07.DTOs.WebApplicationCrud;
using _2022NetCoreWithReact07.Services.WebApplicationCrud;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _2022NetCoreWithReact07.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiActionsController : ControllerBase
    {
        private readonly IWebApplicationCrudService _wepAppCrudService;

        public ApiActionsController(IWebApplicationCrudService wepAppCrudService)
        {
            _wepAppCrudService = wepAppCrudService;
        }

        // GET: api/<ApiActions>
        [HttpGet]
        public async Task<object> Get()
        {
            var strings = GetStrings();
            var results = await PerformAsync<List<string>>(strings);
            return StatusCode(200, results);
        }

        // GET api/<ApiActions>/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            return "value";
        }

        // POST api/<ApiActions>
        [HttpPost(Name = "CreateTwinClients")]
        public async Task<object> Post([FromBody] Client client)
        {
            var result = await _wepAppCrudService.CreateClientTwins(client);
            return StatusCode((int)HttpStatusCode.Created, result);
        }

        // PUT api/<ApiActions>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ApiActions>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
        }
        
        private List<string> GetStrings()
        {
            return new List<string> { "value1", "value2", "siemka" };
        }

        private async Task<T> PerformAsync <T>(object func)
        {
            var type = typeof(T);
            return (await Task.Run(() => (T)Convert.ChangeType(func, type)!))!;
        }
    }
}
