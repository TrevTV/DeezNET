using Newtonsoft.Json;

namespace DeezNET.Data;

public class AlbumPage
{
    [JsonProperty("DATA")]
    public DataData Data { get; set; }

    [JsonProperty("SONGS")]
    public CommentsData Songs { get; set; }

    [JsonProperty("ALBUMS")]
    public AlbumsData Albums { get; set; }

    [JsonProperty("RELATED_ARTISTS")]
    public RelatedArtistsData RelatedArtists { get; set; }

    [JsonProperty("COMMENTS")]
    public CommentsData Comments { get; set; }

    public class AlbumsData
    {
        [JsonProperty("data")]
        public object[] Data { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("start")]
        public long Start { get; set; }

        [JsonProperty("nb")]
        public long Nb { get; set; }

        [JsonProperty("art_id")]
        public long ArtId { get; set; }
    }

    public class CommentsData
    {
        [JsonProperty("data")]
        public Datum[] Data { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("filtered_count", NullValueHandling = NullValueHandling.Ignore)]
        public long? FilteredCount { get; set; }
    }

    public class Datum
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
        public DatumArtist[] Artists { get; set; }

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
        public MediaData[] Media { get; set; }

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
        public ExplicitContent ExplicitTrackContent { get; set; }

        [JsonProperty("__TYPE__")]
        public string DatumType { get; set; }
    }

    public class DatumArtist
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

        [JsonProperty("__TYPE__")]
        public string Type { get; set; }

        [JsonProperty("SMARTRADIO", NullValueHandling = NullValueHandling.Ignore)]
        public long? Smartradio { get; set; }
    }

    public class ExplicitContent
    {
        [JsonProperty("EXPLICIT_LYRICS_STATUS")]
        public long ExplicitLyricsStatus { get; set; }

        [JsonProperty("EXPLICIT_COVER_STATUS")]
        public long ExplicitCoverStatus { get; set; }
    }

    public class MediaData
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

        [JsonProperty("featuring", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Featuring { get; set; }
    }

    public class DataData
    {
        [JsonProperty("ALB_ID")]
        public string AlbId { get; set; }

        [JsonProperty("ART_ID")]
        public string ArtId { get; set; }

        [JsonProperty("ART_NAME")]
        public string ArtName { get; set; }

        [JsonProperty("ARTISTS")]
        public DataArtist[] Artists { get; set; }

        [JsonProperty("URL_REWRITING")]
        public string UrlRewriting { get; set; }

        [JsonProperty("LABEL_NAME")]
        public string LabelName { get; set; }

        [JsonProperty("STYLE_NAME")]
        public string StyleName { get; set; }

        [JsonProperty("ALB_TITLE")]
        public string AlbTitle { get; set; }

        [JsonProperty("VERSION")]
        public string Version { get; set; }

        [JsonProperty("ALB_PICTURE")]
        public string AlbPicture { get; set; }

        [JsonProperty("DIGITAL_RELEASE_DATE")]
        public string DigitalReleaseDate { get; set; }

        [JsonProperty("PHYSICAL_RELEASE_DATE")]
        public string PhysicalReleaseDate { get; set; }

        [JsonProperty("ORIGINAL_RELEASE_DATE")]
        public string OriginalReleaseDate { get; set; }

        [JsonProperty("PROVIDER_ID")]
        public string ProviderId { get; set; }

        [JsonProperty("SONY_PROD_ID")]
        public string SonyProdId { get; set; }

        [JsonProperty("UPC")]
        public string Upc { get; set; }

        [JsonProperty("STATUS")]
        public string Status { get; set; }

        [JsonProperty("ALB_CONTRIBUTORS")]
        public AlbContributors AlbContributors { get; set; }

        [JsonProperty("NB_FAN")]
        public long NbFan { get; set; }

        [JsonProperty("AVAILABLE")]
        public bool Available { get; set; }

        [JsonProperty("EXPLICIT_ALBUM_CONTENT")]
        public ExplicitContent ExplicitAlbumContent { get; set; }

        [JsonProperty("SUBTYPES")]
        public Subtypes Subtypes { get; set; }

        [JsonProperty("PRODUCER_LINE")]
        public string ProducerLine { get; set; }

        [JsonProperty("COPYRIGHT")]
        public string Copyright { get; set; }

        [JsonProperty("__TYPE__")]
        public string Type { get; set; }
    }

    public class AlbContributors
    {
        [JsonProperty("main_artist")]
        public string[] MainArtist { get; set; }
    }

    public class DataArtist
    {
        [JsonProperty("ART_ID")]
        public string ArtId { get; set; }

        [JsonProperty("ROLE_ID")]
        public string RoleId { get; set; }

        [JsonProperty("ARTISTS_ALBUMS_ORDER")]
        public string ArtistsAlbumsOrder { get; set; }

        [JsonProperty("ART_NAME")]
        public string ArtName { get; set; }

        [JsonProperty("ART_PICTURE")]
        public string ArtPicture { get; set; }

        [JsonProperty("RANK")]
        public string Rank { get; set; }

        [JsonProperty("ARTIST_IS_DUMMY")]
        public bool ArtistIsDummy { get; set; }

        [JsonProperty("__TYPE__")]
        public string Type { get; set; }
    }

    public class Subtypes
    {
        [JsonProperty("isStudio")]
        public bool IsStudio { get; set; }

        [JsonProperty("isLive")]
        public bool IsLive { get; set; }

        [JsonProperty("isCompilation")]
        public bool IsCompilation { get; set; }

        [JsonProperty("isKaraoke")]
        public bool IsKaraoke { get; set; }
    }

    public class RelatedArtistsData
    {
        [JsonProperty("data")]
        public object[] Data { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("list_payload")]
        public object ListPayload { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("filtered_count")]
        public long FilteredCount { get; set; }

        [JsonProperty("element_id")]
        public long ElementId { get; set; }

        [JsonProperty("start")]
        public long Start { get; set; }

        [JsonProperty("nb")]
        public long Nb { get; set; }
    }
}