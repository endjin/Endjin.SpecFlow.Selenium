namespace Endjin.SpecFlow.Selenium.Sample.Specs.Spa.Pages.Home.Index
{
    using Endjin.SpecFlow.Selenium.Framework.Views;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    public class HomePageView : PageView
    {
        [FindsBy(How = How.Id, Using = "Manage")]
        public IWebElement ManageLink { get; set; }

        [FindsBy(How = How.Id, Using = "HomePageSpa1Link")]
        public IWebElement Spa1Link { get; set; }
    }
}