namespace Endjin.SpecFlow.Selenium.Sample.Specs.Steps
{
    #region Using directives

    using System;

    using Endjin.SpecFlow.Selenium.Framework.Navigation;
    using Endjin.SpecFlow.Selenium.Sample.Specs.Pages.Account.Register;
    using Endjin.SpecFlow.Selenium.Sample.Specs.Pages.Home.Index;

    using Should;

    using TechTalk.SpecFlow;

    #endregion

    [Binding]
    public class RegisterSteps
    {
        [Given(@"I enter a matching confirm password")]
        public void GivenIEnterAMatchingConfirmPassword()
        {
            var password = ScenarioContext.Current.Get<string>("Password");
            var page = Navigator.Browser.CurrentPage.As<RegisterPageModel>();
            page.EnterConfirmPassword(password);
        }

        [Given(@"I enter a valid email address")]
        public void GivenIEnterAValidEmailAddress()
        {
            var email = Guid.NewGuid().ToString("N") + "@endjin.com";
            ScenarioContext.Current.Set(email, "Email");

            var page = Navigator.Browser.CurrentPage.As<RegisterPageModel>();
            page.EnterEmail(email);
        }

        [Given(@"I enter a valid password")]
        public void GivenIEnterAValidPassword()
        {
            var password = "P455w0rd!";
            ScenarioContext.Current.Set(password, "Password");

            var page = Navigator.Browser.CurrentPage.As<RegisterPageModel>();
            page.EnterPassword(password);
        }

        [When(@"I register")]
        public void WhenIRegister()
        {
            var page = Navigator.Browser.CurrentPage.As<RegisterPageModel>();
            page.ClickRegister();
        }

        [Then(@"I should be logged in")]
        public void ThenIShouldBeLoggedIn()
        {
            var page = Navigator.Browser.CurrentPage.As<HomePageModel>();

            page.HasUserLink().ShouldBeTrue();
        }

        [Then(@"the welcome message should contain my email address")]
        public void ThenTheWelcomeMessageShouldContainMyEmailAddress()
        {
            var page = Navigator.Browser.CurrentPage.As<HomePageModel>();

            var email = ScenarioContext.Current.Get<string>("Email");
            
            page.GetUserLinkText().ShouldContain(email);
        }
    }
}