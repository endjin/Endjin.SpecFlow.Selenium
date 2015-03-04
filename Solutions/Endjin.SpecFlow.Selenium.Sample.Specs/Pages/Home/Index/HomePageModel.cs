namespace Endjin.SpecFlow.Selenium.Sample.Specs.Pages.Home.Index
{
    using Endjin.SpecFlow.Selenium.Framework.Models;

    public class HomePageModel : PageModel<HomePageView>
    {
        public bool HasManageLink()
        {
            try
            {
                var text = this.View.ManageLink.Text;
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public string GetManageLinkText()
        {
            return this.View.ManageLink.Text;
        }
    }
}