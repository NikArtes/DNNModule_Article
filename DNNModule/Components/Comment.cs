using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Christoc.Modules.DNNModule.Components
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string BodyComment { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public string NameAutor { get; set; }
        public string ImgNameUrl { get; set; }
        public int ModuleId { get; set; }
        public int ArticleId { get; set; }
        public int UserId { get; set; }
    }
}