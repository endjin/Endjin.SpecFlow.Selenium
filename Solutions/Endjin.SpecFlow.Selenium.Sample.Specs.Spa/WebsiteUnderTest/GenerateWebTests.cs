﻿// -----------------------------------------------------------------------------------------
//  <auto-generated>
//      This code is auto-generated and changes to this file will be lost when regenerated.
//  </auto-generated>
// -----------------------------------------------------------------------------------------
// ReSharper disable once CheckNamespace
namespace Endjin.SpecFlow.Selenium.Framework
{
    #region Using Directives

    using Endjin.SpecFlow.Selenium.Framework.Environment;
    using Endjin.SpecFlow.Selenium.Framework.Navigation;

    using TechTalk.SpecFlow;

    #endregion

    [Binding]
    public class FeatureHooks
    {
        [BeforeFeature]
        public static void FeatureSetup()
        {
            if (TestEnvironment.Current.IsLocal && TestEnvironment.Current.AutoStartIIS)
            {
                TestEnvironment.Current.StartWebsite();
            }
        }

        [AfterFeature]
        public static void FeatureTeardown()
        {
            TestEnvironment.Current.StopWebsite();
        }
    }

    [Binding]
    public class WebTestSetupHooks
    {
        [BeforeScenario]
        public static void BeforeScenarioSetup()
        {
            BrowserTest.Setup(new WebsiteNavigationMap());
        }
    }

    [Binding]
    public class WebTestTeardownHooks
    {
        [AfterScenario]
        public static void ScenarioTeardown()
        {
            BrowserTest.Teardown();
        }
    }
}

namespace  Endjin.SpecFlow.Selenium.Sample.Specs.Spa.Features
{
    #region Using Directives
    
    using System.Diagnostics;
    using NUnit.Framework;
    using TechTalk.SpecFlow;
    using Endjin.SpecFlow.Selenium.Framework;
    using Endjin.SpecFlow.Selenium.Framework.Features;

    #endregion

        public partial class  HomeFeature : BrowserTestFeature
        {
            public  HomeFeature()
            {
            }

            public  HomeFeature(string platform, string browser, string browserVersion)
                : base(platform, browser, browserVersion)
            {
            }
        }
}

