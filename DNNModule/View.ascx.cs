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
using Christoc.Modules.DNNModule.Components;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;

namespace Christoc.Modules.DNNModule
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from DNNModuleModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : DNNModuleModuleBase, IActionable
    {
        //override protected void OnInit(EventArgs e)
        //{
        //    InitializeComponent();
        //    base.OnInit(e);
        //}

        //private void InitializeComponent()
        //{
        //    Load += Page_Load;
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var controlToLoad = "Controls/ArticleList.ascx";
                if (ArticleId > 0)
                {
                    controlToLoad = "Controls/ArticleView.ascx";
                }
                var mbl = (DNNModuleModuleBase)LoadControl(controlToLoad);
                mbl.ModuleConfiguration = ModuleConfiguration;
                mbl.ID = System.IO.Path.GetFileNameWithoutExtension(controlToLoad);
                phViewControl.Controls.Add(mbl);
                
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        public ModuleActionCollection ModuleActions
        {
            get
            {
                ModuleActionCollection actions;
                if (ArticleId > 0)
                {
                    actions = new ModuleActionCollection
                                  {
                                      {
                                          GetNextActionID(), Localization.GetString("EditArticle", LocalResourceFile),
                                          "", "", "", EditUrl(string.Empty, string.Empty, "Edit", "aid=" + ArticleId),
                                          false, SecurityAccessLevel.Edit, true, false
                                          }
                                  };
                }
                else
                {
                    actions = new ModuleActionCollection
                                  {
                                      {
                                          GetNextActionID(), Localization.GetString("AddArticle", LocalResourceFile),
                                          "", "", "", EditUrl(), false, SecurityAccessLevel.Edit, true, false
                                          }
                                  };
                }

                return actions;
            }
        }
    }
}