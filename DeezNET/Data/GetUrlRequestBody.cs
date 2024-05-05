using Newtonsoft.Json;

namespace DeezNET.Data;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public class GetUrlRequestBody
{
    [JsonProperty("license_token")]
    public string LicenseToken { get; set; }

    [JsonProperty("media")]
    public MediaHolder[] Media { get; set; }

    [JsonProperty("track_tokens")]
    public string[] TrackTokens { get; set; }

    public class MediaHolder
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("formats")]
        public Format[] Formats { get; set; }
    }

    public class Format
    {
        [JsonProperty("cipher")]
        public string Cipher { get; set; }

        [JsonProperty("format")]
        public string FormatFormat { get; set; }
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.