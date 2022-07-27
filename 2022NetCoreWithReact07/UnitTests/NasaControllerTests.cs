using System;
using System.Threading.Tasks;
using _2022NetCoreWithReact07.Controllers;
using _2022NetCoreWithReact07.DTOs.NasaImages.Input;
using _2022NetCoreWithReact07.Services.Nasa;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace UnitTests;

public class NasaControllerTests
{
    private readonly Mock<INasaAppService> _appServiceMock = new Mock<INasaAppService>();
    private readonly Mock<ILogger<NasaController>> _loggerMock = new Mock<ILogger<NasaController>>();
    private readonly NasaController _controller;
    public NasaControllerTests()
    {
        _controller = new NasaController(_appServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Get_ReceivesFaultyData_500ErrorIsReturned()
    {
        //arrange
        _appServiceMock
            .Setup(x => x.GetNasaImages(It.IsAny<NasaQueryParameters>()))
            .Throws(new InvalidOperationException(message: "UnitTest"));

        //act
        var results = await _controller.Get() as ObjectResult;

        //assert
        Assert.Equal(500, results.StatusCode);
    }
}