namespace Endjin.SpecFlow.Selenium.Sample.Specs.Spa.Pages.Home.Spa2
{
    using Endjin.SpecFlow.Selenium.Framework.Views;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    public class Spa2View : PageView
    {
        [FindsBy(How = How.Id, Using = "SpaView1Link")]
        public IWebElement Spa1Link { get; set; }
    }
}