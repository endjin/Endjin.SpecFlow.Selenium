namespace Endjin.SpecFlow.Selenium.Framework.Hooks
{
    #region Using Directives

    using System.Diagnostics;

    using Endjin.SpecFlow.Selenium.Framework.Constants;
    using Endjin.SpecFlow.Selenium.Framework.Navigation;

    using TechTalk.SpecFlow;

    #endregion

    [Binding]
    public class TraceHooks
    {
        [BeforeStep(ScopeTag.TraceUrl)]
        public void BeforeStep()
        {
            Debug.WriteLine("** [BeforeStep]");
            Debug.WriteLine(Navigator.Browser.CurrentUrl);
            Debug.WriteLine("==================");
        }

        [AfterStep(ScopeTag.TraceUrl)]
        public void AfterStep()
        {
            Debug.WriteLine("** [AfterStep]");
            Debug.WriteLine(Navigator.Browser.CurrentUrl);
            Debug.WriteLine("==================");
        }
    }
}