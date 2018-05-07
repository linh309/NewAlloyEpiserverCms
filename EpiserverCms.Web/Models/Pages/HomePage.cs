using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpiserverCms.Web.Models.Pages
{
    [SiteContentType(
      GUID = "723192C7-634C-495B-A440-830C4C44E74A",
      GroupName = Global.GroupNames.Specialized)]
    public class HomePageData : SitePageData
    {
        public virtual LinkItemCollection SocialLinks { get; set; }
    }
}