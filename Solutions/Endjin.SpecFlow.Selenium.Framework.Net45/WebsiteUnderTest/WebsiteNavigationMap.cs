// ReSharper disable once CheckNamespace

namespace Endjin.SpecFlow.Selenium.Framework.Navigation
{
    public class WebsiteNavigationMap : NavigationMap
    {
        public override string HomePageName
        {
            get
            {
                return "default";
            }
        }

        // Maps the type to the name, slug, and shared sections contained on the page if any.
        protected override void MapPages()
        {
        }
    }
}