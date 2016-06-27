namespace Endjin.SpecFlow.Selenium.Framework.Navigation
{
    #region Using Directives

    using System;
    using System.Diagnostics;
    using System.Drawing.Imaging;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;

    using Endjin.SpecFlow.Selenium.Framework.Contracts;
    using Endjin.SpecFlow.Selenium.Framework.Drivers;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.Events;
    using OpenQA.Selenium.Support.Extensions;
    using OpenQA.Selenium.Support.PageObjects;

    #endregion

    public class Navigator
    {
        private readonly INavigationMap navigationMap;

        private static Navigator navigator;

        private static EventFiringWebDriver driver;

        private string queryParams;
        private readonly bool useNavigationMap;

        private Navigator(IWebDriver driver, INavigationMap map, bool useNavigationMap)
        {
            var rwd = driver as RemoteDriver;
            this.SessionId = rwd == null ? Guid.NewGuid().ToString() : rwd.GetSessionId();

            Navigator.driver = new EventFiringWebDriver(driver);
            this.navigationMap = map;

            this.useNavigationMap = useNavigationMap;

            this.SubscribeToEvents();
        }

        public static EventFiringWebDriver Driver
        {
            get
            {
                if (Navigator.driver == null)
                {
                    throw new InvalidOperationException("driver == null");
                }
                return Navigator.driver;
            }
        }

        public static Navigator Browser
        {
            get
            {
                if (navigator == null)
                {
                    throw new InvalidOperationException("navigator == null");
                }

                return navigator;
            }
        }

        public static bool CanNavigate
        {
            get
            {
                return navigator != null;
            }
        }

        public IPageModel CurrentPage
        {
            get;
            set;
        }

        public string CurrentUrl
        {
            get
            {
                return Navigator.driver == null ? string.Empty : Navigator.driver.Url;
            }
        }

        public bool IsQuietMode
        {
            get;
            set;
        }

        public string SessionId
        {
            get;
            private set;
        }

        public static void Initialize(NavigatorSessionParameters session)
        {
            if (navigator != null)
            {
                navigator.Close();
            }

            var driver = WebDriverFactory.Create(session);

            navigator = new Navigator(driver, session.NavigationMap, session.UseNavigationMap)
                        {
                                IsQuietMode =
                                        session.WebDriverType
                                        == WebDriverType.PhantomJs
                        };
        }

        public void Bind(IWebView page)
        {
            PageFactory.InitElements(Navigator.driver, page);
        }

        public void BuildQueryParameters(string format, params string[] parameters)
        {
            var encoded = parameters.Select(HttpUtility.UrlEncode).ToArray();
            this.queryParams = string.Format(format, encoded);
        }

        public void ClearQueryParameters()
        {
            this.queryParams = null;
        }

        public void Click(IWebElement element)
        {
            this.Perform(actions => actions.Click(element));
        }

        public void Close()
        {
            if (Navigator.driver == null)
            {
                return;
            }

            this.UnsubscribeToEvents();

            try
            {
                Navigator.driver.Close();
            }
            catch (Exception ex)
            {
                // When running on build server, we might get timeouts => ignore.
                var error = string.Format("[ERROR] Navigator.Close(){0}{1}", Environment.NewLine, ex);
                Trace.TraceError(error);
            }
            finally
            {
                // Quit() calls Dispose() and sends a DriverCommand.Quit command for RemoteWebDrivers.
                Navigator.driver.Quit();
                Navigator.driver = null;
            }
        }

        public void ContextClick(IWebElement element)
        {
            this.Perform(actions => actions.ContextClick(element));
        }

        public void DoubleClick(IWebElement element)
        {
            this.Perform(actions => actions.DoubleClick(element));
        }

        public void DragAndDrop(IWebElement fromElement, IWebElement toElement)
        {
            var actions = new Actions(Navigator.driver);
            actions.DragAndDrop(fromElement, toElement).Build().Perform();
        }

        public object Execute(string script, params object[] args)
        {
            return Navigator.driver.ExecuteScript(script, args);
        }

        public IPageModel GoToHomePage()
        {
            this.ThrowIfUseNavigationMapIsFalse();
            var pageName = this.navigationMap.HomePageName;
            return this.GoToPageByName(pageName);
        }

        private void ThrowIfUseNavigationMapIsFalse()
        {
            if (!this.useNavigationMap)
            {
                throw new InvalidOperationException("Method not supported when use_navigation_map is false");
            }
        }

        public IPageModel GoToPageByName(string pageName)
        {
            this.ThrowIfUseNavigationMapIsFalse();
            var url = this.navigationMap.GetPageUrl(pageName);

            if (this.queryParams != null)
            {
                url += "?" + this.queryParams;
            }

            var navigation = Navigator.driver.Navigate();
            navigation.GoToUrl(url);

            var page = this.navigationMap.GetPage(pageName);

            this.CurrentPage = page;
            this.CurrentPage.Title = Navigator.driver.Title;

            return page;
        }

        public T GoToPageByUrl<T>(string url) where T : IPageModel, new()
        {
            var navigation = Navigator.driver.Navigate();
            navigation.GoToUrl(url);

            // GoToUrl is blocking => page should be loaded.
            var page = new T { Url = Navigator.driver.Url };

            this.CurrentPage = page;
            this.CurrentPage.Title = Navigator.driver.Title;

            return page;
        }

        public void Hover(IWebElement element)
        {
            this.Perform(actions => actions.MoveToElement(element));
        }

        public bool IsOnPage(string pageName)
        {
            this.ThrowIfUseNavigationMapIsFalse();
            var currentUri = new Uri(this.CurrentUrl);

            var uri = this.navigationMap.GetPageUri(pageName);

            return Uri.Compare(
                               currentUri,
                               uri,
                               UriComponents.Path,
                               UriFormat.UriEscaped,
                               StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        public bool IsRedirectedToPage(string pageName)
        {
            this.ThrowIfUseNavigationMapIsFalse();
            var currentUri = this.CurrentUrl;
            var uri = this.navigationMap.GetPageUri(pageName).ToString();

            return currentUri.Contains(uri);
        }

        public void MaximizeWindow()
        {
            Navigator.driver.Manage().Window.Maximize();
        }

        public void Pause(int delay = 5)
        {
            var seconds = TimeSpan.FromSeconds(delay);
            Task.Delay(seconds).Wait();
        }

        public void ResetCurrentPage()
        {
            this.CurrentPage = null;
        }

        public void ScrollToBottom()
        {
            Navigator.driver.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
        }

        public void ScrollToElement(IWebElement element)
        {
            Navigator.driver.ExecuteScript("window.scrollTo(0, " + element.Location.Y + ");");
        }

        public void SelectTab(int tabIndex)
        {
            var tab = Navigator.driver.WindowHandles[tabIndex];
            Navigator.driver.SwitchTo().Window(tab);
        }

        public void TakeScreenshot(string fileName)
        {
            try
            {
                var screenshot = Navigator.driver.TakeScreenshot();
                screenshot.SaveAsFile(fileName, ImageFormat.Png);
            }
            catch (Exception ex)
            {
                var error = string.Format("** Error{0}{1}{0}{2}", Environment.NewLine, "TakeScreenshot", ex);
                Trace.TraceError(error);
            }
        }

        private static void OnExceptionThrown(object sender, WebDriverExceptionEventArgs args)
        {
            var error = string.Format(
                                      "** Error{0}{1}{0}{2}",
                                      Environment.NewLine,
                                      args.Driver.Url,
                                      args.ThrownException);

            Trace.TraceError(error);
        }

        private void OnElementClicked(object sender, WebElementEventArgs e)
        {
            this.Pause(2);

            var navigated = new Uri(driver.Url);
            this.SetCurrentPage(navigated);
        }

        private void OnNavigated(object sender, WebDriverNavigationEventArgs args)
        {
            this.Pause(2);

            var navigated = new Uri(args.Url);
            this.SetCurrentPage(navigated);
        }

        private void Perform(Func<Actions, Actions> func)
        {
            var actions = new Actions(Navigator.driver);
            func(actions).Build().Perform();
        }

        private void SetCurrentPage(Uri navigated)
        {
            if (this.CurrentPage != null)
            {
                // Moved setting title above the URI comparison to cover cases where we are 
                // detecting a server error by the page title though the URI is the same.
                this.CurrentPage.Title = Navigator.driver.Title;

                var current = new Uri(this.CurrentPage.Url);

                var noChange = Uri.Compare(
                                           current,
                                           navigated,
                                           UriComponents.AbsoluteUri,
                                           UriFormat.UriEscaped,
                                           StringComparison.InvariantCultureIgnoreCase) == 0;

                if (noChange)
                {
                    return;
                }
            }

            // Is this exact URL unmapped, but of a type for which a page type 
            // and page name are known? If so, add a mapping.
            if (this.useNavigationMap)
            {
                this.navigationMap.MapIfNewKnownPageType(navigated);
                this.CurrentPage = this.navigationMap.GetPage(navigated);
                this.CurrentPage.Title = Navigator.driver.Title;
            }
        }

        private void SubscribeToEvents()
        {
            Navigator.driver.ExceptionThrown += OnExceptionThrown;
            Navigator.driver.Navigated += this.OnNavigated;
            Navigator.driver.ElementClicked += this.OnElementClicked;
        }

        private void UnsubscribeToEvents()
        {
            Navigator.driver.ExceptionThrown -= OnExceptionThrown;
            Navigator.driver.Navigated -= this.OnNavigated;
            Navigator.driver.ElementClicked -= this.OnElementClicked;
        }
    }
}