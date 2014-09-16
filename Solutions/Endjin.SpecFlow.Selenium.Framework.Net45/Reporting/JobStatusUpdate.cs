namespace Endjin.SpecFlow.Selenium.Framework.Reporting
{
    #region Using Directives

    using System.Collections.Generic;

    using Newtonsoft.Json;

    #endregion

    public class JobStatusUpdate
    {
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "tags", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Tags
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "public", NullValueHandling = NullValueHandling.Ignore)]
        public string Public
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "passed", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Passed
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "build", NullValueHandling = NullValueHandling.Ignore)]
        public int? Build
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "customData", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<string, string> Data
        {
            get;
            set;
        }
    }
}