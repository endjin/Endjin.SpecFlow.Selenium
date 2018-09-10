namespace Endjin.SpecFlow.Selenium.Framework.Transforms
{
    #region Using Directives

    using System;

    using TechTalk.SpecFlow;

    #endregion

    [Binding]
    public class StepArgumentTransformations
    {
        [StepArgumentTransformation(@"(\d{2}:\d{2})")]
        public TimeSpan TimeSpanTransform(string ts)
        {
            return TimeSpan.Parse(ts);
        }
    }
}