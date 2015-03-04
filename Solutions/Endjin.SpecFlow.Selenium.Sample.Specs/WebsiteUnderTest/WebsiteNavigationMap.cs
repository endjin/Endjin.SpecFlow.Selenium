// ReSharper disable once CheckNamespace

namespace Endjin.SpecFlow.Selenium.Framework.Navigation
{
    using Endjin.SpecFlow.Selenium.Sample.Specs.Pages.Account.Register;
    using Endjin.SpecFlow.Selenium.Sample.Specs.Pages.Home.Index;

    public class WebsiteNavigationMap : NavigationMap
    {
        public override string HomePageName
        {
            get
            {
                return Page.Home;
            }
        }

        // Maps the type to the name, slug, and shared sections contained on the page if any.
        protected override void MapPages()
        {
            this.AddPage<HomePageModel>(this.HomePageName, string.Empty);
            this.AddPage<RegisterPageModel>(Page.Register, "Account/Register");
        }

        public static class Page
        {
            public const string Home = "home";
            public const string Register = "register";
        }
    }
}