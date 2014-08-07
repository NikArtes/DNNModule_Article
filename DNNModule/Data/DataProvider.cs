/*
' Copyright (c) 2014 Christoc.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Framework.Providers;
using Christoc.Modules.DNNModule.Components;


namespace Christoc.Modules.DNNModule.Data
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// An abstract class for the data access layer
    /// 
    /// The abstract data provider provides the methods that a control data provider (sqldataprovider)
    /// must implement. You'll find two commented out examples in the Abstract methods region below.
    /// </summary>
    /// -----------------------------------------------------------------------------
    public abstract class DataProvider
    {

        #region Shared/Static Methods

        private static DataProvider provider;

        // return the provider
        public static DataProvider Instance()
        {
            if (provider == null)
            {
                const string assembly = "Christoc.Modules.DNNModule.Data.SqlDataprovider,DNNModule";
                Type objectType = Type.GetType(assembly, true, true);

                provider = (DataProvider)Activator.CreateInstance(objectType);
                DataCache.SetCache(objectType.FullName, provider);
            }

            return provider;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Not returning class state information")]
        public static IDbConnection GetConnection()
        {
            const string providerType = "data";
            ProviderConfiguration _providerConfiguration = ProviderConfiguration.GetProviderConfiguration(providerType);

            Provider objProvider = ((Provider)_providerConfiguration.Providers[_providerConfiguration.DefaultProvider]);
            string _connectionString;
            if (!String.IsNullOrEmpty(objProvider.Attributes["connectionStringName"]) && !String.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings[objProvider.Attributes["connectionStringName"]]))
            {
                _connectionString = System.Configuration.ConfigurationManager.AppSettings[objProvider.Attributes["connectionStringName"]];
            }
            else
            {
                _connectionString = objProvider.Attributes["connectionString"];
            }

            IDbConnection newConnection = new System.Data.SqlClient.SqlConnection();
            newConnection.ConnectionString = _connectionString.ToString();
            newConnection.Open();
            return newConnection;
        }

        #endregion

        #region Abstract methods

        //public abstract IDataReader GetItems(int userId, int portalId);

        //public abstract IDataReader GetItem(int itemId);  

        public abstract List<Article> GeneralGetArticleOnDate(int ModuleId, DateTime dt);

        public abstract List<Article> GeneralGetArticleOnTitle(int ModuleId, string Title);

        public abstract List<Article> GeneralGetArticlesOfTeg(string teg);

        public abstract List<Article> GeneralGetArticlesOfDay(int ModuleId, DateTime dt);

        public abstract IDataReader GetArticle(int ArticleId);

        public abstract IDataReader GetComments(int ArticleId);

        public abstract IDataReader GetArticles(int ModuleId);

        public abstract IDataReader GetArticlesTitle(int ModuleId, string Title);

        public abstract IDataReader GetArticlesToDate(DateTime dt);

        public abstract int AddArticle(Article a);

        public abstract void AddComment(Comment c);

        public abstract void UpdateArticle(Article a);

        public abstract void DeleteArticle(int articleId);

        public abstract void DeleteArticles(int moduleId);

        public abstract void AddTegs(string NameTeg);

        public abstract IDataReader GetTegs();

        public abstract void DeleteTeg(string NameTeg);

        public abstract IDataReader GetTegsForID(string NameTeg);

        public abstract void AddTegsOnArticle(int IdTeg, int IdArticle);

        public abstract IDataReader GetTegsOnArticle(int ArticleId);

        public abstract void UpdateTegs(string NameTeg1, string NameTeg2);

        public abstract string GetSC();

        #endregion

    }

}