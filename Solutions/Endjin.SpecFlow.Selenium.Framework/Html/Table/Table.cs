namespace Endjin.SpecFlow.Selenium.Framework.Html.Table
{
    #region Using Directives

    using System.Collections.Generic;

    #endregion

    public class Table
    {
        public Table()
        {
            this.Headers = new List<Column>();
            this.Body = new List<Row>();
        }

        public static Table Empty
        {
            get
            {
                return new Table();
            }
        }

        public string Name
        {
            get;
            set;
        }

        public List<Column> Headers
        {
            get;
            set;
        }

        public List<Row> Body
        {
            get;
            set;
        }
    }
}