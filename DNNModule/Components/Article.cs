using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Entities.Content;
using DotNetNuke.Common.Utilities;

namespace Christoc.Modules.DNNModule.Components
{
    public class Article : ContentItem
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public int ModuleId { get; set; }
        public DateTime CreatedOnDate { get; set; }



        public override void Fill(System.Data.IDataReader dr)
        {
            //Call the base classes fill method to populate base class properties
            base.FillInternal(dr);

            ArticleId = Null.SetNullInteger(dr["Id"]);
            ModuleId = Null.SetNullInteger(dr["ModuleId"]);
            Title = Null.SetNullString(dr["Title"]);
            Description = Null.SetNullString(dr["Description"]);
            Body = Null.SetNullString(dr["Body"]);
            //PortalId = Null.SetNullInteger(dr["PortalId"]);
            CreatedOnDate = Null.SetNullDateTime(dr["CreatedOnDate"]);
        }

    }
}