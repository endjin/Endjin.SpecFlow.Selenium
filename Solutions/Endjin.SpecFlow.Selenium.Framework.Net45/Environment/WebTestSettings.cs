namespace Endjin.SpecFlow.Selenium.Framework.Environment
{
    #region Using Directives

    using Newtonsoft.Json;

    #endregion

    public class WebTestSettings
    {
        [JsonProperty("accept_untrusted_certificates")]
        public bool? AcceptUntrustedCertificates
        {
            get;
            set;
        }

        [JsonProperty("auto_start_iis")]
        public bool? AutoStartIIS
        {
            get;
            set;
        }

        [JsonProperty("delay_close")]
        public int? DelayClose
        {
            get;
            set;
        }

        [JsonProperty("hide_iis")]
        public bool? HideIIS
        {
            get;
            set;
        }

        [JsonProperty("implicitly_wait")]
        public int? ImplicitlyWait
        {
            get;
            set;
        }

        [JsonProperty("page_load_timeout")]
        public int? PageLoadTimeout
        {
            get;
            set;
        }

        [JsonProperty("remote_browser")]
        public string RemoteBrowser
        {
            get;
            set;
        }

        [JsonProperty("remote_browser_version")]
        public string RemoteBrowserVersion
        {
            get;
            set;
        }

        [JsonProperty("remote_key")]
        public string RemoteKey
        {
            get;
            set;
        }

        [JsonProperty("remote_platform")]
        public string RemotePlatform
        {
            get;
            set;
        }

        [JsonProperty("remote_url")]
        public string RemoteUrl
        {
            get;
            set;
        }

        [JsonProperty("remote_username")]
        public string RemoteUsername
        {
            get;
            set;
        }

        [JsonProperty("script_timeout")]
        public int? ScriptTimeout
        {
            get;
            set;
        }

        [JsonProperty("web_app_name")]
        public string WebAppName
        {
            get;
            set;
        }

        [JsonProperty("web_driver_type")]
        public string WebDriverType
        {
            get;
            set;
        }

        [JsonProperty("website_url")]
        public string WebsiteUrl
        {
            get;
            set;
        }
    }
}