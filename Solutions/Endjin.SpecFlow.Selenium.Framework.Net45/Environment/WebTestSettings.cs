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

        [JsonProperty("local_auto_start_iis")]
        public bool? LocalAutoStartIIS
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

        [JsonProperty("local_hide_command_prompt_window")]
        public bool? LocalHideCommandPromptWindow
        {
            get;
            set;
        }

        [JsonProperty("run_using_sauce_labs")]
        public bool? RunUsingSauceLabs
        {
            get;
            set;
        }

        [JsonProperty("local_hide_iis")]
        public bool? LocalHideIIS
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

        [JsonProperty("sauce_labs_remote_browser")]
        public string SauceLabsRemoteBrowser
        {
            get;
            set;
        }

        [JsonProperty("sauce_labs_remote_browser_version")]
        public string SauceLabsRemoteBrowserVersion
        {
            get;
            set;
        }

        [JsonProperty("sauce_labs_remote_key")]
        public string SauceLabsRemoteKey
        {
            get;
            set;
        }

        [JsonProperty("sauce_labs_remote_platform")]
        public string SauceLabsRemotePlatform
        {
            get;
            set;
        }

        [JsonProperty("sauce_labs_remote_url")]
        public string SauceLabsRemoteUrl
        {
            get;
            set;
        }

        [JsonProperty("sauce_labs_remote_username")]
        public string SauceLabsRemoteUsername
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