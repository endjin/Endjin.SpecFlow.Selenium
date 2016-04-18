namespace Endjin.SpecFlow.Selenium.Framework.Drivers
{
    #region Using Directives

    using System;

    using Endjin.SpecFlow.Selenium.Framework.Constants;
    using Endjin.SpecFlow.Selenium.Framework.Navigation;

    using NUnit.Framework;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.IE;
    using OpenQA.Selenium.PhantomJS;
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

        private static IWebDriver Chrome(NavigatorSessionParameters session)
        {
            var service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = session.HideCommandPromptWindow;

            return new ChromeDriver(service, new ChromeOptions());
        }

        private static IWebDriver CreateDriver(NavigatorSessionParameters session)
        {
            switch (session.WebDriverType)
            {
                case WebDriverType.Chrome:
                    return Chrome(session);
                case WebDriverType.Firefox:
                    return Firefox(session);
                case WebDriverType.InternetExplorer:
                    return InternetExplorer(session);
                case WebDriverType.PhantomJs:
                    return PhantomJs(session);
                case WebDriverType.Remote:
                    return RemoteDriver(session);
                case WebDriverType.Safari:
                    return Safari();
                default:
                    return PhantomJs(session);
            }
        }

        private static FirefoxDriver Firefox(NavigatorSessionParameters session)
        {
            var profile = new FirefoxProfile { AcceptUntrustedCertificates = session.AcceptUntrustedCertificates };
            profile.SetPreference("browser.startup.homepage", "about:blank");

            return new FirefoxDriver(profile);
        }

        private static DesiredCapabilities GetDefaultCapabilities(string browser)
        {
            switch (browser)
            {
                case "internet explorer":
                    return DesiredCapabilities.InternetExplorer();
                case "firefox":
                    return DesiredCapabilities.Firefox();
                case "phantomjs":
                    return DesiredCapabilities.PhantomJS();
                case "htmlunit":
                    return DesiredCapabilities.HtmlUnitWithJavaScript();
                case "iPhone":
                    return DesiredCapabilities.IPhone();
                case "iPad":
                    return DesiredCapabilities.IPad();
                case "android":
                    return DesiredCapabilities.Android();
                case "opera":
                    return DesiredCapabilities.Opera();
                case "safari":
                    return DesiredCapabilities.Safari();
                case "chrome":
                default:
                    return DesiredCapabilities.Chrome();
            }
        }

        private static IWebDriver InternetExplorer(NavigatorSessionParameters session)
        {
            var service = InternetExplorerDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = session.HideCommandPromptWindow;

            return new InternetExplorerDriver(service, new InternetExplorerOptions());
        }

        private static void Manage(IWebDriver driver, NavigatorSessionParameters session)
        {
            var elementSearchTimeout = TimeSpan.FromSeconds(session.ImplicitlyWait);
            driver.Manage().Timeouts().ImplicitlyWait(elementSearchTimeout);

            // Note: Issue with SauceLabs. Some platform+browser+version 
            // combinations throw an exception when trying to set this.
            if (!session.RunUsingSauceLabs)
            {
                var pageLoadTimeout = TimeSpan.FromSeconds(session.PageLoadTimeout);
                driver.Manage().Timeouts().SetPageLoadTimeout(pageLoadTimeout);
            }

            var scriptTimeout = TimeSpan.FromSeconds(session.ScriptTimeout);
            driver.Manage().Timeouts().SetScriptTimeout(scriptTimeout);
        }

        private static IWebDriver PhantomJs(NavigatorSessionParameters session)
        {
            var service = PhantomJSDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = session.HideCommandPromptWindow;

            return new PhantomJSDriver(service, new PhantomJSOptions());
        }

        private static IWebDriver RemoteDriver(NavigatorSessionParameters session)
        {
            var remoteAddress = new Uri(session.SauceLabsRemoteDriverUrl);

            var capabilities = GetDefaultCapabilities(session.SauceLabsRemoteBrowser);

            capabilities.SetCapability(CapabilityType.Platform, session.SauceLabsRemotePlatform);
            capabilities.SetCapability(CapabilityType.Version, session.SauceLabsRemoteBrowserVersion);
            capabilities.SetCapability(CapabilityTypeExt.Username, session.SauceLabsRemoteUsername);
            capabilities.SetCapability(CapabilityTypeExt.AccessKey, session.SauceLabsRemoteKey);
            capabilities.SetCapability(CapabilityTypeExt.TestName, TestContext.CurrentContext.Test.Name);

            if (session.AcceptUntrustedCertificates)
            {
                capabilities.SetCapability("AcceptUntrustedCertificates", true);
            }

            return new RemoteDriver(remoteAddress, capabilities);
        }

        private static IWebDriver Safari()
        {
            return new SafariDriver();
        }
    }
}