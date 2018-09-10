namespace Endjin.SpecFlow.Selenium.Framework.Drivers
{
    #region Using Directives

    using System;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    #endregion

    public class RemoteDriver : RemoteWebDriver
    {
        public RemoteDriver(ICommandExecutor commandExecutor, ICapabilities desiredCapabilities)
                : base(commandExecutor, desiredCapabilities)
        {
        }

        public RemoteDriver(ICapabilities desiredCapabilities)
                : base(desiredCapabilities)
        {
        }

        public RemoteDriver(Uri remoteAddress, ICapabilities desiredCapabilities)
                : base(remoteAddress, desiredCapabilities)
        {
        }

        public RemoteDriver(Uri remoteAddress, ICapabilities desiredCapabilities, TimeSpan commandTimeout)
                : base(remoteAddress, desiredCapabilities, commandTimeout)
        {
        }

        public string GetSessionId()
        {
            return this.SessionId.ToString();
        }
    }
}