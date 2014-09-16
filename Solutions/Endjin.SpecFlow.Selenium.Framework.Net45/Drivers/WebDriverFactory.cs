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

        private static IWebDriver Chrome()
        {
            return new ChromeDriver();
        }

        private static IWebDriver CreateDriver(NavigatorSessionParameters session)
        {
            switch (session.WebDriverType)
            {
                case WebDriverType.Chrome:
                    return Chrome();
                case WebDriverType.Firefox:
                    return Firefox();
                case WebDriverType.InternetExplorer:
                    return InternetExplorer();
                case WebDriverType.PhantomJs:
                    return PhantomJs();
                case WebDriverType.Remote:
                    return RemoteDriver(session);
                case WebDriverType.Safari:
                    return Safari();
                default:
                    return PhantomJs();
            }
        }

        private static FirefoxDriver Firefox()
        {
            return new FirefoxDriver();
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

        private static IWebDriver InternetExplorer()
        {
            return new InternetExplorerDriver();
        }

        private static void Manage(IWebDriver driver, NavigatorSessionParameters session)
        {
            var elementSearchTimeout = TimeSpan.FromSeconds(session.ImplicitlyWait);
            driver.Manage().Timeouts().ImplicitlyWait(elementSearchTimeout);

            // Note: Issue with SauceLabs. Some platform+browser+version 
            // combinations throw an exception when trying to set this.
            // var pageLoadTimeout = TimeSpan.FromSeconds(session.PageLoadTimeout);
            // driver.Manage().Timeouts().SetPageLoadTimeout(pageLoadTimeout);
            var scriptTimeout = TimeSpan.FromSeconds(session.ScriptTimeout);
            driver.Manage().Timeouts().SetScriptTimeout(scriptTimeout);
        }

        private static IWebDriver PhantomJs()
        {
            return new PhantomJSDriver();
        }

        private static IWebDriver RemoteDriver(NavigatorSessionParameters session)
        {
            var remoteAddress = new Uri(session.RemoteDriverUrl);

            var capabilities = GetDefaultCapabilities(session.RemoteBrowser);

            capabilities.SetCapability(CapabilityType.Platform, session.RemotePlatform);
            capabilities.SetCapability(CapabilityType.Version, session.RemoteBrowserVersion);
            capabilities.SetCapability(CapabilityTypeExt.Username, session.RemoteUsername);
            capabilities.SetCapability(CapabilityTypeExt.AccessKey, session.RemoteKey);
            capabilities.SetCapability(CapabilityTypeExt.TestName, TestContext.CurrentContext.Test.Name);

            if (session.AcceptUntrustedCertificates
                && session.RemoteBrowser == DesiredCapabilities.Firefox().BrowserName)
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