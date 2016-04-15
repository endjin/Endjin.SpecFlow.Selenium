namespace Endjin.SpecFlow.Selenium.Sample.Specs.Spa.Steps
{
    #region Using directives

    using Endjin.SpecFlow.Selenium.Framework.Navigation;

    using Should;

    using TechTalk.SpecFlow;

    #endregion

    [Binding]
    public class CommonSteps
    {
        [Given(@"I go to the (.*) page")]
        public void IGoToThePage(string page)
        {
            Navigator.Browser.GoToPageByName(page);
        }

        [Then(@"I should be taken to the (.*) page")]
        public void IShouldBeOnThePage(string page)
        {
            Navigator.Browser.IsOnPage(page).ShouldBeTrue();
        }
    }
}