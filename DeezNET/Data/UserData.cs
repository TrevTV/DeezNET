using Newtonsoft.Json;

namespace DeezNET.Data;

public class UserData
{
    [JsonProperty("USER")]
    public UserInfo User { get; set; }

    [JsonProperty("SETTING_LANG")]
    public string SettingLang { get; set; }

    [JsonProperty("SETTING_LOCALE")]
    public string SettingLocale { get; set; }

    [JsonProperty("DIRECTION")]
    public string Direction { get; set; }

    [JsonProperty("SESSION_ID")]
    public string SessionId { get; set; }

    [JsonProperty("USER_TOKEN")]
    public string UserToken { get; set; }

    [JsonProperty("OFFER_ID")]
    public long OfferId { get; set; }

    [JsonProperty("OFFER_NAME")]
    public string OfferName { get; set; }

    [JsonProperty("OFFER_ELIGIBILITIES")]
    public object[] OfferEligibilities { get; set; }

    [JsonProperty("COUNTRY")]
    public string Country { get; set; }

    [JsonProperty("COUNTRY_CATEGORY")]
    public string CountryCategory { get; set; }

    [JsonProperty("MIN_LEGAL_AGE")]
    public long MinLegalAge { get; set; }

    [JsonProperty("FAMILY_KIDS_AGE")]
    public long FamilyKidsAge { get; set; }

    [JsonProperty("SERVER_TIMESTAMP")]
    public long ServerTimestamp { get; set; }

    [JsonProperty("PLAYER_TOKEN")]
    public string PlayerToken { get; set; }

    [JsonProperty("checkForm")]
    public string CheckForm { get; set; }

    [JsonProperty("SETTING_REFERER_UPLOAD")]
    public string SettingRefererUpload { get; set; }

    [JsonProperty("URL_MEDIA")]
    public Uri UrlMedia { get; set; }

    public class UserInfo
    {
        [JsonProperty("USER_ID")]
        public long UserId { get; set; }

        [JsonProperty("USER_PICTURE")]
        public string UserPicture { get; set; }

        [JsonProperty("INSCRIPTION_DATE")]
        public string InscriptionDate { get; set; }

        [JsonProperty("TOOLBAR")]
        public object[] Toolbar { get; set; }

        [JsonProperty("OPTIONS")]
        public Options Options { get; set; }

        [JsonProperty("AUDIO_SETTINGS")]
        public AudioSettings AudioSettings { get; set; }

        [JsonProperty("SETTING")]
        public Setting Setting { get; set; }

        [JsonProperty("LASTFM")]
        public Entrypoints Lastfm { get; set; }

        [JsonProperty("TWITTER")]
        public Entrypoints Twitter { get; set; }

        [JsonProperty("FACEBOOK")]
        public Entrypoints Facebook { get; set; }

        [JsonProperty("GOOGLEPLUS")]
        public Entrypoints Googleplus { get; set; }

        [JsonProperty("FAVORITE_TAG")]
        public long FavoriteTag { get; set; }

        [JsonProperty("ABTEST")]
        public Abtest Abtest { get; set; }

        [JsonProperty("MULTI_ACCOUNT")]
        public MultiAccount MultiAccount { get; set; }

        [JsonProperty("ONBOARDING_MODAL")]
        public bool OnboardingModal { get; set; }

        [JsonProperty("ADS_OFFER")]
        public string AdsOffer { get; set; }

        [JsonProperty("ENTRYPOINTS")]
        public Entrypoints Entrypoints { get; set; }

        [JsonProperty("ADS_TEST_FORMAT")]
        public string AdsTestFormat { get; set; }

        [JsonProperty("NEW_USER")]
        public bool NewUser { get; set; }

        [JsonProperty("CONSENT_STRING")]
        public object[] ConsentString { get; set; }

        [JsonProperty("RECOMMENDATION_COUNTRY")]
        public string RecommendationCountry { get; set; }

        [JsonProperty("CAN_BE_CONVERTED_TO_INDEPENDENT")]
        public bool CanBeConvertedToIndependent { get; set; }

        [JsonProperty("IS_FREEMIUM_COUNTRY")]
        public long IsFreemiumCountry { get; set; }

        [JsonProperty("EXPLICIT_CONTENT_LEVEL")]
        public string ExplicitContentLevel { get; set; }

        [JsonProperty("EXPLICIT_CONTENT_LEVELS_AVAILABLE")]
        public string[] ExplicitContentLevelsAvailable { get; set; }

        [JsonProperty("CAN_EDIT_EXPLICIT_CONTENT_LEVEL")]
        public bool CanEditExplicitContentLevel { get; set; }

        [JsonProperty("BLOG_NAME")]
        public string BlogName { get; set; }

        [JsonProperty("FIRSTNAME")]
        public string Firstname { get; set; }

        [JsonProperty("LASTNAME")]
        public string Lastname { get; set; }

        [JsonProperty("USER_GENDER")]
        public string UserGender { get; set; }

        [JsonProperty("USER_AGE")]
        public string UserAge { get; set; }

        [JsonProperty("EMAIL")]
        public string Email { get; set; }

        [JsonProperty("DEVICES_COUNT")]
        public long DevicesCount { get; set; }

        [JsonProperty("HAS_UPNEXT")]
        public bool HasUpnext { get; set; }

        [JsonProperty("LOVEDTRACKS_ID")]
        public string LovedtracksId { get; set; }

        [JsonProperty("OPTINS")]
        public Optins Optins { get; set; }
    }

    public class Abtest
    {
        [JsonProperty("triforce_queuelist_ui")]
        public ModPlaylists TriforceQueuelistUi { get; set; }

        [JsonProperty("share_android_image_preview")]
        public ModPlaylists ShareAndroidImagePreview { get; set; }

        [JsonProperty("mod_playlists")]
        public ModPlaylists ModPlaylists { get; set; }
    }

    public class ModPlaylists
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("option")]
        public string Option { get; set; }

        [JsonProperty("behaviour")]
        public string Behaviour { get; set; }

        [JsonProperty("percent")]
        public long Percent { get; set; }
    }

    public class AudioSettings
    {
        [JsonProperty("default_preset")]
        public string DefaultPreset { get; set; }

        [JsonProperty("default_download_on_mobile_network")]
        public bool DefaultDownloadOnMobileNetwork { get; set; }

        [JsonProperty("presets")]
        public Preset[] Presets { get; set; }

        [JsonProperty("connected_device_streaming_preset")]
        public string ConnectedDeviceStreamingPreset { get; set; }
    }

    public class Preset
    {
        [JsonProperty("mobile_download")]
        public string MobileDownload { get; set; }

        [JsonProperty("mobile_streaming")]
        public string MobileStreaming { get; set; }

        [JsonProperty("wifi_download")]
        public string WifiDownload { get; set; }

        [JsonProperty("wifi_streaming")]
        public string WifiStreaming { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class Entrypoints
    {
    }

    public class MultiAccount
    {
        [JsonProperty("ENABLED")]
        public bool Enabled { get; set; }

        [JsonProperty("ACTIVE")]
        public bool Active { get; set; }

        [JsonProperty("CHILD_COUNT")]
        public long ChildCount { get; set; }

        [JsonProperty("MAX_CHILDREN")]
        public long MaxChildren { get; set; }

        [JsonProperty("PARENT")]
        public object Parent { get; set; }

        [JsonProperty("IS_KID")]
        public bool IsKid { get; set; }

        [JsonProperty("IS_SUB_ACCOUNT")]
        public bool IsSubAccount { get; set; }
    }

    public class Optins
    {
        [JsonProperty("channel")]
        public ChannelElement[] Channel { get; set; }

        [JsonProperty("group")]
        public ChannelElement[] Group { get; set; }

        [JsonProperty("optin")]
        public Optin[] Optin { get; set; }

        [JsonProperty("service_name")]
        public string ServiceName { get; set; }
    }

    public class ChannelElement
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class Optin
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("channel")]
        public OptinChannel Channel { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("channels_requiring_validation")]
        public object[] ChannelsRequiringValidation { get; set; }
    }

    public class OptinChannel
    {
        [JsonProperty("optin_push", NullValueHandling = NullValueHandling.Ignore)]
        public bool? OptinPush { get; set; }

        [JsonProperty("optin_sms", NullValueHandling = NullValueHandling.Ignore)]
        public bool? OptinSms { get; set; }

        [JsonProperty("optin_mail", NullValueHandling = NullValueHandling.Ignore)]
        public bool? OptinMail { get; set; }

        [JsonProperty("optin_whatsapp", NullValueHandling = NullValueHandling.Ignore)]
        public bool? OptinWhatsapp { get; set; }
    }

    public class Options
    {
        [JsonProperty("mobile_preview")]
        public bool MobilePreview { get; set; }

        [JsonProperty("mobile_radio")]
        public bool MobileRadio { get; set; }

        [JsonProperty("mobile_streaming")]
        public bool MobileStreaming { get; set; }

        [JsonProperty("mobile_streaming_duration")]
        public long MobileStreamingDuration { get; set; }

        [JsonProperty("mobile_offline")]
        public bool MobileOffline { get; set; }

        [JsonProperty("mobile_sound_quality")]
        public SoundQuality MobileSoundQuality { get; set; }

        [JsonProperty("default_download_on_mobile_network")]
        public bool DefaultDownloadOnMobileNetwork { get; set; }

        [JsonProperty("mobile_hq")]
        public bool MobileHq { get; set; }

        [JsonProperty("mobile_lossless")]
        public bool MobileLossless { get; set; }

        [JsonProperty("tablet_sound_quality")]
        public SoundQuality TabletSoundQuality { get; set; }

        [JsonProperty("audio_quality_default_preset")]
        public string AudioQualityDefaultPreset { get; set; }

        [JsonProperty("web_preview")]
        public bool WebPreview { get; set; }

        [JsonProperty("web_radio")]
        public bool WebRadio { get; set; }

        [JsonProperty("web_streaming")]
        public bool WebStreaming { get; set; }

        [JsonProperty("web_streaming_duration")]
        public long WebStreamingDuration { get; set; }

        [JsonProperty("web_offline")]
        public bool WebOffline { get; set; }

        [JsonProperty("web_hq")]
        public bool WebHq { get; set; }

        [JsonProperty("web_lossless")]
        public bool WebLossless { get; set; }

        [JsonProperty("web_sound_quality")]
        public SoundQuality WebSoundQuality { get; set; }

        [JsonProperty("license_token")]
        public string LicenseToken { get; set; }

        [JsonProperty("expiration_timestamp")]
        public long ExpirationTimestamp { get; set; }

        [JsonProperty("license_country")]
        public string LicenseCountry { get; set; }

        [JsonProperty("ads_display")]
        public bool AdsDisplay { get; set; }

        [JsonProperty("ads_audio")]
        public bool AdsAudio { get; set; }

        [JsonProperty("dj")]
        public bool Dj { get; set; }

        [JsonProperty("nb_devices")]
        public string NbDevices { get; set; }

        [JsonProperty("multi_account")]
        public bool MultiAccount { get; set; }

        [JsonProperty("multi_account_max_allowed")]
        public long MultiAccountMaxAllowed { get; set; }

        [JsonProperty("radio_skips")]
        public long RadioSkips { get; set; }

        [JsonProperty("too_many_devices")]
        public bool TooManyDevices { get; set; }

        [JsonProperty("business")]
        public bool Business { get; set; }

        [JsonProperty("business_mod")]
        public bool BusinessMod { get; set; }

        [JsonProperty("business_box_owner")]
        public bool BusinessBoxOwner { get; set; }

        [JsonProperty("business_box_manager")]
        public bool BusinessBoxManager { get; set; }

        [JsonProperty("business_box")]
        public bool BusinessBox { get; set; }

        [JsonProperty("business_no_right")]
        public bool BusinessNoRight { get; set; }

        [JsonProperty("allow_subscription")]
        public bool AllowSubscription { get; set; }

        [JsonProperty("allow_trial_mobile")]
        public string AllowTrialMobile { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("can_subscribe")]
        public bool CanSubscribe { get; set; }

        [JsonProperty("can_subscribe_family")]
        public bool CanSubscribeFamily { get; set; }

        [JsonProperty("show_subscription_section")]
        public bool ShowSubscriptionSection { get; set; }

        [JsonProperty("streaming_group")]
        public string StreamingGroup { get; set; }

        [JsonProperty("queuelist_edition")]
        public bool QueuelistEdition { get; set; }

        [JsonProperty("web_streaming_used")]
        public long WebStreamingUsed { get; set; }

        [JsonProperty("ads")]
        public bool Ads { get; set; }
    }

    public class SoundQuality
    {
        [JsonProperty("low")]
        public bool Low { get; set; }

        [JsonProperty("standard")]
        public bool Standard { get; set; }

        [JsonProperty("high")]
        public bool High { get; set; }

        [JsonProperty("lossless")]
        public bool Lossless { get; set; }

        [JsonProperty("reality")]
        public bool Reality { get; set; }
    }

    public class Setting
    {
        [JsonProperty("global")]
        public Global Global { get; set; }

        [JsonProperty("adjust")]
        public object[] Adjust { get; set; }

        [JsonProperty("audio_quality_settings")]
        public AudioQualitySettings AudioQualitySettings { get; set; }
    }

    public class AudioQualitySettings
    {
        [JsonProperty("preset")]
        public string Preset { get; set; }

        [JsonProperty("download_on_mobile_network")]
        public bool DownloadOnMobileNetwork { get; set; }

        [JsonProperty("connected_device_streaming_preset")]
        public bool ConnectedDeviceStreamingPreset { get; set; }
    }

    public class Global
    {
        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("social")]
        public bool Social { get; set; }

        [JsonProperty("popup_unload")]
        public bool PopupUnload { get; set; }

        [JsonProperty("filter_explicit_lyrics")]
        public bool FilterExplicitLyrics { get; set; }

        [JsonProperty("is_kid")]
        public bool IsKid { get; set; }

        [JsonProperty("has_up_next")]
        public bool HasUpNext { get; set; }

        [JsonProperty("dark_mode")]
        public string DarkMode { get; set; }

        [JsonProperty("onboarding_progress")]
        public long OnboardingProgress { get; set; }

        [JsonProperty("onboarding_musictogether")]
        public bool OnboardingMusictogether { get; set; }

        [JsonProperty("onboarding_musictogether_progress")]
        public long OnboardingMusictogetherProgress { get; set; }

        [JsonProperty("cookie_consent_string")]
        public string CookieConsentString { get; set; }

        [JsonProperty("has_root_consent")]
        public long HasRootConsent { get; set; }

        [JsonProperty("happy_hour")]
        public string HappyHour { get; set; }

        [JsonProperty("recommendation_country")]
        public string RecommendationCountry { get; set; }

        [JsonProperty("has_joined_family")]
        public bool HasJoinedFamily { get; set; }

        [JsonProperty("has_already_tried_premium")]
        public bool HasAlreadyTriedPremium { get; set; }

        [JsonProperty("explicit_level_forced")]
        public bool ExplicitLevelForced { get; set; }

        [JsonProperty("onboarding")]
        public bool Onboarding { get; set; }
    }
}