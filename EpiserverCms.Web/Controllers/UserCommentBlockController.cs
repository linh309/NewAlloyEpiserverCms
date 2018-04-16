using EPiServer;
using EPiServer.Core;
using EPiServer.Data.Dynamic;
using EPiServer.ServiceLocation;
using EPiServer.Util;
using EPiServer.Web.Mvc;
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
            var model = new UserCommentViewModel();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult PostComment(UserCommentViewModel comment)
        {
            var pageId = PageHelper.GetCurrentPageId();
            var commentStore = CommentHelper.GetCommentStore(pageId);
            var currentUrl = HttpContext.Request.UrlReferrer.AbsoluteUri;

            var store = DynamicDataStoreFactory.Instance.CreateStore(commentStore, typeof(UserCommentViewModel));
            comment.CreatedDate = DateTime.Now;
            comment.PageId = pageId;
            store.Save(comment);

            return Redirect(currentUrl);
        }
    }
}