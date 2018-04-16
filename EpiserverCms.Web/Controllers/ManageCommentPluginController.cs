using EPiServer.Data.Dynamic;
using EPiServer.PlugIn;
using EPiServer.Web.Mvc;
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
            var model = new PluginCommentViewModel { };            
            var store = DynamicDataStoreFactory.Instance.CreateStore(DynamicDataStoreList.COMMENT_STORE, typeof(UserCommentViewModel));
            var listAllComment = store.LoadAll<UserCommentViewModel>();
            model.ListComment = listAllComment;

            return View(model);
        }
    }
}