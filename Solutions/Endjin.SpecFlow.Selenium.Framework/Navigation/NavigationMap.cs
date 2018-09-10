namespace Endjin.SpecFlow.Selenium.Framework.Navigation
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Endjin.SpecFlow.Selenium.Framework.Contracts;

    #endregion

    public abstract class NavigationMap : INavigationMap
    {
        private string baseUrl;

        private List<PageMapEntry> pageMappings;

        private List<SectionMapEntry> sectionMappings;

        private PageMapEntry HomePage
        {
            get
            {
                return this.pageMappings.Single(mapping => mapping.Name == this.HomePageName);
            }
        }

        public abstract string HomePageName
        {
            get;
        }

        public IPageModel GetPage(string pageName, string url = null)
        {
            var type = this.GetPageModelType(pageName);

            var page = (IPageModel)Activator.CreateInstance(type);
            page.Url = url ?? this.GetPageUrl(pageName);

            this.AddSections(pageName, page);

            return page;
        }

        public IPageModel GetPage(Uri url)
        {
            var localPath = url.LocalPath.TrimStart('/');

            var entry = this.pageMappings.SingleOrDefault(mapping => mapping.Slug == localPath) ?? this.HomePage;

            return this.GetPage(entry.Name, url.AbsoluteUri);
        }

        public Uri GetPageUri(string pageName)
        {
            var url = this.GetPageUrl(pageName);
            return new Uri(url);
        }

        public string GetPageUrl(string pageName)
        {
            var entry = this.pageMappings.SingleOrDefault(mapping => mapping.Name == pageName) ?? this.HomePage;
            return this.Format(entry.Slug);
        }

        public virtual void MapIfNewKnownPageType(Uri navigated)
        {
        }

        public void Initialize(string url)
        {
            this.baseUrl = url.EndsWith("/") ? url.TrimEnd('/') : url;
            this.pageMappings = new List<PageMapEntry>();
            this.sectionMappings = new List<SectionMapEntry>();

            this.MapPages();
            this.MapSections();
        }

        private void AddSections(string pageName, IPageModel page)
        {
            var sections = this.GetSectionsForPage(pageName);

            foreach (var section in sections)
            {
                page.Sections.Add(section.Name, section);
            }
        }

        private IEnumerable<ISectionModel> GetSectionsForPage(string pageName)
        {
            // Look up the section names for the page name from the mappings.
            var entry = this.pageMappings.SingleOrDefault(mapping => mapping.Name == pageName) ?? this.HomePage;
            if (entry == null || entry.Sections == null)
            {
                return Enumerable.Empty<ISectionModel>();
            }

            return
                    entry.Sections.Select(
                                    sectionName =>
                                    {
                                        var type = this.GetSectionModelType(sectionName);
                                        var section = (ISectionModel)Activator.CreateInstance(type);
                                        section.Name = sectionName;
                                        return section;
                                    })
                            .ToList();
        }

        protected void AddPage<T>(string pageName, string slug, IEnumerable<string> sections = null) where T : IPageModel
        {
            var entry = new PageMapEntry
                        {
                                Name = pageName,
                                Type = typeof(T),
                                Slug = slug
                        };

            if (sections != null)
            {
                entry.Sections = sections.ToList();
            }

            this.pageMappings.Add(entry);
        }

        protected void AddRedirectedPage<T>(string pageName, string slug, IEnumerable<string> sections = null) where T : IPageModel
        {
            var entry = new PageMapEntry
                        {
                                Name = pageName,
                                Type = typeof(T),
                                Slug = slug
                        };

            if (sections != null)
            {
                entry.Sections = sections.ToList();
            }

            this.pageMappings.Add(entry);
        }

        protected void AddSection<T>(string sectionName) where T : ISectionModel
        {
            var entry = new SectionMapEntry { Name = sectionName, Type = typeof(T) };
            this.sectionMappings.Add(entry);
        }

        protected bool IsMappedPage(string pageName)
        {
            return this.pageMappings.SingleOrDefault(mapping => mapping.Name == pageName) != null;
        }

        protected bool IsMappedSection(string sectionName)
        {
            return this.sectionMappings.SingleOrDefault(mapping => mapping.Name == sectionName) != null;
        }

        protected abstract void MapPages();

        private string Format(string slug)
        {
            return string.IsNullOrWhiteSpace(slug) ? this.baseUrl : string.Format("{0}/{1}", this.baseUrl, slug);
        }

        private Type GetPageModelType(string pageName)
        {
            var entry = this.pageMappings.SingleOrDefault(mapping => mapping.Name == pageName) ?? this.HomePage;
            return entry.Type;
        }

        private Type GetSectionModelType(string sectionName)
        {
            var entry = this.sectionMappings.SingleOrDefault(mapping => mapping.Name == sectionName);
            return entry == null ? null : entry.Type;
        }

        protected virtual void MapSections()
        {
        }
    }
}