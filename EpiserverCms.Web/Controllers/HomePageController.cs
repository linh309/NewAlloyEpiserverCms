using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using EpiserverCms.Web.Models.Pages;
using EpiserverCms.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EpiserverCms.Web.Controllers
{
    public class HomePageController : PageController<HomePageData>
    {
        // GET: HomePage
        public ActionResult Index(HomePageData currentPage)
        {
            foreach(var link in currentPage.SocialLinks)
            {
                var href = link.Href;
                var hrefUrl = UrlResolver.Current.GetUrl(href);
            }

            var viewModel = new HomePageViewModel(currentPage);
            return View(viewModel);
        }
    }
}