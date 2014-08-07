using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Christoc.Modules.DNNModule.Data;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;

namespace Christoc.Modules.DNNModule.Components
{
    public class ArticleController : DNNModuleModuleBase
    {
        public static Article GetArticle(int ArticleId)
        {
            if (ArticleId == -1)
                return null;
            IDataReader dr = DataProvider.Instance().GetArticle(ArticleId);
            Article a = new Article();
            //{
            //    Title = (string)dr["Title"],
            //    Description = (string)dr["Description"],
            //    Body = (string)dr["Body"],
            //    ModuleId = (int)dr["ModuleId"],
            //    CreatedOnDate = (DateTime)dr["CreatedOnDate"]
            //};
            if (dr.Read())
            {
                a.ArticleId = (int)dr["ID"];
                a.Title = (string)dr["Title"];
                a.Description = (string)dr["Description"];
                a.Body = (string)dr["Body"];
                a.ModuleId = (int)dr["ModuleId"];
                a.CreatedOnDate = (DateTime)dr["CreatedOnDate"];
            }
            return a;
            
        }

        public static List<Article> GeneralGetArticlesOfDate(int ModuleId, DateTime dt)
        {
            return DataProvider.Instance().GeneralGetArticleOnDate(ModuleId, dt);
        }

        public static List<Article> GeneralGetArticlesOfTitle(int ModuleId, string Title)
        {
            return DataProvider.Instance().GeneralGetArticleOnTitle(ModuleId, Title);
        }

        public static List<Article> GeneralGetArticleOfTeg(string teg)
        {
            return DataProvider.Instance().GeneralGetArticlesOfTeg(teg);
        }

        public static List<Article> GeneralGetArticlesOfDay(int ModuleId, DateTime dt)
        {
            return DataProvider.Instance().GeneralGetArticlesOfDay(ModuleId, dt);
        }

        public static List<Comment> GetComments(int ArticleId, int ModuleID)
        {
            List<Comment> list = new List<Comment>();
            string s = DataProvider.Instance().GetSC();
            SqlConnection con = new SqlConnection(s);
            string buf = ArticleId.ToString();
            string buf1 = ModuleID.ToString();
            string query = "SELECT a.ID, a.UserId, a.ModuleId, a.BodyComments, a.CreatedOnDate, u.Username, f.FileName FROM DNNModule_Article_Comments a, dbo.Users u left outer join dbo.Files f on u.UserID=f.CreatedByUserID WHERE u.UserID=a.UserId and (f.FileName like '%_xs.jpg' or f.FileName is null) and a.ArticleId='" + buf + "' and a.ModuleId='"+buf1+"'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Comment c = new Comment();
                c.ArticleId = (int)rdr["ID"];
                c.BodyComment = (string)rdr["BodyComments"];
                c.ModuleId = (int)rdr["ModuleId"];
                c.CreatedOnDate = (DateTime)rdr["CreatedOnDate"];
                c.UserId = (int)rdr["UserId"];
                c.NameAutor = (string)rdr["Username"];
                string ImageNameBuf;
                string standartPath="~/DesktopModules/DNNModule/Sourse/no_avatar_xs.jpg";
                c.ImgNameUrl = standartPath;
                if(rdr["FileName"]!=DBNull.Value)
                {
                    ImageNameBuf = (string)rdr["FileName"];
                    string UserNumber = c.UserId.ToString();
                    string ParthToImage = "~/Portals/_default/Users" + "/00" + UserNumber + "/0" + UserNumber + "/" + UserNumber + "/" + ImageNameBuf;
                    c.ImgNameUrl = ParthToImage;
                }
                list.Add(c);
            }
            rdr.Close();
            return list;
        }

        public static List<Teg> GetTegs()
        {
            List<Teg> list = new List<Teg>();
            string s = DataProvider.Instance().GetSC();
            SqlConnection con = new SqlConnection(s);
            string query = "SELECT * FROM dbo.DNNModule_Article_Teg_Article";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Teg  teg = new Teg();
                teg.IdTeg=Convert.ToInt32(rdr["IdTeg"]);
                teg.NameTeg = Convert.ToString(rdr["NameTeg"]);
                list.Add(teg);
            }
            rdr.Close();
            return list;
        }

        public static int SaveArticle(Article a)
        {
            if (a.ArticleId < 1)
            {
                a.ArticleId = DataProvider.Instance().AddArticle(a);
                //a.ArticleId++;
                SaveArticle(a);
            }
            else
            {
                DataProvider.Instance().UpdateArticle(a);
            }
            return a.ArticleId;
        }

        public static void SaveComment(Comment c)
        {
            DataProvider.Instance().AddComment(c);
        }

        public static void SaveTegs(string NameTeg)
        {
            DataProvider.Instance().AddTegs(NameTeg);
        }

        public static void DeleteArticle(int ArticleId)
        {
            DataProvider.Instance().DeleteArticle(ArticleId);
        }
        public static void DeleteArticles(int ModuleId)
        {
            DataProvider.Instance().DeleteArticles(ModuleId);
        }

        public static void DeleteTeg(string NameTeg)
        {
            DataProvider.Instance().DeleteTeg(NameTeg);
        }

        public static int GetTegsForID(string NameTeg)
        {
            Teg teg = new Teg();
            string s = DataProvider.Instance().GetSC();
            SqlConnection con = new SqlConnection(s);
            string query = "SELECT * FROM dbo.DNNModule_Article_Teg_Article a WHERE a.NameTeg='"+NameTeg+"'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                teg.IdTeg = (int)rdr["IdTeg"];
                teg.NameTeg=(string)rdr["NameTeg"];
            }
            rdr.Close();
            return teg.IdTeg;
        }

        public static void AddTegOnArticle(int TegId, int ArticleId)
        {
            Data.DataProvider.Instance().AddTegsOnArticle(TegId, ArticleId);
        }

        public static List<Teg> GetTegsOnArticle(int ArticleId)
        {
            List<Teg> list = new List<Teg>();
            string s = DataProvider.Instance().GetSC();
            SqlConnection con = new SqlConnection(s);
            string query = "SELECT dat.* FROM dbo.DNNModule_Article da, dbo.DNNModule_Article_TegOnArticle dt, dbo.DNNModule_Article_Teg_Article dat WHERE da.ID=dt.IdArticle and dt.IdTeg=dat.IdTeg and da.ID="+ArticleId.ToString()+"";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Teg teg = new Teg();
                teg.IdTeg=(int)rdr["IdTeg"];
                teg.NameTeg=(string)rdr["NameTeg"];
                list.Add(teg);
            }
            rdr.Close();
            return list;
        }

        public static void UpdateTeg(string NameTeg1, string NameTeg2)
        {
            DataProvider.Instance().UpdateTegs(NameTeg1, NameTeg2);
        }

        static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
    }
}