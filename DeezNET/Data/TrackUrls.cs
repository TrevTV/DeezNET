using Newtonsoft.Json;

namespace DeezNET.Data;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public class TrackUrls
{
    [JsonProperty("data")]
    public Datum[] Data { get; set; }

    public class Datum
    {
        [JsonProperty("media")]
        public MediaData[] Media { get; set; }
    }

    public class MediaData
    {
        [JsonProperty("media_type")]
        public string MediaType { get; set; }

        [JsonProperty("cipher")]
        public Cipher Cipher { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("sources")]
        public Source[] Sources { get; set; }

        [JsonProperty("nbf")]
        public long Nbf { get; set; }

        [JsonProperty("exp")]
        public long Exp { get; set; }
    }

    public class Cipher
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Source
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("provider")]
        public string Provider { get; set; }
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.