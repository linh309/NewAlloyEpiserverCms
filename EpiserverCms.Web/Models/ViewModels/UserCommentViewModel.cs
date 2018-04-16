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
        
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        
        public Identity Id { get; set; }
    }
}