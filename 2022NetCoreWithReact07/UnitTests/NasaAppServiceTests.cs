using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using _2022NetCoreWithReact07.Caches;
using _2022NetCoreWithReact07.DTOs.NasaImages.Input;
using _2022NetCoreWithReact07.DTOs.NasaImages.Output;
using _2022NetCoreWithReact07.Helpers;
using _2022NetCoreWithReact07.Services.Nasa;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Xunit;

namespace UnitTests
{
    public class NasaAppServiceTests
    {
        private readonly Mock<IConfigProvider> _configProviderMock = new Mock<IConfigProvider>();
        private readonly Mock<ILogger<NasaAppService>> _loggerMock = new Mock<ILogger<NasaAppService>>();
        private readonly INasaAppService nasaAppService;
        private readonly HttpClient _httpClientMock;

        public NasaAppServiceTests()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(SerializeNasaDto())});

            _httpClientMock = new HttpClient(mockHttpMessageHandler.Object);
            _configProviderMock.Setup(x => x.GetNasaBaseUrl()).Returns("http://xyz");
            
            nasaAppService = new NasaAppService(_httpClientMock, _configProviderMock.Object, _loggerMock.Object, new RequestsCache());
        }

        private static string SerializeNasaDto()
        {
            var nasaDto = new NasaImagesDto()
            {
                Collection = new Collection()
                {
                    Href = string.Empty,
                    Items = new List<Item>()
                    {
                        new Item()
                        {
                            Data = new List<Datum>(),
                            Links = new List<Link>()
                            {
                                new Link()
                                {
                                    Href = "a"
                                }
                            },
                        }
                    },
                    Links = new List<Link>(),
                    Metadata = new Metadata(),
                    Version = "1"
                }
            };
            return JsonConvert.SerializeObject(nasaDto);
        }


        [Fact]
        public async Task GetNasaImages_ProperDataProvided_TwelveMinimalImageDataReturned()
        {
            //arrange
            var nasaQueryParameters = new NasaQueryParameters(string.Empty, "1990", string.Empty, "image");

            //act
            var results = await nasaAppService.GetNasaImages(nasaQueryParameters);

            //assert
            Assert.True(results.Any());
            Assert.Equal("a",results.Single().Href);
        }        
        
        [Fact]
        public async Task CompareMinimalImageDto_ShouldEqual()
        {
            //arrange
            var dateTimeNow = DateTime.Now;
            var imageA = new MinimalImageData()
            {
                Description = "d",
                DateCreated = dateTimeNow
            };
            var imageB = new MinimalImageData()
            {
                Description = "d",
                DateCreated = dateTimeNow
            };

            //act
            var equal = imageB.Equals(imageB);

            //assert
            Assert.True(equal);
        }
    }
}