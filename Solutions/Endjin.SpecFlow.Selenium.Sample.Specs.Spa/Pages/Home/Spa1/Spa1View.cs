namespace Endjin.SpecFlow.Selenium.Sample.Specs.Spa.Pages.Home.Spa1
{
    using Endjin.SpecFlow.Selenium.Framework.Views;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    public class Spa1View : PageView
    {
        [FindsBy(How = How.Id, Using = "SpaView2Link")]
        public IWebElement Spa2Link { get; set; }
    }
}