using System.Collections.Concurrent;
using _2022NetCoreWithReact07.DTOs.NasaImages.Input;
using _2022NetCoreWithReact07.DTOs.NasaImages.Output;

namespace _2022NetCoreWithReact07.Caches
{
    public interface IRequestsCache
    {
        public bool Contains(string key);
        public IEnumerable<MinimalImageData> GetValue(string key);
        public void AddValue(string key, IEnumerable<MinimalImageData> value);
    }

    public class RequestsCache : IRequestsCache
    {
        private ConcurrentDictionary<string, IEnumerable<MinimalImageData>> _requestsCache = new ConcurrentDictionary<string, IEnumerable<MinimalImageData>>();

        public bool Contains(string key)
        {
            var contains = _requestsCache.ContainsKey(key);
            return contains;
        }

        public IEnumerable<MinimalImageData> GetValue(string key)
        {
            var value = _requestsCache[key];
            return value;
        }

        public void AddValue(string key, IEnumerable<MinimalImageData> value)
        {
            _requestsCache.TryAdd(key, value);
        }
    }
}
