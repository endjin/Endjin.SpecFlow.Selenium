namespace Endjin.SpecFlow.Selenium.Framework.Navigation
{
    #region Using Directives

    using Endjin.SpecFlow.Selenium.Framework.Contracts;
    using Endjin.SpecFlow.Selenium.Framework.Drivers;

    #endregion

    /// <summary>
    /// Navigator session parameters.
    /// </summary>
    /// <remarks>
    /// For a list of valid remote browsers, versions and platforms, 
    /// check https://saucelabs.com/platforms/webdriver
    /// </remarks>
    public class NavigatorSessionParameters
    {
        public NavigatorSessionParameters()
        {
            this.ImplicitlyWait = 30;
            this.PageLoadTimeout = 30;
            this.ScriptTimeout = 3600;

            this.WebDriverType = WebDriverType.Default;
        }

        public bool AcceptUntrustedCertificates
        {
            get;
            set;
        }

        public bool HideCommandPromptWindow
        {
            get;
            set;
        }

        /// <summary>
        /// Specifies the amount of time (in seconds) the driver should wait 
        /// when searching for an element if it is not immediately present.
        /// </summary>
        /// <remarks>
        /// Default Value: 30
        /// </remarks>
        /// <see cref="http://docs.seleniumhq.org/"/>
        public int ImplicitlyWait
        {
            get;
            set;
        }

        public INavigationMap NavigationMap
        {
            get;
            set;
        }

        /// <summary>
        /// Specifies the amount of time (in seconds) the driver should wait 
        /// for a page to load when setting the Url property.
        /// </summary>
        /// <remarks>
        /// Default Value: 30
        /// </remarks>
        /// <see cref="http://docs.seleniumhq.org/"/>
        public int PageLoadTimeout
        {
            get;
            set;
        }

        /// <summary>
        /// Remote Browser Values:
        /// "internet explorer", "firefox", "phantomjs", "htmlunit", "iPhone", "iPad", "android", "opera", "safari", "chrome"
        /// </summary>
        public string SauceLabsRemoteBrowser
        {
            get;
            set;
        }

        public string SauceLabsRemoteBrowserVersion
        {
            get;
            set;
        }

        public string SauceLabsRemoteDriverUrl
        {
            get;
            set;
        }

        public string SauceLabsRemoteKey
        {
            get;
            set;
        }

        public string SauceLabsRemotePlatform
        {
            get;
            set;
        }

        public string SauceLabsRemoteUsername
        {
            get;
            set;
        }

        /// <summary>
        /// Specifies the amount of time (in seconds) the driver should wait 
        /// when executing JavaScript asynchronously.
        /// </summary>
        /// <remarks>
        /// Default Value: 3600
        /// </remarks>
        /// <see cref="http://docs.seleniumhq.org/"/>
        public int ScriptTimeout
        {
            get;
            set;
        }

        public WebDriverType WebDriverType
        {
            get;
            set;
        }

        public bool RunUsingSauceLabs { get; set; }
    }
}