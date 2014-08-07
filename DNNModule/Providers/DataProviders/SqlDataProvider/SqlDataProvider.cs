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

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Framework.Providers;
using Christoc.Modules.DNNModule.Components;
using Microsoft.ApplicationBlocks.Data;

namespace Christoc.Modules.DNNModule.Data
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// SQL Server implementation of the abstract DataProvider class
    /// 
    /// This concreted data provider class provides the implementation of the abstract methods 
    /// from data dataprovider.cs
    /// 
    /// In most cases you will only modify the Public methods region below.
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class SqlDataProvider : DataProvider
    {

        #region Private Members

        private const string ProviderType = "data";
        private const string ModuleQualifier = "DNNModule_";

        private readonly ProviderConfiguration _providerConfiguration = ProviderConfiguration.GetProviderConfiguration(ProviderType);
        private readonly string _connectionString;
        private readonly string _providerPath;
        private readonly string _objectQualifier;
        private readonly string _databaseOwner;

        #endregion

        #region Constructors

        public SqlDataProvider()
        {

            // Read the configuration specific information for this provider
            Provider objProvider = (Provider)(_providerConfiguration.Providers[_providerConfiguration.DefaultProvider]);

            // Read the attributes for this provider

            //Get Connection string from web.config
            _connectionString = Config.GetConnectionString();

            if (string.IsNullOrEmpty(_connectionString))
            {
                // Use connection string specified in provider
                _connectionString = objProvider.Attributes["connectionString"];
            }

            _providerPath = objProvider.Attributes["providerPath"];

            _objectQualifier = objProvider.Attributes["objectQualifier"];
            if (!string.IsNullOrEmpty(_objectQualifier) && _objectQualifier.EndsWith("_", StringComparison.Ordinal) == false)
            {
                _objectQualifier += "_";
            }

            _databaseOwner = objProvider.Attributes["databaseOwner"];
            if (!string.IsNullOrEmpty(_databaseOwner) && _databaseOwner.EndsWith(".", StringComparison.Ordinal) == false)
            {
                _databaseOwner += ".";
            }

        }

        #endregion

        #region Properties

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        public string ProviderPath
        {
            get
            {
                return _providerPath;
            }
        }

        public string ObjectQualifier
        {
            get
            {
                return _objectQualifier;
            }
        }

        public string DatabaseOwner
        {
            get
            {
                return _databaseOwner;
            }
        }

        // used to prefect your database objects (stored procedures, tables, views, etc)
        private string NamePrefix
        {
            get { return DatabaseOwner + ObjectQualifier + ModuleQualifier; }
        }

        #endregion

        #region Private Methods

        private static object GetNull(object field)
        {
            return Null.GetNull(field, DBNull.Value);
        }

        #endregion

        #region Public Methods

        //public override IDataReader GetItem(int itemId)
        //{
        //    return SqlHelper.ExecuteReader(ConnectionString, NamePrefix + "spGetItem", itemId);
        //}

        //public override IDataReader GetItems(int userId, int portalId)
        //{
        //    return SqlHelper.ExecuteReader(ConnectionString, NamePrefix + "spGetItemsForUser", userId, portalId);
        //}

        public override List<Article> GeneralGetArticleOnDate(int ModuleId, DateTime dt)
        {
            List<Article> list = new List<Article>();
            SqlConnection con = new SqlConnection(ConnectionString);
            string buf = dt.ToString("dd.MM.yyyy");
            string query = "SELECT * FROM DNNModule_Article WHERE (ModuleId=" + ModuleId + " and CreatedOnDate >= '" + buf + "') ORDER BY YEAR(CreatedOnDate),MONTH(CreatedOnDate),DAY(CreatedOnDate) DESC";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Article a = new Article();
                a.ArticleId = (int)rdr["ID"];
                a.Title = (string)rdr["Title"];
                a.Description = (string)rdr["Description"];
                a.Body = (string)rdr["Body"];
                a.ModuleId = (int)rdr["ModuleId"];
                a.CreatedOnDate = (DateTime)rdr["CreatedOnDate"];
                list.Add(a);
            }
            rdr.Close();
            return list;
        }

        public override List<Article> GeneralGetArticleOnTitle(int ModuleId, string Title)
        {
            List<Article> list = new List<Article>();
            SqlConnection con = new SqlConnection(ConnectionString);
            string buf1 = ModuleId.ToString();
            string query = "SELECT * FROM DNNModule_Article WHERE (ModuleId = " + buf1 + "and (Title like '%" + Title + "%' or Description like '%" + Title + "%' or Body like '%" + Title + "%')) ORDER BY YEAR(CreatedOnDate),MONTH(CreatedOnDate),DAY(CreatedOnDate) DESC";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Article a = new Article();
                a.ArticleId = (int)rdr["ID"];
                a.Title = (string)rdr["Title"];
                a.Description = (string)rdr["Description"];
                a.Body = (string)rdr["Body"];
                a.ModuleId = (int)rdr["ModuleId"];
                a.CreatedOnDate = (DateTime)rdr["CreatedOnDate"];
                list.Add(a);
            }
            rdr.Close();
            return list;
        }

        public override List<Article> GeneralGetArticlesOfTeg(string teg)
        {
            List<Article> list = new List<Article>();
            SqlConnection con = new SqlConnection(ConnectionString);
            string query = "SELECT a.* FROM dbo.DNNModule_Article_Teg_Article ata, dbo.DNNModule_Article_TegOnArticle atoa, dbo.DNNModule_Article a WHERE ata.IdTeg=atoa.IdTeg and atoa.IdArticle=a.ID and ata.NameTeg='" + teg + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Article a = new Article();
                a.ArticleId = (int)rdr["ID"];
                a.Title = (string)rdr["Title"];
                a.Description = (string)rdr["Description"];
                a.Body = (string)rdr["Body"];
                a.ModuleId = (int)rdr["ModuleId"];
                a.CreatedOnDate = (DateTime)rdr["CreatedOnDate"];
                list.Add(a);
            }
            rdr.Close();
            return list;
        }

        public override List<Article> GeneralGetArticlesOfDay(int ModuleId, DateTime dt)
        {
            List<Article> list = new List<Article>();
            SqlConnection con = new SqlConnection(ConnectionString);
            string buf = dt.ToString("dd.MM.yyyy");
            string query = "SELECT * FROM DNNModule_Article WHERE (ModuleId=" + ModuleId + " and YEAR(CreatedOnDate) = YEAR('" + buf + "') and MONTH(CreatedOnDate) = MONTH('" + buf + "') and DAY(CreatedOnDate) = DAY('" + buf + "')) ORDER BY YEAR(CreatedOnDate),MONTH(CreatedOnDate),DAY(CreatedOnDate) DESC";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Article a = new Article();
                a.ArticleId = (int)rdr["ID"];
                a.Title = (string)rdr["Title"];
                a.Description = (string)rdr["Description"];
                a.Body = (string)rdr["Body"];
                a.ModuleId = (int)rdr["ModuleId"];
                a.CreatedOnDate = (DateTime)rdr["CreatedOnDate"];
                list.Add(a);
            }
            rdr.Close();
            return list;
        }

        public override IDataReader GetArticle(int ArticleId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, NamePrefix + "Article_GetArticle", new SqlParameter("@ArticleId", ArticleId));
        }

        public override IDataReader GetArticles(int ModuleId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, NamePrefix + "Article_GetArticles", new SqlParameter("@ModuleId", ModuleId));
        }

        public override IDataReader GetComments(int ArticleId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, NamePrefix + "Article_GetComments", new SqlParameter("@ArticleId", ArticleId));
        }

        public override IDataReader GetArticlesTitle(int ModuleId, string Title)
        {

            return SqlHelper.ExecuteReader(ConnectionString, NamePrefix + "Article_GetArticlesTitle", new SqlParameter("ModuleId", ModuleId),
                new SqlParameter("@Title", Title));
        }

        public override IDataReader GetArticlesToDate(DateTime dt)
        {
            return SqlHelper.ExecuteReader(ConnectionString, NamePrefix + "Article_GetArticleToDate", new SqlParameter("@CreatedOnDate", dt));
        }

        public override int AddArticle(Components.Article a)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, NamePrefix + "Article_AddArticle"
                , new SqlParameter("@ModuleId", a.ModuleId)
                , new SqlParameter("@Title", a.Title)
                , new SqlParameter("@Description", a.Description)
                , new SqlParameter("@Body", a.Body)
                , new SqlParameter("@CreatedOnDate", a.CreatedOnDate)));
        }

        public override void AddComment(Components.Comment c)
        {
            SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, NamePrefix + "Article_AddComment",
                new SqlParameter("@ModuleId", c.ModuleId),
                new SqlParameter("@BodyComment", c.BodyComment),
                new SqlParameter("@CreatedOnDate", c.CreatedOnDate),
                new SqlParameter("@ArticleId", c.ArticleId),
                new SqlParameter("@UserId", c.UserId));
        }

        public override void DeleteArticle(int articleId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, NamePrefix + "Article_DeleteArticle", new SqlParameter("@ArticleId", articleId));
        }
        public override void DeleteArticles(int moduleId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, NamePrefix + "Article_DeleteArticles", new SqlParameter("@ModuleId", moduleId));
        }
        public override void UpdateArticle(Components.Article a)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, NamePrefix + "Article_UpdateArticle"
                , new SqlParameter("@ArticleId", a.ArticleId)
                , new SqlParameter("@ModuleId", a.ModuleId)
               , new SqlParameter("@Title", a.Title)
               , new SqlParameter("@Description", a.Description)
               , new SqlParameter("@Body", a.Body)
               );
        }

        public override void AddTegs(string NameTeg)
        {
            SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, NamePrefix+"Article_AddTegs", new SqlParameter("@NameTeg", NameTeg)); 
        }

        public override IDataReader GetTegs()
        {
            return SqlHelper.ExecuteReader(ConnectionString, NamePrefix + "Article_GetTegs");
        }

        public override void DeleteTeg(string NameTeg)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, NamePrefix + "Article_DeleteTeg", new SqlParameter("@NameTeg", NameTeg));
        }

        public override IDataReader GetTegsForID(string NameTeg)
        {
            return SqlHelper.ExecuteReader(ConnectionString, NamePrefix + "Article_GetTegsForID", new SqlParameter("@NameTeg", NameTeg));
        }

        public override void AddTegsOnArticle(int IdTeg, int IdArticle)
        {
            SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, NamePrefix + "Article_AddTegsOnArticle", new SqlParameter("@IdTeg", IdTeg), new SqlParameter("@IdArticle", IdArticle));
        }

        public override IDataReader GetTegsOnArticle(int ArticleId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, NamePrefix + "Article_GetTegsOnArticle", new SqlParameter("@ArticleId", ArticleId));
        }

        public override void UpdateTegs(string NameTeg1, string NameTeg2)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, NamePrefix + "Article_UpdateTegs", new SqlParameter("@NameTeg1", NameTeg1),
                new SqlParameter("@NameTeg2", NameTeg2));
        }

        public override string GetSC()
        {
            return ConnectionString;
        }

        #endregion

    }

}