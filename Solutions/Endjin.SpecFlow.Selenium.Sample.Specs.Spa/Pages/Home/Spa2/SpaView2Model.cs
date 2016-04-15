namespace Endjin.SpecFlow.Selenium.Sample.Specs.Spa.Pages.Home.Spa2
{
    using Endjin.SpecFlow.Selenium.Framework.Models;

    public class SpaView2Model : PageModel<Spa2View>
    {
        public void GoToSpaView1()
        {
            this.View.Spa1Link.Click();
        }
    }
}