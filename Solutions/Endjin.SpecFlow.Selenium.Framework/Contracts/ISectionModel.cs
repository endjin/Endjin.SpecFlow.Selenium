namespace Endjin.SpecFlow.Selenium.Framework.Contracts
{
    public interface ISectionModel
    {
        string Name
        {
            get;
            set;
        }

        TModel As<TModel>() where TModel : class, ISectionModel;
    }
}