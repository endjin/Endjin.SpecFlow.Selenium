namespace Endjin.SpecFlow.Selenium.Framework.Retry
{
    #region Using Directives

    using System;

    using Endjin.Core.Retry.Policies;
    using Endjin.SpecFlow.Selenium.Framework.Exceptions;

    #endregion

    public class UnexpectedPageRetryPolicy : IRetryPolicy
    {
        public bool CanRetry(Exception ex)
        {
            return ex is PageNotExpectedException;
        }
    }
}