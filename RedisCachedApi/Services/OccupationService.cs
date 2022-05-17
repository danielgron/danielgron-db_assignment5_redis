using System.Runtime.Serialization.Formatters.Binary;
using StackExchange.Redis;

using RestSharp;
using Newtonsoft.Json;

namespace RedisCachedApi.Services
{
    public class OccupationService
    {
        private ILogger<OccupationService> _logger;
        private IDatabase _cache;
        private RestClient _http;

        public OccupationService(ILogger<OccupationService> logger, IDatabase cache)
        {
            _logger = logger;
            _cache = cache;
            _http = new RestClient("https://datausa.io");
        }

        public async Task<IEnumerable<Occupation>> GetOccupationsCached()
        {
            IEnumerable<Occupation> result = null;
            var cached = await _cache.ListRangeAsync("occupations");
            if (cached.Length == 0)
            {
                result = (await _http.GetAsync<OccupationWrapper>(new RestRequest("/api/data?measures=Average%20Wage,Average%20Wage%20Appx%20MOE&drilldowns=Detailed%20Occupation")))!.Data;
                foreach (Occupation occ in result)
                {
                    await _cache.ListRightPushAsync("occupations", JsonConvert.SerializeObject(occ));
                    await CacheAvgWage(occ);
                }
                var exp = await _cache.KeyExpireAsync("occupations", new TimeSpan(0,0,30));
                return result;

            }
            return cached.Select(x => JsonConvert.DeserializeObject<Occupation>((string)x)!);
        }        

        private async Task CacheAvgWage(Occupation occupation){
            await _cache.HashSetAsync("wages-year-occ-id"+occupation.IDYear, occupation.IDDetailedOccupation, occupation.AverageWage);
            var exp1 = await _cache.KeyExpireAsync("wages-year-occ-id"+occupation.IDYear, new TimeSpan(0,0,30));
            await _cache.ListRightPushAsync("wages-year-"+occupation.IDYear, JsonConvert.SerializeObject(occupation));
            var exp2 = await _cache.KeyExpireAsync("wages-year-"+occupation.IDYear, new TimeSpan(0,0,30));
        }

        public async Task<IEnumerable<Occupation>> GetOccupationsUnCached()
        {
            IEnumerable<Occupation> result = null;
            result = (await _http.GetAsync<OccupationWrapper>(new RestRequest()))!.Data;
            
            foreach (Occupation occ in result)
                {
                    await _cache.ListRightPushAsync("occupations", JsonConvert.SerializeObject(occ));
                }
                await _cache.KeyExpireAsync("occupations", new TimeSpan(0,0,30));

            return result;
        }
    }
}