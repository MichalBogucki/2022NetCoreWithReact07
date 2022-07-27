using System;
using System.Threading.Tasks;
using _2022NetCoreWithReact07.Caches;
using _2022NetCoreWithReact07.DTOs.NasaImages.Input;
using Xunit;

namespace UnitTests;

public class RequestsCacheTests
{

    [Fact]
    public async Task CacheIsEmpty_AddsOneCollection()
    {
        //arrange
        IRequestsCache cache = new RequestsCache();
        var query1 = new NasaQueryParameters(string.Empty, string.Empty, string.Empty, string.Empty).ToString();

        //act
        var empty = !cache.Contains(query1);

        //assert
        Assert.True(empty);

    }
}