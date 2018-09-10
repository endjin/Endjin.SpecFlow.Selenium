namespace Endjin.SpecFlow.Selenium.Framework.Drivers
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    using Endjin.SpecFlow.Selenium.Framework.Constants;
    using Endjin.SpecFlow.Selenium.Framework.Navigation;

    using NUnit.Framework;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Edge;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.IE;
    using OpenQA.Selenium.Remote;
    using OpenQA.Selenium.Safari;

    #endregion

    public static class WebDriverFactory
    {
        public static IWebDriver Create(NavigatorSessionParameters session)
        {
            var driver = CreateDriver(session);
            Manage(driver, session);
            return driver;
        }

        private static IWebDriver CreateDriver(NavigatorSessionParameters session)
        {
            var options = GetDefaultOptions(session.WebDriverType);
            if (session.AcceptUntrustedCertificates)
            {
                options.AcceptInsecureCertificates = true;
            }
            
            switch (session.WebDriverType)
            {
                case WebDriverType.Remote:
                    return Remote(session, options);
                case WebDriverType.Edge:
                    return Edge(options);
                case WebDriverType.Firefox:
                    return Firefox(options);
                case WebDriverType.InternetExplorer:
                    return InternetExplorer(session, options);
                case WebDriverType.Safari:
                    return Safari(options);
                default:
                    return Chrome(session, options);
            }
        }

        private static IWebDriver Remote(NavigatorSessionParameters session, DriverOptions options)
        {
            var remoteAddress = new Uri(
                "http://" + session.SauceLabsRemoteUsername + ":" + session.SauceLabsRemoteKey
                + session.SauceLabsRemoteDriverUrl);
          
            options.PlatformName = session.SauceLabsRemotePlatform;
            options.BrowserVersion = session.SauceLabsRemoteBrowserVersion;

            return new RemoteWebDriver(remoteAddress, options);
        }
        private static DriverOptions GetDefaultOptions(WebDriverType type)
        {
            switch (type)
            {
                case WebDriverType.InternetExplorer:
                    return new InternetExplorerOptions();
                case WebDriverType.Firefox:
                    return new FirefoxOptions();
                case WebDriverType.Edge:
                    return new EdgeOptions();
                case WebDriverType.Safari:
                    return new SafariOptions();
                default:
                    return new ChromeOptions();
            }
        }

        private static IWebDriver Chrome(NavigatorSessionParameters session, DriverOptions options)
        {
            var chromeOptions = (ChromeOptions)options;
            
            var service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = session.HideCommandPromptWindow;

            return new ChromeDriver(service, chromeOptions);
        }

        private static FirefoxDriver Firefox(DriverOptions options)
        {          
            var firefoxOptions = (FirefoxOptions)options;
            var firefoxProfile = new FirefoxProfile();
            firefoxProfile.SetPreference("browser.startup.homepage", "about:blank");
            firefoxOptions.Profile = firefoxProfile;

            return new FirefoxDriver((FirefoxOptions)options);
        }

        private static EdgeDriver Edge(DriverOptions options)
        {
            var edgeOptions = (EdgeOptions)options;
            
            return new EdgeDriver(edgeOptions);
        }

        private static IWebDriver InternetExplorer(NavigatorSessionParameters session, DriverOptions options)
        {
            var service = InternetExplorerDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = session.HideCommandPromptWindow;

            var ieOptions = (InternetExplorerOptions)options;
            ieOptions.AcceptInsecureCertificates = false;
            return new InternetExplorerDriver(service, ieOptions);
        }
        private static IWebDriver Safari(DriverOptions options)
        {
            var safariOptions = (SafariOptions)options;
            return new SafariDriver(safariOptions);
        }

        private static void Manage(IWebDriver driver, NavigatorSessionParameters session)
        {
            var elementSearchTimeout = TimeSpan.FromSeconds(session.ImplicitlyWait);
            
            driver.Manage().Timeouts().ImplicitWait = elementSearchTimeout;

            // Note: Issue with SauceLabs. Some platform+browser+version 
            // combinations throw an exception when trying to set this.
            if (!session.RunUsingSauceLabs)
            {
                var pageLoadTimeout = TimeSpan.FromSeconds(session.PageLoadTimeout);
                driver.Manage().Timeouts().PageLoad = pageLoadTimeout;
            }

            var scriptTimeout = TimeSpan.FromSeconds(session.ScriptTimeout);
            driver.Manage().Timeouts().AsynchronousJavaScript = scriptTimeout;
        }


        


    }
}