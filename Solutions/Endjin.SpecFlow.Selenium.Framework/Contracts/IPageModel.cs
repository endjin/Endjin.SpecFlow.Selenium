namespace Endjin.SpecFlow.Selenium.Framework.Contracts
{
    #region Using Directives

    using System.Collections.Generic;

    #endregion

    public interface IPageModel
    {
        string Title
        {
            get;
            set;
        }

        string Url
        {
            get;
            set;
        }

        Dictionary<string, ISectionModel> Sections
        {
            get;
            set;
        }

        TModel As<TModel>() where TModel : class, IPageModel;

        bool HasServerError();

        bool IsSecure();

        bool IsValid(string expectedUrl);

        bool ContainsText(string text);

        TSectionModel Section<TSectionModel>() where TSectionModel : class, ISectionModel;
    }
}