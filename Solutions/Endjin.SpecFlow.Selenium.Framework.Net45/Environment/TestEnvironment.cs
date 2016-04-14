namespace Endjin.SpecFlow.Selenium.Framework.Environment
{
    #region Using Directives

    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    using Endjin.SpecFlow.Selenium.Framework.Drivers;
    using Endjin.SpecFlow.Selenium.Framework.Extensions;

    using Newtonsoft.Json;

    using NUnit.Framework;

    #endregion

    public class TestEnvironment
    {
        private const string TeamCityVersion = "TEAMCITY_VERSION";

        private const string TeamCitySauceUserName = "SAUCE_USER_NAME";

        private const string TeamCitySauceApiKey = "SAUCE_API_KEY";

        private static TestEnvironment instance;

        private readonly WebTestSettings settings;

        private Process iis;

        private string sauceLabsRemoteBrowser;

        private string sauceLabsRemoteBrowserVersion;

        private string remotePlatform;

        static TestEnvironment()
        {
            var variable = Environment.GetEnvironmentVariable(TeamCityVersion);
            IsOnBuildServer = !string.IsNullOrEmpty(variable);
        }

        private TestEnvironment()
        {
            var pathToConfig = @"WebsiteUnderTest\web_test.config.json";
            // If tests are running on NUnit > 3.0, use test directory to find files
            // A null check for this directory doesn't suffice as null checking TestContext.CurrentContext.TestDirectory throws a null reference exception with older versions of NUnit on TeamCity
            try
            {
                pathToConfig = Path.Combine(TestContext.CurrentContext.TestDirectory, pathToConfig);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("TestContext.CurrentContext.TestDirectory not found: " + ex.Message);
            }
            
            var json = File.ReadAllText(new FileInfo(pathToConfig).FullName);

            this.settings = JsonConvert.DeserializeObject<WebTestSettings>(json);
        }

        public static TestEnvironment Current
        {
            get
            {
                return instance ?? (instance = new TestEnvironment());
            }
        }

        public bool AutoStartLocalIIS
        {
            get
            {
                return this.settings.AutoStartLocalIIS ?? false;
            }
        }

        public int DelayClose
        {
            get
            {
                return this.settings.DelayClose ?? 0;
            }
        }

        public bool HideLocalIIS
        {
            get
            {
                return this.settings.HideLocalIIS ?? false;
            }
        }

        public bool HideCommandPromptWindow
        {
            get
            {
                return this.settings.HideCommandPromptWindow ?? false;
            }
        }

        public int ImplicitlyWait
        {
            get
            {
                return this.settings.ImplicitlyWait ?? 30;
            }
        }

        public bool IsLocal
        {
            get
            {
                return this.WebDriverType != WebDriverType.Remote && !this.WebsiteUrl.IsNullOrEmpty()
                       && (this.WebsiteUrl.Contains("localhost") || this.WebsiteUrl.Contains("127.0.0.1"));
            }
        }

        public bool IsRemote
        {
            get
            {
                return this.WebDriverType == WebDriverType.Remote;
            }
        }

        public int PageLoadTimeout
        {
            get
            {
                return this.settings.PageLoadTimeout ?? 30;
            }
        }

        public string SauceLabsRemoteBrowser
        {
            get
            {
                if (!this.sauceLabsRemoteBrowser.IsNullOrEmpty())
                {
                    return this.sauceLabsRemoteBrowser;
                }

                return this.settings.SauceLabsRemoteBrowser ?? "internet explorer";
            }
            private set
            {
                this.sauceLabsRemoteBrowser = value;
            }
        }

        public string SauceLabsRemoteBrowserVersion
        {
            get
            {
                if (!this.sauceLabsRemoteBrowserVersion.IsNullOrEmpty())
                {
                    return this.sauceLabsRemoteBrowserVersion;
                }

                return this.settings.SauceLabsRemoteBrowserVersion ?? "11";
            }
            private set
            {
                this.sauceLabsRemoteBrowserVersion = value;
            }
        }

        public string SauceLabsRemoteUrl
        {
            get
            {
                return this.settings.SauceLabsRemoteUrl;
            }
        }

        public string SauceLabsRemoteKey
        {
            get
            {
                return Environment.GetEnvironmentVariable(TeamCitySauceApiKey) ?? this.settings.SauceLabsRemoteKey;
            }
        }

        public string SauceLabsRemotePlatform
        {
            get
            {
                if (!string.IsNullOrEmpty(this.remotePlatform))
                {
                    return this.remotePlatform;
                }

                return this.settings.SauceLabsRemotePlatform ?? "Windows 7";
            }

            private set
            {
                this.remotePlatform = value;
            }
        }

        public string SauceLabsRemoteUsername
        {
            get
            {
                return Environment.GetEnvironmentVariable(TeamCitySauceUserName) ?? this.settings.SauceLabsRemoteUsername;
            }
        }

        public int ScriptTimeout
        {
            get
            {
                return this.settings.ScriptTimeout ?? 3600;
            }
        }

        public string WebAppName
        {
            get
            {
                return this.settings.WebAppName;
            }
        }

        public WebDriverType WebDriverType
        {
            get
            {
                if (this.RunUsingSauceLabs)
                {
                    return WebDriverType.Remote;
                }
                if (IsOnBuildServer && !this.RunUsingSauceLabs)
                {
                    return WebDriverType.PhantomJs;
                }

                var value = this.settings.WebDriverType;

                WebDriverType type;
                return Enum.TryParse(value, true, out type) ? type : WebDriverType.Default;
            }
        }

        public string WebsiteUrl
        {
            get
            {
                return this.settings.WebsiteUrl;
            }
        }

        private static bool IsOnBuildServer
        {
            get;
            set;
        }

        public bool RunUsingSauceLabs
        {
            get { return this.settings.RunUsingSauceLabs ?? false; }
        }

        public string Description
        {
            get
            {
                var sb = new StringBuilder();

                sb.AppendLine("WebAppName" + this.WebAppName);
                sb.AppendLine("WebsiteUrl = " + this.WebsiteUrl);
                sb.AppendLine("RunUsingSauceLabs = " + this.RunUsingSauceLabs);
                sb.AppendLine("WebDriverType = " + this.WebDriverType);
                sb.AppendLine("IsRemote" + this.IsRemote);
                sb.AppendLine("IsLocal" + this.IsLocal);
                sb.AppendLine("AutoStartLocalIIS" + this.AutoStartLocalIIS);
                sb.AppendLine("HideLocalIIS" + this.HideLocalIIS);
                sb.AppendLine("DelayClose" + this.DelayClose);
                sb.AppendLine("AcceptUntrustedCertificates" + this.AcceptUntrustedCertificates);
                sb.AppendLine("ImplicitlyWait = " + this.ImplicitlyWait);
                sb.AppendLine("PageLoadTimeout = " + this.PageLoadTimeout);
                sb.AppendLine("ScriptTimeout = " + this.ScriptTimeout);
                sb.AppendLine("SauceLabsRemoteBrowser = " + this.SauceLabsRemoteBrowser);
                sb.AppendLine("SauceLabsRemoteBrowserVersion = " + this.SauceLabsRemoteBrowserVersion);
                sb.AppendLine("SauceLabsRemotePlatform = " + this.SauceLabsRemotePlatform);
                sb.AppendLine("SauceLabsRemoteDriverUrl = " + this.SauceLabsRemoteUrl);
                sb.AppendLine("SauceLabsRemoteUsername = " + this.SauceLabsRemoteUsername);
                sb.AppendLine("SauceLabsRemoteKey = " + this.SauceLabsRemoteKey);

                return sb.ToString();
            }
        }

        public bool AcceptUntrustedCertificates
        {
            get
            {
                return this.settings.AcceptUntrustedCertificates ?? false;
            }
        }

        public void SetRemoteBrowserContext(string platform, string browser, string browserVersion)
        {
            this.SauceLabsRemotePlatform = platform;
            this.SauceLabsRemoteBrowser = browser;
            this.SauceLabsRemoteBrowserVersion = browserVersion;
        }

        public void StartWebsite()
        {
            this.StopWebsite();

            if (this.WebAppName.IsNullOrEmpty())
            {
                return;
            }

            Uri uri;
            if (!Uri.TryCreate(this.WebsiteUrl, UriKind.RelativeOrAbsolute, out uri))
            {
                Debug.WriteLine(
                        "[Error] Failed to parse website url config value{0}{1}",
                        Environment.NewLine,
                        this.WebsiteUrl);

                return;
            }

            var port = uri.Port;
            var path = GetApplicationPath(this.WebAppName);
            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            var psi = new ProcessStartInfo
                      {
                              FileName = string.Format("{0}\\IIS Express\\iisexpress.exe", programFiles),
                              Arguments = string.Format("/path:\"{0}\" /port:{1}", path, port),
                              WindowStyle =
                                      this.HideLocalIIS
                                              ? ProcessWindowStyle.Hidden
                                              : ProcessWindowStyle.Normal,
                              ErrorDialog = true,
                              CreateNoWindow = this.HideLocalIIS,
                              UseShellExecute = false
                      };

            try
            {
                this.iis = new Process { StartInfo = psi };
                this.iis.Start();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[Error] Failed to start iisexpress.exe{0}{1}", Environment.NewLine, ex);
                this.StopWebsite();
            }
        }

        public void StopWebsite()
        {
            if (this.iis == null || this.iis.HasExited)
            {
                return;
            }

            try
            {
                if (this.HideLocalIIS)
                {
                    // If we don't have a window, we have to 'kill' the process.
                    this.iis.Kill();
                }
                else
                {
                    this.iis.CloseMainWindow();
                    this.iis.Dispose();
                }

                if (this.iis.HasExited || this.iis.WaitForExit(5000))
                {
                    return;
                }

                throw new TimeoutException("iisexpress.exe is taking longer than expected to exit");
            }
            catch (InvalidOperationException ioe)
            {
                // If we no processs is associated => process already exited.
                if (!ioe.Message.Contains("No process is associated"))
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[Error] Failed to stop iisexpress.exe{0}{1}", Environment.NewLine, ex);
                try
                {
                    // Try to kill the process again if it hasn't exited yet.
                    this.iis.Kill();
                }
                catch
                {
                    Debug.WriteLine("iisexpress.exe already exited");
                }
            }
            finally
            {
                this.iis = null;
            }
        }

        private static string GetApplicationPath(string webAppName)
        {
            var solutionFolder =
                    Path.GetDirectoryName(
                            Path.GetDirectoryName(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)));

            return Path.Combine(solutionFolder, webAppName);
        }
    }
}