namespace Endjin.SpecFlow.Selenium.Framework.Navigation
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    #endregion

    public class PageMapEntry
    {
        public string Name
        {
            get;
            set;
        }

        public List<string> Sections
        {
            get;
            set;
        }

        public string Slug
        {
            get;
            set;
        }

        public Type Type
        {
            get;
            set;
        }
    }
}