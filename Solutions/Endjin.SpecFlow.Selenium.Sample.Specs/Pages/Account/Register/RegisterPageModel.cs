namespace Endjin.SpecFlow.Selenium.Sample.Specs.Pages.Account.Register
{
    using Endjin.SpecFlow.Selenium.Framework.Models;

    public class RegisterPageModel : PageModel<RegisterPageView>
    {
        public void EnterEmail(string email)
        {
            this.View.EmailInput.SendKeys(email);
        }

        public void EnterPassword(string password)
        {
            this.View.PasswordInput.SendKeys(password);
        }

        public void EnterConfirmPassword(string password)
        {
            this.View.ConfirmPasswordInput.SendKeys(password);
        }

        public void ClickRegister()
        {
            this.View.RegisterButton.Click();
        }
    }
}