using EPiServer.Core;
using EPiServer.Data.Dynamic;
using EPiServer.Framework.Web.Resources;
using EPiServer.PlugIn;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using EpiserverCms.Web.Helpers;
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
    [GuiPlugIn(
        Area = PlugInArea.AdminMenu,
        DisplayName = "Manage user comments",
        Url = "/ManageCommentPlugin/"
    )]
    public class ManageCommentPluginController : Controller
    {
        private readonly IRequiredClientResourceList requiredClientResourceList;
        private readonly IVirtualPathResolver virtualPathResolver;

        public ManageCommentPluginController(IRequiredClientResourceList requiredClientResourceList, IVirtualPathResolver virtualPathResolver)
        {
            this.requiredClientResourceList = requiredClientResourceList;
            this.virtualPathResolver = virtualPathResolver;
        }

        public ActionResult Index()
        {
            var listPages = PageHelper.FilterPagesByExistedProperty(GetChildrenPageOfStartPage(), PageProperty.UserComment);
            var selectedPage = listPages.FirstOrDefault();
            var pageCommentStore = CommentHelper.GetCommentStoreName();

            var model = new PluginCommentViewModel { };
            model.ListComment = CommentHelper.GetCommentByPageId(pageCommentStore, selectedPage.ContentLink.ID);
            model.ListPages = listPages;

            //add jquery-js to the bottom of page
            RequireClientResources();

            return View(model);
        }

        public ActionResult LoadComment(int pageId = 0)
        {
            var pageCommentStore = CommentHelper.GetCommentStoreName();
            IEnumerable<UserCommentViewModel> listComment = CommentHelper.GetCommentByPageId(pageCommentStore, pageId);
            return PartialView("_ListComment", listComment);
        }

        [HttpPost]
        public ActionResult UpdateComment(string commentId, ActionTypes action = ActionTypes.Restore)
        {
            var pageCommentStore = CommentHelper.GetCommentStoreName();
            var id = EPiServer.Data.Identity.NewIdentity(new Guid(commentId));
            var comment = CommentHelper.GetCommentById(pageCommentStore, id);

            if (comment != null)
            {
                comment.IsDeleted = action == ActionTypes.Deleted ? true : false;
                CommentHelper.UpdateComment(pageCommentStore, comment);
            }
            else
            {
                return  Json(new
                {
                    status = "error",
                    message = "Not found"
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "ok"
            }, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<PageData> GetChildrenPageOfStartPage()
        {
            var listChilrenPage = PageHelper.GetAllChildrenPages(PageReference.StartPage);
            return listChilrenPage;
        }

        private void RequireClientResources()
        {
            requiredClientResourceList.RequireScript(virtualPathResolver.ToAbsolute("~/Static/js/jquery-ui.js")).AtFooter();
        }
    }
}