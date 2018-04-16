using EPiServer.Core;
using EPiServer.Data.Dynamic;
using EPiServer.PlugIn;
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
        public ActionResult Index()
        {
            var listPages = PageHelper.FilterPagesByExistedProperty(GetChildrenPageOfStartPage(), PageProperty.UserComment);
            var selectedPage = listPages.FirstOrDefault();
            var pageCommentStore = GetPageCommentStore(selectedPage);

            var model = new PluginCommentViewModel { };
            model.ListComment = CommentHelper.GetCommentByPage(pageCommentStore);
            model.ListPages = PageHelper.FilterPagesByExistedProperty(GetChildrenPageOfStartPage(), PageProperty.UserComment);

            return View(model);
        }

        private IEnumerable<PageData> GetChildrenPageOfStartPage()
        {
            var listChilrenPage = PageHelper.GetAllChildrenPages(PageReference.StartPage);
            return listChilrenPage;
        }

        private string GetPageCommentStore(PageData pageData)
        {
            var pageId = pageData != null ? pageData.ContentLink.ID : PageReference.StartPage.ID;
            return CommentHelper.GetCommentStore(pageId);
        }
    }
}