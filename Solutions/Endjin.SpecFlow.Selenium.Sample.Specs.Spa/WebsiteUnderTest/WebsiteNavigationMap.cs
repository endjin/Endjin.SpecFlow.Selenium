// ReSharper disable once CheckNamespace

namespace Endjin.SpecFlow.Selenium.Framework.Navigation
{
    using Endjin.SpecFlow.Selenium.Sample.Specs.Spa.Pages.Home.Index;
    using Endjin.SpecFlow.Selenium.Sample.Specs.Spa.Pages.Home.Spa1;
    using Endjin.SpecFlow.Selenium.Sample.Specs.Spa.Pages.Home.Spa2;

    public class WebsiteNavigationMap : NavigationMap
    {
        public override string HomePageName
        {
            get
            {
                return Page.Home;
            }
        }


        protected override string SpaRoutingMarker => "#";

        // Maps the type to the name, slug, and shared sections contained on the page if any.
        protected override void MapPages()
        {
            this.AddPage<HomePageModel>(this.HomePageName, string.Empty);
            this.AddPage<SpaView1Model>(Page.Spa1, "/view1");
            this.AddPage<SpaView2Model>(Page.Spa2, "/view2");
        }

        public static class Page
        {
            public const string Home = "home";
            public const string Spa1 = "spaView1";
            public const string Spa2 = "spaView2";
        }
    }
}