namespace Endjin.SpecFlow.Selenium.Framework.Features
{
    #region Using Directives

    using Endjin.SpecFlow.Selenium.Framework.Environment;

    #endregion

    public class BrowserTestFeature
    {
        protected BrowserTestFeature()
        {
        }

        protected BrowserTestFeature(string platform, string browser, string browserVersion)
        {
            TestEnvironment.Current.SetRemoteBrowserContext(platform, browser, browserVersion);
        }
    }
}