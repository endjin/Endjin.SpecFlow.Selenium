namespace Endjin.SpecFlow.Selenium.Framework
{
    #region Using Directives

    using System;
    using System.Text;
    using System.Threading.Tasks;

    using Endjin.SpecFlow.Selenium.Framework.Contracts;
    using Endjin.SpecFlow.Selenium.Framework.Environment;
    using Endjin.SpecFlow.Selenium.Framework.Navigation;
    using Endjin.SpecFlow.Selenium.Framework.Reporting;

    using TechTalk.SpecFlow;

    #endregion

    public static class BrowserTest
    {
        public static void Setup(INavigationMap map)
        {
            try
            {
                map.Initialize(TestEnvironment.Current.WebsiteUrl);

                var parameters = new NavigatorSessionParameters
                                 {
                                         NavigationMap = map,
                                         AcceptUntrustedCertificates = TestEnvironment.Current.AcceptUntrustedCertificates,
                                         WebDriverType = TestEnvironment.Current.WebDriverType,
                                         HideCommandPromptWindow = TestEnvironment.Current.HideCommandPromptWindow,
                                         ImplicitlyWait = TestEnvironment.Current.ImplicitlyWait,
                                         PageLoadTimeout = TestEnvironment.Current.PageLoadTimeout,
                                         ScriptTimeout = TestEnvironment.Current.ScriptTimeout,
                                         RunUsingSauceLabs = TestEnvironment.Current.RunUsingSauceLabs,
                                         SauceLabsRemoteBrowser = TestEnvironment.Current.SauceLabsRemoteBrowser,
                                         SauceLabsRemoteBrowserVersion = TestEnvironment.Current.SauceLabsRemoteBrowserVersion,
                                         SauceLabsRemotePlatform = TestEnvironment.Current.SauceLabsRemotePlatform,
                                         SauceLabsRemoteDriverUrl = TestEnvironment.Current.SauceLabsRemoteUrl,
                                         SauceLabsRemoteUsername = TestEnvironment.Current.SauceLabsRemoteUsername,
                                         SauceLabsRemoteKey = TestEnvironment.Current.SauceLabsRemoteKey
                                 };

                Navigator.Initialize(parameters);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.AppendLine("[Error] BrowserSetup");

                sb.AppendLine("Exception:");
                sb.AppendLine("   ");
                sb.AppendLine(ex.ToString());

                sb.AppendLine("Parameters:");
                sb.AppendLine(TestEnvironment.Current.Description);

                throw new ApplicationException(sb.ToString());
            }
        }

        public static void Teardown()
        {
            // We need to wait for this session to end
            // before we can start a new one.
            EndBrowserSessionAsync().Wait();
        }

        private static async Task EndBrowserSessionAsync()
        {
            if (!Navigator.CanNavigate)
            {
                return;
            }

            var delay = TestEnvironment.Current.DelayClose;
            if (delay > 0)
            {
                Navigator.Browser.Pause(delay);
            }

            await TraceResultAsync();
            Navigator.Browser.Close();
        }

        private static Task TraceResultAsync()
        {
            var sessionId = Navigator.Browser.SessionId;

            var jobName = ScenarioContext.Current == null ? "n/a" : ScenarioContext.Current.ScenarioInfo.Title;
            var error = ScenarioContext.Current == null ? new Exception("Unknown") : ScenarioContext.Current.TestError;

            var reporter = new TestStatusReporter();
            return reporter.ReportAsync(sessionId, jobName, error == null, error);
        }
    }
}