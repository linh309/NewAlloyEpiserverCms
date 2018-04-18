using EPiServer.Data;
using EPiServer.Data.Dynamic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EpiserverCms.Web.Models.ViewModels
{
    public class UserCommentViewModel : IDynamicData
    {
        [Required(ErrorMessage = "Please enter email")]
        [RegularExpression(@"[a-zA-Z0-9.-_]{1,}@[a-zA-Z.-]{2,}[.]{1}[a-zA-Z]{2,}", ErrorMessage = "Invalid e-mail address")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Please enter comment")]
        public string CommentBody { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public Identity Id { get; set; }

        public int PageId { get; set; }

        public string PageName { get; set; }
    }
}