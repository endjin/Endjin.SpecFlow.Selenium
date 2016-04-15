namespace Endjin.SpecFlow.Selenium.Sample.Specs.Spa.Pages.Home.Spa1
{
    using Endjin.SpecFlow.Selenium.Framework.Models;

    public class SpaView1Model : PageModel<Spa1View>
    {
        public void GoToSpaView2()
        {
            this.View.Spa2Link.Click();
        }
    }
}