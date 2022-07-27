using System.Net;
using _2022NetCoreWithReact07.DTOs.NasaImages.Input;
using _2022NetCoreWithReact07.Helpers;
using _2022NetCoreWithReact07.Services.Nasa;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _2022NetCoreWithReact07.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NasaController : ControllerBase
    {
        private readonly LoggerHelper<NasaController> _loggerHelper;
        private readonly INasaAppService _nasaAppService;

        public NasaController(INasaAppService nasaAppService, ILogger<NasaController> logger)
        {
            _nasaAppService = nasaAppService;
            _loggerHelper = new LoggerHelper<NasaController>(logger, GetType().Name);
        }
        
        [HttpGet]
        public async Task<object> Get(string? query = null, string? startYear = null, string? endYear = null, string mediaType = "image")
        {
            var queryInputs = new NasaQueryParameters(query, startYear, endYear, mediaType);

            _loggerHelper.LogStart(queryInputs.ToString());
            var result = await _nasaAppService.GetNasaImages(queryInputs);

            _loggerHelper.LogFinish(queryInputs.ToString());
            return StatusCode((int)HttpStatusCode.OK, result);
        }
        
    }
}
