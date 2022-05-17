using Newtonsoft.Json;

namespace RedisCachedApi;

public class OccupationResult
{
        public IEnumerable<Occupation> Occupations {get;set;}
        public long TimeElapsed {get;set;}
    }
