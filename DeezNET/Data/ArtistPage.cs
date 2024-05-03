namespace DeezNET.Data;

using System;
using Newtonsoft.Json;

public class ArtistPage
{
    [JsonProperty("DATA")]
    public ArtistPageData Data { get; set; }

    [JsonProperty("TOP")]
    public TopData Top { get; set; }

    [JsonProperty("HIGHLIGHT")]
    public HighlightData Highlight { get; set; }

    [JsonProperty("BIO")]
    public BioData Bio { get; set; }

    [JsonProperty("SELECTED_PLAYLIST")]
    public SelectedPlaylistData SelectedPlaylist { get; set; }

    [JsonProperty("RELATED_PLAYLIST")]
    public RelatedPlaylistData RelatedPlaylist { get; set; }

    [JsonProperty("RELATED_ARTISTS")]
    public RelatedArtistsData RelatedArtists { get; set; }

    [JsonProperty("VIDEO")]
    public VideoData Video { get; set; }

    [JsonProperty("ALBUMS")]
    public AlbumsData Albums { get; set; }

    public class AlbumsData
    {
        [JsonProperty("data")]
        public AlbumsDatum[] Data { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("cache_version")]
        public long CacheVersion { get; set; }

        [JsonProperty("filtered_count")]
        public long FilteredCount { get; set; }

        [JsonProperty("art_id")]
        public long ArtId { get; set; }

        [JsonProperty("start")]
        public long Start { get; set; }

        [JsonProperty("nb")]
        public long Nb { get; set; }
    }

    public class AlbumsDatum
    {
        [JsonProperty("PRODUCT_ALBUM_ID")]
        public string ProductAlbumId { get; set; }

        [JsonProperty("ALB_ID")]
        public string AlbId { get; set; }

        [JsonProperty("ALB_TITLE")]
        public string AlbTitle { get; set; }

        [JsonProperty("ALB_PICTURE")]
        public string AlbPicture { get; set; }

        [JsonProperty("HIGHLIGHT")]
        public string Highlight { get; set; }

        [JsonProperty("DIGITAL_RELEASE_DATE")]
        public string DigitalReleaseDate { get; set; }

        [JsonProperty("PHYSICAL_RELEASE_DATE")]
        public string PhysicalReleaseDate { get; set; }

        [JsonProperty("ORIGINAL_RELEASE_DATE")]
        public string OriginalReleaseDate { get; set; }

        [JsonProperty("PRODUCER_LINE")]
        public string ProducerLine { get; set; }

        [JsonProperty("TYPE")]
        public string Type { get; set; }

        [JsonProperty("SUBTYPES")]
        public Subtypes Subtypes { get; set; }

        [JsonProperty("ROLE_ID")]
        public long RoleId { get; set; }

        [JsonProperty("URL_REWRITING")]
        public string UrlRewriting { get; set; }

        [JsonProperty("ARTISTS")]
        public Artist[] Artists { get; set; }

        [JsonProperty("ART_NAME")]
        public string ArtName { get; set; }

        [JsonProperty("RANK")]
        public string Rank { get; set; }

        [JsonProperty("COPYRIGHT")]
        public string Copyright { get; set; }

        [JsonProperty("EXPLICIT_ALBUM_CONTENT")]
        public ExplicitContent ExplicitAlbumContent { get; set; }

        [JsonProperty("ARTISTS_ALBUMS_IS_OFFICIAL")]
        public bool ArtistsAlbumsIsOfficial { get; set; }

        [JsonProperty("__TYPE__")]
        public string DatumType { get; set; }

        [JsonProperty("SONGS")]
        public SelectedPlaylistData Songs { get; set; }
    }

    public class Artist
    {
        [JsonProperty("ART_ID")]
        public string ArtId { get; set; }

        [JsonProperty("ROLE_ID")]
        public string RoleId { get; set; }

        [JsonProperty("ARTISTS_ALBUMS_ORDER", NullValueHandling = NullValueHandling.Ignore)]
        public string ArtistsAlbumsOrder { get; set; }

        [JsonProperty("ART_NAME")]
        public string ArtName { get; set; }

        [JsonProperty("ART_PICTURE")]
        public string ArtPicture { get; set; }

        [JsonProperty("RANK")]
        public string Rank { get; set; }

        [JsonProperty("LOCALES")]
        public LocalesClass Locales { get; set; }

        [JsonProperty("ARTIST_IS_DUMMY")]
        public bool ArtistIsDummy { get; set; }

        [JsonProperty("__TYPE__")]
        public string Type { get; set; }

        [JsonProperty("ARTISTS_SONGS_ORDER", NullValueHandling = NullValueHandling.Ignore)]
        public string ArtistsSongsOrder { get; set; }

        [JsonProperty("SMARTRADIO", NullValueHandling = NullValueHandling.Ignore)]
        public long? Smartradio { get; set; }
    }

    public class LocalesClass
    {
        [JsonProperty("lang_en")]
        public Lang LangEn { get; set; }
    }

    public class Lang
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class ExplicitContent
    {
        [JsonProperty("EXPLICIT_LYRICS_STATUS")]
        public long ExplicitLyricsStatus { get; set; }

        [JsonProperty("EXPLICIT_COVER_STATUS")]
        public long ExplicitCoverStatus { get; set; }
    }

    public class SelectedPlaylistData
    {
        [JsonProperty("data")]
        public SelectedPlaylistDatum[] Data { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("filtered_count", NullValueHandling = NullValueHandling.Ignore)]
        public long? FilteredCount { get; set; }
    }

    public class SelectedPlaylistDatum
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

        [JsonProperty("GAIN", NullValueHandling = NullValueHandling.Ignore)]
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
        public ExplicitContent ExplicitTrackContent { get; set; }

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

        [JsonProperty("__TYPE__")]
        public string DatumType { get; set; }
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

        [JsonProperty("artist", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Artist { get; set; }

        [JsonProperty("composer", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Composer { get; set; }

        [JsonProperty("author", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Author { get; set; }

        [JsonProperty("featuring", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Featuring { get; set; }

        [JsonProperty("remixer", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Remixer { get; set; }
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

    public class BioData
    {
        [JsonProperty("BIO")]
        public string BioBio { get; set; }

        [JsonProperty("RESUME")]
        public string Resume { get; set; }

        [JsonProperty("SOURCE")]
        public string Source { get; set; }
    }

    public class ArtistPageData
    {
        [JsonProperty("ART_ID")]
        public string ArtId { get; set; }

        [JsonProperty("ART_NAME")]
        public string ArtName { get; set; }

        [JsonProperty("URL_REWRITING")]
        public string UrlRewriting { get; set; }

        [JsonProperty("ART_PICTURE")]
        public string ArtPicture { get; set; }

        [JsonProperty("NB_FAN")]
        public long NbFan { get; set; }

        [JsonProperty("URL")]
        public string Url { get; set; }

        [JsonProperty("TWITTER")]
        public string Twitter { get; set; }

        [JsonProperty("STATUS")]
        public Status Status { get; set; }

        [JsonProperty("USER_ID")]
        public string UserId { get; set; }

        [JsonProperty("DATA")]
        public DataData Data { get; set; }

        [JsonProperty("__TYPE__")]
        public string Type { get; set; }

        [JsonProperty("SMARTRADIO")]
        public bool Smartradio { get; set; }
    }

    public class DataData
    {
        [JsonProperty("locales")]
        public LocalesClass Locales { get; set; }

        [JsonProperty("COPYRIGHT_PICTURE")]
        public string CopyrightPicture { get; set; }
    }

    public class Status
    {
        [JsonProperty("status")]
        public string StatusStatus { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }
    }

    public class HighlightData
    {
        [JsonProperty("TYPE")]
        public string Type { get; set; }

        [JsonProperty("TITLE")]
        public string Title { get; set; }

        [JsonProperty("ITEM")]
        public Item Item { get; set; }
    }

    public class Item
    {
        [JsonProperty("ALB_ID")]
        public string AlbId { get; set; }

        [JsonProperty("ALB_TITLE")]
        public string AlbTitle { get; set; }

        [JsonProperty("ALB_PICTURE")]
        public string AlbPicture { get; set; }

        [JsonProperty("EXPLICIT_ALBUM_CONTENT")]
        public ExplicitContent ExplicitAlbumContent { get; set; }

        [JsonProperty("ART_NAME")]
        public string ArtName { get; set; }

        [JsonProperty("ARTIST_IS_DUMMY")]
        public bool ArtistIsDummy { get; set; }

        [JsonProperty("ARTISTS")]
        public Artist[] Artists { get; set; }

        [JsonProperty("DIGITAL_RELEASE_DATE")]
        public string DigitalReleaseDate { get; set; }

        [JsonProperty("NUMBER_TRACK")]
        public string NumberTrack { get; set; }

        [JsonProperty("ORIGINAL_RELEASE_DATE")]
        public string OriginalReleaseDate { get; set; }

        [JsonProperty("PHYSICAL_RELEASE_DATE")]
        public string PhysicalReleaseDate { get; set; }

        [JsonProperty("PRODUCER_LINE")]
        public string ProducerLine { get; set; }

        [JsonProperty("TYPE")]
        public string Type { get; set; }

        [JsonProperty("SUBTYPES")]
        public Subtypes Subtypes { get; set; }

        [JsonProperty("URL_REWRITING")]
        public string UrlRewriting { get; set; }

        [JsonProperty("RANK")]
        public string Rank { get; set; }

        [JsonProperty("ROLE_ID")]
        public long RoleId { get; set; }

        [JsonProperty("ALBUM_EXCLUDED_FROM")]
        public string AlbumExcludedFrom { get; set; }

        [JsonProperty("__TYPE__")]
        public string ItemType { get; set; }

        [JsonProperty("SONGS")]
        public Songs Songs { get; set; }
    }

    public class Songs
    {
        [JsonProperty("data")]
        public SelectedPlaylistDatum[] Data { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("filtered_count")]
        public long FilteredCount { get; set; }
    }

    public class RelatedArtistsData
    {
        [JsonProperty("data")]
        public RelatedArtistsDatum[] Data { get; set; }

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

    public class RelatedArtistsDatum
    {
        [JsonProperty("ART_ID")]
        public string ArtId { get; set; }

        [JsonProperty("ART_NAME")]
        public string ArtName { get; set; }

        [JsonProperty("ART_PICTURE")]
        public string ArtPicture { get; set; }

        [JsonProperty("NB_FAN")]
        public long NbFan { get; set; }

        [JsonProperty("URL_REWRITING")]
        public string UrlRewriting { get; set; }

        [JsonProperty("DATA")]
        public DatumData Data { get; set; }

        [JsonProperty("__TYPE__")]
        public string Type { get; set; }

        [JsonProperty("__PAYLOAD__")]
        public PurplePayload Payload { get; set; }
    }

    public class DatumData
    {
        [JsonProperty("COPYRIGHT_PICTURE")]
        public string CopyrightPicture { get; set; }

        [JsonProperty("ART_BANNER", NullValueHandling = NullValueHandling.Ignore)]
        public string ArtBanner { get; set; }

        [JsonProperty("ART_BANNER_ALIGN", NullValueHandling = NullValueHandling.Ignore)]
        public string ArtBannerAlign { get; set; }

        [JsonProperty("ART_BANNER_BGCOLOR", NullValueHandling = NullValueHandling.Ignore)]
        public string ArtBannerBgcolor { get; set; }

        [JsonProperty("ART_BANNER_LINK", NullValueHandling = NullValueHandling.Ignore)]
        public string ArtBannerLink { get; set; }

        [JsonProperty("DISABLE_CATALOG", NullValueHandling = NullValueHandling.Ignore)]
        public bool? DisableCatalog { get; set; }
    }

    public class LocalesLocales
    {
        [JsonProperty("lang_en")]
        public Lang LangEn { get; set; }

        [JsonProperty("lang_ar", NullValueHandling = NullValueHandling.Ignore)]
        public Lang LangAr { get; set; }

        [JsonProperty("lang_ja-hrkt", NullValueHandling = NullValueHandling.Ignore)]
        public Lang LangJaHrkt { get; set; }

        [JsonProperty("lang_ja-jpan", NullValueHandling = NullValueHandling.Ignore)]
        public Lang LangJaJpan { get; set; }

        [JsonProperty("lang_ja-kana", NullValueHandling = NullValueHandling.Ignore)]
        public Lang LangJaKana { get; set; }
    }

    public class PurplePayload
    {
        [JsonProperty("SCORE")]
        public long Score { get; set; }
    }

    public class RelatedPlaylistData
    {
        [JsonProperty("data")]
        public RelatedPlaylistDatum[] Data { get; set; }

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

        [JsonProperty("filtered_items")]
        public object[] FilteredItems { get; set; }

        [JsonProperty("next")]
        public long Next { get; set; }
    }

    public class RelatedPlaylistDatum
    {
        [JsonProperty("PLAYLIST_ID")]
        public string PlaylistId { get; set; }

        [JsonProperty("TITLE")]
        public string Title { get; set; }

        [JsonProperty("PLAYLIST_PICTURE")]
        public string PlaylistPicture { get; set; }

        [JsonProperty("PICTURE_TYPE")]
        public string PictureType { get; set; }

        [JsonProperty("PARENT_USER_ID")]
        public string ParentUserId { get; set; }

        [JsonProperty("PARENT_USERNAME")]
        public string ParentUsername { get; set; }

        [JsonProperty("NB_FAN")]
        public long NbFan { get; set; }

        [JsonProperty("NB_SONG")]
        public long NbSong { get; set; }

        [JsonProperty("STATUS")]
        public long Status { get; set; }

        [JsonProperty("__TYPE__")]
        public string Type { get; set; }

        [JsonProperty("__PAYLOAD__")]
        public FluffyPayload Payload { get; set; }
    }

    public class FluffyPayload
    {
    }

    public class TopData
    {
        [JsonProperty("data")]
        public TopDatum[] Data { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("filtered_count")]
        public long FilteredCount { get; set; }

        [JsonProperty("filtered_items")]
        public object[] FilteredItems { get; set; }

        [JsonProperty("next")]
        public long Next { get; set; }
    }

    public class TopDatum
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
        public ExplicitContent ExplicitTrackContent { get; set; }

        [JsonProperty("__TYPE__")]
        public string DatumType { get; set; }

        [JsonProperty("__PAYLOAD__")]
        public TentacledPayload Payload { get; set; }
    }

    public class TentacledPayload
    {
        [JsonProperty("COUNTRY_RANK")]
        public long CountryRank { get; set; }

        [JsonProperty("GLOBAL_RANK")]
        public long GlobalRank { get; set; }
    }

    public class VideoData
    {
        [JsonProperty("error")]
        public Error Error { get; set; }
    }

    public class Error
    {
        [JsonProperty("DATA_ERROR")]
        public string DataError { get; set; }
    }
}