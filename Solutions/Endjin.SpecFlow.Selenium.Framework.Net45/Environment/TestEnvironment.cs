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

        private string remoteBrowser;

        private string remoteBrowserVersion;

        private string remotePlatform;

        static TestEnvironment()
        {
            var variable = Environment.GetEnvironmentVariable(TeamCityVersion);
            IsOnBuildServer = !string.IsNullOrEmpty(variable);
        }

        private TestEnvironment()
        {
            var config = new FileInfo(TestContext.CurrentContext.TestDirectory + @"\WebsiteUnderTest\web_test.config.json");
            var json = File.ReadAllText(config.FullName);

            this.settings = JsonConvert.DeserializeObject<WebTestSettings>(json);
        }

        public static TestEnvironment Current
        {
            get
            {
                return instance ?? (instance = new TestEnvironment());
            }
        }

        public bool AutoStartIIS
        {
            get
            {
                return this.settings.LocalAutoStartIIS ?? false;
            }
        }

        public int DelayClose
        {
            get
            {
                return this.settings.DelayClose ?? 0;
            }
        }

        public bool HideIIS
        {
            get
            {
                return this.settings.LocalHideIIS ?? false;
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

        public string RemoteBrowser
        {
            get
            {
                if (!this.remoteBrowser.IsNullOrEmpty())
                {
                    return this.remoteBrowser;
                }

                return this.settings.RemoteBrowser ?? "internet explorer";
            }
            private set
            {
                this.remoteBrowser = value;
            }
        }

        public string RemoteBrowserVersion
        {
            get
            {
                if (!this.remoteBrowserVersion.IsNullOrEmpty())
                {
                    return this.remoteBrowserVersion;
                }

                return this.settings.RemoteBrowserVersion ?? "11";
            }
            private set
            {
                this.remoteBrowserVersion = value;
            }
        }

        public string RemoteUrl
        {
            get
            {
                return this.settings.RemoteUrl;
            }
        }

        public string RemoteKey
        {
            get
            {
                return Environment.GetEnvironmentVariable(TeamCitySauceApiKey) ?? this.settings.RemoteKey;
            }
        }

        public string RemotePlatform
        {
            get
            {
                if (!string.IsNullOrEmpty(this.remotePlatform))
                {
                    return this.remotePlatform;
                }

                return this.settings.RemotePlatform ?? "Windows 7";
            }

            private set
            {
                this.remotePlatform = value;
            }
        }

        public string RemoteUsername
        {
            get
            {
                return Environment.GetEnvironmentVariable(TeamCitySauceUserName) ?? this.settings.RemoteUsername;
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
                if (IsOnBuildServer && !this.RunLocally)
                {
                    // Run tests using a remote driver (e.g. tests are running against Sauce Labs).
                    return WebDriverType.Remote;
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

        public bool RunLocally
        {
            get { return this.settings.RunLocally ?? true; }
        }

        public string Description
        {
            get
            {
                var sb = new StringBuilder();

                sb.AppendLine("WebAppName" + this.WebAppName);
                sb.AppendLine("WebsiteUrl = " + this.WebsiteUrl);
                sb.AppendLine("WebDriverType = " + this.WebDriverType);
                sb.AppendLine("IsRemote" + this.IsRemote);
                sb.AppendLine("IsLocal" + this.IsLocal);
                sb.AppendLine("AutoStartIIS" + this.AutoStartIIS);
                sb.AppendLine("HideIIS" + this.HideIIS);
                sb.AppendLine("DelayClose" + this.DelayClose);
                sb.AppendLine("AcceptUntrustedCertificates" + this.AcceptUntrustedCertificates);
                sb.AppendLine("ImplicitlyWait = " + this.ImplicitlyWait);
                sb.AppendLine("PageLoadTimeout = " + this.PageLoadTimeout);
                sb.AppendLine("ScriptTimeout = " + this.ScriptTimeout);
                sb.AppendLine("RemoteBrowser = " + this.RemoteBrowser);
                sb.AppendLine("RemoteBrowserVersion = " + this.RemoteBrowserVersion);
                sb.AppendLine("RemotePlatform = " + this.RemotePlatform);
                sb.AppendLine("RemoteDriverUrl = " + this.RemoteUrl);
                sb.AppendLine("RemoteUsername = " + this.RemoteUsername);
                sb.AppendLine("RemoteKey = " + this.RemoteKey);

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
            this.RemotePlatform = platform;
            this.RemoteBrowser = browser;
            this.RemoteBrowserVersion = browserVersion;
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
                                      this.HideIIS
                                              ? ProcessWindowStyle.Hidden
                                              : ProcessWindowStyle.Normal,
                              ErrorDialog = true,
                              CreateNoWindow = this.HideIIS,
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
                if (this.HideIIS)
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