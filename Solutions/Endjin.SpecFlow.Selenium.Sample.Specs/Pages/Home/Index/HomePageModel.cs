namespace Endjin.SpecFlow.Selenium.Sample.Specs.Pages.Home.Index
{
    using Endjin.SpecFlow.Selenium.Framework.Models;

    public class HomePageModel : PageModel<HomePageView>
    {
        public bool HasUserLink()
        {
            try
            {
                var text = this.View.UserLink.Text;
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public string GetUserLinkText()
        {
            return this.View.UserLink.Text;
        }
    }
}