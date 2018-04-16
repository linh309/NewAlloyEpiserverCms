﻿using EPiServer.Web.Mvc;
using EpiserverCms.Web.Models.Blocks;
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
            //var 
            //comment.CreatedDate = DateTime.Now;
            //DynamicDataStore store = DynamicDataStoreFactory.Instance.CreateStore(DynamicDataStoreList.COMMENT_STORE, typeof(CommentUserBlock));

            var currentUrl = HttpContext.Request.UrlReferrer.AbsoluteUri;
            //var id = store.Save(comment);
            //var loadedPerson = store.LoadAll<CommentUserBlock>();

            return Redirect(currentUrl);
        }

    }
}