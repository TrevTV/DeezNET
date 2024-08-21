using Newtonsoft.Json;

namespace DeezNET.Data
{
    public class SyncLyrics
    {
        [JsonProperty("lrc_timestamp")]
        public string? LrcTimestamp { get; set; }

        [JsonProperty("milliseconds")]
        public string? Milliseconds { get; set; }

        [JsonProperty("duration")]
        public string? Duration { get; set; }

        [JsonProperty("line")]
        public string? Line { get; set; }
    }
}
