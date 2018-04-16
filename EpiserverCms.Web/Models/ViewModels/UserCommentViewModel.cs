using EPiServer.Data;
using EPiServer.Data.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpiserverCms.Web.Models.ViewModels
{
    public class UserCommentViewModel: IDynamicData
    {
        public string UserEmail { get; set; }

        public string CommentBody { get; set; }
        
        public virtual DateTime CreatedDate { get; set; }
        
        public virtual Identity Id { get; set; }
    }
}