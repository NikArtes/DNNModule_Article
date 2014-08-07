/*
' Copyright (c) 2014  Christoc.com
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
using System.Collections.Generic;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Common;
using Christoc.Modules.DNNModule.Components;

namespace Christoc.Modules.DNNModule
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The EditDNNModule class is used to manage content
    /// 
    /// Typically your edit control would be used to create new content, or edit existing content within your module.
    /// The ControlKey for this control is "Edit", and is defined in the manifest (.dnn) file.
    /// 
    /// Because the control inherits from DNNModuleModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Edit : DNNModuleModuleBase
    {
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            Load += Page_Load;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {

                    var article = new Article();
                    article = ArticleController.GetArticle(ArticleId);
                    List<Teg> list= ArticleController.GetTegs();
                    CheckBoxListTag.DataSource = ArticleController.GetTegs();
                    CheckBoxListTag.DataBind();
                    
                    

                    if (article != null)
                    {
                        
                        txtTitle.Text = article.Title;
                        txtDescription.Text = article.Description;
                        txtBody.Text = article.Body;
                    }
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected void lbSave_Click(object sender, EventArgs e)
        {
            Article a;
            if (ArticleId > 0)
            {
                a = ArticleController.GetArticle(ArticleId);
                a.Title = txtTitle.Text.Trim();
                a.Description = txtDescription.Text.Trim();
                a.Body = txtBody.Text;
                a.ModuleId = ModuleId;

            }
            else
            {
                a = new Article
                {
                    Title = txtTitle.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    Body = txtBody.Text,
                    CreatedOnDate = DateTime.Now,
                    ModuleId = ModuleId
                };
            }
            int bufArticleId=ArticleController.SaveArticle(a);
            List<int> TegsId = new List<int>();
            for (int i = 0; i < CheckBoxListTag.Items.Count; i++)
            {
                if (CheckBoxListTag.Items[i].Selected)
                {
                    string buf = CheckBoxListTag.Items[i].ToString();
                    int bufint=ArticleController.GetTegsForID(buf);
                    TegsId.Add(bufint);
                    ArticleController.AddTegOnArticle(bufint, bufArticleId);
                }
            }

            Response.Redirect(Globals.NavigateURL(TabId));
        }

        protected void lbCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(TabId));
        }
    }
}