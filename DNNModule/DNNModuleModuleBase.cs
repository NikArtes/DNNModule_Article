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
using System.Web;
using DotNetNuke.Entities.Modules;
using DotNetNuke;

namespace Christoc.Modules.DNNModule
{
    public class DNNModuleModuleBase : PortalModuleBase
    {
        public int ArticleId
        {
            get
            {
                var qs = Request.QueryString["aid"];
                if (qs != null)
                    return Convert.ToInt32(qs);
                return -1;
            }
        }

        public string Teg
        {
            get 
            {
                var qs = Convert.ToString(Request.QueryString["Teg"]);
                string sd = HttpUtility.UrlDecode(qs);
                return Convert.ToString(qs);
            }
        }

        public int PageNumber
        {
            get
            {
                var qs = Request.QueryString["p"];
                if (qs != null)
                    return Convert.ToInt32(qs);
                return 0;
            }
        }

        public int PageSize
        {
            get
            {
                if (Settings.Contains("PageSize"))
                    return Convert.ToInt32(Settings["PageSize"]);
                return 10;
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "PageSize", value.ToString());
            }
        }

        public string GetArticleLink(string articleId)
        {
            return DotNetNuke.Common.Globals.NavigateURL(TabId, String.Empty, "aid=" + articleId);
        }
    }
}