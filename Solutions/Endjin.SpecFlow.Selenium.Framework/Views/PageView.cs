namespace Endjin.SpecFlow.Selenium.Framework.Views
{
    #region Using Directives

    using Endjin.SpecFlow.Selenium.Framework.Contracts;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    #endregion

    public class PageView : IPageView
    {
        protected PageView()
        {
        }

        [FindsBy(How = How.CssSelector, Using = "BODY")]
        public IWebElement Body
        {
            get;
            set;
        }

        [FindsBy(How = How.TagName, Using = "header")]
        public IWebElement Header
        {
            get;
            set;
        }

        [FindsBy(How = How.TagName, Using = "footer")]
        public IWebElement Footer
        {
            get;
            set;
        }
    }
}