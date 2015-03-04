namespace Endjin.SpecFlow.Selenium.Sample.Specs.Pages.Account.Register
{
    using Endjin.SpecFlow.Selenium.Framework.Views;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    public class RegisterPageView : PageView
    {
        [FindsBy(How = How.Id, Using = "Email")]
        public IWebElement EmailInput { get; set; }

        [FindsBy(How = How.Id, Using = "Password")]
        public IWebElement PasswordInput { get; set; }
        
        [FindsBy(How = How.Id, Using = "ConfirmPassword")]
        public IWebElement ConfirmPasswordInput { get; set; }

        [FindsBy(How = How.Id, Using = "Register")]
        public IWebElement RegisterButton { get; set; }
    }
}