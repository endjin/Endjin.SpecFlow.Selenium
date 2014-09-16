namespace Endjin.SpecFlow.Selenium.Framework.Exceptions
{
    #region Using Directives

    using System;

    using Endjin.SpecFlow.Selenium.Framework.Contracts;

    #endregion

    public class PageNotExpectedException : Exception
    {
        public PageNotExpectedException(IPageModel model, string expectedUrl)
                : base(ToMessage(model, expectedUrl))
        {
        }

        private static string ToMessage(IPageModel model, string expectedUrl)
        {
            return string.Format("Expected URL was {0} but page was created with URL {1}", expectedUrl, model.Url);
        }
    }
}