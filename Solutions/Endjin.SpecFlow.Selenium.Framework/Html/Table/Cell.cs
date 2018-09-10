namespace Endjin.SpecFlow.Selenium.Framework.Html.Table
{
    #region Using Directives

    using OpenQA.Selenium;

    #endregion

    public class Cell
    {
        public string Value
        {
            get;
            set;
        }

        public int ColumnId
        {
            get;
            set;
        }

        public IWebElement WebElement
        {
            get;
            set;
        }
    }
}