using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace RedisCachedApi;

public class Occupation
{
        [JsonProperty("ID Detailed Occupation")]
        [JsonPropertyName("ID Detailed Occupation")]
        public string IDDetailedOccupation { get; set; }

        [JsonProperty("Detailed Occupation")]
        [JsonPropertyName("Detailed Occupation")]
        public string DetailedOccupation { get; set; }

        [JsonProperty("ID Year")]
        [JsonPropertyName("ID Year")]
        public int IDYear { get; set; }
        public string Year { get; set; }

        [JsonProperty("Average Wage")]
        [JsonPropertyName("Average Wage")]
        public double AverageWage { get; set; }

        [JsonProperty("Average Wage Appx MOE")]
        [JsonPropertyName("Average Wage Appx MOE")]
        public double AverageWageAppxMOE { get; set; }

        [JsonProperty("Slug Detailed Occupation")]
        [JsonPropertyName("Slug Detailed Occupation")]
        public string SlugDetailedOccupation { get; set; }
    }
