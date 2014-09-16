namespace Endjin.SpecFlow.Selenium.Framework.Models
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    using Endjin.SpecFlow.Selenium.Framework.Contracts;
    using Endjin.SpecFlow.Selenium.Framework.Navigation;
    using Endjin.SpecFlow.Selenium.Framework.Views;

    using OpenQA.Selenium;

    #endregion

    public class PageModel<TView> : IPageModel
            where TView : PageView, new()
    {
        protected PageModel()
        {
            this.Initialise();
            this.Sections = new List<ISectionModel>();
        }

        protected TView View
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
        }

        public List<ISectionModel> Sections
        {
            get;
            set;
        }

        public TModel As<TModel>() where TModel : class, IPageModel
        {
            return this as TModel;
        }

        public bool HasServerError()
        {
            return this.Title != null && this.Title.ToLowerInvariant().Contains("error");
        }

        public bool IsSecure()
        {
            return !string.IsNullOrEmpty(this.Url) && this.Url.StartsWith("https");
        }

        public bool IsValid(string expectedUrl)
        {
            var actual = new Uri(this.Url);
            var expected = new Uri(expectedUrl);

            return Uri.Compare(
                    actual,
                    expected,
                    UriComponents.AbsoluteUri,
                    UriFormat.UriEscaped,
                    StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        public bool ContainsText(string text)
        {
            return this.View.Body.Text.ToLowerInvariant().Contains(text.ToLowerInvariant());
        }

        protected void ClearAndType(IWebElement element, string value)
        {
            element.Clear();
            element.SendKeys(value);
        }

        private void BindPageView()
        {
            this.View = new TView();
            Navigator.Browser.Bind(this.View);
        }

        private void Initialise()
        {
            this.BindPageView();
        }
    }
}