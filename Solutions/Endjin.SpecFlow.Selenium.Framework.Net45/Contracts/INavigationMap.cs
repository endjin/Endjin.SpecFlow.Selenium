namespace Endjin.SpecFlow.Selenium.Framework.Contracts
{
    #region Using Directives

    using System;

    #endregion

    public interface INavigationMap
    {
        string HomePageName
        {
            get;
        }

        IPageModel GetPage(string pageName, string url = null);

        IPageModel GetPage(Uri url);

        Uri GetPageUri(string pageName);

        string GetPageUrl(string pageName);

        void MapIfNewKnownPageType(Uri navigated);

        void Initialize(string baseUrl);
    }
}