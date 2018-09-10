namespace Endjin.SpecFlow.Selenium.Framework.Models
{
    #region Using Directives

    using Endjin.SpecFlow.Selenium.Framework.Contracts;
    using Endjin.SpecFlow.Selenium.Framework.Navigation;
    using Endjin.SpecFlow.Selenium.Framework.Views;

    #endregion

    public class SectionModel<TView> : ISectionModel
            where TView : SectionView, new()
    {
        protected SectionModel()
        {
            this.Initialise();
        }

        protected TView View
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public TPartialViewModel As<TPartialViewModel>() where TPartialViewModel : class, ISectionModel
        {
            return this as TPartialViewModel;
        }

        private void BindSectionView()
        {
            this.View = new TView();
            Navigator.Browser.Bind(this.View);
        }

        private void Initialise()
        {
            this.BindSectionView();
        }
    }
}