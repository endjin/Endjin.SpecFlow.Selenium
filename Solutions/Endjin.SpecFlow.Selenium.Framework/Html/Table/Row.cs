namespace Endjin.SpecFlow.Selenium.Framework.Html.Table
{
    #region Using Directives

    using System.Collections.Generic;

    #endregion

    public class Row
    {
        public Row()
        {
            this.Cells = new List<Cell>();
        }

        public List<Cell> Cells
        {
            get;
            set;
        }

        public int Index
        {
            get;
            set;
        }
    }
}