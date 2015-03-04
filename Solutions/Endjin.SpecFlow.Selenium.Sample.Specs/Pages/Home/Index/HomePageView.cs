namespace Endjin.SpecFlow.Selenium.Sample.Specs.Pages.Home.Index
{
    using Endjin.SpecFlow.Selenium.Framework.Views;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    public class HomePageView : PageView
    {
        [FindsBy(How = How.Id, Using = "Manage")]
        public IWebElement ManageLink { get; set; }
    }
}