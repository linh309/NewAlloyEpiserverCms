using EPiServer;
using EPiServer.Core;
using EPiServer.Data.Dynamic;
using EPiServer.ServiceLocation;
using EPiServer.Util;
using EPiServer.Web.Mvc;
using EPiServer.Web.PageExtensions;
using EPiServer.Web.Routing;
using EpiserverCms.Web.Helpers;
using EpiserverCms.Web.Models.Blocks;
using EpiserverCms.Web.Models.Constant;
using EpiserverCms.Web.Models.Pages;
using EpiserverCms.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EpiserverCms.Web.Controllers
{
    public class UserCommentBlockController : BlockController<UserCommentBlock>
    {
        // GET: UserCommentBlock
        public override ActionResult Index(UserCommentBlock currentBlock)
        {
            var currentPage = PageHelper.GetCurrentPageDataOfBlock();
            var model = new UserCommentViewModel
            {
                PageId = currentPage.ContentLink.ID,
                PageName = currentPage.Name
            };

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult PostComment(UserCommentViewModel comment)
        {
            var commentStore = CommentHelper.GetCommentStoreName();
            var currentUrl = HttpContext.Request.UrlReferrer.AbsoluteUri;

            var store = DynamicDataStoreFactory.Instance.CreateStore(commentStore, typeof(UserCommentViewModel));
            comment.CreatedDate = DateTime.Now;
            store.Save(comment);

            return Redirect(currentUrl);
        }
    }
}