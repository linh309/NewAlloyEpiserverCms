using EPiServer.Data;
using EPiServer.Data.Dynamic;
using EPiServer.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpiserverCms.Web.Models.Blocks
{
    [ContentType(DisplayName = "User Comment Block", Description = "Comment user", GUID = "B9A16A68-57C2-4BEE-88DB-E15160BEB1BE")]
    public class UserCommentBlock : SiteBlockData
    {
        //[Ignore]
        //public  string UserEmail { get; set; }

        //[Ignore]
        //public  string CommentBody { get; set; }

        //[Ignore]
        //public DateTime CreatedDate { get; set; }

        //[Ignore]
        //public Identity Id { get; set; }
    }
}