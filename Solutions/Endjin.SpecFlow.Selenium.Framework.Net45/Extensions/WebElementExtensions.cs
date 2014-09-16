namespace Endjin.SpecFlow.Selenium.Framework.Extensions
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;

    using Endjin.SpecFlow.Selenium.Framework.Html.Table;
    using Endjin.SpecFlow.Selenium.Framework.Navigation;

    using OpenQA.Selenium;

    #endregion

    public static class WebElementExtensions
    {
        public static object Execute(this IWebElement element, string script)
        {
            return Navigator.Browser.Execute(script, element);
        }

        public static Table ToTable(this IWebElement tableElement)
        {
            return tableElement.TagName != "table"
                    ? Table.Empty
                    : new Table
                      {
                              Headers = GetTableHeaders(tableElement).ToList(),
                              Body = GetTableBody(tableElement).ToList()
                      };
        }

        private static IEnumerable<Row> GetTableBody(IWebElement tableElement)
        {
            var bodyElement = tableElement.FindElement(By.TagName("tbody"));
            var bodyRows = bodyElement.FindElements(By.TagName("tr"));

            var rows = new List<Row>();

            for (int rowIndex = 0; rowIndex < bodyRows.Count; rowIndex++)
            {
                var bodyRow = bodyRows[rowIndex];
                var cells = bodyRow.FindElements(By.TagName("td"));

                var row = new Row { Index = rowIndex };

                for (int columnIndex = 0; columnIndex < cells.Count; columnIndex++)
                {
                    var cell = cells[columnIndex];
                    row.Cells.Add(new Cell { ColumnId = columnIndex, Value = cell.Text, WebElement = cell });
                }

                rows.Add(row);
            }

            return rows;
        }

        private static IEnumerable<Column> GetTableHeaders(IWebElement tableElement)
        {
            var headElement = tableElement.FindElement(By.TagName("thead"));

            var headRow = headElement.FindElement(By.TagName("tr"));
            var headCells = headRow.FindElements(By.TagName("th"));

            return headCells.Select((headCell, index) => new Column { Id = index, Index = index, Name = headCell.Text });
        }
    }
}