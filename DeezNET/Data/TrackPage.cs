using Newtonsoft.Json;

namespace DeezNET.Data;

public class TrackPage
{
    [JsonProperty("DATA")]
    public DataData Data { get; set; }

    [JsonProperty("ISRC")]
    public IsrcData Isrc { get; set; }

    [JsonProperty("RELATED_ALBUMS")]
    public IsrcData RelatedAlbums { get; set; }

    public class DataData
    {
        [JsonProperty("SNG_ID")]
        public string SngId { get; set; }

        [JsonProperty("PRODUCT_TRACK_ID")]
        public string ProductTrackId { get; set; }

        [JsonProperty("UPLOAD_ID")]
        public long UploadId { get; set; }

        [JsonProperty("SNG_TITLE")]
        public string SngTitle { get; set; }

        [JsonProperty("ART_ID")]
        public string ArtId { get; set; }

        [JsonProperty("PROVIDER_ID")]
        public string ProviderId { get; set; }

        [JsonProperty("ART_NAME")]
        public string ArtName { get; set; }

        [JsonProperty("ARTIST_IS_DUMMY")]
        public bool ArtistIsDummy { get; set; }

        [JsonProperty("ARTISTS")]
        public Artist[] Artists { get; set; }

        [JsonProperty("ALB_ID")]
        public string AlbId { get; set; }

        [JsonProperty("ALB_TITLE")]
        public string AlbTitle { get; set; }

        [JsonProperty("TYPE")]
        public long Type { get; set; }

        [JsonProperty("MD5_ORIGIN")]
        public string Md5Origin { get; set; }

        [JsonProperty("VIDEO")]
        public bool Video { get; set; }

        [JsonProperty("DURATION")]
        public string Duration { get; set; }

        [JsonProperty("ALB_PICTURE")]
        public string AlbPicture { get; set; }

        [JsonProperty("ART_PICTURE")]
        public string ArtPicture { get; set; }

        [JsonProperty("RANK_SNG")]
        public string RankSng { get; set; }

        [JsonProperty("FILESIZE_AAC_64")]
        public string FilesizeAac64 { get; set; }

        [JsonProperty("FILESIZE_MP3_64")]
        public string FilesizeMp364 { get; set; }

        [JsonProperty("FILESIZE_MP3_128")]
        public string FilesizeMp3128 { get; set; }

        [JsonProperty("FILESIZE_MP3_256")]
        public string FilesizeMp3256 { get; set; }

        [JsonProperty("FILESIZE_MP3_320")]
        public string FilesizeMp3320 { get; set; }

        [JsonProperty("FILESIZE_FLAC")]
        public string FilesizeFlac { get; set; }

        [JsonProperty("FILESIZE")]
        public string Filesize { get; set; }

        [JsonProperty("GAIN")]
        public string Gain { get; set; }

        [JsonProperty("MEDIA_VERSION")]
        public string MediaVersion { get; set; }

        [JsonProperty("DISK_NUMBER")]
        public string DiskNumber { get; set; }

        [JsonProperty("TRACK_NUMBER")]
        public string TrackNumber { get; set; }

        [JsonProperty("TRACK_TOKEN")]
        public string TrackToken { get; set; }

        [JsonProperty("TRACK_TOKEN_EXPIRE")]
        public long TrackTokenExpire { get; set; }

        [JsonProperty("VERSION")]
        public string Version { get; set; }

        [JsonProperty("MEDIA")]
        public Media[] Media { get; set; }

        [JsonProperty("EXPLICIT_LYRICS")]
        public string ExplicitLyrics { get; set; }

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
        public string PhysicalReleaseDate { get; set; }

        [JsonProperty("S_MOD")]
        public long SMod { get; set; }

        [JsonProperty("S_PREMIUM")]
        public long SPremium { get; set; }

        [JsonProperty("DATE_START_PREMIUM")]
        public string DateStartPremium { get; set; }

        [JsonProperty("DATE_START")]
        public string DateStart { get; set; }

        [JsonProperty("STATUS")]
        public long Status { get; set; }

        [JsonProperty("USER_ID")]
        public long UserId { get; set; }

        [JsonProperty("URL_REWRITING")]
        public string UrlRewriting { get; set; }

        [JsonProperty("SNG_STATUS")]
        public string SngStatus { get; set; }

        [JsonProperty("AVAILABLE_COUNTRIES")]
        public AvailableCountries AvailableCountries { get; set; }

        [JsonProperty("UPDATE_DATE")]
        public string UpdateDate { get; set; }

        [JsonProperty("__TYPE__")]
        public string DataType { get; set; }
    }

    public class Artist
    {
        [JsonProperty("ART_ID")]
        public string ArtId { get; set; }

        [JsonProperty("ROLE_ID")]
        public string RoleId { get; set; }

        [JsonProperty("ARTISTS_SONGS_ORDER")]
        public string ArtistsSongsOrder { get; set; }

        [JsonProperty("ART_NAME")]
        public string ArtName { get; set; }

        [JsonProperty("ARTIST_IS_DUMMY")]
        public bool ArtistIsDummy { get; set; }

        [JsonProperty("ART_PICTURE")]
        public string ArtPicture { get; set; }

        [JsonProperty("RANK")]
        public string Rank { get; set; }

        [JsonProperty("LOCALES")]
        public object[] Locales { get; set; }

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
        public string StreamAds { get; set; }

        [JsonProperty("STREAM_SUB_AVAILABLE")]
        public bool StreamSubAvailable { get; set; }

        [JsonProperty("STREAM_SUB")]
        public string StreamSub { get; set; }
    }

    public class SngContributors
    {
        [JsonProperty("main_artist")]
        public string[] MainArtist { get; set; }

        [JsonProperty("featuring")]
        public string[] Featuring { get; set; }
    }

    public class IsrcData
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
        public string ArtId { get; set; }

        [JsonProperty("ALB_PICTURE")]
        public string AlbPicture { get; set; }

        [JsonProperty("ALB_ID")]
        public string AlbId { get; set; }

        [JsonProperty("ALB_TITLE")]
        public string AlbTitle { get; set; }

        [JsonProperty("DURATION")]
        public string Duration { get; set; }

        [JsonProperty("DIGITAL_RELEASE_DATE")]
        public string DigitalReleaseDate { get; set; }

        [JsonProperty("RIGHTS")]
        public Rights Rights { get; set; }

        [JsonProperty("LYRICS_ID")]
        public long LyricsId { get; set; }

        [JsonProperty("__TYPE__")]
        public string Type { get; set; }
    }
}