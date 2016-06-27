namespace Endjin.SpecFlow.Selenium.Sample.Specs.Spa.Steps
{
    #region Using Directives

    using Endjin.SpecFlow.Selenium.Framework.Navigation;
    using Endjin.SpecFlow.Selenium.Sample.Specs.Spa.Pages.Home.Index;

    using TechTalk.SpecFlow;

    #endregion

    [Binding]
    public class HomeSteps
    {
        [When(@"I click the Spa1 link")]
        public void WhenIClickTheSpaLink()
        {
            var page = Navigator.Browser.CurrentPage.As<HomePageModel>();
            page.ClickSpa1Link();
        }
    }
}