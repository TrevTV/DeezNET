using Newtonsoft.Json;

namespace DeezNET.Data;

public class TrackPage
{
    [JsonProperty("DATA")]
    public DataHolder Data { get; set; }

    [JsonProperty("LYRICS")]
    public LyricsHolder Lyrics { get; set; }

    [JsonProperty("ISRC")]
    public Isrc IsrcData { get; set; }

    [JsonProperty("RELATED_ALBUMS")]
    public Isrc RelatedAlbums { get; set; }

    public class DataHolder
    {
        [JsonProperty("SNG_ID")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long SngId { get; set; }

        [JsonProperty("PRODUCT_TRACK_ID")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ProductTrackId { get; set; }

        [JsonProperty("UPLOAD_ID")]
        public long UploadId { get; set; }

        [JsonProperty("SNG_TITLE")]
        public string SngTitle { get; set; }

        [JsonProperty("ART_ID")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ArtId { get; set; }

        [JsonProperty("PROVIDER_ID")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ProviderId { get; set; }

        [JsonProperty("ART_NAME")]
        public string ArtName { get; set; }

        [JsonProperty("ARTIST_IS_DUMMY")]
        public bool ArtistIsDummy { get; set; }

        [JsonProperty("ARTISTS")]
        public Artist[] Artists { get; set; }

        [JsonProperty("ALB_ID")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long AlbId { get; set; }

        [JsonProperty("ALB_TITLE")]
        public string AlbTitle { get; set; }

        [JsonProperty("TYPE")]
        public long Type { get; set; }

        [JsonProperty("VIDEO")]
        public bool Video { get; set; }

        [JsonProperty("DURATION")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Duration { get; set; }

        [JsonProperty("ALB_PICTURE")]
        public string AlbPicture { get; set; }

        [JsonProperty("ART_PICTURE")]
        public string ArtPicture { get; set; }

        [JsonProperty("RANK_SNG")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long RankSng { get; set; }

        [JsonProperty("FILESIZE_AAC_64")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long FilesizeAac64 { get; set; }

        [JsonProperty("FILESIZE_AC4_IMS")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long FilesizeAc4Ims { get; set; }

        [JsonProperty("FILESIZE_DD_JOC")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long FilesizeDdJoc { get; set; }

        [JsonProperty("FILESIZE_MP3_64")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long FilesizeMp364 { get; set; }

        [JsonProperty("FILESIZE_MP3_128")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long FilesizeMp3128 { get; set; }

        [JsonProperty("FILESIZE_MP3_256")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long FilesizeMp3256 { get; set; }

        [JsonProperty("FILESIZE_MP3_320")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long FilesizeMp3320 { get; set; }

        [JsonProperty("FILESIZE_MP4_RA1")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long FilesizeMp4Ra1 { get; set; }

        [JsonProperty("FILESIZE_MP4_RA2")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long FilesizeMp4Ra2 { get; set; }

        [JsonProperty("FILESIZE_MP4_RA3")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long FilesizeMp4Ra3 { get; set; }

        [JsonProperty("FILESIZE_MHM1_RA1")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long FilesizeMhm1Ra1 { get; set; }

        [JsonProperty("FILESIZE_MHM1_RA2")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long FilesizeMhm1Ra2 { get; set; }

        [JsonProperty("FILESIZE_MHM1_RA3")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long FilesizeMhm1Ra3 { get; set; }

        [JsonProperty("FILESIZE_FLAC")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long FilesizeFlac { get; set; }

        [JsonProperty("FILESIZE")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Filesize { get; set; }

        [JsonProperty("GAIN")]
        public string Gain { get; set; }

        [JsonProperty("MEDIA_VERSION")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long MediaVersion { get; set; }

        [JsonProperty("DISK_NUMBER")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long DiskNumber { get; set; }

        [JsonProperty("TRACK_NUMBER")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TrackNumber { get; set; }

        [JsonProperty("TRACK_TOKEN")]
        public string TrackToken { get; set; }

        [JsonProperty("TRACK_TOKEN_EXPIRE")]
        public long TrackTokenExpire { get; set; }

        [JsonProperty("VERSION")]
        public string Version { get; set; }

        [JsonProperty("MEDIA")]
        public Media[] Media { get; set; }

        [JsonProperty("EXPLICIT_LYRICS")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ExplicitLyrics { get; set; }

        [JsonProperty("RIGHTS")]
        public Rights Rights { get; set; }

        [JsonProperty("ISRC")]
        public string Isrc { get; set; }

        [JsonProperty("HIERARCHICAL_TITLE")]
        public string HierarchicalTitle { get; set; }

        [JsonProperty("SNG_CONTRIBUTORS")]
        public SngContributors SngContributors { get; set; }

        [JsonProperty("LYRICS_ID")]
        public long LyricsId { get; set; }

        [JsonProperty("EXPLICIT_TRACK_CONTENT")]
        public ExplicitTrackContent ExplicitTrackContent { get; set; }

        [JsonProperty("COPYRIGHT")]
        public string Copyright { get; set; }

        [JsonProperty("PHYSICAL_RELEASE_DATE")]
        public DateTimeOffset PhysicalReleaseDate { get; set; }

        [JsonProperty("S_MOD")]
        public long SMod { get; set; }

        [JsonProperty("S_PREMIUM")]
        public long SPremium { get; set; }

        [JsonProperty("DATE_START_PREMIUM")]
        public DateTimeOffset DateStartPremium { get; set; }

        [JsonProperty("DATE_START")]
        public DateTimeOffset DateStart { get; set; }

        [JsonProperty("STATUS")]
        public long Status { get; set; }

        [JsonProperty("USER_ID")]
        public long UserId { get; set; }

        [JsonProperty("URL_REWRITING")]
        public string UrlRewriting { get; set; }

        [JsonProperty("SNG_STATUS")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long SngStatus { get; set; }

        [JsonProperty("AVAILABLE_COUNTRIES")]
        public AvailableCountries AvailableCountries { get; set; }

        [JsonProperty("UPDATE_DATE")]
        public DateTimeOffset UpdateDate { get; set; }

        [JsonProperty("__TYPE__")]
        public string DataType { get; set; }
    }

    public class Artist
    {
        [JsonProperty("ART_ID")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ArtId { get; set; }

        [JsonProperty("ROLE_ID")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long RoleId { get; set; }

        [JsonProperty("ARTISTS_SONGS_ORDER")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ArtistsSongsOrder { get; set; }

        [JsonProperty("ART_NAME")]
        public string ArtName { get; set; }

        [JsonProperty("ARTIST_IS_DUMMY")]
        public bool ArtistIsDummy { get; set; }

        [JsonProperty("ART_PICTURE")]
        public string ArtPicture { get; set; }

        [JsonProperty("RANK")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Rank { get; set; }

        [JsonProperty("__TYPE__")]
        public string Type { get; set; }
    }

    public class AvailableCountries
    {
        [JsonProperty("STREAM_ADS")]
        public string[] StreamAds { get; set; }

        [JsonProperty("STREAM_SUB_ONLY")]
        public object[] StreamSubOnly { get; set; }
    }

    public class ExplicitTrackContent
    {
        [JsonProperty("EXPLICIT_LYRICS_STATUS")]
        public long ExplicitLyricsStatus { get; set; }

        [JsonProperty("EXPLICIT_COVER_STATUS")]
        public long ExplicitCoverStatus { get; set; }
    }

    public class Media
    {
        [JsonProperty("TYPE")]
        public string Type { get; set; }

        [JsonProperty("HREF")]
        public Uri Href { get; set; }
    }

    public class Rights
    {
        [JsonProperty("STREAM_ADS_AVAILABLE")]
        public bool StreamAdsAvailable { get; set; }

        [JsonProperty("STREAM_ADS")]
        public DateTimeOffset StreamAds { get; set; }

        [JsonProperty("STREAM_SUB_AVAILABLE")]
        public bool StreamSubAvailable { get; set; }

        [JsonProperty("STREAM_SUB")]
        public DateTimeOffset StreamSub { get; set; }
    }

    public class SngContributors
    {
        [JsonProperty("artist")]
        public string[] Artist { get; set; }

        [JsonProperty("main_artist")]
        public string[] MainArtist { get; set; }

        [JsonProperty("composer")]
        public string[] Composer { get; set; }

        [JsonProperty("author")]
        public string[] Author { get; set; }
    }

    public class Isrc
    {
        [JsonProperty("data")]
        public Datum[] Data { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }

    public class Datum
    {
        [JsonProperty("ART_NAME")]
        public string ArtName { get; set; }

        [JsonProperty("ART_ID")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ArtId { get; set; }

        [JsonProperty("ALB_PICTURE")]
        public string AlbPicture { get; set; }

        [JsonProperty("ALB_ID")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long AlbId { get; set; }

        [JsonProperty("ALB_TITLE")]
        public string AlbTitle { get; set; }

        [JsonProperty("DURATION")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Duration { get; set; }

        [JsonProperty("DIGITAL_RELEASE_DATE")]
        public DateTimeOffset DigitalReleaseDate { get; set; }

        [JsonProperty("RIGHTS")]
        public Rights Rights { get; set; }

        [JsonProperty("LYRICS_ID")]
        public long LyricsId { get; set; }

        [JsonProperty("__TYPE__")]
        public string Type { get; set; }
    }

    public class LyricsHolder
    {
        [JsonProperty("LYRICS_ID")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long LyricsId { get; set; }

        [JsonProperty("LYRICS_SYNC_JSON")]
        public LyricsSyncJson[] LyricsSyncJson { get; set; }

        [JsonProperty("LYRICS_TEXT")]
        public string LyricsText { get; set; }

        [JsonProperty("LYRICS_COPYRIGHTS")]
        public string LyricsCopyrights { get; set; }

        [JsonProperty("LYRICS_WRITERS")]
        public string LyricsWriters { get; set; }
    }

    public class LyricsSyncJson
    {
        [JsonProperty("lrc_timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public string LrcTimestamp { get; set; }

        [JsonProperty("milliseconds", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Milliseconds { get; set; }

        [JsonProperty("duration", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Duration { get; set; }

        [JsonProperty("line")]
        public string Line { get; set; }
    }
}

internal class ParseStringConverter : JsonConverter
{
    public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        long l;
        if (Int64.TryParse(value, out l))
        {
            return l;
        }
        throw new Exception("Cannot unmarshal type long");
    }

    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
    {
        if (untypedValue == null)
        {
            serializer.Serialize(writer, null);
            return;
        }
        var value = (long)untypedValue;
        serializer.Serialize(writer, value.ToString());
        return;
    }

    public static readonly ParseStringConverter Singleton = new ParseStringConverter();
}