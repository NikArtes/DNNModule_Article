using System;
using System.Data;
using System.Web.UI;
using DotNetNuke.Entities.Content.Common;
using DotNetNuke.Entities.Modules;
using Christoc.Modules.DNNModule.Components;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.WebControls;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using Globals = DotNetNuke.Common.Globals;

namespace Christoc.Modules.DNNModule.Controls
{
    public partial class ArticleList : DNNModuleModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsPostBack)
                {
                    if (ViewState["DateCount"] != null)
                    {
                        rptArticleList.DataSource = ArticleController.GeneralGetArticlesOfDate(ModuleId, (DateTime)ViewState["DateCount"]);
                        rptArticleList.DataBind();
                        ViewState["DateCount"] = null;
                    }
                    if (ViewState["TitleCount"] != null)
                    {
                        rptArticleList.DataSource = ArticleController.GeneralGetArticlesOfTitle(ModuleId, (string)ViewState["TitleCount"]);
                        rptArticleList.DataBind();
                        ViewState["TitleCount"] = null;
                    }
                    if (ViewState["DateCountDay"] != null)
                    {
                        rptArticleList.DataSource = ArticleController.GeneralGetArticlesOfDay(ModuleId, (DateTime)ViewState["DateCountDay"]);
                        rptArticleList.DataBind();
                        ViewState["DateCountDay"] = null;
                    }
                }
                else
                {
                    if (Teg != null)
                    {
                        rptArticleList.DataSource = ArticleController.GeneralGetArticleOfTeg(Teg);
                        rptArticleList.DataBind();
                    }
                    else 
                    {
                        rptArticleList.DataSource = ArticleController.GeneralGetArticlesOfTitle(ModuleId, "");
                        rptArticleList.DataBind();
                    }
                }
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        private static void SetPagingLink(NameValueCollection queryString
            , HyperLink link, bool showLink, int linkedPageId, int tabId)
        {
            if (showLink)
            {
                link.Visible = true;
                queryString = new NameValueCollection(queryString);
                queryString["p"] = linkedPageId.ToString(CultureInfo.InvariantCulture);
                var additionalParameters = new List<string>(queryString.Count);

                for (int i = 0; i < queryString.Count; i++)
                {
                    if (string.Equals(queryString.GetKey(i), "TABID", StringComparison.OrdinalIgnoreCase))
                    {
                        int newTabId;
                        if (int.TryParse(queryString[i], NumberStyles.Integer, CultureInfo.InvariantCulture, out newTabId))
                        {
                            tabId = newTabId;
                        }
                    }
                    else if (!string.Equals(queryString.GetKey(i), "LANGUAGE", StringComparison.OrdinalIgnoreCase))
                    {
                        additionalParameters.Add(queryString.GetKey(i) + "=" + queryString[i]);
                    }
                }

                link.NavigateUrl = Globals.NavigateURL(tabId, string.Empty, additionalParameters.ToArray());
            }
            else
            {
                link.Visible = false;
            }
        }

        protected void RptArticleListOnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //configure the Tags
            var lnkDelete = e.Item.FindControl("lnkDelete") as LinkButton;
            var lnkEdit = e.Item.FindControl("lnkEdit") as LinkButton;
            var pnlAdminControls = e.Item.FindControl("pnlAdminControls") as Panel;
            var pnlOtherControls = e.Item.FindControl("pnlOtherControls") as Panel;

            var curArticle = (Article)e.Item.DataItem;

            
            
            if (IsEditable && lnkDelete != null && lnkEdit != null)
            {
                pnlAdminControls.Visible = true;
                lnkDelete.Visible = lnkDelete.Enabled = lnkEdit.Visible = lnkEdit.Enabled = true;
                ClientAPI.AddButtonConfirm(lnkDelete, Localization.GetString("ConfirmDelete", LocalResourceFile));
                lnkEdit.Attributes["href"] = EditUrl(String.Empty, String.Empty, "Edit", "aid=" + curArticle.ArticleId);
                lnkDelete.CommandArgument = curArticle.ArticleId.ToString();
                lnkEdit.CommandArgument = curArticle.ArticleId.ToString();
            }
            else
            {
                pnlAdminControls.Visible = false;
            }
        }

        public void RptArticleListOnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                ArticleController.DeleteArticle(Convert.ToInt32(e.CommandArgument));
            }

            Response.Redirect(Globals.NavigateURL());
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ViewState["TitleCount"] = TextBox.Text;
            Page_Load(sender, e);
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            if (TextBox.Text != null)
                TextBox.Text = string.Empty;
            DateTime dt = Calendar1.SelectedDate;
            ViewState["DateCountDay"] = dt;
            Page_Load(sender, e);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            ViewState["DateCount"] = DateTime.Now.AddDays(-1);
            Calendar1.SelectedDates.Clear();
            Page_Load(sender, e);
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            ViewState["DateCount"] = DateTime.Now.AddMonths(-1);
            Calendar1.SelectedDates.Clear();
            Page_Load(sender, e);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            ViewState["DateCount"] = DateTime.Now.AddDays(-7);
            Calendar1.SelectedDates.Clear();
            Page_Load(sender, e);
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            ViewState["TitleCount"] = "";
            Calendar1.SelectedDates.Clear();
            Page_Load(sender, e);
        }
    }
}