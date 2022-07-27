using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _2022NetCoreWithReact07.Caches;
using _2022NetCoreWithReact07.DTOs.NasaImages.Input;
using _2022NetCoreWithReact07.DTOs.NasaImages.Output;
using Xunit;

namespace UnitTests;

public class RequestsCacheTests
{

    [Fact]
    public async Task CacheIsEmpty_CheckIfContains_WasEmpty()
    {
        //arrange
        IRequestsCache cache = new RequestsCache();
        var query1 = new NasaQueryParameters(string.Empty, string.Empty, string.Empty, string.Empty).ToString();

        //act
        var empty = !cache.Contains(query1);

        //assert
        Assert.True(empty);
    }
    
    [Fact]
    public async Task CacheIsEmpty_AddOnceUniqueItem_WillContain()
    {
        //arrange
        IRequestsCache cache = new RequestsCache();
        var query1 = new NasaQueryParameters(string.Empty, string.Empty, string.Empty, string.Empty).ToString();
        var value1 = new List<MinimalImageData>() { new MinimalImageData() };

        //act
        var emptyBefore = !cache.Contains(query1);
        cache.AddValue(query1, value1);

        var containsAfter = cache.Contains(query1);
        var valueAfter = cache.GetValue(query1);
        
        //assert
        Assert.True(emptyBefore);
        Assert.Equal(value1, valueAfter);
    }


    [Fact]
    public async Task CacheIsEmpty_AddTwiceThesameUniqueItem_WillContainOne()
    {
        //arrange
        IRequestsCache cache = new RequestsCache();
        var query1 = new NasaQueryParameters(string.Empty, string.Empty, string.Empty, string.Empty).ToString();
        var value1 = new List<MinimalImageData>() { new MinimalImageData() };

        //act
        var emptyBefore = !cache.Contains(query1);
        cache.AddValue(query1, value1);
        cache.AddValue(query1, value1);

        var containsAfter = cache.Contains(query1);
        var valueAfter = cache.GetValue(query1);
        var keyCount = cache.GetKeyCount();
        
        //assert
        Assert.True(emptyBefore);
        Assert.Equal(value1, valueAfter);
        Assert.Equal(1, keyCount);
    }

}