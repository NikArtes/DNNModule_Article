using System;
using System.Web.UI;
using DotNetNuke.Common;
using DotNetNuke.Framework;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Content.Common;
using Christoc.Modules.DNNModule.Components;
using System.Web.UI.WebControls;
using System.Web;

namespace Christoc.Modules.DNNModule.Controls
{
    public partial class ArticleView : DNNModuleModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ArticleId > 0)
            {
                var curArticle = ArticleController.GetArticle(ArticleId);
                //display article info on the view control

                plArticleTitle.Controls.Add(new LiteralControl(curArticle.Title));
                plArticleBody.Controls.Add(new LiteralControl(Server.HtmlDecode(curArticle.Body)));

                //change the page title, description, and add categories to keywords

                var tp = (CDefault)Page;
                tp.Title = curArticle.Title;
                tp.Description = curArticle.Description;
                lnkEdit.Attributes["href"] = EditUrl(String.Empty, String.Empty, "Edit", "aid=" + curArticle.ArticleId);

                if (!IsEditable && UserId==-1)
                    ArticleAdmin.Visible = false;

                rptArticleComments.DataSource = ArticleController.GetComments(ArticleId, ModuleId);
                rptArticleComments.DataBind();

                rptTegsOnArticle.DataSource = ArticleController.GetTegsOnArticle(ArticleId);
                rptTegsOnArticle.DataBind();

            }   
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            if (IsEditable)
            {
                var s = EditUrl(string.Empty, string.Empty, "Edit", "aid" + ArticleId);
                Response.Redirect(s);
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            if (IsEditable)
            {
                ArticleController.DeleteArticle(ArticleId);
                Response.Redirect(Globals.NavigateURL());
            }
        }

        protected void rptArticleComments_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            var pnlOtherControls = e.Item.FindControl("pnlOtherControls") as Panel;
        }

        protected void ButtonEditComment_Click(object sender, EventArgs e)
        {
            if (TextBoxComment.Text != null)
            {
                Comment c=new Comment();
                c.ArticleId = ArticleId;
                c.BodyComment = TextBoxComment.Text;
                c.CreatedOnDate = DateTime.Now;
                c.ModuleId = ModuleId;
                c.UserId = UserId;
                ArticleController.SaveComment(c);
            }
        }

        protected void rptTegsOnArticle_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            LinkButton buf = (LinkButton)e.CommandSource;
            string buf1 = buf.Text;
            var s = Globals.NavigateURL() + "?Teg=" + HttpUtility.UrlEncode(buf1);
            Response.Redirect(s);
        }
    }
}