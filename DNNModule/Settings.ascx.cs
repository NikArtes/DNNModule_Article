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
using DotNetNuke.Entities.Modules;
using Christoc.Modules.DNNModule.Components;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;


namespace Christoc.Modules.DNNModule
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Settings class manages Module Settings
    /// 
    /// Typically your settings control would be used to manage settings for your module.
    /// There are two types of settings, ModuleSettings, and TabModuleSettings.
    /// 
    /// ModuleSettings apply to all "copies" of a module on a site, no matter which page the module is on. 
    /// 
    /// TabModuleSettings apply only to the current module on the current page, if you copy that module to
    /// another page the settings are not transferred.
    /// 
    /// If you happen to save both TabModuleSettings and ModuleSettings, TabModuleSettings overrides ModuleSettings.
    /// 
    /// Below we have some examples of how to access these settings but you will need to uncomment to use.
    /// 
    /// Because the control inherits from DNNModuleSettingsBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Settings : DNNModuleModuleSettingsBase
    {
        #region Base Method Implementations

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// LoadSettings loads the settings from the Database and displays them
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void LoadSettings()
        {
            try
            {
                if (Page.IsPostBack == false)
                {
                    //Check for existing settings and use those on this page
                    //Settings["SettingName"]

                    /* uncomment to load saved settings in the text boxes
                    if(Settings.Contains("Setting1"))
                        txtSetting1.Text = Settings["Setting1"].ToString();
			
                    if (Settings.Contains("Setting2"))
                        txtSetting2.Text = Settings["Setting2"].ToString();

                    */
                    CheckBoxListDeleteTeg.DataSource = ArticleController.GetTegs();
                    CheckBoxListDeleteTeg.DataBind();

                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpdateSettings saves the modified settings to the Database
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void UpdateSettings()
        {
            try
            {
                var modules = new ModuleController();

                //the following are two sample Module Settings, using the text boxes that are commented out in the ASCX file.
                //module settings
                //modules.UpdateModuleSetting(ModuleId, "Setting1", txtSetting1.Text);
                //modules.UpdateModuleSetting(ModuleId, "Setting2", txtSetting2.Text);

                //tab module settings
                //modules.UpdateTabModuleSetting(TabModuleId, "Setting1",  txtSetting1.Text);
                //modules.UpdateTabModuleSetting(TabModuleId, "Setting2",  txtSetting2.Text);
                //TextBox1.Text = "Добавьте тег";
                //CheckBoxListDeleteTeg.DataSource = ArticleController.GetTegs();
                //CheckBoxListDeleteTeg.DataBind();
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion

        protected void ButtonAddTegs_Click(object sender, EventArgs e)
        {
            if (TextBox1 != null)
            {
                string buf = TextBox1.Text;
                string[] stringMas = buf.Split(' ');
                for (int i = 0; i < stringMas.Length; i++)
                {
                    ArticleController.SaveTegs(stringMas[i]);
                }
            }
        }

        protected void ButtonDeleteTegs_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < CheckBoxListDeleteTeg.Items.Count; i++)
            {
                if (CheckBoxListDeleteTeg.Items[i].Selected)
                {
                    string buf = CheckBoxListDeleteTeg.Items[i].ToString();
                    list.Add(buf);
                    ArticleController.DeleteTeg(buf);
                }
            }
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            if (txtBoxUp != null)
            { 
                string buf=txtBoxUp.Text;
                string [] masstr=buf.Split(' ');
                if (masstr.Length >= 2)
                {
                    ArticleController.UpdateTeg(masstr[0], masstr[1]);
                }
            }
        }
    }
}